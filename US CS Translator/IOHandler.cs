using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace US_CS_Translator {
    class IOHandler {
        static void Main(string[] args) {
            //DEBUG:just for now we'll shortcut the arg
            args = new string[1];
            args[0] = "C:\\Work\\US CS Translator\\US CS Translator\\bin\\Debug\\FadeOut.js";

            try {   
                //Check that a file was provided.
                if (args.Length <= 0) throw new Exception("No file passed to prog.");

                //Check that the provided file has the javascrip extension.
                if (Path.GetExtension(args[0]) != ".js") throw new Exception(args[0] + " is not a .js file.");

                //Get the needed directory information about the file.
                string usFileName = Path.GetFileNameWithoutExtension(args[0]),
                        workingDirectory = Path.GetDirectoryName(args[0]),
                        csFileName = workingDirectory + "\\" + usFileName + ".cs";

                //Read the code from given file
                System.IO.StreamReader streamReader = new System.IO.StreamReader(args[0]);
                string[] jsCode = streamReader.ReadToEnd().Split('\n'),
                         csCode = Converter.ConvertUStoCS(jsCode, usFileName);

                //Save the file out 
                System.IO.File.WriteAllText(csFileName, string.Join("\n", csCode));

            } catch (IOException e) {
                if (args[0] == null) Console.WriteLine("Please provide a file to translate.");
                else Console.WriteLine("Unable to load file." + e.Message);
            } finally {
                //Finished, notify user then quit.
                Console.WriteLine("UnityScript to C# conversion complete.");
                //TODO:Rename old file so that it won't compile
                Console.ReadKey();
            }

        }

    }   
}
