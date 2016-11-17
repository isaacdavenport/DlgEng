using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace DialogEngine
{
    public class DialogTracker
    {
        //master list of all the various types of phrase exchanges that could happen to make up a dialog
        protected const int RecentDialogsQueSize = 4;
        public List<ModelDialog> ModelDialogs = new List<ModelDialog>();
        public Queue<int> RecentDialogs = new Queue<int>();
        public List<Character> CharacterList = new List<Character>();
        public int Character1Num = 0;
        public int Character2Num = 1;
        int priorCharacter1Num = 100;
        int priorCharacter2Num = 100;
        int currentDialogModel = 1;
        public MP3Player player = new MP3Player();
        public bool sameCharactersAsLast = false;
        public bool lastPhraseImpliedMovement = false;
        private static int movementWaitCount = 0;
        public double popularitySum;


        public DialogTracker() {
            CharacterList.Add(new Cowboy());
            CharacterList.Add(new SchoolMarm());
            CharacterList.Add(new ReOrgLead());
            // CharacterList.Add(new Skyler());
            CharacterList.Add(new SchoolBoy());
            CharacterList.Add(new Cartman());
            for (int i = 0; i < RecentDialogsQueSize; i++) {
                RecentDialogs.Enqueue(0);  // Fill the que with greeting dialogs
            }
        }

        public void PlayAudio(string pathAndFileName)
        {
            if (File.Exists(pathAndFileName))
            {
                FileInfo fileInfo = new FileInfo(pathAndFileName);
                //empirical hack to wait reasonable time since Play() has no return to know when playing is complete
                long songMilliSeconds = fileInfo.Length / 14;
                var playSuccess = player.Play(pathAndFileName);
                if (playSuccess != 0)
                {
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

        public void WriteDialogInfo(int CurrentDialogModel, int Character1Num, int Character2Num) {
            string dialogModelString = "  --DiMod " + CurrentDialogModel + " " + ModelDialogs[CurrentDialogModel].Name +
                                       " NextChars: " + CharacterList[Character1Num].CharacterPrefix + " " +
                                       CharacterList[Character2Num].CharacterPrefix + " " + DateTime.Now;

            Console.WriteLine(dialogModelString);
            if (SessionVars.WriteSerialLog) {
                using (StreamWriter serialLogDialogModels = new StreamWriter(
                @"c:\Isaac\Toys2LifeResources\CapturesAndAnalysis\SerialLogDialog.txt", true))
                {
                    serialLogDialogModels.WriteLine(dialogModelString);
                    serialLogDialogModels.Close();
                }

            }
        }

        int PickAWeightedDialog(int character1Num, int character2Num, bool sameCharactersAsLast) {
            int dialogModel = 0;
            double dialogWeightIndex = 0.0;
            int attempts = 0;
            int currentCharacter = character1Num;
            bool dialogModelFitsCharacters = false;
            const int MAX_ATTEMPTS = 4000;

            if (!sameCharactersAsLast) {
                dialogModel = RandomNumbers.Gen.Next(0, 9); //increase odds of starting with greeting
                if (dialogModel < 2) // TODO need a better way to call out greeting dialog models than 0 and 1 in list {
                    return dialogModel;
            }

            while (!dialogModelFitsCharacters && attempts < MAX_ATTEMPTS) {
                attempts++;
                dialogModelFitsCharacters = true;
                // exclude greetings at 0 and 1 TODO use .Greeting instead of hard coded const
                dialogWeightIndex = RandomNumbers.Gen.NextDouble();
                dialogWeightIndex *= (popularitySum - 0.4);
                dialogWeightIndex += 0.4;  // to avoid greetings which weigh 0.2 each
                double currentDialogWeightSum = 0;
                foreach (var dialog in ModelDialogs) {
                    currentDialogWeightSum += dialog.Popularity;
                    if (currentDialogWeightSum > dialogWeightIndex) {
                        dialogModel = ModelDialogs.IndexOf(dialog);
                        break;
                    }
                }
                foreach (var recentDialogQueueEntry in RecentDialogs)  // try again if dialog model recentlyused
                {
                    if (recentDialogQueueEntry == dialogModel)
                    {
                        dialogModelFitsCharacters = false;
                        if (SessionVars.ShowDupePhrases)
                        {
                            Console.WriteLine("Duplicate Dialog [" + dialogModel + "]");
                        }
                        break;  // doesn't matter if duplicated more than once
                    }
                }
                foreach (var phraseType in ModelDialogs[dialogModel].PhraseTypeSequence) {  
                    //try again if characters lack phrases for this model
                    if (CharacterList[currentCharacter].PhraseTotals.PhraseWeights[phraseType] < 0.015f) {
                        dialogModelFitsCharacters = false;
                        break;
                    }
                    if (currentCharacter == character1Num) {
                        currentCharacter = character2Num;
                    }
                    else {
                        currentCharacter = character1Num;
                    }
                }
            }
            return dialogModel;
        }

        public PhraseEntry PickAWeightedPhrase(int SpeakingCharacter, PhraseTypes currentPhraseType) {
            PhraseEntry selectedPhrase = CharacterList[SpeakingCharacter].Phrases[0]; //initialize to unused phrase
            //Randomly select a phrase of correct Type
            bool phraseIsDuplicate = true;
            for (int k = 0; k < 6 && phraseIsDuplicate; k++) //do retries if selected phrase is recently used
            {
                phraseIsDuplicate = false;
                var phraseTableWeightedIndex = ((double)RandomNumbers.Gen.NextDouble()); // rand 0.0 - 1.0
                phraseTableWeightedIndex *= CharacterList[SpeakingCharacter].PhraseTotals.PhraseWeights[currentPhraseType];
                double amountOfCurrentPhraseType = 0;
                foreach (var currentPhraseTableEntry in CharacterList[SpeakingCharacter].Phrases)
                {
                    if (currentPhraseTableEntry.PhraseWeights.ContainsKey(currentPhraseType))
                        amountOfCurrentPhraseType += currentPhraseTableEntry.PhraseWeights[currentPhraseType];

                    if (amountOfCurrentPhraseType > phraseTableWeightedIndex)
                    {
                        selectedPhrase = currentPhraseTableEntry;
                        break; //inner foreach since we have the phrase we want
                    }
                }
                foreach (var recentPhraseQueueEntry in CharacterList[SpeakingCharacter].RecentPhrases)
                {
                    if (recentPhraseQueueEntry == selectedPhrase)
                    {
                        phraseIsDuplicate = true; //send through retry loop k again
                        if (SessionVars.ShowDupePhrases)
                        {
                            Console.WriteLine("Duplicate [" + selectedPhrase.DialogStr + "]");
                        }
                        break;  // doesn't matter if duplicated more than once
                    }
                }
            }

            //eventually overload enque to remove first to keep size same or create a replace
            CharacterList[SpeakingCharacter].RecentPhrases.Dequeue();
            CharacterList[SpeakingCharacter].RecentPhrases.Enqueue(selectedPhrase);
            return selectedPhrase;
        }

        bool SelectCharacters() {
            int tempChar1, tempChar2;
            tempChar1 = SerialComs.nextCharacter1;
            tempChar2 = SerialComs.nextCharacter2;
            if (tempChar1 == tempChar2 || tempChar1 > 4 || tempChar2 > 4) {
                return false;
            }
            sameCharactersAsLast =
                (tempChar1 == priorCharacter1Num || tempChar1 == priorCharacter2Num) &&
                (tempChar2 == priorCharacter1Num || tempChar2 == priorCharacter2Num);

            Character1Num = tempChar1;
            Character2Num = tempChar2;
            priorCharacter1Num = Character1Num;
            priorCharacter2Num = Character2Num;
            return true;
        }

        bool ActIfWeHaveBeenWaiting() {
            if (lastPhraseImpliedMovement && sameCharactersAsLast && !SessionVars.NoSerialPort)
            {
                Thread.Sleep(RandomNumbers.Gen.Next(0, 3000));
                movementWaitCount++;
                if (movementWaitCount == 3)
                {
                    var ch1RetreatPhrase = PickAWeightedPhrase(Character1Num, PhraseTypes.Retreat);
                    Console.Write(CharacterList[Character1Num].CharacterName + " Wait3 : ");
                    Console.WriteLine(ch1RetreatPhrase.DialogStr);
                    PlayAudio(@"c:\DialogAudio\" + CharacterList[Character1Num].CharacterPrefix +
                              "_" + ch1RetreatPhrase.FileName + ".mp3");
                    return true;
                }
                if (movementWaitCount == 7)
                {
                    lastPhraseImpliedMovement = false;
                    var ch2RetreatPhrase = PickAWeightedPhrase(Character2Num, PhraseTypes.Retreat);
                    Console.Write(CharacterList[Character2Num].CharacterName + " Wait5 : ");
                    Console.WriteLine(ch2RetreatPhrase.DialogStr);
                    PlayAudio(@"c:\DialogAudio\" + CharacterList[Character2Num].CharacterPrefix +
                              "_" + ch2RetreatPhrase.FileName + ".mp3");
                    return true;
                }
                if (movementWaitCount > 11)
                {
                    movementWaitCount = 0;
                    lastPhraseImpliedMovement = false; //reset the wait for movement flag after waiting a long time
                }
                return true;
            }
            lastPhraseImpliedMovement = false; //reset the wait for movement flag when we are no longer same characters as last time
            movementWaitCount = 0;
            return false;
        }

        bool DetermineIfMovementImplied(PhraseEntry selectedPhrase ) {
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

        public void GenerateADialog(params int[] DialogDirectives) {

            if (!SelectCharacters()) {
                return;
            }
            currentDialogModel = PickAWeightedDialog(Character1Num, Character2Num, sameCharactersAsLast);
            if (ActIfWeHaveBeenWaiting()) {
                return;
            }

            if (DialogDirectives.Count() == 3) //if the array input is correct size and inputs don't exceed bounds set dialog parameters 
            {
                if (DialogDirectives[0] < ModelDialogs.Count)
                    currentDialogModel = DialogDirectives[0];
                if (DialogDirectives[1] < CharacterList.Count)
                    Character1Num = DialogDirectives[1];
                if (DialogDirectives[2] < CharacterList.Count)
                    Character2Num = DialogDirectives[2];
            }

            if (SessionVars.DebugFlag) {
                WriteDialogInfo(currentDialogModel, Character1Num, Character2Num);
            }
            if (SessionVars.HeatMapFullMatrixDispMode) {
                SerialComs.PrintHeatMap();
            }
            if (SessionVars.HeatMapSumsMode)
            {
                SerialComs.PrintHeatMapSums();
            }

            int SpeakingCharacter = Character1Num;
            PhraseEntry selectedPhrase = CharacterList[SpeakingCharacter].Phrases[0]; //initialize to unused placeholder phrase

            //step through the current model dialog an select a phrase from each character 
            //  that matches the prescribed phrase type sequence
            foreach (var currentPhraseType in ModelDialogs[currentDialogModel].PhraseTypeSequence) {
                Console.Write(CharacterList[SpeakingCharacter].CharacterName + ": ");
                if (CharacterList[SpeakingCharacter].PhraseTotals.PhraseWeights[currentPhraseType] < 0.01f)
                    Console.WriteLine("   Missing PhraseType: " + currentPhraseType + "\r\n");

                selectedPhrase = PickAWeightedPhrase(SpeakingCharacter, currentPhraseType);
                Console.WriteLine(selectedPhrase.DialogStr);

                if (SessionVars.WriteSerialLog)
                {
                    using (StreamWriter serialLogDialogLines = new StreamWriter(
                        @"/Users/joey/Documents/Coding/Toys2Life/DialogEngine/CapturesAndAnalysis/serialLog.txt", true))
                    {
                        serialLogDialogLines.WriteLine(CharacterList[SpeakingCharacter].CharacterName + ": " + selectedPhrase.DialogStr);
                        serialLogDialogLines.Close();
                    }

                }

                var pathAndFileName = @"/Users/joey/Documents/Coding/Toys2Life/DialogAudio" + CharacterList[SpeakingCharacter].CharacterPrefix +
                                      "_" + selectedPhrase.FileName + ".mp3";
                PlayAudio(pathAndFileName);

                if (!SessionVars.ForceCharacterSelection && 
                    !((Character1Num == SerialComs.nextCharacter1 || Character1Num == SerialComs.nextCharacter2)
                      && (Character2Num == SerialComs.nextCharacter2 || Character2Num == SerialComs.nextCharacter1))) {
                    sameCharactersAsLast = false;
                    return; // the characters have moved  TODO break into charactersSame() and use also with prior
                }
                //Toggle character
                if (SpeakingCharacter == Character1Num) //toggle which character is speaking next
                    SpeakingCharacter = Character2Num;
                else
                    SpeakingCharacter = Character1Num;
            }
            RecentDialogs.Dequeue();
            RecentDialogs.Enqueue(currentDialogModel);
            lastPhraseImpliedMovement = DetermineIfMovementImplied(selectedPhrase);
        }
    }
}