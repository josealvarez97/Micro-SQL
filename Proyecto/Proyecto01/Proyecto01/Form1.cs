using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Proyecto01
{
    public partial class Form1 : Form
    {
        StreamReader dictionary;
        string[] keyWords;
        public Form1()
        {
            InitializeComponent();
            dictionary = new StreamReader(new FileStream("C:/microSQL/microSQL.ini", FileMode.Open), new UTF8Encoding());
            keyWords = new string[9];
            FindKeywords();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.PaintKeyword(keyWords[0], Color.Blue, 0);
            this.PaintKeyword(keyWords[1], Color.Blue, 0);
            this.PaintKeyword(keyWords[2], Color.Blue, 0);
            this.PaintKeyword(keyWords[3], Color.Blue, 0);
            this.PaintKeyword(keyWords[4], Color.Blue, 0);
            this.PaintKeyword(keyWords[5], Color.Blue, 0);
            this.PaintKeyword(keyWords[6], Color.Blue, 0);
            this.PaintKeyword(keyWords[7], Color.Blue, 0);
            this.PaintKeyword(keyWords[8], Color.Blue, 0);
        }

        private void PaintKeyword(string word, Color color, int startIndex)
        {
            if (this.richTextBox1.Text.ToUpper().Contains(word))
            {
                int index = -1;
                int selectStart = this.richTextBox1.SelectionStart;

                while ((index = this.richTextBox1.Text.ToUpper().IndexOf(word, (index + 1))) != -1)
                {
                    this.richTextBox1.Select((index + startIndex), word.Length);
                    this.richTextBox1.SelectionColor = color;
                    this.richTextBox1.Select(selectStart, 0);
                    this.richTextBox1.SelectionColor = Color.Black;
                }
            }
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            //Sera una cola
            Queue<SQLCommands> commandsToExecute = SQLTextParser.ReadActions(richTextBox1);
            
            while (commandsToExecute.Count != 0)
            {
                SQLFunctions.ExecuteCommand(commandsToExecute.Dequeue(), richTextBox1);
            }
        }

        private void FindKeywords()
        {
            string line = "";
            string correctedKeyWord = "";
            int i = 0;
            while (!dictionary.EndOfStream)
            {
                line = dictionary.ReadLine();
                var keyWordsInFile = line.Split(',');
                for (int j = 0; j < keyWordsInFile[1].Length; j++)
                {
                    keyWordsInFile[1] = keyWordsInFile[1].Trim();
                    if (keyWordsInFile[1][j] != '<' && keyWordsInFile[1][j] != '>')
                        correctedKeyWord += keyWordsInFile[1][j];
                }
                keyWords[i] = correctedKeyWord;
                correctedKeyWord = "";
                i++;
            }
            dictionary.Dispose();
        }
    }
}
