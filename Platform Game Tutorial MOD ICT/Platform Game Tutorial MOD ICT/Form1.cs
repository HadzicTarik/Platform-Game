using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Platform_Game_Tutorial_MOD_ICT
{
    public partial class Form1 : Form
    {
        bool goLeft, goRight, jumping, gameOver;
        int jumpSpeed, force;

        int score = 0;
        int playerSpeed = 7;

        int horizontalSpeed = 5;
        int verticalSpeed = 3;

        int enemyOneSpeed = 5;
        int enemyTwoSpeed = 3;
        
        public Form1()
        {
            InitializeComponent();

            /*GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(0, 0, freeObj.Width - 1, freeObj.Height - 1);
            Region rg = new Region(gp);
            freeObj.Region = rg;

            GraphicsPath gp2 = new GraphicsPath();
            gp2.AddEllipse(0, 0, freeObj2.Width - 1, freeObj2.Height - 1);
            Region rg2 = new Region(gp2);
            freeObj2.Region = rg2;*/
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void MaainGameTimerEvent(object sender, EventArgs e)
        {
            lblScore.Text = "Score: " + score;
            
            player.Top += jumpSpeed;

            if(goLeft == true)
            {
                player.Left -= playerSpeed;
            }
            if (goRight == true)
            {
                player.Left += playerSpeed;
            }

            if(jumping == true && force < 0)
            {
                jumping = false;
            }
            if(jumping == true)
            {
                jumpSpeed = -8;
                force -= 1;
            }
            else
            {
                jumpSpeed = 10;
            }

            foreach (Control x in this.Controls)
            {
                if(x is PictureBox)
                {
                    /*if ((string)x.Tag == "freeObj")
                    {
                        freeObj.Top += playerSpeed;
                        freeObj2.Top += playerSpeed;
                        if((freeObj.Top < 95 && freeObj2.Top < 70) || (freeObj.Top > 325 && freeObj2.Top > 300))
                        {
                            playerSpeed = -playerSpeed;
                        }
                    }*/

                    if ((string)x.Tag == "platform")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = 8;
                            player.Top = x.Top - player.Height;

                            if(((string)x.Name == "horizontalPlatform" && goLeft == false) || ((string)x.Name == "horizontalPlatform" && goRight == false))
                            {
                                player.Left -= horizontalSpeed;
                            }
                        }
                        x.BringToFront();
                    }
                    if ((string)x.Tag == "coin")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                        {
                            x.Visible = false;
                            score++;
                        }
                    }
                    if ((string)x.Tag == "enemy")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameTimer.Stop();
                            gameOver = true;
                            lblScore.Text = "Score: " + score + Environment.NewLine + "You were killed in your journey!!!" ;
                        }
                    }
                }
            }

            horizontalPlatform.Left -= horizontalSpeed; 
            if(horizontalPlatform.Left < 0 || (horizontalPlatform.Left + horizontalPlatform.Width > this.ClientSize.Width))
            {
                horizontalSpeed = -horizontalSpeed;
            }

            verticalPlatform.Top += verticalSpeed;
            if(verticalPlatform.Top < 271 || verticalPlatform.Top > 585)
            {
                verticalSpeed = -verticalSpeed;
            }

            enemyOne.Left -= enemyOneSpeed;
            if(enemyOne.Left < pictureBox5.Left || (enemyOne.Left + enemyOne.Width > pictureBox5.Left + pictureBox5.Width))
            {
                enemyOneSpeed = -enemyOneSpeed;
            }

            enemyTwo.Left -= enemyTwoSpeed;
            if (enemyTwo.Left < pictureBox2.Left || (enemyTwo.Left + enemyTwo.Width > pictureBox2.Left + pictureBox2.Width))
            {
                enemyTwoSpeed = -enemyTwoSpeed;
            }

            if (player.Top + player.Height > this.ClientSize.Height + 50)
            {
                gameTimer.Stop();
                gameOver = true;
                lblScore.Text = "Score: " + score + Environment.NewLine + "You are death!!!" ;
            }

            if (player.Bounds.IntersectsWith(pbDoor.Bounds) && score > 30)
            {
                gameTimer.Stop();
                gameOver = true;
                lblScore.Text = "Score: " + score + Environment.NewLine + "You quest is complete";
            }
            else
            {
                lblScore.Text = "Score: " + score + Environment.NewLine + "Collect at least 30 coins";
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (jumping == true)
            {
                jumping = false;
            }
            if(e.KeyCode == Keys.Enter && gameOver == true)
            {
                RestartGame();
            }
        }
        private void RestartGame()
        {
            jumping = false;
            goLeft = false;
            goRight = false;
            gameOver = false;
            score = 0;

            lblScore.Text = "Score: " + score;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == false)
                {
                    x.Visible = true;
                }
            }
            //Reset the position of: player, platform and enemy
            player.Left = 76;
            player.Top = 660;

            enemyOne.Left = 427;
            enemyOne.Top = 358;
            enemyTwo.Left = 486;
            enemyTwo.Top = 622;

            horizontalPlatform.Left = 244;
            horizontalPlatform.Top = 208;
            verticalPlatform.Left = 568;
            verticalPlatform.Top = 585;


            gameTimer.Start();
        }
    }
}
