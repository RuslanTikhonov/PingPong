using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PingPong
{
    public partial class Form1 : Form
    {
        private int speed_vertical = 3; //скорость движения кубика по вертикали
        private int speed_hor = 3; //скорость движения кубика по горизонтали
        private int score = 0; //счетчик очков


        public Form1()
        {
            InitializeComponent();
            timer.Enabled = true; // активизирую таймер
            Cursor.Hide(); // скрываю курсор
            //this.FormBorderStyle = FormBorderStyle.None; //убираю рамки окна программы
            this.TopMost = true; // поверх других окон

           this.Bounds = Screen.PrimaryScreen.Bounds; //установка размера экрана во весь экран

            gamePanel.Top = background.Bottom - (background.Bottom / 10); // положение игровой панели

            looseLabel.Left = (background.Width / 2) - (looseLabel.Width / 2);
            looseLabel.Top = (background.Height / 2) - (looseLabel.Height / 2);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        //закрываем программу по нажатию Esc
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            //начинаем игру заново при проигрыша
            if (e.KeyCode == Keys.R)
            {
                gameBall.Top = 50;
                gameBall.Left = 70;
                speed_hor = 2;
                speed_vertical = 2;
                score = 0;
                looseLabel.Visible = false;
                timer.Enabled = true;
                result.Text = "Результат: 0";
            }

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            gamePanel.Left = Cursor.Position.X - (gamePanel.Width / 2); //привязываем панель к движениям мышки


            //движение мячика
            gameBall.Left += speed_hor;
            gameBall.Top += speed_vertical;
            // отбиваемся от стенок и игровой панели
            if (gameBall.Left <= background.Left)
                speed_hor *= -1;
            if (gameBall.Right >= background.Right)
                speed_hor *= -1;
            if (gameBall.Top <= background.Top)
                speed_vertical *= -1;
            if (gameBall.Bottom >= background.Bottom)
            {
                looseLabel.Visible = true;
                timer.Enabled = false;
            }
            if (gameBall.Bottom >= gamePanel.Top && gameBall.Bottom <= gamePanel.Bottom 
                && gameBall.Left >= gamePanel.Left && gameBall.Right <= gamePanel.Right)
            {
                //увеличиваем скорость мячика
                speed_hor += 2;
                speed_vertical += 2;
                speed_vertical *= -1;
                score += 1; //добовляем очки

                result.Text = "Результат: " + score.ToString();

                Random ranColor = new Random();
                background.BackColor = Color.FromArgb(ranColor.Next(150, 255), ranColor.Next(150, 255), ranColor.Next(150, 255));
            }

        }
    }
}

