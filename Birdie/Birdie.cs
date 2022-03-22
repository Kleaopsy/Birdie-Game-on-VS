using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Birdie
{
    public partial class Birdie : Form
    {
        public Birdie()
        {
            InitializeComponent();
        }

        List<Image> BirdieKS = new List<Image>();
        private void Birdie_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile("BackGround.png");
            BirdieKS.Add(Image.FromFile("Birdie.png"));
            BirdieKS.Add(Image.FromFile("Birdie 2.png"));
            timer2.Enabled = true;

            label1.Text = BlokKırmaHakkı.ToString();
        }

        bool SariKusKirmiziKus;
        int Baslamaya5Saniye=1;
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (SariKusKirmiziKus==false)
            {
                SariKusKirmiziKus = true;
                pictureBox1.BackgroundImage = BirdieKS[0];
            }
            else
            {
                SariKusKirmiziKus = false;
                pictureBox1.BackgroundImage = BirdieKS[1];
            }

            if (Baslamaya5Saniye==3)
            {
                Baslamaya5Saniye = 0;
                timer1.Start();
            }
            else if (Baslamaya5Saniye<3 && Baslamaya5Saniye>0)
            {
                Baslamaya5Saniye++;
            }
        }

        int ButonSayisi,BlokKırmaHakkı=5;
        List<Button> SpaceButton = new List<Button>();
        Button BtnMaker;

        private void PressToSpace(object sender,KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Space && pictureBox1.Location.Y != 60)
            {
                BtnMaker = new Button();
                BtnMaker.Name = "SpaceButton"+(ButonSayisi+1);
                BtnMaker.FlatStyle = FlatStyle.Popup;
                BtnMaker.BackColor = Color.White;
                BtnMaker.Enabled = false;
                BtnMaker.Size = new Size(40,60);
                BtnMaker.BringToFront();
                BtnMaker.Location = new Point(pictureBox1.Location.X + 10 , pictureBox1.Location.Y);
                SpaceButton.Add(BtnMaker);
                this.Controls.Add(BtnMaker);

                pictureBox1.Location = new Point(pictureBox1.Location.X,pictureBox1.Location.Y-60);

                ButonSayisi++;
            }
            if (e.KeyCode==Keys.E && pictureBox1.Location.Y != 420 && BlokKırmaHakkı !=0)
            {
                this.Controls.Remove(this.Controls["SpaceButton"+ButonSayisi]);
                SpaceButton.Remove(SpaceButton[ButonSayisi-1]);

                pictureBox1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y + 60);

                BlokKırmaHakkı--;
                label1.Text = BlokKırmaHakkı.ToString();
                ButonSayisi--;
            }
        }

        //İlk Bloğun Koyulduğu Y Kordinatı 420 Son Koyulduğu İse 120
        Random RastgaleSayi = new Random();
        List<Button> ParkourButtons = new List<Button>();
        int s=20,KırılanBlokSayısı;
        bool GameOver = false,Kırıldı=false,yn;
        int KırıldıSay,hafıza;
        int Skorr;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (s==20)
            {
                Parkour_Maker(RastgaleSayi.Next(1, 7));
                s = 0;
            }
            
            foreach (Button BBTTNN in ParkourButtons)
            {
                BBTTNN.Location = new Point(BBTTNN.Location.X - 15, BBTTNN.Location.Y);

                if (BBTTNN.Location.X == 150 && pictureBox1.Location.Y == BBTTNN.Location.Y)
                {
                    timer1.Stop();
                    GameOver = true;
                }                

                if (BBTTNN.Location.X < 0)
                {
                    this.Controls.Remove(BBTTNN);
                }

                if (BBTTNN.Location.X >= 135 && BBTTNN.Location.X < 150 && BBTTNN.Location.Y == 420)
                {
                    Skorr++;
                    label4.Text = Skorr.ToString();
                    BlokKırmaHakkı++;
                    label1.Text = BlokKırmaHakkı.ToString();
                }

                if (GameOver != true)
                {
                    foreach (Button BBTTNN2 in SpaceButton)
                    {
                        if (BBTTNN.Location.Y == BBTTNN2.Location.Y && BBTTNN.Location.X == 150)
                        {
                            for (int i = 0; i < hafıza; i++)
                            {
                                if(BBTTNN2.Name == "SpaceButton" + (i+1))
                                {
                                    yn = true;
                                }
                            }
                            if (yn == false)
                            {
                                KırılanBlokSayısı++;
                                Kırıldı = true;
                            }
                            else
                            {
                                yn = false;
                            }
                            
                        }
                    }
                }                
            }

            if (Kırıldı == true)
            {
                KırıldıSay++;
                for (int i = 0; i < KırılanBlokSayısı; i++)
                {
                    Controls.Remove(Controls["SpaceButton"+(ButonSayisi-i)]);
                }
            }
            if (KırıldıSay == 4)
            {
                KırıldıSay = 0; Kırıldı = false;

                pictureBox1.Location = new Point(120, pictureBox1.Location.Y + (60 * KırılanBlokSayısı));
                hafıza += KırılanBlokSayısı;
                KırılanBlokSayısı = 0;
            }

            if (GameOver == true)
            {
                DialogResult Retry = MessageBox.Show("Game Over", ":.(", MessageBoxButtons.OK);

                if (Retry == DialogResult.Retry)
                {
                //    for (int i = 0; i < SpaceButton.Count; i++)
                //    {
                //        this.Controls.Remove(SpaceButton[i]);
                //    }
                //    for (int i = 0; i < ParkourButtons.Count; i++)
                //    {
                //        this.Controls.Remove(ParkourButtons[i]);
                //        ParkourButtons.Remove(ParkourButtons[i]);
                //    }
                //    SpaceButton.Clear();
                //ParkourButtons.Clear();

                //    KırıldıSay = 0;
                //    hafıza = 0;

                //    GameOver = false;
                //    Kırıldı = false;
                //    yn = false;

                //    s = 20;

                //    KırılanBlokSayısı = 0;

                //    X = 0;
                //    ParkourButtonSayisi = 0;


                //    pictureBox1.Location = new Point(120, 420);

                //    timer1.Start();
                }
            }
            s++;
        }

        int X,ParkourButtonSayisi;
        private void Parkour_Maker(int S1)
        {
            if (S1==1)
            {
                for (int i = 1; i <= 5; i++)
                {
                    BtnMaker = new Button();
                    BtnMaker.Name = "ParkourButton" + (ParkourButtonSayisi+1);
                    BtnMaker.Size = new Size(40,60);
                    BtnMaker.SendToBack();
                    BtnMaker.FlatStyle = FlatStyle.Popup;
                    BtnMaker.BackColor = Color.LightSkyBlue;
                    BtnMaker.Enabled = false;

                    BtnMaker.Location = new Point(900,420-X);
                    X += 60;

                    ParkourButtons.Add(BtnMaker);
                    Controls.Add(BtnMaker);

                    ParkourButtonSayisi++;
                }                
            }

            if (S1==2)
            {
                
                for (int i = 1; i <= 3; i++)
                {
                    BtnMaker = new Button();
                    BtnMaker.Name = "ParkourButton" + (ParkourButtonSayisi + 1);
                    BtnMaker.Size = new Size(40, 60);
                    BtnMaker.SendToBack();
                    BtnMaker.FlatStyle = FlatStyle.Popup;
                    BtnMaker.BackColor = Color.LightSkyBlue;
                    BtnMaker.Enabled = false;

                    BtnMaker.Location = new Point(900, 420 - X);
                    X += 60;

                    ParkourButtons.Add(BtnMaker);
                    Controls.Add(BtnMaker);

                    ParkourButtonSayisi++;
                }
                X += 120;

                for (int i = 0; i < 2; i++)
                {
                    BtnMaker = new Button();
                    BtnMaker.Name = "ParkourButton" + (ParkourButtonSayisi + 1);
                    BtnMaker.Size = new Size(40, 60);
                    BtnMaker.SendToBack();
                    BtnMaker.FlatStyle = FlatStyle.Popup;
                    BtnMaker.BackColor = Color.LightSkyBlue;
                    BtnMaker.Enabled = false;

                    BtnMaker.Location = new Point(900, 420 - X);
                    X += 60;

                    ParkourButtons.Add(BtnMaker);
                    Controls.Add(BtnMaker);

                    ParkourButtonSayisi++;
                }
            }


            if (S1==3)
            {
                for (int i = 1; i <= 1; i++)
                {
                    BtnMaker = new Button();
                    BtnMaker.Name = "ParkourButton" + (ParkourButtonSayisi + 1);
                    BtnMaker.Size = new Size(40, 60);
                    BtnMaker.SendToBack();
                    BtnMaker.FlatStyle = FlatStyle.Popup;
                    BtnMaker.BackColor = Color.LightSkyBlue;
                    BtnMaker.Enabled = false;

                    BtnMaker.Location = new Point(900, 420 - X);
                    X += 60;

                    ParkourButtons.Add(BtnMaker);
                    Controls.Add(BtnMaker);

                    ParkourButtonSayisi++;
                }

                X += 120;

                for (int i = 0; i < 4; i++)
                {
                    BtnMaker = new Button();
                    BtnMaker.Name = "ParkourButton" + (ParkourButtonSayisi + 1);
                    BtnMaker.Size = new Size(40, 60);
                    BtnMaker.SendToBack();
                    BtnMaker.FlatStyle = FlatStyle.Popup;
                    BtnMaker.BackColor = Color.LightSkyBlue;
                    BtnMaker.Enabled = false;

                    BtnMaker.Location = new Point(900, 420 - X);
                    X += 60;

                    ParkourButtons.Add(BtnMaker);
                    Controls.Add(BtnMaker);

                    ParkourButtonSayisi++;
                }
            }

            if (S1 == 4)
            {
                for (int i = 1; i <= 2; i++)
                {
                    BtnMaker = new Button();
                    BtnMaker.Name = "ParkourButton" + (ParkourButtonSayisi + 1);
                    BtnMaker.Size = new Size(40, 60);
                    BtnMaker.SendToBack();
                    BtnMaker.FlatStyle = FlatStyle.Popup;
                    BtnMaker.BackColor = Color.LightSkyBlue;
                    BtnMaker.Enabled = false;

                    BtnMaker.Location = new Point(900, 420 - X);
                    X += 60;

                    ParkourButtons.Add(BtnMaker);
                    Controls.Add(BtnMaker);

                    ParkourButtonSayisi++;
                }

                X += 120;

                for (int i = 0; i < 3; i++)
                {
                    BtnMaker = new Button();
                    BtnMaker.Name = "ParkourButton" + (ParkourButtonSayisi + 1);
                    BtnMaker.Size = new Size(40, 60);
                    BtnMaker.SendToBack();
                    BtnMaker.FlatStyle = FlatStyle.Popup;
                    BtnMaker.BackColor = Color.LightSkyBlue;
                    BtnMaker.Enabled = false;

                    BtnMaker.Location = new Point(900, 420 - X);
                    X += 60;

                    ParkourButtons.Add(BtnMaker);
                    Controls.Add(BtnMaker);

                    ParkourButtonSayisi++;
                }
            }

            if (S1 == 5)
            {
                for (int i = 1; i <= 4; i++)
                {
                    BtnMaker = new Button();
                    BtnMaker.Name = "ParkourButton" + (ParkourButtonSayisi + 1);
                    BtnMaker.Size = new Size(40, 60);
                    BtnMaker.SendToBack();
                    BtnMaker.FlatStyle = FlatStyle.Popup;
                    BtnMaker.BackColor = Color.LightSkyBlue;
                    BtnMaker.Enabled = false;

                    BtnMaker.Location = new Point(900, 420 - X);
                    X += 60;

                    ParkourButtons.Add(BtnMaker);
                    Controls.Add(BtnMaker);

                    ParkourButtonSayisi++;
                }

                X += 120;

                for (int i = 0; i < 1; i++)
                {
                    BtnMaker = new Button();
                    BtnMaker.Name = "ParkourButton" + (ParkourButtonSayisi + 1);
                    BtnMaker.Size = new Size(40, 60);
                    BtnMaker.SendToBack();
                    BtnMaker.FlatStyle = FlatStyle.Popup;
                    BtnMaker.BackColor = Color.LightSkyBlue;
                    BtnMaker.Enabled = false;

                    BtnMaker.Location = new Point(900, 420 - X);
                    X += 60;

                    ParkourButtons.Add(BtnMaker);
                    Controls.Add(BtnMaker);

                    ParkourButtonSayisi++;
                }
            }

            if (S1 == 6)
            {
                for (int i = 1; i <= 1; i++)
                {
                    BtnMaker = new Button();
                    BtnMaker.Name = "ParkourButton" + (ParkourButtonSayisi + 1);
                    BtnMaker.Size = new Size(40, 60);
                    BtnMaker.SendToBack();
                    BtnMaker.FlatStyle = FlatStyle.Popup;
                    BtnMaker.BackColor = Color.LightSkyBlue;
                    BtnMaker.Enabled = false;

                    BtnMaker.Location = new Point(900, 420 - X);
                    X += 60;

                    ParkourButtons.Add(BtnMaker);
                    Controls.Add(BtnMaker);

                    ParkourButtonSayisi++;
                }

                X += 60;

                for (int i = 0; i < 4; i++)
                {
                    BtnMaker = new Button();
                    BtnMaker.Name = "ParkourButton" + (ParkourButtonSayisi + 1);
                    BtnMaker.Size = new Size(40, 60);
                    BtnMaker.SendToBack();
                    BtnMaker.FlatStyle = FlatStyle.Popup;
                    BtnMaker.BackColor = Color.LightSkyBlue;
                    BtnMaker.Enabled = false;

                    BtnMaker.Location = new Point(900, 420 - X);
                    X += 60;

                    ParkourButtons.Add(BtnMaker);
                    Controls.Add(BtnMaker);

                    ParkourButtonSayisi++;
                }
            }
            X = 0;
        }
    }
}
