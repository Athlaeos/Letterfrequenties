using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LetterFrequencyReader
{
    public partial class Form1 : Form
    {
        private readonly TextBox _inBox = new TextBox();
        private readonly TextBox _outBox = new TextBox();
        private readonly Dictionary<char, int> _letterFrequencies = new Dictionary<char, int>();
        public Form1()
        {
            InitializeComponent();

            Size = new Size(290, 540);

            _inBox.Size = new Size(215, 25);
            _inBox.Multiline = true;
            _inBox.Text = "D:/minecraft_server/1.17/plugins/ValhallaMMO/recipes.yml";
            _inBox.Location = new Point(30, 20);
            Controls.Add(_inBox);
            
            _outBox.Size = new Size(215, 300);
            _outBox.Multiline = true;
            _outBox.Text = "output :)";
            _outBox.Location = new Point(30, 110);
            Controls.Add(_outBox);

            var b = new Button();
            b.Text = "Read!";
            b.Size = new Size(65, 35);
            b.Location = new Point(100, 60);
            Controls.Add(b);

            b.Click += ReadFile;
        }

        private void ReadFile(Object sender, EventArgs args)
        {
            try
            {
                MessageBox.Show("trying to read " + _inBox.Text);
                var text = File.ReadAllLines(_inBox.Text);

                foreach (var l in text)
                {
                    foreach (var c in l)
                    {
                        if (c.Equals(' ')) continue;
                        AddChar(Char.ToLower(c));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred when trying to read the file: " + ex.GetType());
            }

            _outBox.Text = "Letter : Frequency";
            
            foreach (var c in GetSortedLetterfrequencies())
            {
                _outBox.AppendText(
                    Environment.NewLine + $"{c.Key} : {c.Value}x");
            }
        }

        private List<KeyValuePair<char, int>> GetSortedLetterfrequencies()
        {
            var myList = _letterFrequencies.ToList();

            myList.Sort(
                delegate(KeyValuePair<char, int> pair1,
                    KeyValuePair<char, int> pair2)
                {
                    return pair1.Value.CompareTo(pair2.Value);
                }
            );

            return myList;
        }

        private void AddChar(char c)
        {
            if (!_letterFrequencies.ContainsKey(c))
            {
                _letterFrequencies.Add(c, 1);
            }
            else
            {
                _letterFrequencies[c] += 1;
            }
        }
    }
}