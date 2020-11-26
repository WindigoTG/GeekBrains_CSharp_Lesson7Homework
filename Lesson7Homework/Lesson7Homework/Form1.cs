using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

//Олесов Максим

/*1. а) Добавить в программу «Удвоитель» подсчет количества отданных команд.
б) Добавить меню и команду «Играть». При нажатии появляется сообщение, какое число должен получить игрок.
    Игрок должен постараться получить это число за минимальное количество ходов.
в) *Добавить кнопку «Отменить», которая отменяет последние ходы.*/

namespace Lesson7Homework
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();
        int tgtNmbr;
        int optimal;
        List<string> actionLog = new List<string>();

        //Рассчитываем оптимальное количество действий, необходимых для достижения результата.
        private void CalculateOptimal(int i)
        {
            if (i > 1)
            {
                optimal++;
                if (i % 2 == 0)
                    CalculateOptimal(i / 2);
                else
                    CalculateOptimal(--i);
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void btnCommand1_Click(object sender, EventArgs e)
        {
            actionCount.Text = (int.Parse(actionCount.Text) + 1).ToString();
            actionLog.Add(lblNumber.Text);
            btnUndo.Enabled = true;
            lblNumber.Text = (int.Parse(lblNumber.Text) + 1).ToString();
        }

        private void btnCommand2_Click(object sender, EventArgs e)
        {
            actionCount.Text = (int.Parse(actionCount.Text) + 1).ToString();
            actionLog.Add(lblNumber.Text);
            btnUndo.Enabled = true;
            lblNumber.Text = (int.Parse(lblNumber.Text) * 2).ToString();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            actionCount.Text = (int.Parse(actionCount.Text) + 1).ToString();
            lblNumber.Text = "1";
            btnCommand1.Enabled = true;
            btnCommand2.Enabled = true;
            btnUndo.Enabled = false;
            actionLog.Clear();
        }

        private void PlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            actionLog.Clear();
            optimal = 0;
            btnCommand1.Enabled = true;
            btnCommand2.Enabled = true;
            btnReset.Enabled = false;
            btnUndo.Enabled = false;
            lblNumber.Visible = true;
            btnCommand1.Visible = true;
            btnCommand2.Visible = true;
            btnReset.Visible = true;
            btnUndo.Visible = true;
            lblTarget.Visible = true;
            tgtNmbr = rnd.Next(2, 100);
            lblTarget.Text = $"Вам нужно набрать число: {tgtNmbr}";
            progressBar.Maximum = tgtNmbr;
            statusStrip1.Visible = true;
            CalculateOptimal(tgtNmbr);
            lblNumber.Text = "1";
            btnMenuPlay.Text = "Заново";
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblNumber_TextChanged(object sender, EventArgs e)
        {
            if (int.Parse(lblNumber.Text) == 1)
                btnReset.Enabled = false;
            else
                btnReset.Enabled = true;
            if (int.Parse(lblNumber.Text) <= tgtNmbr)
            {
                progressBar.Value = int.Parse(lblNumber.Text);
                if (int.Parse(lblNumber.Text) == tgtNmbr)
                {
                    btnCommand1.Enabled = false;
                    btnCommand2.Enabled = false;
                    btnReset.Enabled = false;
                    btnUndo.Enabled = false;
                    MessageBox.Show($"Поздравляем, вы достигли требуемого результата!\nДля этого вам потребовалось {int.Parse(actionCount.Text)} действий\n" +
                        $"Оптимальное количество действие, требовавшееся для достижения результата: {optimal}");
                }
            }
            else
            {
                progressBar.Value = 0;
                btnCommand1.Enabled = false;
                btnCommand2.Enabled = false;
                MessageBox.Show("Перебор!");
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            actionCount.Text = (int.Parse(actionCount.Text) + 1).ToString();
            lblNumber.Text = int.Parse(actionLog.Last()).ToString();
            actionLog.Remove(actionLog.Last());
            btnCommand1.Enabled = true;
            btnCommand2.Enabled = true;
            if (actionLog.Count == 0)
                btnUndo.Enabled = false;

        }
    }
}
