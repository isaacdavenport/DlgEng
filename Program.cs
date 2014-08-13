using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Dialog_Generator
{
    public enum PhraseTypes 
    {
        Exclamation, Greeting, Threat, Retreat, Proposal, Yes, No, RequestCatchup, GiveAffirmation, RequestAffirmation, 
        GiveDisbelief, GiveRecentHistory, GiveSurprisingStatement,  Ramble, ShutUp, RequestJoke, GiveJoke, Insult, RequestActivity, GiveActivity, PhraseTypesSize
    };

    public enum GenderType { Male, Female }
    
    public class PhraseTableEntry
    {
        public string DialogStr;
        public float [] PhraseProperties {get; set;}

        public PhraseTableEntry()
        {
            DialogStr = "UnInitialized";
            float [] PhraseProperties = new float [(int)PhraseTypes.PhraseTypesSize];
        }
    }

    public class Character
    {     
        public string CharacterName { get; set; } 
        public GenderType Gender { get; set; }
        public int Age { get; set; }
        public List<PhraseTableEntry> PhraseTable = new List<PhraseTableEntry>();
          //a character's PhraseTable holds all the phrases they might say along with heuristics on what parts of a 
          //model dialog they might use them in.
    }

    public class ModelDialog
    {
        // a ModelDialog is a sequence of phrase types that represent an exchange between characters 
        // the model dialog will be filled with randomly selected character phrases of the appropriate phrase type
        public float Popularity;
        public string Name; 
        public List<PhraseTypes> PhraseTypeSequence = new List<PhraseTypes>(); 
    }

    public class DialogTracker
    {
        //master list of all the various types of phrase exchanges that could happen to make up a dialog
        public List<ModelDialog> ModelDialogTable = new List<ModelDialog>();

        public Random RandNumGenerator = new Random();
        public List<Character> CharacterList = new List<Character>();
        int Character1Num = 0; 
        int Character2Num = 1;
        int CurrentDialogModel;

        public DialogTracker()
        {
            CharacterList.Add(new Character());
            InitCowboy.SetDefaults(CharacterList[0]);
            CharacterList.Add(new Character());
            InitSchoolboy.SetDefaults(CharacterList[1]);
            CharacterList.Add(new Character());
            InitSchoolmarm.SetDefaults(CharacterList[2]);
        }

        public void GenerateADialog(params int[] DialogDirectives)
        {
            if (RandNumGenerator.Next(1, 100) > 66)  //third of the time switch up characters
            {
                Character1Num = RandNumGenerator.Next(0, 3);
                Character2Num = RandNumGenerator.Next(0, 3);
                while (Character1Num == Character2Num)  //ensure we get two different characters talking to one another
                {
                    Character2Num = RandNumGenerator.Next(0, 3);
                }
            }

            //randomly select a DialogModel.  Eventually use the popularity of each DialogModel to select more popular more often
            CurrentDialogModel = RandNumGenerator.Next(0, ModelDialogTable.Count);  //should occasionally pick last one since count is one larger than index from zero

            for (int i = 0; i < DialogDirectives.Length; i++)
            {
                if (i > 0)
                    CurrentDialogModel = DialogDirectives[0];
                if (i > 1)
                    Character1Num = DialogDirectives[1];
                if (i > 2)
                    Character2Num = DialogDirectives[2];
            }    


            Console.Write("Dialog Model: ");
            Console.WriteLine(ModelDialogTable[CurrentDialogModel].Name);
            int SpeakingCharacter = Character1Num;

            //step through the current model dialog an select a phrase from each character that matches the prescribed phrase type sequence
            foreach (PhraseTypes CurrentPhraseType in ModelDialogTable[CurrentDialogModel].PhraseTypeSequence)
            {
                Console.Write(CharacterList[SpeakingCharacter].CharacterName + ": ");
                float AmountOfCurrentPhraseType = 0;
                //Go through the characters phrasetable and look up how many of the currently desired phrasetype he has, add their property weights
                foreach (PhraseTableEntry CurrentPhraseTableEntry in CharacterList[SpeakingCharacter].PhraseTable)
                {
                    AmountOfCurrentPhraseType += CurrentPhraseTableEntry.PhraseProperties[(int)CurrentPhraseType];
                }
                //Now that we know how much weight of the current phrasetype the character has in their PhraseTable randomly select a phrase of correct Type
                float PhraseTableWeightedIndex = ((float)RandNumGenerator.NextDouble())*AmountOfCurrentPhraseType;
                AmountOfCurrentPhraseType = 0;
                foreach (PhraseTableEntry CurrentPhraseTableEntry in CharacterList[SpeakingCharacter].PhraseTable)
                {
                    AmountOfCurrentPhraseType += CurrentPhraseTableEntry.PhraseProperties[(int)CurrentPhraseType];
                    if (AmountOfCurrentPhraseType > PhraseTableWeightedIndex)
                    {
                        Console.WriteLine(CurrentPhraseTableEntry.DialogStr);
                        break; 
                    }
                }
                                
                if (SpeakingCharacter == Character1Num)
                    SpeakingCharacter = Character2Num;
                else
                    SpeakingCharacter = Character1Num;
            }
        }
    }
      
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Dialog Engine Isaac Davenport ");
            DateTime now = DateTime.Now;
            Console.WriteLine(now);
            Console.WriteLine();
            // Console.ReadLine();
            
            DialogTracker TheDialogs = new DialogTracker();
            InitModelDialogs.SetDefaults(TheDialogs);

            TheDialogs.GenerateADialog();

            while (true)
            {
                string[] keyboardInput = Console.ReadLine().Split(' ');
                if (keyboardInput.Length == 3)
                {
                    int j = 0;
                    int[] ModelAndCharacters = new int [3];
                    foreach (string AsciiInt in keyboardInput) 
                    {
                        ModelAndCharacters[j] = Int32.Parse(AsciiInt); 
                        j++;
                    }

                    TheDialogs.GenerateADialog();
                }
                else if (keyboardInput.Length == 0)
                {
                    TheDialogs.GenerateADialog();
                }
                else
                {
                    TheDialogs.GenerateADialog();
                }
            }  
        }   
    }
}
