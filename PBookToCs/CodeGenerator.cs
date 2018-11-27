using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;

namespace PBookToCs
{
    public class CodeGenerator
    {
        const string NEWLINE = "\r\n";

        public string GenerateFrom(string pBook) {
            var lines = new List<string>(pBook.Split(NEWLINE));

            var prefix = "";

            var className = ExtractClassNameFrom(lines[0]);
            var result = ("namespace AutomationSpike" + NEWLINE + "{" + NEWLINE + "    enum " + className + NEWLINE + "    {");

            foreach(string line in lines.GetRange(1, lines.Count-1)) {
                foreach(Match m in Regex.Matches(line, "^(\\s+)(.+?)(\\s+)(.+?)POS\\((\\d+)\\)"))
                {
                    var enumElementName = new PL1IdentifierToTitleCaseConverter(m.Groups[2].Value).ToTitleCase();
                    result += (prefix + NEWLINE + "        " + enumElementName + " = " + m.Groups[5].Value);
                    prefix = ",";
                }
            }
            
            return result + NEWLINE + "    }" + NEWLINE + "}";
        }

        private string ExtractClassNameFrom(string declaration)
        {
            return new PL1IdentifierToTitleCaseConverter(Regex.Matches(declaration, "DCL (.+?) ")[0].Groups[1].Value).ToTitleCase();
        }

        static void Main(string[] args)
        {
            Console.WriteLine(new CodeGenerator().GenerateFrom(File.ReadAllText(args[0])));
        }
    }
}
