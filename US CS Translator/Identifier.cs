using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace US_CS_Translator {
    class PatternIdentifier {

        const string globalPermissions = "(public|private|protected)?\\s*(static)?\\s*";

        internal static bool IsImportStatement(string usCodeLine) {
            Regex importCheck = new Regex("import(.*?);");
            return importCheck.IsMatch(usCodeLine);
        }

        public static bool IsVariableDecleration(string codeLine, int scope) {
            //Test the line for variables.
            string variablePattern = "var\\s(.*?)\\s:\\s(.*?);";
            if (scope == 0) variablePattern = globalPermissions + variablePattern;
            return RegexCheck(variablePattern, codeLine);
        }

        internal static bool IsClassDecleration(string usCodeLine) {
            string classPattern = "(static)?\\s?class\\s+(\\w*)\\s+(extends\\s+(\\S*))?\\s*{";
            return RegexCheck(classPattern, usCodeLine);
        }

        public static bool IsFunctionDecleration(string codeLine) {
            string functionPattern = globalPermissions+"function\\s*(\\S*)\\((.*?)\\)\\s*(:\\s*(\\S*)\\s*)?{";
            return RegexCheck(functionPattern, codeLine);
        }

        public static bool IsForEachPattern(string codeLine){
            throw new NotImplementedException();
        }

        internal static bool IsCatchStatement(string usCodeLine) {
            throw new NotImplementedException();
        }

        internal static bool HasInstanceOf(string usCodeLine) {
            throw new NotImplementedException();
        }

        internal static bool IsNullVariableIfCheck(string usCodeLine) {
            throw new NotImplementedException();
        }

        internal static bool HasSuperCall(string usCodeLine) {
            throw new NotImplementedException();
        }

        private static bool RegexCheck(string pattern, string line) {
            Regex check = new Regex(pattern);
            return check.IsMatch(line);
        }

    }
}
