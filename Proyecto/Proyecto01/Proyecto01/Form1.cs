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
        ReservedWord[] keyWords;
        public Form1()
        {
            InitializeComponent();
            dictionary = new StreamReader(new FileStream("C:/microSQL/microSQL.ini", FileMode.Open), new UTF8Encoding());
            keyWords = new ReservedWord[9];
            keyWords = ReservedWord.FindKeywords(dictionary);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            // Por si escribe con las originales
            this.PaintKeyword(keyWords[0].originalReservedWord, Color.Blue, 0);
            this.PaintKeyword(keyWords[1].originalReservedWord, Color.Blue, 0);
            this.PaintKeyword(keyWords[2].originalReservedWord, Color.Blue, 0);
            this.PaintKeyword(keyWords[3].originalReservedWord, Color.Blue, 0);
            this.PaintKeyword(keyWords[4].originalReservedWord, Color.Blue, 0);
            this.PaintKeyword(keyWords[5].originalReservedWord, Color.Blue, 0);
            this.PaintKeyword(keyWords[6].originalReservedWord, Color.Blue, 0);
            this.PaintKeyword(keyWords[7].originalReservedWord, Color.Blue, 0);
            this.PaintKeyword(keyWords[8].originalReservedWord, Color.Blue, 0);

            //O con su traduccion
            this.PaintKeyword(keyWords[0].translation, Color.Blue, 0);
            this.PaintKeyword(keyWords[1].translation, Color.Blue, 0);
            this.PaintKeyword(keyWords[2].translation, Color.Blue, 0);
            this.PaintKeyword(keyWords[3].translation, Color.Blue, 0);
            this.PaintKeyword(keyWords[4].translation, Color.Blue, 0);
            this.PaintKeyword(keyWords[5].translation, Color.Blue, 0);
            this.PaintKeyword(keyWords[6].translation, Color.Blue, 0);
            this.PaintKeyword(keyWords[7].translation, Color.Blue, 0);
            this.PaintKeyword(keyWords[8].translation, Color.Blue, 0);
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
            //SQLTextParser.GetInsertInstructions(richTextBox1);
            //SQLTextParser.GetSelectInstructions(richTextBox1);
            //SQLTextParser.GetDeleteInstructions(richTextBox1);
            SQLTextParser.GetUpdateInstructions(richTextBox1);

            //Sera una cola
            Queue<SQLCommands> commandsToExecute = SQLTextParser.ReadActions(richTextBox1);
            Queue<string> instructionsToExecute = SQLTextParser.ReadInstructions(richTextBox1);
            
            while (commandsToExecute.Count != 0)
            {
                SQLFunctions.ExecuteCommand(commandsToExecute.Dequeue(), instructionsToExecute.Dequeue(), outputGridView);
            }

            
        }

    }
}
