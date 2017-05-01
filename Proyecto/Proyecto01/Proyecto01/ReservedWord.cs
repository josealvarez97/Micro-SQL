using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Proyecto01
{
    class ReservedWord
    {
        public string originalReservedWord { get; set; }
        public string translation { get; set; }

        public static ReservedWord[] FindKeywords(StreamReader dictionary)
        {
            ReservedWord[] keyWords = new ReservedWord[10];
            ReservedWord reservedWord;
            string line = "";
            int i = 0;
            while (!dictionary.EndOfStream && i != 9)
            {
                reservedWord = new ReservedWord();
                line = dictionary.ReadLine();
                var keyWordsInFile = line.Split(',');

                keyWordsInFile[1] = keyWordsInFile[1].Trim();
                keyWordsInFile[1] = keyWordsInFile[1].TrimStart('<');
                keyWordsInFile[1] = keyWordsInFile[1].TrimEnd('>');
                keyWordsInFile[0] = keyWordsInFile[0].TrimStart('<');
                keyWordsInFile[0] = keyWordsInFile[0].TrimEnd('>');
                reservedWord.originalReservedWord = keyWordsInFile[0];
                reservedWord.translation = keyWordsInFile[1];

                keyWords[i] = reservedWord;
                i++;
            }
            reservedWord = new ReservedWord();
            reservedWord.originalReservedWord = "UPDATE";
            reservedWord.translation = "UPDATE";
            keyWords[9] = reservedWord;
            dictionary.Dispose();
            return keyWords;
        }

    }
}
