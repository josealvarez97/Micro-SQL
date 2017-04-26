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
        public Form1()
        {
            InitializeComponent();
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.PaintKeyword("while", Color.Blue, 0);
            this.PaintKeyword("if", Color.Blue, 0);
        }

        private void PaintKeyword(string word, Color color, int startIndex)
        {
            if (this.richTextBox1.Text.Contains(word))
            {
                int index = -1;
                int selectStart = this.richTextBox1.SelectionStart;

                while ((index = this.richTextBox1.Text.IndexOf(word, (index + 1))) != -1)
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
    }
}
