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

namespace UnturnedNPCMaker
{
    public partial class Form1 : Form
    {
        int ID = 0;

        public Form1()
        {
            InitializeComponent();
            CharEdit.Visible = false;
            label1credits.Text = "Thanks to u/CaptainStar and u/KurvaZelena\n on reddit for the face, beard, and hair pictures!\n Post can be found here:\n https://www.reddit.com/r/unturned/comments/6e6n1f/npcs_face_hair_and_beard_ids/";
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
            string Color_Skin = colorhair.R.ToString("X2") + colorhair.G.ToString("X2") + colorhair.B.ToString("X2");

            contents.Add("\nFace " + (int)trackBarFace.Value);
            contents.Add("Beard " + (int)trackBarBeard.Value);
            contents.Add("Hair " + (int)trackBarHair.Value);
            contents.Add("Color_Skin " + Color_Skin);
            contents.Add("Color_Hair " + Color_Hair);

            string equippedItem = "";
            if (PrimaryBox.Checked)
                equippedItem = "Primary";
            if (SecondaryBox.Checked)
                equippedItem = "Secondary";
            if (TertiaryBox.Checked)
                equippedItem = "Tertiary";

            string Pose = "";
            if (standbox.Checked)
                equippedItem = "Stand";
            if (sitbox.Checked)
                equippedItem = "Sit";
            if (asleepbox.Checked)
                equippedItem = "Asleep";
            if (passivebox.Checked)
                equippedItem = "Passive";
            if (crouchbox.Checked)
                equippedItem = "Couch";

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

        }
    }
}
