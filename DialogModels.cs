using System;
using Newtonsoft.Json;
using System.IO;


namespace DialogEngine
{
    public static class InitModelDialogs
    {
        public static void SetDefaults(DialogTracker inObj)
        {
            DateTime firstRound = new DateTime(2016, 5, 18);
            DateTime adventureRound = new DateTime(2016, 9, 17);

            //Dialogs JSON parse here.
            DirectoryInfo dialogs_d = new DirectoryInfo(SessionVars.DialogsDirectory);
            foreach (FileInfo file in dialogs_d.GetFiles("*.json")) //file of type FileInfo for each .json in directory
            {
                string inDialog;
                FileStream fs = file.OpenRead();    //open a read-only FileStream
                using (StreamReader reader = new StreamReader(fs))   //creates new streamerader for fs stream. Could also construct with filename...
                {

                    inDialog = reader.ReadToEnd();//create string of JSON file
                    ModelDialogInput dialogsInClass = JsonConvert.DeserializeObject< ModelDialogInput >(inDialog);  //string to Object.
                    foreach(ModelDialog curDialog in dialogsInClass.inList)
                    {
                        //Add to dialog List
                        inObj.ModelDialogs.Add(curDialog);
                        //pop sum
                        inObj.DialogModelPopularitySum += curDialog.Popularity;
                    }
                }
            }
        }
    }
}
