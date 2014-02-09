using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace US_CS_Translator {
    class Mediator {
        public static Dictionary<Func<string, string>, string> globalMediator = new Dictionary<Func<string, string>, string>();
        public static Dictionary<Func<string, string>, string> functionMediator = new Dictionary<Func<string, string>, string>();
        static Mediator() {

            //Global score patterns
            const string globalPermissions = "(public|private|protected)?\\s*(static)?\\s*";
            //TODO: IEnumerator detection/conversions
            globalMediator.Add( new Func<string, string>(Transliterateor.TransliterateImport),  "import(.*?);");
            globalMediator.Add( new Func<string, string>(Transliterateor.TransliteratePragma),  "#pragma strict");
            globalMediator.Add( new Func<string, string>(Transliterateor.TransliterateClass),   "(static)?\\s?class\\s+(\\w*)\\s+(extends\\s+(\\S*))?\\s*{");
            globalMediator.Add( new Func<string, string>(Transliterateor.TransliterateFunction), globalPermissions + "function\\s*(\\S*)\\s?\\((.*?)\\)\\s*(:\\s*(\\S*)\\s*)?{");
            globalMediator.Add( new Func<string, string>(Transliterateor.TransliterateVariable), globalPermissions + "var\\s(.*?)\\s?:\\s(.*?);");

            //Functional Scope
            //functionMediator.Add( new Func<string, string>(Transliterateor.TransliterateVariable),  "var\\s(.*?)\\s?:\\s(.*?);");
            globalMediator.Add(new Func<string, string>(Transliterateor.TransliterateFor), "for\\s*\\(\\s*var\\s*(.*)\\){");
            globalMediator.Add(new Func<string, string>(Transliterateor.TransliterateDouble), "\\d*.\\d*");
        }

        public static string MediateMatches(string usCode, Dictionary<Func<string, string>, string> mediator) {
            //for every different thing the line could be 
            foreach (KeyValuePair<Func<string, string>, string> searchPatternSet in mediator) {
                //Search for matches
                Regex regEx = new Regex(searchPatternSet.Value);
                if (regEx.IsMatch(usCode)) {
                    return searchPatternSet.Key.Invoke(usCode);
                }
            }
            return usCode;
        }
    }
}