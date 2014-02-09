using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

//REFERENCE: http://wiki.unity3d.com/index.php?title=UnityScript_Keywords

namespace US_CS_Translator {
    class Converter {

        /// <summary>
        /// Convets code from UnityScript to C#.
        /// </summary>
        /// <param name="usCode">The UnityScript code from the given file</param>
        /// <param name="fileName">The fallback class name when no class found</param>
        /// <returns>Given UnityScript code as C#.</returns>
        public static string[] ConvertUStoCS(string[] usCode, string fileName) {
            //TODO: sanatize non K&R bracketing for functions.

            List<string> csCode = new List<string>();
            //Isolate and process classes one at a time
            foreach (string[] usClass in IsolateClasses(usCode, fileName)) {
                //Add the the converted class to output c# code.
                foreach (string s in ConvertUSClassToCS(usClass))
                    csCode.Add(s);
            }

            return csCode.ToArray();
        }

        //REFERENCE: http://msdn.microsoft.com/en-us/library/2s05feca.aspx
        private static string[][] IsolateClasses(string[] usCode, string fileName) {
            //TODO: detect no class declerations
            string[][] tempReturn = { usCode };
            return tempReturn;
        }

        private static string[] ConvertUSClassToCS(string[] usClassCode){
            int scope = 0;
            List<string> csClassCode = new List<string>();

            foreach (string usCodeLine in usClassCode) {
                string[] codeAndComments = Converter.StripComments(usCodeLine);
                string csCode = "",
                    //BOOKMARK: Spliting comments out for re-integration post-conversion
                       usCode = codeAndComments[0],
                       comment = codeAndComments[1];

                //Global scope
                //if (scope <= 1) {

                    csCode = Mediator.MediateMatches(usCode, Mediator.globalMediator);
                    //csCodeLine += PatternIdentifier.IsImportStatement(usCodeLineNoComment) ? "Y" : "N";
                    //csCodeLine += PatternIdentifier.IsClassDecleration(usCodeLineNoComment) ? "Y" : "N";

                    //csCodeLine += PatternIdentifier.IsFunctionDecleration(usCodeLineNoComment) ? "Y" : "N";

                //Functional scope
                //} else {
                    //csCode = Mediator.MediateMatches(usCode, Mediator.functionMediator);
                    //TODO: Check for foreach us pattern
                    //Translator.IsForEachPattern(usCodeLine);
                    //TODO: enforce catch error type
                    //Translator.IsCatchStatement(usCodeLine);
                    //TODO: instanceOf
                    //Translator.HasInstanceOf(usCodeLine);
                    //TODO: null variable check
                    //Translator.IsNullVariableIfCheck(usCodeLine);
                    //TODO: calls to superclass through Super
                    //Translator.HasSuperCall(usCodeLine);
                    //TODO: transform assignment etc 
                //}

                //Either scope
                //Check for variable declerations
                //csCodeLine += PatternIdentifier.IsVariableDecleration(usCodeLineNoComment, scope) ? "Y" : "N";
                //TODO: Enum conversion
                //Translator.IsEnumPattern(usCodeLine);

                //If conversion was not required, use us code.
                if (csCode == "") csCode = usCode;

                Console.WriteLine(scope + csCode + comment);
                csClassCode.Add(csCode + comment);

                //csCode.Add(csCodeLine+usCodeLine);
                scope = UpdateScope(usCodeLine, scope);
            }

            //Move Using to tup of script
            MoveUsing(ref csClassCode);

            return csClassCode.ToArray();
        }


        public static void MoveUsing(ref List<String> csClassCode) {
            for(int i = 0; i < csClassCode.Count; i++){
                if (new Regex("using(.*?);").IsMatch(StripComments(csClassCode[i])[0])) {
                    string usingLine = csClassCode[i];
                    csClassCode.RemoveAt(i);
                    csClassCode.Insert(0, usingLine.Trim('\t') );
                }
                i++;
            }
        }

        public static int UpdateScope(string codeLine, int scope) {
            codeLine = Converter.StripComments(codeLine)[0];
            //Increment/decrement scope as needed
            if (codeLine.Contains('{')) return scope++;
            if (codeLine.Contains('}')) return scope--;//BUG: this needs to happen after line is handled
            return scope;
        }

        //Remove any comments
        public static string[] StripComments(string codeLine) {
            string[] commentStr = new string[] { "//" };
            List<string> codeAndComments = codeLine.Split(commentStr, StringSplitOptions.None).ToList();

            //if comments exist
            if (codeAndComments.Count > 1) {

                //Grab the code and isolate the comments.
                string code = codeAndComments.First();
                codeAndComments.RemoveAt(0);
                string comments = String.Join(commentStr[0], codeAndComments);
                return new string[] { code, commentStr[0] + comments };
            } else { 
                return new string[]{ codeLine, "" };
            }

        }
    }
}
