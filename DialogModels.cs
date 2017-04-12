using System;
using Newtonsoft.Json;
using System.IO;


namespace DialogEngine
{
    public static class InitModelDialogs
    {
        public static void SetDefaults(DialogTracker inObj)
        {
            //Dialogs JSON parse here.
            DirectoryInfo dialogs_d = new DirectoryInfo(SessionVars.DialogsDirectory);
            foreach (FileInfo file in dialogs_d.GetFiles("*.json")) //file of type FileInfo for each .json in directory
            {
                Console.WriteLine(" opening dialog models in " + file.FullName);
                string inDialog;
                FileStream fs = file.OpenRead();    //open a read-only FileStream
                using (StreamReader reader = new StreamReader(fs))   //creates new streamerader for fs stream. Could also construct with filename...
                {
                    inDialog = reader.ReadToEnd();//create string of JSON file
                    ModelDialogInput dialogsInClass = JsonConvert.DeserializeObject<ModelDialogInput>(inDialog);  //string to Object.
                    foreach (ModelDialog curDialog in dialogsInClass.inList)
                    {
                        //Add to dialog List
                        inObj.ModelDialogs.Add(curDialog);
                        //pop sum
                        inObj.DialogModelPopularitySum += curDialog.Popularity;
                    }
                }
                Console.WriteLine(" completed " + file.FullName);
            }
            if (inObj.ModelDialogs.Count < 2)
            {
                Console.WriteLine("  Insufficient dialog models found in " + SessionVars.DialogsDirectory + " exiting.");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
    }
}
