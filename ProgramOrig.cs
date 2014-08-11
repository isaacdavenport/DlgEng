using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LI_TextAdventure
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("             Lets have some dialog ");
            Console.ReadLine();
            Console.Clear();
            Character Cowboy = new Character
            {
                DialogTableLen = 2,
                CharacterName = "Cowboy",
                Gender = 0,
                Age = 50,
            };
            /*
            //Exclamation, Greeting, Threat, Retreat, Proposal, Yes, No, CatchupRequest, RecentHistory, Ramble, ShutUp, JokeRequest, TellJoke;
            Cowboy.DialogTable[0].DialogStr = "Howdy\n\r";
            Cowboy.DialogTable[0].DialogStrAttrib = new short[13] { 10, 100, 5, 5, 5, 5, 5, 40, 5, 5, 30, 5, 5 };
            Cowboy.DialogTable[1].DialogStr = "Time to saddle up\n\r";
            Cowboy.DialogTable[1].DialogStrAttrib = new short[13] { 10, 40, 25, 60, 5, 20, 5, 5, 5, 5, 15, 5, 5};

            
                
                Ain't that purdy as a pig under a Christmas Tree.
                These spurs ain't afraid to kick up some dust.
                Lets us hunker down here for a minute.
                That ain't cowpoke work your talkin bout there.
                What did you think I meant when I said posse?
                Calm down there big feller.
                What in tarnation?
                Wrap me in calf-leather and call me an ankle biter.
                Is that you George?
                Those two gonna get hitched?
                Yup
                Don't get your knickers in a wad
                I think my nose was assaulted by a skunk that ate a case of rotten cabbage
                Lets vamoos            * 
             */


            Console.WriteLine(Cowboy.DialogTable[0].DialogStr);
            Console.WriteLine(Cowboy.DialogTable[1].DialogStr);
 


        }


        public class Character
        {
           public string [] DialogStr;
           public short [ , ] DialogStrAttrib; 
           // 0 to 100% string could be of type Exclamation, Greeting, Threat, Retreat, Proposal, Yes, No, CatchupRequest, RecentHistory, Ramble, ShutUp, JokeRequest, TellJoke;
           //seems like the strings and string attributes should have been a struct but c# wouldn't seem to let me fill in the struct in the constructor and outside it had no extension method though it compiled
           public short DialogTableLen;
           public string CharacterName;
           public short Gender;
           public short Age;
        }
 
    }
    
}
 