using System;
using Newtonsoft.Json;
using System.IO;


namespace DialogEngine  //Lets create some protections here for invalid files/entries in files. both error reporting and recovery.
{
    public static class InitModelDialogs
    {
        public static void SetDefaults(DialogTracker inObj)
        {
            DateTime firstRound = new DateTime(2016, 5, 18);
            DateTime adventureRound = new DateTime(2016, 9, 17);
            Boolean errorFlag = false;

            //Dialogs JSON parse here.
            DirectoryInfo dialogs_d = new DirectoryInfo(SessionVars.DialogsDirectory);
            foreach (FileInfo file in dialogs_d.GetFiles("*.json")) //file of type FileInfo for each .json in directory (can be multiple ;D )
            {
                string inDialog;    //for input dialogs as string.
                FileStream fs = null;
                if (!errorFlag)
                {
                    try
                    { fs = file.OpenRead(); }    //open a read-only FileStream
                    catch (IOException e)
                    {
                        errorFlag = true;
                        Console.WriteLine("ioexception thrown. likely that another thread has the file open already. " + e.GetType().Name);
                        Console.WriteLine(e.Message);
                        Console.WriteLine(e.Data);
                    }
                }
                if (!errorFlag)
                {
                    using (StreamReader reader = new StreamReader(fs))   //creates new streamerader for fs stream. Could also construct with filename...
                    {

                        inDialog = reader.ReadToEnd();//create string of JSON file
                        ModelDialogInput dialogsInClass = JsonConvert.DeserializeObject<ModelDialogInput>(inDialog);  //string to Object.
                        foreach (ModelDialog curDialog in dialogsInClass.inList)
                        {
                            //Add to dialog List
                            inObj.ModelDialogs.Add(curDialog);
                            //popularity sum 
                            inObj.DialogModelPopularitySum += curDialog.Popularity;
                        }
                    }
                }
            }
        }
    }
}
