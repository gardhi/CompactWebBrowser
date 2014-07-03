
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace Compact13WebBrowser
{
    public partial class Compact13ToolsIEWebBrowser : Form
    {
        public string HomePage = "http://google.com";
        public string strAppDir { get; set; }

        public Compact13ToolsIEWebBrowser()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            strAppDir = Path.GetDirectoryName(
            Assembly.GetExecutingAssembly().GetName().CodeBase);
            HomePage = Path.Combine(strAppDir, "default.html");

            statusBar1.Text = "";


            PlaceCenter();
            this.WindowState = FormWindowState.Maximized;
        }

        private void PlaceCenter()
        {
            int Left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
            int Top = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
            if (Left < 0)
                Left = 0;
            if (Top < 20)
                Top = 20;
            this.Left = Left;
            this.Top = Top;
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            Navigate("http://wwww."+textBox1.Text);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == '\r')
                Navigate("http://wwww."+textBox1.Text);
        }

        private void Navigate(string url)
        {
            textBox1.Text = url;
            try
            {
                if (webBrowser1.IsOffline) { MessageBox.Show("webrowser is offline"); }

                    
                webBrowser1.Navigate(new System.Uri(url)); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
               // MessageBox.Show("Error: Could not open web page " + url + " because of sucky SW");
            }
        }

        private void menuItem9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuItem7_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = @"\";
            openFileDialog1.Filter = "Web Documents  (*.html);*.htm)|*.html;*.htm|Web Archive Single File (*.mht)|*.mht|Any file (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            DialogResult dr = openFileDialog1.ShowDialog();
            string filename = "";

            if (dr == DialogResult.OK)
            {
                try
                {
                    filename = openFileDialog1.FileName;
                    Navigate(filename);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not open web page " + filename + " Original error: " + ex.Message);
                }
            }
        }

        private void menuItem8_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Menu SaveAs:\n\nSorry. Not yet implemented\n\nNo kb alternative.", "Compact 2013 Web Browser:");
            //return;


            /////////////////////
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = @"\";
            saveFileDialog1.Filter = "Web Documents 1  (*.html)|.html|Web Documents 2  (*.htm)|.htm|Web Archive Single File (*.mht)|.mht|Text File (*.txt)|.txt|Any file (*.*)|.*";
            saveFileDialog1.FilterIndex = 1;
            if (webBrowser1.Url != null)
            {
                if (webBrowser1.Url.IsFile)
                {
                    string path = webBrowser1.Url.ToString();
                    path = path.Replace("file:///", "");
                    path = path.Replace("/", "\\");
                    path = "\\" + path;
                    saveFileDialog1.FileName = path;
                    FileInfo inf = new FileInfo(path);
                    string directory = inf.Directory.ToString();
                    saveFileDialog1.InitialDirectory = directory;

                    switch (inf.Extension)
                    {
                        case ".html":
                            saveFileDialog1.FilterIndex = 1;
                            break;
                        case ".htm":
                            saveFileDialog1.FilterIndex = 2;
                            break;
                        case ".mhtm":
                            saveFileDialog1.FilterIndex = 3;
                            break;
                        case ".txt":
                            saveFileDialog1.FilterIndex = 4;
                            break;
                        default:
                            saveFileDialog1.FilterIndex = 5;
                            break;
                    }
                }
            }

            


            DialogResult dr = saveFileDialog1.ShowDialog();
            string filename = "";

            if (dr == DialogResult.OK)
            {
                try
                {
                    filename = saveFileDialog1.FileName;
                    System.IO.Stream strm = System.IO.File.Create(filename);
                    using (System.IO.StreamWriter txr = new System.IO.StreamWriter(strm))
                    {
                        //string txt = webBrowser1.
                        //txr.Write(webBrowser1.DocumentText);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not save web page " + filename + " Original error: " + ex.Message);
                }
            }
        }

        
        private void menuItem14_Click(object sender, EventArgs e)
        {
            //webBrowser1.Navigate(System.Uri(HomePage));
        }

        private void menuItem10_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void menuItem11_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void menuItem12_Click(object sender, EventArgs e)
        {
            webBrowser1.Stop();
        }

        private void menuItem13_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }



        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            textBox1.Text = webBrowser1.Url.ToString();
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            statusBar1.Text = "Loading";
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            statusBar1.Text = "";
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            int val = hScrollBar1.Value;
            hScrollBar1.Width = System.Windows.Forms.SystemInformation.BorderSize.Width;
            this.Controls.Add(hScrollBar1);
        }

        private void menuItem16_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Menu Zoom In:\n\nSorry. Not yet implemented.\n\nUse Cntrl + to Zoom In\nUse Cntrl - to Zoom Out.\nUse Cntrl 0 to Zoom to 100%", "Compact 2013 Web Browser:");
        }

        private void menuItem17_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Menu Zoom Out\n\nSorry. Not yet implemented.\n\nUse Cntrl - to Zoom Out\nUse Cntrl + to Zoom In.\nUse Cntrl 0 to Zoom to 100%", "Compact 2013 Web Browser:");
        }

        private void menuItem18_Click(object sender, EventArgs e)
        {
            AboutBox.ShowAsDialog();
            this.BringToFront();
        }

        private void menuItem19_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Menu Zoom 100%\n\nSorry. Not yet implemented.\n\nUse Cntrl 0 to Zoom to 100%\nUse Cntrl - to Zoom Out\nUse Cntrl + to Zoom In.", "Compact 2013 Web Browser:");
        }

        private void menuItem20_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = @"\";
            openFileDialog1.Filter = "Text File (*.txt)|*.txt|Any file (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            DialogResult dr = openFileDialog1.ShowDialog();
            string filename = "";

            if (dr == DialogResult.OK)
            {
                try
                {
                    filename = openFileDialog1.FileName;
                    Navigate(filename);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not open text file " + filename + " Original error: " + ex.Message);
                }
            }
       
        }

        private void menuItem21_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = @"\";
            openFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            DialogResult dr = openFileDialog1.ShowDialog();
            string filename = "";

            if (dr == DialogResult.OK)
            {
                try
                {
                    filename = openFileDialog1.FileName;
                    Navigate(filename);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not open iamge file " + filename + " Original error: " + ex.Message);
                }
            }
        }


        private void menuItem22_Click(object sender, EventArgs e)
        {
            Interop.SipShowIM(0);
        }

        private void webBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void menuItem2_Click(object sender, EventArgs e)
        {

        }
    }



}

