using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dialog_Generator
{

    public enum PhraseTypes 
    {
        Exclamation,
        Greeting,
        Threat,
        Retreat,
        Proposal,
        Yes,
        No,
        RequestCatchup,
        GiveAffirmation,
        RequestAffirmation,
        GiveDisbelief,
        GiveRecentHistory,
        GiveSurprisingStatement,  
        Ramble,
        ShutUp,
        RequestJoke,
        GiveJoke,
        PhraseTypesSize
    };

    public enum GenderType { Male, Female }

    
    public class PhraseTableEntry
    {
        public string DialogStr;
        public float [] DialogStrProperties {get; set;}

        public PhraseTableEntry()
        {
            DialogStr = "UnInitialized";
            float [] DialogStrProperties = new float [(int)PhraseTypes.PhraseTypesSize];

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
        //mast list of all the various types of phrase exchanges that could happen to make up a dialog
        public List<ModelDialog> ModelDialogTable = new List<ModelDialog>();
        public 
    }
      
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Lets have a dialog");
            Console.ReadLine();
            Console.Clear();

            DialogTracker TheDialogs = new DialogTracker();
            InitModelDialogs.SetDefaults(TheDialogs); 

            Character Cowboy = new Character();
            InitCowboy.SetDefaults(Cowboy);

            Character Schoolboy = new Character();
            InitSchoolboy.SetDefaults(Schoolboy);

            Character Schoolmarm = new Character();
            InitSchoolmarm.SetDefaults(Schoolmarm);
            
            Console.ReadLine();


            Console.WriteLine(Cowboy.PhraseTable[0].DialogStr);

            Console.WriteLine(Cowboy.PhraseTable[0].DialogStrProperties[(int)PhraseTypes.Exclamation]);

            Console.WriteLine(" ");

            Console.WriteLine(Cowboy.PhraseTable[1].DialogStr);
            Console.WriteLine(Cowboy.PhraseTable[1].DialogStrProperties[(int)PhraseTypes.Exclamation]);
            //SchoolBoy.PhraseTable.Add(new PhraseTableEntry());
                
            //Console.WriteLine(SchoolBoy.PhraseTable[0].DialogStr);
            BeginGame(Cowboy);
        }

   
        static public void BeginGame(Character Cowboy)
        {
           // Console.WriteLine("Print Dialog Table");
            Console.ReadLine();
  
            while (true)
            {
                string newGame = Console.ReadLine();
                for (int i = 0; i < 2; i++)
                {
                    Console.WriteLine(Cowboy.PhraseTable[i].DialogStr);
                    String DSProps = "";
                    String DSPfloat = "";
                    for (int j = 0; j < (int)PhraseTypes.PhraseTypesSize; j++)
                    {
                        DSPfloat = Cowboy.PhraseTable[i].DialogStrProperties[j].ToString();
                        DSProps += DSPfloat + " ";
                    }

                    Console.WriteLine(DSProps);
                }

 			    foreach (PhraseTableEntry Entry in Cowboy.PhraseTable)
			    {
                    Console.WriteLine(Entry.DialogStr);
                    string CatProperties = "";
                    String TempString = "";
                    foreach (float Probability in Entry.DialogStrProperties)
                    {
                        TempString = Probability.ToString();
                        CatProperties += TempString;
                        CatProperties += " ";
                    }
                    Console.WriteLine(CatProperties);
                }

            }

        }
    }
}
