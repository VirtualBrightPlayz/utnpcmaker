using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnturnedNPCMaker
{
    public partial class Form1 : Form
    {
        int ID = 0;
        int IDchat = 0;
        int totalMSGs = 0;
        int totalMSGsall = 0;
        int totalRESPs = 0;
        List<Button> btns = new List<Button>();
        List<Button> btns2 = new List<Button>();
        List<MessageSettings> btnsmsg = new List<MessageSettings>();
        List<ResponseSettings> btnsresp = new List<ResponseSettings>();
        List<Button> btns3 = new List<Button>();
        List<Button> btns4 = new List<Button>();
        TextBox msgbox = new TextBox();
        Button msgexitbtn = new Button();
        Panel msgPanel2 = new Panel();
        Button addmsgPage = new Button();
        Button refmsgPage = new Button();
        TextBox respbox = new TextBox();
        Button respexitbtn = new Button();
        Panel respPanel2 = new Panel();

        public Form1()
        {
            InitializeComponent();
            CharEdit.Visible = false;
            ChatEdit.Visible = false;
            label1credits.Text = "Made By VirtualBrightPlayz\n Thanks to u/CaptainStar and u/KurvaZelena\n on reddit for the face, beard, and hair pictures!\n Post can be found here:\n https://www.reddit.com/r/unturned/comments/6e6n1f/npcs_face_hair_and_beard_ids/ \nThanks to http://steamcommunity.com/id/knifedera on steam for the guide of ids";
            //messages
            msgexitbtn.Visible = false;
            msgexitbtn.Parent = mesgpanel;
            msgexitbtn.Text = "Back";
            msgexitbtn.Left = 4;
            msgexitbtn.Top = 4 + 20;
            msgexitbtn.Width = 75;
            msgexitbtn.Height = 23;
            msgbox.Visible = false;
            msgbox.Parent = mesgpanel;
            msgbox.Top = 4;
            msgbox.Left = 4;
            msgbox.Width = 334;
            msgbox.Height = 20;
            msgPanel2.Visible = false;
            msgPanel2.Parent = mesgpanel;
            msgPanel2.Top = 4 + 23 + 20;
            msgPanel2.Left = 4 + 75;
            msgPanel2.AutoScroll = true;
            //#To lazy to do math
            msgPanel2.Width = 334 - 75;
            msgPanel2.Height = 300;
            addmsgPage.Visible = false;
            addmsgPage.Parent = mesgpanel;
            addmsgPage.Click += (object sender, EventArgs e) => {
                string name = "";
                foreach (string str in addmsgPage.Text.Split(new char[] { ':' }))
                {
                    if (addmsgPage.Text.Split(new char[] { ':' })[0] != str)
                        name += str;
                }
                Button btn = new Button();
                btn.Visible = true;
                MessageSettings.findMessageSettings(btnsmsg, name).totalPages++;
                btn.Text = "Page #" + MessageSettings.findMessageSettings(btnsmsg, name).totalPages;
                btn.Parent = msgPanel2;
                btn.Height = 23;
                btn.Width = 100;
                btn.Top = ((4 + 23) * (MessageSettings.findMessageSettings(btnsmsg, name).findTotalPages())) - msgPanel2.VerticalScroll.Value;
                btn.Left = 4;
                btn.Click += (object sender2, EventArgs e2) => {
                    int totalPGS = 0;
                    foreach (Button thebtn in MessageSettings.findMessageSettings(btnsmsg, name).messages)
                    {
                        totalPGS++;
                        //Controls.Remove(thebtn);
                        thebtn.Visible = false;
                        thebtn.Top = ((4 + 23) * (totalPGS - 1)) - msgPanel2.VerticalScroll.Value;
                    }
                    totalPGS = 0;
                    foreach (Button thebtn in MessageSettings.findMessageSettings(btnsmsg, name).messages2)
                    {
                        totalPGS++;
                        //Controls.Remove(thebtn);
                        thebtn.Visible = false;
                        thebtn.Top = ((4 + 23) * (totalPGS - 1)) - msgPanel2.VerticalScroll.Value;
                    }
                    msgbox.Text = btn.Text;
                    msgPanel2.Visible = false;
                    addmsgPage.Visible = false;
                    FieldInfo f1 = typeof(Control).GetField("EventClick", BindingFlags.Static | BindingFlags.NonPublic);
                    object obj = f1.GetValue(msgexitbtn);
                    PropertyInfo pi = msgexitbtn.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);
                    EventHandlerList list = (EventHandlerList)pi.GetValue(msgexitbtn, null);
                    list.RemoveHandler(obj, list[obj]);
                    msgexitbtn.Click += (object sender3, EventArgs e3) => {
                        //msgbox.Text = name;
                        msgPanel2.Visible = true;
                        addmsgPage.Visible = true;
                        //refmesg_Click(sender3, e3);
                        btn.Text = msgbox.Text;
                        foreach (Button thebtn in MessageSettings.findMessageSettings(btnsmsg, name).messages)
                        {
                            totalPGS++;
                            //Controls.Remove(thebtn);
                            thebtn.Visible = false;
                            thebtn.Top = ((4 + 23) * (totalPGS - 1)) - msgPanel2.VerticalScroll.Value;
                        }
                        totalPGS = 0;
                        foreach (Button thebtn in MessageSettings.findMessageSettings(btnsmsg, name).messages2)
                        {
                            totalPGS++;
                            //Controls.Remove(thebtn);
                            thebtn.Visible = false;
                            thebtn.Top = ((4 + 23) * (totalPGS - 1)) - msgPanel2.VerticalScroll.Value;
                        }
                        msgexitbtn.Click += (object sender4, EventArgs e4) => {
                            msgbox.Text = "";
                            msgbox.Visible = false;
                            msgexitbtn.Visible = false;
                            msgPanel2.Visible = false;
                            addmsgPage.Visible = false;
                            refmsgPage.Visible = false;
                            //refmesg_Click(sender3, e3);
                        };
                    };
                };
                Button btn2 = new Button();
                btn2.Visible = true;
                btn2.Text = "X";
                btn2.Parent = msgPanel2;
                btn2.Height = 23;
                btn2.Width = 23;
                btn2.Top = btn.Top;
                btn2.Left = 4 + btn.Width;
                MessageSettings.findMessageSettings(btnsmsg, name).addPage(btn, btn2);
                btn2.Click += (object sender2, EventArgs e2) => {
                    Button thebtn1 = btn;
                    Button thebtn2 = btn2;
                    thebtn1.Visible = false;
                    thebtn2.Visible = false;
                    MessageSettings.findMessageSettings(btnsmsg, name).removePage(btn, btn2);
                    Controls.Remove(thebtn1);
                    Controls.Remove(thebtn2);
                    int totalPGS = 0;
                    foreach (Button thebtn in MessageSettings.findMessageSettings(btnsmsg, name).messages)
                    {
                        totalPGS++;
                        //Controls.Remove(thebtn);
                        thebtn.Visible = true;
                        thebtn.Top = ((4 + 23) * (totalPGS - 1)) - msgPanel2.VerticalScroll.Value;
                    }
                    totalPGS = 0;
                    foreach (Button thebtn in MessageSettings.findMessageSettings(btnsmsg, name).messages2)
                    {
                        totalPGS++;
                        //Controls.Remove(thebtn);
                        thebtn.Visible = true;
                        thebtn.Top = ((4 + 23) * (totalPGS - 1)) - msgPanel2.VerticalScroll.Value;
                    }
                };
            };
            addmsgPage.Text = "Add Page";
            addmsgPage.Top = 4 + 23 + 20;
            addmsgPage.Left = 4;
            addmsgPage.Width = 75;
            addmsgPage.Height = 40;
            refmsgPage.Visible = false;
            refmsgPage.Parent = mesgpanel;
            refmsgPage.Click += (object sender, EventArgs e) => {
                string name = "";
                foreach (string str in addmsgPage.Text.Split(new char[] { ':' }))
                {
                    if (addmsgPage.Text.Split(new char[] { ':' })[0] != str)
                        name += str;
                }
                int totalPGS = 0;
                foreach (Button thebtn in MessageSettings.findMessageSettings(btnsmsg, name).messages)
                {
                    totalPGS++;
                    //Controls.Remove(thebtn);
                    thebtn.Visible = true;
                    thebtn.Top = ((4 + 23) * (totalPGS - 1)) - msgPanel2.VerticalScroll.Value;
                }
                totalPGS = 0;
                foreach (Button thebtn in MessageSettings.findMessageSettings(btnsmsg, name).messages2)
                {
                    totalPGS++;
                    //Controls.Remove(thebtn);
                    thebtn.Visible = true;
                    thebtn.Top = ((4 + 23) * (totalPGS - 1)) - msgPanel2.VerticalScroll.Value;
                }
            };
            refmsgPage.Text = "Refresh Pages";
            refmsgPage.Top = 4 + 23 + 20 + 40;
            refmsgPage.Left = 4;
            refmsgPage.Width = 75;
            refmsgPage.Height = 40;
            //responses
            respexitbtn.Visible = false;
            respexitbtn.Parent = resppanel;
            respexitbtn.Text = "Back";
            respexitbtn.Left = 4;
            respexitbtn.Top = 4 + 20;
            respexitbtn.Width = 75;
            respexitbtn.Height = 23;
            respbox.Visible = false;
            respbox.Parent = resppanel;
            respbox.Top = 4;
            respbox.Left = 4;
            respbox.Width = 334;
            respbox.Height = 20;
            respPanel2.Visible = false;
            respPanel2.Parent = resppanel;
            respPanel2.Top = 4 + 23 + 20;
            respPanel2.Left = 4;
            respPanel2.Width = 334;
            respPanel2.Height = 300;
        }

        private void AddmsgPage_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void new_char(object sender, EventArgs e)
        {
            int id = 0;
            if (int.TryParse(IDChar.Text, out id)) {
                NewChar.Visible = false;
                LoadChar.Visible = false;
                IDChar.Visible = false;
                CharEdit.Visible = true;
                ID = id;
            }
        }

        private void save_char(object sender, EventArgs e)
        {
            List<string> contents = new List<string>();
            contents.Add("Type NPC");
            contents.Add("ID " + ID);

            contents.Add("\nShirt " + ShirtBox.Text);
            contents.Add("Pants " + PantsBox.Text);
            contents.Add("Mask " + MaskBox.Text);
            contents.Add("Hat " + HatBox.Text);
            contents.Add("Backpack " + BackpackBox.Text);
            contents.Add("Glasses " + GlassesBox.Text);
            contents.Add("Vest " + VestBox.Text);

            Color colorhair = Color.FromArgb((int)redHair.Value, (int)greenHair.Value, (int)blueHair.Value);
            string Color_Hair = colorhair.R.ToString("X2") + colorhair.G.ToString("X2") + colorhair.B.ToString("X2");
            Color colorskin = Color.FromArgb((int)redSkin.Value, (int)greenSkin.Value, (int)blueSkin.Value);
            string Color_Skin = colorskin.R.ToString("X2") + colorskin.G.ToString("X2") + colorskin.B.ToString("X2");

            contents.Add("\nFace " + (int)trackBarFace.Value);
            contents.Add("Beard " + (int)trackBarBeard.Value);
            contents.Add("Hair " + (int)trackBarHair.Value);
            contents.Add("Color_Skin #" + Color_Skin);
            contents.Add("Color_Hair #" + Color_Hair);

            string equippedItem = "";
            if (PrimaryBox.Checked)
                equippedItem = "Primary";
            if (SecondaryBox.Checked)
                equippedItem = "Secondary";
            if (TertiaryBox.Checked)
                equippedItem = "Tertiary";

            string Pose = "";
            if (standbox.Checked)
                Pose = "Stand";
            if (sitbox.Checked)
                Pose = "Sit";
            if (asleepbox.Checked)
                Pose = "Asleep";
            if (passivebox.Checked)
                Pose = "Passive";
            if (crouchbox.Checked)
                Pose = "Crouch";

            contents.Add("\n");
            if (primaryon.Checked)
                contents.Add("Primary " + primary.Text);
            if (secondaryon.Checked)
                contents.Add("Secondary " + secondary.Text);
            if (tertiaryon.Checked)
                contents.Add("Tertiary " + tertiary.Text);
            if (equippedon.Checked)
                contents.Add("Equipped " + equippedItem);
            if (backwardson.Checked)
                contents.Add("Backward");
            contents.Add("Pose " + Pose);

            //if (hasDialog)
            //contents.Add("\nDialog " + Dialog);
            File.WriteAllLines(Directory.GetCurrentDirectory() + "/char." + ID + ".Asset.dat", contents.ToArray());
        }

        private void charetcguide_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://steamcommunity.com/sharedfiles/filedetails/?id=384578439");
        }

        private void LoadChar_Click(object sender, EventArgs e)
        {
            int id = 0;
            if (int.TryParse(IDChar.Text, out id))
            {
                NewChar.Visible = false;
                LoadChar.Visible = false;
                IDChar.Visible = false;
                CharEdit.Visible = true;
                ID = id;
            }
            List<string> contents = new List<string>();
            if (!File.Exists(Directory.GetCurrentDirectory() + "/char." + ID + ".Asset.dat")) {
                return;
            }
            foreach (string myString in File.ReadAllLines(Directory.GetCurrentDirectory() + "/char." + ID + ".Asset.dat"))
            {
                try
                {
                    char[] sep = new char[] { ' ' };
                    if (myString.ToLower().StartsWith("shirt"))
                        ShirtBox.Text = myString.Split(sep)[1];
                    else if (myString.ToLower().StartsWith("pants"))
                        PantsBox.Text = myString.Split(sep)[1];
                    else if (myString.ToLower().StartsWith("mask"))
                        MaskBox.Text = myString.Split(sep)[1];
                    else if (myString.ToLower().StartsWith("hat"))
                        HatBox.Text = myString.Split(sep)[1];
                    else if (myString.ToLower().StartsWith("backpack"))
                        BackpackBox.Text = myString.Split(sep)[1];
                    else if (myString.ToLower().StartsWith("glasses"))
                        GlassesBox.Text = myString.Split(sep)[1];
                    else if (myString.ToLower().StartsWith("vest"))
                        VestBox.Text = myString.Split(sep)[1];

                    else if (myString.ToLower().StartsWith("face"))
                        trackBarFace.Value = decimal.Parse(myString.Split(sep)[1]);
                    else if (myString.ToLower().StartsWith("beard"))
                        trackBarBeard.Value = decimal.Parse(myString.Split(sep)[1]);
                    else if (myString.ToLower().StartsWith("hair"))
                        trackBarHair.Value = decimal.Parse(myString.Split(sep)[1]);
                    else if (myString.ToLower().StartsWith("color_skin"))
                    {
                        Color color_Skin = (Color)new ColorConverter().ConvertFromString(myString.Split(sep)[1]);
                        redSkin.Value = color_Skin.R;
                        greenSkin.Value = color_Skin.G;
                        blueSkin.Value = color_Skin.B;
                    }
                    else if (myString.ToLower().StartsWith("color_hair"))
                    {
                        Color color_Hair = (Color)new ColorConverter().ConvertFromString(myString.Split(sep)[1]);
                        redHair.Value = color_Hair.R;
                        greenHair.Value = color_Hair.G;
                        blueHair.Value = color_Hair.B;
                    }
                    else if (myString.ToLower().StartsWith("primary"))
                    {
                        primary.Text = myString.Split(sep)[1];
                        primaryon.Checked = true;
                    }
                    else if (myString.ToLower().StartsWith("secondary"))
                    {
                        secondary.Text = myString.Split(sep)[1];
                        secondaryon.Checked = true;
                    }
                    else if (myString.ToLower().StartsWith("tertiary"))
                    {
                        tertiary.Text = myString.Split(sep)[1];
                        tertiaryon.Checked = true;
                    }
                    else if (myString.ToLower().StartsWith("backward"))
                        backwardson.Checked = true;
                    else if (myString.ToLower().StartsWith("equipped"))
                    {
                        equippedon.Checked = true;
                        if (myString.Split(sep)[1].ToLower().Equals("primary")) {
                            PrimaryBox.Checked = true;
                        }
                        else if (myString.Split(sep)[1].ToLower().Equals("secondary"))
                        {
                            SecondaryBox.Checked = true;
                        }
                        else if (myString.Split(sep)[1].ToLower().Equals("tertiary"))
                        {
                            TertiaryBox.Checked = true;
                        }
                    }
                    else if (myString.ToLower().StartsWith("pose"))
                    {
                        if (myString.Split(sep)[1].ToLower().Equals("stand"))
                        {
                            standbox.Checked = true;
                        }
                        else if (myString.Split(sep)[1].ToLower().Equals("sit"))
                        {
                            sitbox.Checked = true;
                        }
                        else if (myString.Split(sep)[1].ToLower().Equals("asleep"))
                        {
                            asleepbox.Checked = true;
                        }
                        else if (myString.Split(sep)[1].ToLower().Equals("passive"))
                        {
                            passivebox.Checked = true;
                        }
                        else if (myString.Split(sep)[1].ToLower().Equals("crouch"))
                        {
                            crouchbox.Checked = true;
                        }
                    }
                }
                catch (Exception exec) { }
            }
            foreach (string myString in File.ReadAllLines(Directory.GetCurrentDirectory() + "/char." + ID + ".English.dat"))
            {
                try
                {
                    char[] sep = new char[] { ' ' };
                    char[] sep2 = new char[] { '<', '>', '=' };
                    if (myString.ToLower().StartsWith("name"))
                    {
                        string fin = "";
                        for (int i = 0; i < myString.Split(sep).Length; i++)
                        {
                            if (i > 0)
                                fin += myString.Split(sep)[i];
                        }
                        textBoxNameinedit.Text = fin;
                    }
                    else if (myString.ToLower().StartsWith("character"))
                    {
                        string finname = "";
                        string color = "";
                        int first_color = myString.Split(sep2).ToList().IndexOf("color");
                        int last_color = myString.Split(sep2).ToList().LastIndexOf("/color");
                        if (last_color != first_color)
                        {
                            color = myString.Split(sep2)[first_color + 1];
                            for (int i = 0; i < myString.Split(sep2).Length; i++)
                            {
                                if (i > first_color + 1 && i < last_color)
                                {
                                    finname += myString.Split(sep2)[i];
                                }
                            }
                        }
                        else
                        {
                            for (int y = 0; y < myString.Split(sep).Length; y++)
                            {
                                if (y > 0)
                                    finname += myString.Split(sep)[y];
                            }
                            color = null;
                        }
                        textBoxNameign.Text = finname;
                        if (color.StartsWith("#"))
                        {
                            radioButtonColor.Checked = true;
                            Color npccolor = (Color)new ColorConverter().ConvertFromString(color);
                            redChar.Value = npccolor.R;
                            greenChar.Value = npccolor.G;
                            blueChar.Value = npccolor.B;
                        }
                        else if (color.ToLower().Equals("common"))
                        {
                            radioButtonCommon.Checked = true;
                        }
                        else if (color.ToLower().Equals("uncommon"))
                        {
                            radioButtonUncommon.Checked = true;
                        }
                        else if (color.ToLower().Equals("rare"))
                        {
                            radioButtonRare.Checked = true;
                        }
                        else if (color.ToLower().Equals("epic"))
                        {
                            radioButtonEpic.Checked = true;
                        }
                        else if (color.ToLower().Equals("legendary"))
                        {
                            radioButtonLegendary.Checked = true;
                        }
                        else if (color.ToLower().Equals("mythical"))
                        {
                            radioButtonMythical.Checked = true;
                        }
                    }
                }
                catch (Exception exec) { }
            }
        }

        private void buttonSaveEnglishChar_Click(object sender, EventArgs e)
        {
            List<string> contents = new List<string>();
            string color = "";
            if (radioButtonColor.Checked)
            {
                Color color_raw = Color.FromArgb((int)redChar.Value, (int)greenChar.Value, (int)blueChar.Value);
                color = "#" + color_raw.R.ToString("X2") + color_raw.G.ToString("X2") + color_raw.B.ToString("X2");
            }
            else if (radioButtonCommon.Checked) {
                color = "common";
            }
            else if (radioButtonUncommon.Checked)
            {
                color = "uncommon";
            }
            else if (radioButtonRare.Checked)
            {
                color = "rare";
            }
            else if (radioButtonEpic.Checked)
            {
                color = "epic";
            }
            else if (radioButtonLegendary.Checked)
            {
                color = "legendary";
            }
            else if (radioButtonMythical.Checked)
            {
                color = "mythical";
            }
            contents.Add("Name " + textBoxNameinedit.Text);
            contents.Add("Character <color=" + color + ">" + textBoxNameign.Text + "</color>");

            File.WriteAllLines(Directory.GetCurrentDirectory() + "/char." + ID + ".English.dat", contents.ToArray());
        }

        private void LoadChat_Click(object sender, EventArgs e)
        {

        }

        private void NewChat_Click(object sender, EventArgs e)
        {
            int id = 0;
            if (int.TryParse(IDChat.Text, out id))
            {
                NewChat.Visible = false;
                LoadChat.Visible = false;
                IDChat.Visible = false;
                ChatEdit.Visible = true;
                IDchat = id;
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://steamcommunity.com/sharedfiles/filedetails/?id=384578439");
        }

        private void addmsg_Click(object sender, EventArgs e)
        {
            Button btn = new Button();
            btn.Visible = true;
            totalMSGs++;
            totalMSGsall++;
            btn.Text = "Message #" + totalMSGsall;
            btn.Parent = mesgpanel;
            btn.Height = 23;
            btn.Width = 200;
            btn.Top = ((4 + 23) * (totalMSGs - 1)) - mesgpanel.VerticalScroll.Value;
            btn.Left = 4;
            btnsmsg.Add(new MessageSettings("Message #" + totalMSGsall));
            btns.Add(btn);
            btn.Click += (object sender2, EventArgs e2) => {
                totalMSGs = 0;
                foreach (Button thebtn in btns)
                {
                    totalMSGs++;
                    Controls.Remove(thebtn);
                    thebtn.Visible = false;
                    thebtn.Top = ((4 + 23) * (totalMSGs - 1)) - mesgpanel.VerticalScroll.Value;
                }
                totalMSGs = 0;
                foreach (Button thebtn in btns2)
                {
                    totalMSGs++;
                    Controls.Remove(thebtn);
                    thebtn.Visible = false;
                    thebtn.Top = ((4 + 23) * (totalMSGs - 1)) - mesgpanel.VerticalScroll.Value;
                }
                msgbox.Visible = true;
                msgbox.Text = btn.Text;
                msgexitbtn.Visible = true;
                msgPanel2.Visible = true;
                addmsgPage.Visible = true;
                refmsgPage.Visible = true;
                addmsgPage.Text = "Add Page to:"+btn.Text;
                FieldInfo f1 = typeof(Control).GetField("EventClick", BindingFlags.Static | BindingFlags.NonPublic);
                object obj = f1.GetValue(msgexitbtn);
                PropertyInfo pi = msgexitbtn.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);
                EventHandlerList list = (EventHandlerList)pi.GetValue(msgexitbtn, null);
                list.RemoveHandler(obj, list[obj]);
                msgexitbtn.Click += (object sender3, EventArgs e3) => {
                    if (MessageSettings.findMessageSettings(btnsmsg, btn.Text).changeName(btnsmsg, msgbox.Text)) {
                        btn.Text = msgbox.Text;
                    }
                    msgbox.Text = "";
                    msgbox.Visible = false;
                    msgexitbtn.Visible = false;
                    msgPanel2.Visible = false;
                    addmsgPage.Visible = false;
                    refmsgPage.Visible = false;
                    refmesg_Click(sender3, e3);
                };
            };
            Button btn2 = new Button();
            btn2.Visible = true;
            btn2.Text = "X";
            btn2.Parent = mesgpanel;
            btn2.Height = 23;
            btn2.Width = 23;
            btn2.Top = btn.Top;
            btn2.Left = 4 + btn.Width;
            btns2.Add(btn2);
            btn2.Click += (object sender2, EventArgs e2) => {
                Button thebtn1 = btn;
                Button thebtn2 = btn2;
                thebtn1.Visible = false;
                thebtn2.Visible = false;
                btnsmsg.Remove(MessageSettings.findMessageSettings(btnsmsg, btn.Text));
                btns.Remove(thebtn1);
                Controls.Remove(thebtn1);
                btns2.Remove(thebtn2);
                Controls.Remove(thebtn2);
                refmesg_Click(sender2, e2);
            };
        }

        private void refmesg_Click(object sender, EventArgs e)
        {
            totalMSGs = 0;
            foreach (Button btn in btns)
            {
                totalMSGs++;
                Controls.Remove(btn);
                btn.Visible = true;
                btn.Top = ((4 + 23) * (totalMSGs - 1)) - mesgpanel.VerticalScroll.Value;
            }
            totalMSGs = 0;
            foreach (Button btn in btns2)
            {
                totalMSGs++;
                Controls.Remove(btn);
                btn.Visible = true;
                btn.Top = ((4 + 23) * (totalMSGs - 1)) - mesgpanel.VerticalScroll.Value;
            }
        }

        private void addresp_Click(object sender, EventArgs e)
        {
            Button btn = new Button();
            btn.Visible = true;
            totalRESPs++;
            btn.Text = "Response #" + totalRESPs;
            btn.Parent = resppanel;
            btn.Height = 23;
            btn.Width = 200;
            btn.Top = ((4 + 23) * (totalRESPs - 1)) - resppanel.VerticalScroll.Value;
            btn.Left = 4;
            btns3.Add(btn);
            btn.Click += (object sender2, EventArgs e2) => {
                totalRESPs = 0;
                foreach (Button thebtn in btns3)
                {
                    totalRESPs++;
                    Controls.Remove(thebtn);
                    thebtn.Visible = false;
                    thebtn.Top = ((4 + 23) * (totalRESPs - 1)) - resppanel.VerticalScroll.Value;
                }
                totalRESPs = 0;
                foreach (Button thebtn in btns4)
                {
                    totalRESPs++;
                    Controls.Remove(thebtn);
                    thebtn.Visible = false;
                    thebtn.Top = ((4 + 23) * (totalRESPs - 1)) - resppanel.VerticalScroll.Value;
                }
                respbox.Visible = true;
                respbox.Text = btn.Text;
                respexitbtn.Visible = true;
                FieldInfo f1 = typeof(Control).GetField("EventClick", BindingFlags.Static | BindingFlags.NonPublic);
                object obj = f1.GetValue(respexitbtn);
                PropertyInfo pi = respexitbtn.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);
                EventHandlerList list = (EventHandlerList)pi.GetValue(respexitbtn, null);
                list.RemoveHandler(obj, list[obj]);
                respexitbtn.Click += (object sender3, EventArgs e3) => {
                    btn.Text = respbox.Text;
                    respbox.Text = "";
                    respbox.Visible = false;
                    respexitbtn.Visible = false;
                    refresp_Click(sender3, e3);
                };
            };
            Button btn2 = new Button();
            btn2.Visible = true;
            btn2.Text = "X";
            btn2.Parent = resppanel;
            btn2.Height = 23;
            btn2.Width = 23;
            btn2.Top = btn.Top;
            btn2.Left = 4 + btn.Width;
            btns4.Add(btn2);
            btn2.Click += (object sender2, EventArgs e2) => {
                Button thebtn1 = btn;
                Button thebtn2 = btn2;
                thebtn1.Visible = false;
                thebtn2.Visible = false;
                btns3.Remove(thebtn1);
                Controls.Remove(thebtn1);
                btns4.Remove(thebtn2);
                Controls.Remove(thebtn2);
                refresp_Click(sender2, e2);
            };
        }

        private void refresp_Click(object sender, EventArgs e)
        {
            totalRESPs = 0;
            foreach (Button btn in btns3)
            {
                totalRESPs++;
                Controls.Remove(btn);
                btn.Visible = true;
                btn.Top = ((4 + 23) * (totalRESPs - 1)) - resppanel.VerticalScroll.Value;
            }
            totalRESPs = 0;
            foreach (Button btn in btns4)
            {
                totalRESPs++;
                Controls.Remove(btn);
                btn.Visible = true;
                btn.Top = ((4 + 23) * (totalRESPs - 1)) - resppanel.VerticalScroll.Value;
            }
        }

        private void savedialog_Click(object sender, EventArgs e)
        {
            List<string> contents = new List<string>();
            List<string> contents2 = new List<string>();
            contents.Add("Type Dialogue");
            contents.Add("ID " + IDchat);

            contents.Add("\nMessages " + btnsmsg.Count);
            for (int i = 0; i < btnsmsg.Count; i++) {
                contents.Add("Message_" + i + "_Pages " + btnsmsg[i].findTotalPages());
                for (int y = 0; y < btnsmsg[i].findTotalPages(); y++)
                {
                    contents2.Add("Message_" + i + "_Page_" + y + " " + btnsmsg[i].messages[y].Text);
                }
            }

            File.WriteAllLines(Directory.GetCurrentDirectory() + "/chat." + IDchat + ".English.dat", contents2.ToArray());
            File.WriteAllLines(Directory.GetCurrentDirectory() + "/chat." + IDchat + ".Asset.dat", contents.ToArray());
        }
    }
}
