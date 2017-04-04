using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Newtonsoft.Json; 

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
        int CurrentDialogModel = 1;
        // public Mp3Player Player = new Mp3Player();
        public WindowsMediaPlayerMp3 Audio = new WindowsMediaPlayerMp3();
        public bool SameCharactersAsLast = false;
        public bool LastPhraseImpliedMovement = false;
        private static int _movementWaitCount = 0;
        public double DialogModelPopularitySum;

        public Character ParseCharJSON(FileInfo CharFile) {
            using (StreamReader fi = File.OpenText(CharFile.FullName)) {
                JsonSerializer serializer = new JsonSerializer();
                Character CharObj = (Character)serializer.Deserialize(fi, typeof(Character));
                return CharObj;
            }
        }

        public void HandleDeserializationError(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs errorArgs) {
            Console.WriteLine(errorArgs.ErrorContext.Error.Message);
            Console.WriteLine("Please correct your JSON file and restart the program. Control-C will exit.");
            Console.ReadLine();
            errorArgs.ErrorContext.Handled = true;
        }

        public DialogTracker() {
            //JSON parse here.
            DirectoryInfo d = new DirectoryInfo(SessionVars.CharactersDirectory);
            foreach (FileInfo file in d.GetFiles("*.json")) //file of type FileInfo for each .json in directory
            {
                Console.WriteLine("Begin read of " + file);
                string inChar;
                FileStream fs = file.OpenRead(); //open a read-only FileStream
                using (StreamReader reader = new StreamReader(fs)) //creates new streamerader for fs stream. Could also construct with filename...
                {
                    inChar = reader.ReadToEnd();
                    Character deserializedCharacterJSON = JsonConvert.DeserializeObject<Character>(inChar,
                        new JsonSerializerSettings{
                            Error = HandleDeserializationError

                        });

                    deserializedCharacterJSON.PhraseTotals = new PhraseEntry(); //init PhraseTotals
                    deserializedCharacterJSON.PhraseTotals.DialogStr = "phrase weights";
                    deserializedCharacterJSON.PhraseTotals.FileName = "silence";
                    deserializedCharacterJSON.PhraseTotals.PhraseRating = "G";
                    deserializedCharacterJSON.PhraseTotals.phraseWeights = new Dictionary<string, double>();
                    deserializedCharacterJSON.PhraseTotals.phraseWeights.Add("Greeting", 0.0f);

                    //Calculate Phrase Weight Totals here.
                    foreach (PhraseEntry _curPhrase in deserializedCharacterJSON.Phrases) {
                        foreach (string tag in _curPhrase.phraseWeights.Keys) {
                            if (deserializedCharacterJSON.PhraseTotals.phraseWeights.Keys.Contains(tag)) {
                                deserializedCharacterJSON.PhraseTotals.phraseWeights[tag] += _curPhrase.phraseWeights[tag];
                            }
                            else {
                                deserializedCharacterJSON.PhraseTotals.phraseWeights.Add(tag, _curPhrase.phraseWeights[tag]);
                            }
                        }
                    }
                    for (var i = 0; i < Character.RecentPhrasesQueueSize; i++) {
                        // we always deque after enque so this sets que size
                        deserializedCharacterJSON.RecentPhrases.Enqueue(deserializedCharacterJSON.Phrases[0]);
                    }
                    //list Chars as they come in.
                    Console.WriteLine("Finish read of " + deserializedCharacterJSON.CharacterName);
                    RemovePhrasesOverParentalRating(deserializedCharacterJSON);
                    //Add to Char List
                    CharacterList.Add(deserializedCharacterJSON);
                }
            }
            // Fill the queue with greeting dialogs
            for (int i = 0; i < RecentDialogsQueSize; i++) {
                RecentDialogs.Enqueue(0); // Fill the que with greeting dialogs
            }
        }

        private static void RemovePhrasesOverParentalRating(Character inCharacter) {
            int maxParentalRating = ParentalRatings.GetNumeric(SessionVars.CurrentParentalRating);
            int minParentalRating = ParentalRatings.GetNumeric("G");
            inCharacter.Phrases.RemoveAll(item => 
               ParentalRatings.GetNumeric(item.PhraseRating) > maxParentalRating ||
               ParentalRatings.GetNumeric(item.PhraseRating) < minParentalRating);
        }

        public void PlayAudio(string pathAndFileName) {
            if (!SessionVars.AudioDialogsOn) {
                Thread.Sleep(2200);
                return;
            }
            if (File.Exists(pathAndFileName)) {
                var playSuccess = Audio.PlayMp3(pathAndFileName);
               
                if (playSuccess != 0) {
                    Console.WriteLine("");
                    Console.WriteLine("   MP3 Play Error  ---  " + playSuccess);
                    Console.WriteLine("");
                }
                int i = 0;
                Thread.Sleep(600);
                while (Audio.IsPlaying() && i < 250) {  // 20 seconds is max
                    Thread.Sleep(100);
                }
                Thread.Sleep(1600);  // wait around a second after the audio is done for between phrase pause
            }
            else {
                Console.WriteLine("Could not find: " + pathAndFileName);
            }
        }
        
        void SwapCharactersOneAndTwo() {
            var tempCh1 = Character1Num;
            Character1Num = Character2Num;
            Character2Num = tempCh1;
            // it doesn't appear we should update prior characters 1 and 2 here
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

        public void WriteDialogInfo(int character1Num, int character2Num) {
            string dialogModelString = "  --DiMod " + CurrentDialogModel + " " + ModelDialogs[CurrentDialogModel].Name +
                                       " NextChars: " + CharacterList[character1Num].CharacterPrefix + " " +
                                       CharacterList[character2Num].CharacterPrefix + " " + DateTime.Now;

            Console.WriteLine(dialogModelString);
            if (SessionVars.WriteSerialLog) {
                using (StreamWriter serialLogDialogModels = new StreamWriter(
                    (SessionVars.LogsDirectory + SessionVars.DialogSerialLogFileName), true)) {
                    serialLogDialogModels.WriteLine(dialogModelString);
                    serialLogDialogModels.Close();
                }
            }
        }

        List<int> FindMostRecentAdventureDialogIndexes() {
            List<int> mostRecentAdventureDialogs = new List<int>();
            // most recent will be in the 0 index of list
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
            foreach (string element in ModelDialogs[dialogModel].PhraseTypeSequence)
            {
                //try again if characters lack phrases for this model
                if (CharacterList[currentCharacter].PhraseTotals.phraseWeights.ContainsKey(element))
                {
                    if (CharacterList[currentCharacter].PhraseTotals.phraseWeights[element] < 0.015f) {
                        return false;
                    }
                    if (currentCharacter == character1Num) {
                        currentCharacter = character2Num;
                    }
                    else {
                        currentCharacter = character1Num;
                    }
                }
                else
                {
                    return false;
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

        int FindNextAdventureDialogForCharacters(int character1Num, int character2Num, List<int> mostRecentAdventureDialogIndexes) {
            bool ch1First = new bool();
            bool ch2First = new bool();

            //if we have recently done adventures give priority to adventure dialogs check them first
            foreach (var recentAdventureIdx in mostRecentAdventureDialogIndexes)
                {  //given recent adventures
                foreach (var possibleDialog in ModelDialogs)  //TODO probably a cleaner way to do this with Linq and lamda expressions
                {  //look for follow on adventure possibilities
                    var possibleDialogIdx = ModelDialogs.IndexOf(possibleDialog);
                    if (ModelDialogs[recentAdventureIdx].Adventure == possibleDialog.Adventure)
                    {
                        foreach (var providedStringKey in ModelDialogs[recentAdventureIdx].Provides)
                        {
                            if (possibleDialog.Requires.Contains(providedStringKey))
                            {   //if a the most recent adventure dialog in the adventure provides what we require we won't 
                                //go backwards in adventures
                                ch1First = CheckIfCharactersHavePhrasesForDialog(possibleDialogIdx,
                                    character1Num, character2Num);
                                ch2First = CheckIfCharactersHavePhrasesForDialog(possibleDialogIdx,
                                    character2Num, character1Num);
                                if (ch1First || ch2First) {
                                    if (ch2First) {
                                        //swap character one and two
                                        SwapCharactersOneAndTwo();
                                    }
                                    return possibleDialogIdx;
                                }
                            }
                        }
                        }
                    }   
                }
            return -1;  // code for no next adventure continuance found
            }

        int PickAWeightedDialog(int character1Num, int character2Num) {
            //TODO check that all characters/phrasetypes required for adventure are included before starting adventure?
            int dialogModel = 0;

            var mostRecentAdventureDialogIndexes = FindMostRecentAdventureDialogIndexes();
              // most recent will be in the 0 index of list which will be hit first in foreach
            if (mostRecentAdventureDialogIndexes.Count > 0) {
                var nextAdventureDialogIdx = FindNextAdventureDialogForCharacters(character1Num, character2Num, mostRecentAdventureDialogIndexes);
                if (nextAdventureDialogIdx > 0 && nextAdventureDialogIdx < ModelDialogs.Count) {
                    return nextAdventureDialogIdx;  // we have an adventure dialog for these characters go with it
                }
            }

            if (!SameCharactersAsLast && !SessionVars.WaitIndefinatelyForMove) {  //give next priority to greetings
                dialogModel = RandomNumbers.Gen.Next(0, 9); //increase odds of starting with greeting
                if (dialogModel< 2) // TODO need a better way to call out greeting dialog models than 0 and 1 in list
                    return dialogModel;
            }
            double dialogWeightIndex = 0.0;
            int attempts = 0;
            bool dialogModelFits = false;
            while (!dialogModelFits && attempts< 4000) {
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
        
        public PhraseEntry PickAWeightedPhrase(int speakingCharacter, string currentPhraseType) {
            PhraseEntry selectedPhrase = CharacterList[speakingCharacter].Phrases[0]; //initialize to unused phrase
            //Randomly select a phrase of correct Type
            bool phraseIsDuplicate = true;
            for (int k = 0; k < 6 && phraseIsDuplicate; k++) //do retries if selected phrase is recently used
            {
                phraseIsDuplicate = false;
                var phraseTableWeightedIndex = RandomNumbers.Gen.NextDouble(); // rand 0.0 - 1.0
                phraseTableWeightedIndex *= CharacterList[speakingCharacter].PhraseTotals.phraseWeights[currentPhraseType];
                double amountOfCurrentPhraseType = 0;
                foreach (var currentPhraseTableEntry in CharacterList[speakingCharacter].Phrases) {
                    if (currentPhraseTableEntry.phraseWeights.ContainsKey(currentPhraseType))
                        amountOfCurrentPhraseType += currentPhraseTableEntry.phraseWeights[currentPhraseType];

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
            if (tempChar1 == tempChar2 || tempChar1 > SerialComs.NUM_RADIOS - 1 || tempChar2 > SerialComs.NUM_RADIOS - 1) {
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
                    var ch1RetreatPhrase = PickAWeightedPhrase(Character1Num, "Retreat");
                    Console.Write(CharacterList[Character1Num].CharacterName + " Wait3 : ");
                    Console.WriteLine(ch1RetreatPhrase.DialogStr);
                    PlayAudio(SessionVars.AudioDirectory + CharacterList[Character1Num].CharacterPrefix +
                              "_" + ch1RetreatPhrase.FileName + ".mp3");
                    return true;
                }
                if (_movementWaitCount == 7) {
                    LastPhraseImpliedMovement = false;
                    var ch2RetreatPhrase = PickAWeightedPhrase(Character2Num, "Retreat");
                    Console.Write(CharacterList[Character2Num].CharacterName + " Wait5 : ");
                    Console.WriteLine(ch2RetreatPhrase.DialogStr);
                    PlayAudio(SessionVars.AudioDirectory + CharacterList[Character2Num].CharacterPrefix +
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
            selectedPhrase.phraseWeights.TryGetValue("Insult", out insultWeight);
            selectedPhrase.phraseWeights.TryGetValue("Retreat", out retrWeight);
            selectedPhrase.phraseWeights.TryGetValue("Threat", out threatWeight);
            selectedPhrase.phraseWeights.TryGetValue("ShutUp", out shutUpWeight);
            if (insultWeight + retrWeight + threatWeight + shutUpWeight > 0.1) {
                return true;
            }
            return false;
        }

        void ProcessDebugFlags(params int[] dialogDirectives) {
            if (dialogDirectives.Count() == 3) //if the array input is correct size and inputs don't exceed bounds set dialog parameters 
            {
                if (dialogDirectives[0] < ModelDialogs.Count)
                    CurrentDialogModel = dialogDirectives[0];
                if (dialogDirectives[1] < CharacterList.Count)
                    Character1Num = dialogDirectives[1];
                if (dialogDirectives[2] < CharacterList.Count)
                    Character2Num = dialogDirectives[2];
            }

            if (SessionVars.DebugFlag) {
                WriteDialogInfo(Character1Num, Character2Num);
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
                    (SessionVars.LogsDirectory + SessionVars.DialogSerialLogFileName), true)) {
                    serialLogDialogLines.WriteLine(CharacterList[speakingCharacter].CharacterName + ": " + selectedPhrase.DialogStr);
                    serialLogDialogLines.Close();
                }
            }
        }

        // TODO generate parallel dialogs, using string tags.
        public void GenerateADialog(params int[] dialogDirectives) {
            if (!ImportClosestSerialComsCharacters()) {
                return;
            }
            CurrentDialogModel = PickAWeightedDialog(Character1Num, Character2Num);
            if (WaitingForMovement()  || (SameCharactersAsLast && SessionVars.WaitIndefinatelyForMove)) {
                return;
            }
            ProcessDebugFlags(dialogDirectives);
            AddDialogModelToHistory(CurrentDialogModel, Character1Num, Character2Num);

            int speakingCharacter = Character1Num;
            PhraseEntry selectedPhrase = CharacterList[speakingCharacter].Phrases[0]; //initialize to unused placeholder phrase

            foreach (var currentPhraseType in ModelDialogs[CurrentDialogModel].PhraseTypeSequence)
            {
                if(CharacterList[speakingCharacter].PhraseTotals.phraseWeights.ContainsKey(currentPhraseType))
                {
                    if (SessionVars.TextDialogsOn) {
                        Console.Write(CharacterList[speakingCharacter].CharacterName + ": ");
                    }
                    
                    if (CharacterList[speakingCharacter].PhraseTotals.phraseWeights[currentPhraseType] < 0.01f)
                        Console.WriteLine("   Missing PhraseType: " + currentPhraseType + "\r\n");

                    selectedPhrase = PickAWeightedPhrase(speakingCharacter, currentPhraseType);
                    if (SessionVars.TextDialogsOn) {
                        Console.WriteLine(selectedPhrase.DialogStr);
                    }
                    AddPhraseToHistory(selectedPhrase, speakingCharacter);

                    var pathAndFileName = SessionVars.AudioDirectory + CharacterList[speakingCharacter].CharacterPrefix +
                                          "_" + selectedPhrase.FileName + ".mp3";
                    PlayAudio(pathAndFileName);

                    if (!SessionVars.ForceCharactersAndDialogModel &&
                        !DialogTrackerAndSerialComsCharactersSame())
                    {
                        SameCharactersAsLast = false;
                        return; // the characters have moved  TODO break into charactersSame() and use also with prior
                    }
                    //Toggle character
                    if (speakingCharacter == Character1Num) //toggle which character is speaking next
                        speakingCharacter = Character2Num;
                    else
                        speakingCharacter = Character1Num;
                }
            }
            
            HistoricalDialogs[HistoricalDialogs.Count - 1].Completed = true;
            if (HistoricalDialogs.Count > 2000) {
                HistoricalDialogs.RemoveRange(0, 100);
            }
            if (HistoricalPhrases.Count > 8000) {
                HistoricalPhrases.RemoveRange(0, 100);
            }

            RecentDialogs.Dequeue(); //move to use HistoricalDialogs
            RecentDialogs.Enqueue(CurrentDialogModel);
            LastPhraseImpliedMovement = DetermineIfMovementImplied(selectedPhrase);
        }
    }
}
