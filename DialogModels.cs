using System;
using Newtonsoft.Json;
using System.IO;


namespace DialogEngine
{
    public static class InitModelDialogs    //TODO lets get some graceful failures here. recovery from single file failures.
    {
        public static void SetDefaults(DialogTracker inObj) //TODO is there a good way to identify orphaned tags? (dialog lines)
        {
            //Dialogs JSON parse here.
            try
            {
                DirectoryInfo dialogs_d = new DirectoryInfo(SessionVars.DialogsDirectory);
                var _inFiles = dialogs_d.GetFiles("*.json"); 
                foreach (FileInfo file in _inFiles) //file of type FileInfo for each .json in directory
                {
                    Console.WriteLine(" opening dialog models in " + file.FullName);
                    string inDialog;
                    FileStream fs = file.OpenRead();    //open a read-only FileStream
                    using (StreamReader reader = new StreamReader(fs))   //creates new streamerader for fs stream. Could also construct with filename...
                    {
                        try
                        {
                            inDialog = reader.ReadToEnd();//create string of JSON file
                            ModelDialogInput dialogsInClass = JsonConvert.DeserializeObject<ModelDialogInput>(inDialog);  //string to Object.
                            foreach (ModelDialog curDialog in dialogsInClass.inList)
                            {
                                //Add to dialog List
                                inObj.ModelDialogs.Add(curDialog);
                                //population sums
                                inObj.DialogModelPopularitySum += curDialog.Popularity;
                            }
                        }
                        catch (Newtonsoft.Json.JsonReaderException e)
                        {
                            Console.WriteLine("Error reading " + file.FullName);
                            Console.WriteLine("JSON Parse error at " + e.LineNumber + ", " + e.LinePosition);
                            Console.ReadLine();
                        }
                    }
                    Console.WriteLine(" completed " + file.FullName);
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (OutOfMemoryException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
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
