using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrainingPractice_01_5_7
{
    public partial class Form1 : Form
    {
        private const int N = 3;
        private const int SizeButton = 50;
        private int[,] _map = new int[N * N, N * N];
        private readonly Button[,] _buttons = new Button[N * N, N * N];

        public Form1()
        {
            InitializeComponent();
            GenerateMap();
        }

        private void GenerateMap()
        {
            for (var i = 0; i < N * N; i++)
            {
                for (var j = 0; j < N * N; j++)
                {
                    _map[i, j] = (i * N + i / N + j) % (N * N) + 1;
                    _buttons[i, j] = new Button();
                }
            }

            MatrixTransposition();
            SwapRowsInBlock();
            SwapColumnsInBlock();
            SwapBlocksInRow();
            SwapBlocksInColumn();
            var r = new Random();
            for (var i = 0; i < 40; i++)
            {
                ShuffleMap(r.Next(0, 5));
            }

            CreateMap();
            HideCells();
        }

        public void HideCells()
        {
            var N = 40;
            var r = new Random();
            while (N > 0)
            {
                for (var i = 0; i < Form1.N * Form1.N; i++)
                {
                    for (var j = 0; j < Form1.N * Form1.N; j++)
                    {
                        if (!string.IsNullOrEmpty(_buttons[i, j].Text))
                        {
                            var a = r.Next(0, 3);
                            _buttons[i, j].Text = a == 0 ? "" : _buttons[i, j].Text;
                            _buttons[i, j].Enabled = a == 0 ? true : false;

                            if (a == 0)
                                N--;
                            if (N <= 0)
                                break;
                        }
                    }

                    if (N <= 0)
                        break;
                }
            }
        }

        private void ShuffleMap(int i)
        {
            switch (i)
            {
                case 0:
                    MatrixTransposition();
                    break;
                case 1:
                    SwapRowsInBlock();
                    break;
                case 2:
                    SwapColumnsInBlock();
                    break;
                case 3:
                    SwapBlocksInRow();
                    break;
                case 4:
                    SwapBlocksInColumn();
                    break;
                default:
                    MatrixTransposition();
                    break;
            }
        }

        private void SwapBlocksInColumn()
        {
            var r = new Random();
            var block1 = r.Next(0, N);
            var block2 = r.Next(0, N);
            while (block1 == block2)
                block2 = r.Next(0, N);
            block1 *= N;
            block2 *= N;
            for (var i = 0; i < N * N; i++)
            {
                var k = block2;
                for (var j = block1; j < block1 + N; j++)
                {
                    (_map[i, j], _map[i, k]) = (_map[i, k], _map[i, j]);
                    k++;
                }
            }
        }

        private void SwapBlocksInRow()
        {
            var r = new Random();
            var block1 = r.Next(0, N);
            var block2 = r.Next(0, N);
            while (block1 == block2)
                block2 = r.Next(0, N);
            block1 *= N;
            block2 *= N;
            for (var i = 0; i < N * N; i++)
            {
                var k = block2;
                for (var j = block1; j < block1 + N; j++)
                {
                    (_map[j, i], _map[k, i]) = (_map[k, i], _map[j, i]);
                    k++;
                }
            }
        }
public void SwapRowsInBlock()
        {
            var r = new Random();
            var block = r.Next(0, N);
            var row1 = r.Next(0, N);
            var line1 = block * N + row1;
            var row2 = r.Next(0, N);
            while (row1 == row2)
                row2 = r.Next(0, N);
            var line2 = block * N + row2;
            for (var i = 0; i < N * N; i++)
            {
                (_map[line1, i], _map[line2, i]) = (_map[line2, i], _map[line1, i]);
            }
        }

        public void SwapColumnsInBlock()
        {
            var r = new Random();
            var block = r.Next(0, N);
            var row1 = r.Next(0, N);
            var line1 = block * N + row1;
            var row2 = r.Next(0, N);
            while (row1 == row2)
                row2 = r.Next(0, N);
            var line2 = block * N + row2;
            for (var i = 0; i < N * N; i++)
            {
                (_map[i, line1], _map[i, line2]) = (_map[i, line2], _map[i, line1]);
            }
        }

        public void MatrixTransposition()
        {
            var tMap = new int[N * N, N * N];
            for (var i = 0; i < N * N; i++)
            {
                for (var j = 0; j < N * N; j++)
                {
                    tMap[i, j] = _map[j, i];
                }
            }

            _map = tMap;
        }

        private void CreateMap()
        {
            for (var i = 0; i < N * N; i++)
            {
                for (var j = 0; j < N * N; j++)
                {
                    var button = new Button();
                    _buttons[i, j] = button;
                    button.Size = new Size(SizeButton, SizeButton);
                    button.Text = _map[i, j].ToString();
                    button.BackColor = Color.Pink;
                    button.Click += OnCellPressed;
                    button.Location = new Point(j * SizeButton, i * SizeButton);
                    this.Controls.Add(button);
                }
            }
        }

        private void OnCellPressed(object sender, EventArgs e)
        {
            var pressedButton = sender as Button;
            var buttonText = pressedButton?.Text;
            if (string.IsNullOrEmpty(buttonText))
            {
                pressedButton.Text = "1";
            }
            else
            {
                var num = int.Parse(buttonText);
                num++;
                if (num == 10)
                    num = 1;
                pressedButton.Text = num.ToString();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < N * N; i++)
            {
                for (var j = 0; j < N * N; j++)
                {
                    var btnText = _buttons[i, j].Text;
                    if (btnText == _map[i, j].ToString()) continue;
                    MessageBox.Show("Неверно!");
                    return;
                }
            }

            MessageBox.Show("Верно!");
            for (var i = 0; i < N * N; i++)
            {
                for (var j = 0; j < N * N; j++)
                {
                    this.Controls.Remove(_buttons[i, j]);
                }
            }

            GenerateMap();
        }
    }
}

