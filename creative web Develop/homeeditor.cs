﻿
using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace creative_web_Develop
{
    public partial class homeeditor : Form
    {
        List<string> listFiles = new List<string>();
        public homeeditor()
        {
            InitializeComponent();
        }
        public void DisplayFolder(string folderPath) //irrelevent
        {
            string[] files = System.IO.Directory.GetFiles(folderPath);

            for (int x = 0; x < files.Length; x++)
            {
                listView1.Items.Add(files[x]);
            }
        }
        private void homeeditor_Load(object sender, EventArgs e)
        {
         
            cxFlatRoundProgressBar1.ValueNumber = Properties.Settings.Default.progress;
            label5.Text = Properties.Settings.Default.level;
            if (Properties.Settings.Default.highdpi == true)
            {
                try
                {
                    Cef.EnableHighDPISupport();
                    
                }
                catch
                {

                }
            }
            else
            {

            }
            CefSettings settings = new CefSettings();
            // Initialize cef with the provided settings
            try
            {
                Cef.Initialize(settings);
            }
            catch
            {

            }
          
            groupBox1.Hide();
            groupBox2.Hide();
            if (System.IO.Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Documents\\" + "\\creative web projects\\"))
            {
                System.Diagnostics.Debug.WriteLine("directory exist " + Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Documents\\" + "\\creative web projects\\");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("directory does not exist " + Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Documents\\" + "\\creative web projects\\");
                DirectoryInfo di = Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Documents\\" + "\\creative web projects\\");


            }
            KeyPreview = true;
        }

        private void groupBox1_MouseHover(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox2.Hide();
            groupBox1.Show();

            listView1.Items.Clear();
            foreach (string item in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Documents\\" + "\\creative web projects"))
            {

                FileInfo fi = new FileInfo(item);
                imageList2.Images.Add(System.Drawing.Icon.ExtractAssociatedIcon(item));
                listFiles.Add(fi.FullName);
                listView1.Items.Add(fi.Name, imageList2.Images.Count - 1);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
      
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new settingsbox().ShowDialog();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox1.Hide();
            groupBox2.Show();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            pleasewait pw = new pleasewait();
            pw.TopMost = true;
            pw.Show();
            int intselectedindex = listView1.SelectedIndices[0];
            string text = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Documents" + "\\creative web projects\\" + listView1.Items[intselectedindex].Text);
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            dynamic dobj = jsonSerializer.Deserialize<dynamic>(text);
            string projecttype = dobj["type"].ToString();
            if (projecttype == "HTML") { 
            TabPage tp = new TabPage();

            Htmlprojecteditor hpe = new Htmlprojecteditor();
            hpe.projectFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Documents" + "\\creative web projects\\" + listView1.Items[intselectedindex].Text + "_files";

            tp.Controls.Add(hpe);
            tp.Text = "project - " + listView1.Items[intselectedindex].Text;
            hpe.Dock = DockStyle.Fill;
            hpe.Visible = true;
            hpe.Show();
            tabControl1.TabPages.Add(tp);
            tabControl1.SelectedTab = tp;
                pw.Close();
        }else if (projecttype == "PHP")
            {
                TabPage tp = new TabPage();

                tab_elements.phpprojecteditor ppe = new tab_elements.phpprojecteditor();
                ppe.projectFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Documents" + "\\creative web projects\\" + listView1.Items[intselectedindex].Text + "_files";

                tp.Controls.Add(ppe);
                tp.Text = "project - " + listView1.Items[intselectedindex].Text;
                ppe.Dock = DockStyle.Fill;
                ppe.Visible = true;
                ppe.Show();
                tabControl1.TabPages.Add(tp);
                tabControl1.SelectedTab = tp;
                pw.Close();
           
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            createpro();
            
        }
        public void createpro()
        {

            try
            {
                if (textBox1.Text != "")
                {
                    this.ForeColor = Color.Black;
                    int intselectedindex = listView2.SelectedIndices[0];
                    string repairedenviromentusertext = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    System.IO.File.WriteAllText(repairedenviromentusertext.Replace("\\", "/") + "/Documents" + "/creative web projects/" + textBox1.Text, "{'folder':'" + repairedenviromentusertext.Replace("\\", "/") + "/Documents/" + "/creative web projects/" + textBox1.Text + "_files" + "','type':'" + listView2.Items[intselectedindex].Text + "'}");
                   
                    System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Documents" + "\\creative web projects\\" + textBox1.Text + "_files");
                    if(listView2.Items[intselectedindex].Text == "HTML")
                    {
                        System.IO.File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Documents" + "\\creative web projects\\" + textBox1.Text + "_files\\" + "index.html", "<!DOCTYPE html>\n <html>\n    <head>\n    </head> \n </html>");
                        TabPage tp = new TabPage();
                        Htmlprojecteditor hpe = new Htmlprojecteditor();
                        hpe.projectFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Documents" + "\\creative web projects\\" + textBox1.Text + "_files";
                        tp.Controls.Add(hpe);
                        tp.Text = "project - " + textBox1.Text;
                        hpe.Dock = DockStyle.Fill;
                        hpe.Visible = true;
                        hpe.textBox1.ForeColor = Color.Black;

                        hpe.Show();
                        tp.ForeColor = Color.Black;
                        tp.UseVisualStyleBackColor = true;
                        tabControl1.TabPages.Add(tp);

                        tabControl1.SelectedTab = tp;
                        cxFlatRoundProgressBar1.ValueNumber += 1;
                    }
                    else if(listView2.Items[intselectedindex].Text == "PHP")
                    {
                        System.IO.File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Documents" + "\\creative web projects\\" + textBox1.Text + "_files\\" + "index.php", "<?php \n //Starter project \n \n echo \"hello world\";");
                        cxFlatRoundProgressBar1.ValueNumber += 1;
                    }
                    else
                    {

                        MessageBox.Show("Not supported by CWD");

                        cxFlatRoundProgressBar1.ValueNumber -= 1;
                    }
                
                }
                else
                {
                    MessageBox.Show("Please give your project a name.");
                }
            }
            catch
            {
                MessageBox.Show("please select the project type");
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new about().ShowDialog();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                createpro();
            }
        }

        private void closeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == "Initaltp")
            {

            }
            else
            {
                tabControl1.SelectedTab.Controls.Clear();
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                foreach (var process in Process.GetProcessesByName("php"))
                {
                    process.Kill();
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("are you sure you want to delete this project", "", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
            {
                
                int intselectedindex = listView1.SelectedIndices[0];
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/Documents/" + "/creative web projects/" + listView1.Items[intselectedindex].Text);
                Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/Documents/" + "/creative web projects/" + listView1.Items[intselectedindex].Text + "_files",true);
                listView1.Items.Clear();
                foreach (string item in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/Documents/" + "/creative web projects/"))
                {

                    FileInfo fi = new FileInfo(item);
                    imageList2.Images.Add(System.Drawing.Icon.ExtractAssociatedIcon(item));
                    listFiles.Add(fi.FullName);
                    listView1.Items.Add(fi.Name, imageList2.Images.Count - 1);
                }
            }
        }

     

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new settingsbox().ShowDialog();
        }

        private void softDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("are you sure you want to soft delete this project. soft delete delets the file directing the IDE to the folder but doesnt delete the folder. So you can find it in your file manager, and move the files to another project or delete it in the file manager.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
            {

                int intselectedindex = listView1.SelectedIndices[0];
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Documents\\" + "\\creative web projects\\" + listView1.Items[intselectedindex].Text);
                listView1.Items.Clear();
                foreach (string item in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Documents\\" + "\\creative web projects\\"))
                {

                    FileInfo fi = new FileInfo(item);
                    imageList2.Images.Add(System.Drawing.Icon.ExtractAssociatedIcon(item));
                    listFiles.Add(fi.FullName);
                    listView1.Items.Add(fi.Name, imageList2.Images.Count - 1);
                }
                MessageBox.Show("soft delete was successfull");
            }
        }

        private void homeeditor_MouseEnter(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.darktheme == true)
            {
                BackColor = Color.Black;
                ForeColor = Color.White;
                Initaltp.BackColor = Color.Black;
                listView1.BackColor = Color.DarkGray;
                listView1.ForeColor = Color.White;
                listView2.BackColor = Color.DarkGray;
                listView2.ForeColor = Color.White;
                menuStrip1.BackColor = Color.Black;
                menuStrip2.BackColor = Color.Black;
                menuStrip2.ForeColor = Color.White;
                menuStrip1.ForeColor = Color.White;
                groupBox1.ForeColor = Color.White;
                groupBox2.ForeColor = Color.White;

            }
            else
            {

            }
        }

        private void Homeeditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close CWD?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
            {
                foreach (var process in Process.GetProcessesByName("php"))
                {
                    process.Kill();
                }
            }
            else { e.Cancel = true; }
        }

        private void CxFlatButton1_Click(object sender, EventArgs e)
        {

        }

        private void ListView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FeedbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://docs.google.com/forms/d/e/1FAIpQLSeXeBS8GOeqLs3sZ6yWNWsKWaH8uFrVunwSZl5HI6u8WnmleQ/viewform");
        }

        private void ProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void CxFlatPictureBox1_Click(object sender, EventArgs e)
        {
            if(panel1.Visible == false)
            {
                panel1.Visible = true;
            }
            else
            {
                panel1.Visible = false;
            }
        }

        private void Homeeditor_KeyDown(object sender, KeyEventArgs e)
        {
            Properties.Settings.Default.level = label5.Text.ToString();
            Properties.Settings.Default.progress = cxFlatRoundProgressBar1.ValueNumber;
            Properties.Settings.Default.Save();
        }

        private void PluginsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + "/plugins/");
        }
    }
}




//not found