using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace US_CS_Translator {
    class Transliterateor {

        public static string TransliterateImport(string importCodeLine) {
            //"import(.*?);",
            return importCodeLine.Replace("import", "using");
        }

        internal static string TransliterateClass(string classCodeLine) {
            //"(static)?\\s?class\\s+(\\w*)\\s+(extends\\s+(\\S*))?\\s*{";
            classCodeLine = classCodeLine.Replace("{", "");

            string[] classInfo = classCodeLine.Split(
                new string[] { "class" }, 
                StringSplitOptions.None
            );

            string name = classInfo.Last().Trim(' ').Split(' ').First();

            string permission = classInfo.First();

            string extension = classInfo.Last().Split(
                new string[] {"extends"}, 
                StringSplitOptions.None
            ).Last().Trim();
            
            return permission + "class " + name + " : " + extension + "{";
        }

        internal static string TransliterateFunction(string functionCodeLine) {

            //Return type
            string returnType = "void";
            if (functionCodeLine.Contains(':'))
                returnType = functionCodeLine.Split(':').Last().Replace("{", "");

            //Parameters
            //Isolate paramter code
            string paramsCode = functionCodeLine.Split('(').Last();
            paramsCode = paramsCode.Split(')').First();

            //Isolate distinct perameters
            List<string> paramsCS = new List<string>();
            string[] paramsUS = paramsCode.Split(',');
            foreach (string param in paramsUS) { 
                string[] paramNameAndType = param.Split(':');
                if(param != "")
                    paramsCS.Add(paramNameAndType.Last() + " " + paramNameAndType.First());
            }

            //Function name
            string functionName = functionCodeLine.Split('(').First();
            functionName = functionName.Replace("function", "");
            functionName = Regex.Replace(functionName, "!\\S", "").Replace("\t", "");

            //Permissions
            string permissions = functionCodeLine.Split(new string[]{"function"}, StringSplitOptions.None).First();
            if (permissions.Contains("\t") || permissions == "") 
                permissions = permissions + "public";//Shame US, Shame!

            //Format converted function
            string csFunction = permissions + " " 
                                + returnType + " " 
                                + functionName 
                                + "(" +  String.Join(",", paramsCS) + "){";
            
            //Remove un-wanted white space.
            csFunction = csFunction.Replace(" ,", ",");
            csFunction = Regex.Replace(csFunction, " +", " ");
            
            return csFunction;
        }

        internal static string TransliterateVariable(string variableCodeLine) {
            
            //Type
            string varType = "var";
            char typeQualifier = ':',
                 assignmentChar = '=';
            if (variableCodeLine.Contains(typeQualifier)) {
                varType = variableCodeLine.Split(typeQualifier).Last()//ignore name and permission
                    .Split(assignmentChar).First()//ignore the value
                    .Replace(";", "")//ignore trailing semicolons
                    .Replace(" ", "");//remove white space
            }

            //Special type differences
            if (varType == "boolean") varType = "bool";
            //TODO: Vector3 assignment

            string[] varInfo = variableCodeLine.Split(
                new string[]{"var"}, 
                StringSplitOptions.None
            );

            //Permission
            string permissions = varInfo.First().Trim(' ');

            //Name
            string name = varInfo.Last().Split(':').First();

            //Value
            string value = varInfo.Last();
            if (value.Contains(assignmentChar)) {
                int valueStartIndex = value.IndexOf(assignmentChar),
                    valueEndIndex = value.Length - valueStartIndex;
                value = value.Substring(valueStartIndex, valueEndIndex).Replace(";", "");
            }
            //Format converted variable;
            string csVariable = permissions + " " + varType + " " + name;
            if(variableCodeLine.Contains(assignmentChar))
                csVariable += value;

            //Remove unwated white space
            csVariable = Regex.Replace(csVariable, " +", " ").TrimEnd(' ');

            return csVariable + ";";
        }


        internal static string TransliteratePragma(string arg) {
            return " ";//No pragma in C#
        }

        internal static string TransliterateFor(string arg) {
            string[] forInfo = arg.Split(':');
            string varName = forInfo.First().Split(' ').Last(),
                    varType = forInfo.Last().Trim().Split(' ').First(),
                    collectionName = forInfo.Last().Split(' ').Last().Replace("){", ""),
                    tabbing = forInfo.First().Split(new string[]{"for"}, StringSplitOptions.None).First();
            return tabbing + "foreach("+varType + " " + varName + " in " + collectionName + "){";
        }

        internal static string TransliterateDouble(string arg) {
            return arg;
        }
    }
}
