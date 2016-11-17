using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

// ReSharper disable ConditionIsAlwaysTrueOrFalse

namespace DialogEngine
{
    public class HistoricalDialog
    {
        public int DialogIndex;
        public string DialogName = "";
        public DateTime StartedTime = DateTime.MinValue;
        public bool Completed = false;
        public int Character1;
        public int Character2;
    }

    public class HistoricalPhrase
    {
        public int CharacterIndex;
        public string CharacterPrefix = "";
        public int PhraseIndex;
        public string PhraseFile = "";
        public DateTime StartedTime = DateTime.MinValue;
    }

    public class DialogTracker
    {
        //Here we decide what to say next
        protected const int RecentDialogsQueSize = 4;
        public List<ModelDialog> ModelDialogs = new List<ModelDialog>();
        public List<HistoricalDialog> HistoricalDialogs = new List<HistoricalDialog>();
        public List<HistoricalPhrase> HistoricalPhrases = new List<HistoricalPhrase>();
        public Queue<int> RecentDialogs = new Queue<int>();
        public List<Character> CharacterList = new List<Character>();
        public int Character1Num = 0;
        public int Character2Num = 1;
        int _priorCharacter1Num = 100;
        int _priorCharacter2Num = 100;
        int _currentDialogModel = 1;
        public Mp3Player Player = new Mp3Player();
        public bool SameCharactersAsLast = false;
        public bool LastPhraseImpliedMovement = false;
        private static int _movementWaitCount = 0;
        public double DialogModelPopularitySum;

        public DialogTracker() {
            //CharacterList.Add(new Cowboy());
            CharacterList.Add(new Skylar());
            CharacterList.Add(new SchoolMarm());
            CharacterList.Add(new ReOrgLead());
            CharacterList.Add(new SchoolBoy());
            CharacterList.Add(new Cartman());
            CharacterList.Add(new SchoolHouse());
            for (int i = 0; i < RecentDialogsQueSize; i++) {
                RecentDialogs.Enqueue(0); // Fill the que with greeting dialogs
            }
        }

        public void PlayAudio(string pathAndFileName) {
            if (File.Exists(pathAndFileName)) {
                FileInfo fileInfo = new FileInfo(pathAndFileName);
                //empirical hack to wait reasonable time since Play() has no return to know when playing is complete
                long songMilliSeconds = fileInfo.Length/14;
                var playSuccess = Player.Play(pathAndFileName);
                if (playSuccess != 0) {
                    Console.WriteLine("");
                    Console.WriteLine("   MP3 Play Error  ---  " + playSuccess);
                    Console.WriteLine("");
                }
                Thread.Sleep((int)songMilliSeconds);
                Thread.Sleep(20);
            }
            else {
                Console.WriteLine("Could not find: " + pathAndFileName);
            }
        }

        void AddDialogModelToHistory(int dialogModelIndex, int ch1, int ch2) {
            HistoricalDialogs.Add(new HistoricalDialog(){
                DialogIndex = dialogModelIndex,
                DialogName = ModelDialogs[dialogModelIndex].Name,
                StartedTime = DateTime.Now,
                Completed = false,
                Character1 = ch1,
                Character2 = ch2
            });
        }

        public void WriteDialogInfo(int currentDialogModel, int character1Num, int character2Num) {
            string dialogModelString = "  --DiMod " + currentDialogModel + " " + ModelDialogs[currentDialogModel].Name +
                                       " NextChars: " + CharacterList[character1Num].CharacterPrefix + " " +
                                       CharacterList[character2Num].CharacterPrefix + " " + DateTime.Now;

            Console.WriteLine(dialogModelString);
            if (SessionVars.WriteSerialLog) {
                using (StreamWriter serialLogDialogModels = new StreamWriter(
                    @"c:\Isaac\Toys2LifeResources\CapturesAndAnalysis\SerialLogDialog.txt", true)) {
                    serialLogDialogModels.WriteLine(dialogModelString);
                    serialLogDialogModels.Close();
                }
            }
        }

        List<int> FindMostRecentAdventures() {
            List<int> mostRecentAdventureDialogs = new List<int>();
            List<string> foundAdventures = new List<string>();
            int j = 0;
            for (int i = HistoricalDialogs.Count - 1; i >= 0; i--) {
                var dialog = ModelDialogs[HistoricalDialogs[i].DialogIndex];
                if (dialog.Adventure.Length > 0 && !foundAdventures.Contains(dialog.Adventure)) {
                    //if the dialog was part of an adventure and we haven't already found the most recent 
                    //from that adventure add the dialog to the most recent adventure list
                    foundAdventures.Add(dialog.Adventure);
                    mostRecentAdventureDialogs.Add(HistoricalDialogs[i].DialogIndex);
                }
                j++;
                if (j > 400) break; //don't go through all of time looking for active adventures
            }
            return mostRecentAdventureDialogs;
        }

        bool CheckIfDialogModelUsedRecently(int dialogModel) {
            foreach (var recentDialogQueueEntry in RecentDialogs) // try again if dialog model recentlyused
            {
                if (recentDialogQueueEntry == dialogModel) {
                    if (SessionVars.ShowDupePhrases) {
                        Console.WriteLine("Duplicate Dialog [" + dialogModel + "]");
                    }
                    return true;
                }
            }
            return false;
        }

        bool CheckIfCharactersHavePhrasesForDialog(int dialogModel, int character1Num, int character2Num) {
            int currentCharacter = character1Num;
            foreach (var phraseType in ModelDialogs[dialogModel].PhraseTypeSequence) {
                //try again if characters lack phrases for this model
                if (CharacterList[currentCharacter].PhraseTotals.PhraseWeights[phraseType] < 0.015f) {
                    return false;
                }
                if (currentCharacter == character1Num) {
                    currentCharacter = character2Num;
                }
                else {
                    currentCharacter = character1Num;
                }
            }
            return true;
        }

        bool CheckIfDialogPreRequirementMet(int dialogModel) {
            if (ModelDialogs[dialogModel].Requires == null || ModelDialogs[dialogModel].Requires.Count == 0)
            {    //this dialog model requires nothing, so its requirements are met
                return true;
            }
            if (!HistoricalDialogs.Any()) {
                return false;
            }
            var lastHistoricalDialog = HistoricalDialogs.Last();
            foreach (var requiredTag in ModelDialogs[dialogModel].Requires) {
                var currentRequiredTagSatisfied = false;
                foreach (var histDialog in HistoricalDialogs) { // could speed by only going through unique historical dialog index #s
                    if (ModelDialogs[histDialog.DialogIndex].Adventure == ModelDialogs[dialogModel].Adventure) {
                        foreach (var providedTag in ModelDialogs[histDialog.DialogIndex].Provides) {
                            if (providedTag == requiredTag) {
                                currentRequiredTagSatisfied = true;
                                break;
                            }
                        }
                    }
                    if (currentRequiredTagSatisfied) {
                        break;
                    }
                    if (histDialog == lastHistoricalDialog)
                    {  // went through all the historical dialogs found nothing provides for currentRequiredTag
                        return false;
                    }
                }
            }
            return true;
        }

        int PickAWeightedDialog(int character1Num, int character2Num, bool sameCharactersAsLast) {
            //TODO check that all characters/phrasetypes required for adventure are included before starting adventure?
            int dialogModel = 0;

            var mostRecentAdventureDialogs = FindMostRecentAdventures();
            if (mostRecentAdventureDialogs.Count > 0) {
                //if we have recently done adventures give priority to adventures check them first
                foreach (var recentAdventure in mostRecentAdventureDialogs) {  //given recent adventures
                    foreach (var possibleDialog in ModelDialogs) {  //look for follow on adventure possibilities
                        var possibleDialogIdx = ModelDialogs.IndexOf(possibleDialog);
                        if (ModelDialogs[recentAdventure].Adventure == possibleDialog.Adventure &&
                            ModelDialogs[recentAdventure].Provides == possibleDialog.Requires &&  
                            //if a the most recent adventure dialog in the adventure provides what we require we won't go backwards in adventures
                            CheckIfCharactersHavePhrasesForDialog(possibleDialogIdx, character1Num, character2Num))
                        {
                            return dialogModel;
                        }
                    }
                }
            }

            if (!sameCharactersAsLast) {  //give next priority to greetings
                dialogModel = RandomNumbers.Gen.Next(0, 9); //increase odds of starting with greeting
                if (dialogModel < 2) // TODO need a better way to call out greeting dialog models than 0 and 1 in list
                    return dialogModel;
            }

            double dialogWeightIndex = 0.0;
            int attempts = 0;
            bool dialogModelFits = false;
            while (!dialogModelFits && attempts < 4000) {
                attempts++;
                // exclude greetings at 0 and 1 TODO use .Greeting instead of hard coded const
                dialogWeightIndex = RandomNumbers.Gen.NextDouble();
                dialogWeightIndex *= (DialogModelPopularitySum - 0.4);
                dialogWeightIndex += 0.4; // TODO better way to avoid greetings than by weight 0.2 each
                double currentDialogWeightSum = 0;
                foreach (var dialog in ModelDialogs) {
                    currentDialogWeightSum += dialog.Popularity;
                    if (currentDialogWeightSum > dialogWeightIndex) {
                        dialogModel = ModelDialogs.IndexOf(dialog);
                        break;
                    }
                }
                var dialogModelUsedRecently = CheckIfDialogModelUsedRecently(dialogModel);
                var charactersHavePhrases = CheckIfCharactersHavePhrasesForDialog(dialogModel, Character1Num, Character2Num);
                var dialogPreRequirementsMet = CheckIfDialogPreRequirementMet(dialogModel);

                if (dialogPreRequirementsMet && charactersHavePhrases && !dialogModelUsedRecently) { 
                    dialogModelFits = true;
                }
            }
            return dialogModel;
        }

        public PhraseEntry PickAWeightedPhrase(int speakingCharacter, PhraseTypes currentPhraseType) {
            PhraseEntry selectedPhrase = CharacterList[speakingCharacter].Phrases[0]; //initialize to unused phrase
            //Randomly select a phrase of correct Type
            bool phraseIsDuplicate = true;
            for (int k = 0; k < 6 && phraseIsDuplicate; k++) //do retries if selected phrase is recently used
            {
                phraseIsDuplicate = false;
                var phraseTableWeightedIndex = RandomNumbers.Gen.NextDouble(); // rand 0.0 - 1.0
                phraseTableWeightedIndex *= CharacterList[speakingCharacter].PhraseTotals.PhraseWeights[currentPhraseType];
                double amountOfCurrentPhraseType = 0;
                foreach (var currentPhraseTableEntry in CharacterList[speakingCharacter].Phrases) {
                    if (currentPhraseTableEntry.PhraseWeights.ContainsKey(currentPhraseType))
                        amountOfCurrentPhraseType += currentPhraseTableEntry.PhraseWeights[currentPhraseType];

                    if (amountOfCurrentPhraseType > phraseTableWeightedIndex) {
                        selectedPhrase = currentPhraseTableEntry;
                        break; //inner foreach since we have the phrase we want
                    }
                }
                foreach (var recentPhraseQueueEntry in CharacterList[speakingCharacter].RecentPhrases) {
                    if (recentPhraseQueueEntry == selectedPhrase) {
                        phraseIsDuplicate = true; //send through retry loop k again
                        if (SessionVars.ShowDupePhrases) {
                            Console.WriteLine("Duplicate [" + selectedPhrase.DialogStr + "]");
                        }
                        break; // doesn't matter if duplicated more than once
                    }
                }
            }

            //eventually overload enque to remove first to keep size same or create a replace
            CharacterList[speakingCharacter].RecentPhrases.Dequeue();
            CharacterList[speakingCharacter].RecentPhrases.Enqueue(selectedPhrase);
            return selectedPhrase;
        }

        bool ImportClosestSerialComsCharacters() {
            var tempChar1 = SerialComs.NextCharacter1;
            var tempChar2 = SerialComs.NextCharacter2;
            if (tempChar1 == tempChar2 || tempChar1 > 4 || tempChar2 > 4) {
                return false;
            }
            SameCharactersAsLast =
                (tempChar1 == _priorCharacter1Num || tempChar1 == _priorCharacter2Num) &&
                (tempChar2 == _priorCharacter1Num || tempChar2 == _priorCharacter2Num);

            Character1Num = tempChar1;
            Character2Num = tempChar2;
            _priorCharacter1Num = Character1Num;
            _priorCharacter2Num = Character2Num;
            return true;
        }

        bool WaitingForMovement() {
            if (LastPhraseImpliedMovement && SameCharactersAsLast && !SessionVars.NoSerialPort) {
                Thread.Sleep(RandomNumbers.Gen.Next(0, 3000));
                _movementWaitCount++;
                if (_movementWaitCount == 3) {
                    var ch1RetreatPhrase = PickAWeightedPhrase(Character1Num, PhraseTypes.Retreat);
                    Console.Write(CharacterList[Character1Num].CharacterName + " Wait3 : ");
                    Console.WriteLine(ch1RetreatPhrase.DialogStr);
                    PlayAudio(@"c:\DialogAudio\" + CharacterList[Character1Num].CharacterPrefix +
                              "_" + ch1RetreatPhrase.FileName + ".mp3");
                    return true;
                }
                if (_movementWaitCount == 7) {
                    LastPhraseImpliedMovement = false;
                    var ch2RetreatPhrase = PickAWeightedPhrase(Character2Num, PhraseTypes.Retreat);
                    Console.Write(CharacterList[Character2Num].CharacterName + " Wait5 : ");
                    Console.WriteLine(ch2RetreatPhrase.DialogStr);
                    PlayAudio(@"c:\DialogAudio\" + CharacterList[Character2Num].CharacterPrefix +
                              "_" + ch2RetreatPhrase.FileName + ".mp3");
                    return true;
                }
                if (_movementWaitCount > 11) {
                    _movementWaitCount = 0;
                    LastPhraseImpliedMovement = false; //reset the wait for movement flag after waiting a long time
                }
                return true;
            }
            LastPhraseImpliedMovement = false; //reset the wait for movement flag when we are no longer same characters as last time
            _movementWaitCount = 0;
            return false;
        }

        bool DetermineIfMovementImplied(PhraseEntry selectedPhrase) {
            double insultWeight, retrWeight, threatWeight, shutUpWeight;
            selectedPhrase.PhraseWeights.TryGetValue(PhraseTypes.Insult, out insultWeight);
            selectedPhrase.PhraseWeights.TryGetValue(PhraseTypes.Retreat, out retrWeight);
            selectedPhrase.PhraseWeights.TryGetValue(PhraseTypes.Threat, out threatWeight);
            selectedPhrase.PhraseWeights.TryGetValue(PhraseTypes.ShutUp, out shutUpWeight);
            if (insultWeight + retrWeight + threatWeight + shutUpWeight > 0.1) {
                return true;
            }
            return false;
        }

        void ProcessDebugFlags(params int[] dialogDirectives) {
            if (dialogDirectives.Count() == 3) //if the array input is correct size and inputs don't exceed bounds set dialog parameters 
            {
                if (dialogDirectives[0] < ModelDialogs.Count)
                    _currentDialogModel = dialogDirectives[0];
                if (dialogDirectives[1] < CharacterList.Count)
                    Character1Num = dialogDirectives[1];
                if (dialogDirectives[2] < CharacterList.Count)
                    Character2Num = dialogDirectives[2];
            }

            if (SessionVars.DebugFlag) {
                WriteDialogInfo(_currentDialogModel, Character1Num, Character2Num);
            }
            if (SessionVars.HeatMapFullMatrixDispMode) {
                SerialComs.PrintHeatMap();
            }
            if (SessionVars.HeatMapSumsMode) {
                SerialComs.PrintHeatMapSums();
            }
        }

        public bool DialogTrackerAndSerialComsCharactersSame() {
            if ((Character1Num == SerialComs.NextCharacter1 || Character1Num == SerialComs.NextCharacter2)
                && (Character2Num == SerialComs.NextCharacter2 || Character2Num == SerialComs.NextCharacter1)) {
                return true;
            }
            return false;
        }

        void AddPhraseToHistory(PhraseEntry selectedPhrase, int speakingCharacter) {
            HistoricalPhrases.Add(new HistoricalPhrase(){
                CharacterIndex = speakingCharacter,
                CharacterPrefix = CharacterList[speakingCharacter].CharacterPrefix,
                PhraseIndex = CharacterList[speakingCharacter].Phrases.IndexOf(selectedPhrase),
                PhraseFile = selectedPhrase.FileName,
                StartedTime = DateTime.Now
            });

            if (SessionVars.WriteSerialLog) {
                using (StreamWriter serialLogDialogLines = new StreamWriter(
                    @"c:\Isaac\Toys2LifeResources\CapturesAndAnalysis\SerialLogDialog.txt", true)) {
                    serialLogDialogLines.WriteLine(CharacterList[speakingCharacter].CharacterName + ": " + selectedPhrase.DialogStr);
                    serialLogDialogLines.Close();
                }
            }
        }

        public void GenerateADialog(params int[] dialogDirectives) {
            if (!ImportClosestSerialComsCharacters()) {
                return;
            }
            _currentDialogModel = PickAWeightedDialog(Character1Num, Character2Num, SameCharactersAsLast);
            if (WaitingForMovement()) {
                return;
            }
            ProcessDebugFlags(dialogDirectives);
            AddDialogModelToHistory(_currentDialogModel, Character1Num, Character2Num);

            int speakingCharacter = Character1Num;
            PhraseEntry selectedPhrase = CharacterList[speakingCharacter].Phrases[0]; //initialize to unused placeholder phrase

            //step through the current model dialog an select a phrase from each character 
            //  that matches the prescribed phrase type sequence
            foreach (var currentPhraseType in ModelDialogs[_currentDialogModel].PhraseTypeSequence) {
                Console.Write(CharacterList[speakingCharacter].CharacterName + ": ");
                if (CharacterList[speakingCharacter].PhraseTotals.PhraseWeights[currentPhraseType] < 0.01f)
                    Console.WriteLine("   Missing PhraseType: " + currentPhraseType + "\r\n");

                selectedPhrase = PickAWeightedPhrase(speakingCharacter, currentPhraseType);
                Console.WriteLine(selectedPhrase.DialogStr);

                AddPhraseToHistory(selectedPhrase, speakingCharacter);

                var pathAndFileName = @"c:\DialogAudio\" + CharacterList[speakingCharacter].CharacterPrefix +
                                      "_" + selectedPhrase.FileName + ".mp3";
                PlayAudio(pathAndFileName);

                if (!SessionVars.ForceCharacterSelection &&
                    !DialogTrackerAndSerialComsCharactersSame()) {
                    SameCharactersAsLast = false;
                    return; // the characters have moved  TODO break into charactersSame() and use also with prior
                }
                //Toggle character
                if (speakingCharacter == Character1Num) //toggle which character is speaking next
                    speakingCharacter = Character2Num;
                else
                    speakingCharacter = Character1Num;
            }
            HistoricalDialogs[HistoricalDialogs.Count - 1].Completed = true;
            if (HistoricalDialogs.Count > 2000) {
                HistoricalDialogs.RemoveRange(0, 100);
            }
            if (HistoricalPhrases.Count > 8000) {
                HistoricalPhrases.RemoveRange(0, 100);
            }

            RecentDialogs.Dequeue(); //move to use HistoricalDialogs
            RecentDialogs.Enqueue(_currentDialogModel);
            LastPhraseImpliedMovement = DetermineIfMovementImplied(selectedPhrase);
        }
    }
}
