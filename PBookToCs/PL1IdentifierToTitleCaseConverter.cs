using System;
using System.Collections.Generic;

namespace PBookToCs
{
    public class PL1IdentifierToTitleCaseConverter
    {
        private string _name;

        public string ToTitleCase()
        {
            var titleCaseCharacters = new List<char>();
            var startOfWord = true;
            for(int i = 0; i < _name.Length; i++)
            {
                if(_name[i] == '_') 
                {
                    startOfWord = true;
                    i++;
                }

                if(startOfWord)
                {
                    titleCaseCharacters.Add(char.ToUpper(_name[i]));
                    startOfWord = false;
                }
                else
                    titleCaseCharacters.Add(char.ToLower(_name[i]));
            }
            return String.Join("", titleCaseCharacters.ToArray());
        }

        public PL1IdentifierToTitleCaseConverter(string name)
        {
            _name = name;
        }
    }
}