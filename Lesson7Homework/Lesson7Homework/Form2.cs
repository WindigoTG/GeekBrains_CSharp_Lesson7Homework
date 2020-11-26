using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Олесов Максим

/*2. Используя Windows Forms, разработать игру «Угадай число».
Компьютер загадывает число от 1 до 100, а человек пытается его угадать за минимальное число попыток.
    Для ввода данных от человека используется элемент TextBox.*/

namespace Lesson7Homework
{
    public partial class Form2 : Form
    {
        Random rnd = new Random();
        int tgtNmbr;
        int maxTryCount = 7;
        int tryCount;
        public Form2()
        {
            InitializeComponent();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tgtNmbr = rnd.Next(1, 100);
            btnMenuPlay.Text = "Заново";
            statusStrip1.Visible = true;
            progressBar1.Maximum = maxTryCount;
            tryCount = maxTryCount;
            lblTryCount.Text = tryCount.ToString();
            lblGameInfo.Visible = true;
            lblGameInfo.Text = $"Компьютер загадал число от 1 до 100.\nПопробуйте его отгадать за {maxTryCount} попыток.";
            lblLog.Text = "Ваши ответы: ";
            lblLog.Visible = false;
            lblInfo2.Visible = true;
            txtBoxAnswer.Visible = true;
            btnAnswer.Visible = true;
            btnAnswer.Enabled = true;
            txtBoxAnswer.Clear();
            txtBoxAnswer.Enabled = true;
        }

        private void lblTryCount_TextChanged(object sender, EventArgs e)
        {
            progressBar1.Value = tryCount;
        }

        private void btnAnswer_Click(object sender, EventArgs e)
        {
            int userAnswer;
            if (int.TryParse(txtBoxAnswer.Text, out userAnswer))
            {
                if (userAnswer == tgtNmbr)
                {
                    btnAnswer.Enabled = false;
                    txtBoxAnswer.Enabled = false;
                    MessageBox.Show("Поздравляем, вы угадали!");
                }
                else
                {
                    lblLog.Visible = true;
                    lblLog.Text += $"{txtBoxAnswer.Text}   ";
                    txtBoxAnswer.Clear();
                    tryCount--;
                    lblTryCount.Text = tryCount.ToString();
                    if (userAnswer < tgtNmbr)
                        lblGameInfo.Text = "Недобор";
                    if (userAnswer > tgtNmbr)
                        lblGameInfo.Text = "Перебор";
                    if (tryCount == 0)
                    {
                        btnAnswer.Enabled = false;
                        txtBoxAnswer.Enabled = false;
                        MessageBox.Show("К сожалению, у вас не осталось попыток...");
                    }
                }
            }
        }

        private void txtBoxAnswer_TextChanged(object sender, EventArgs e)
        {
            lblGameInfo.Text = "";
        }

        private void txtBoxAnswer_KeyPress(object sender, KeyPressEventArgs e)
        {
            MessageBox.Show(e.GetType().ToString());
        }
    }
}
