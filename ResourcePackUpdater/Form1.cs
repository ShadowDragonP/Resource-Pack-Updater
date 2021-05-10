using Newtonsoft.Json.Linq;
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

namespace ResourcePackUpdater
{
    public partial class Form1 : Form
    {
        string path = "";
        string[] subdirectories;

        bool formatNull = true;
        bool formatChecked;

        public Form1()
        {
            InitializeComponent();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browserDialog = new FolderBrowserDialog();

            DialogResult result = browserDialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                path = browserDialog.SelectedPath;
                folderPath.Text = path;

                subdirectories = Directory.GetDirectories(path);
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            UpdatePacks();
        }

        private void UpdatePacks()
        {
            try
            {
                if (path == "")
                    logText.Text = "Error: Resource pack folder not selected.";
                else if (packFormat.Text == "")
                    logText.Text = "Error: Input is blank.";
                else
                {
                    browseButton.Enabled = false;
                    updateButton.Enabled = false;
                    packFormat.Enabled = false;
                    clearButton.Enabled = false;

                    logText.Text = "";
                    int packNumber = Int32.Parse(packFormat.Text);
                    logText.Text += "Upgrading to pack format " + packNumber + "...\r\n\r\n";

                    foreach (string str in subdirectories)
                    {
                        string filePath = str + "\\pack.mcmeta";
                        string dirPath = Path.GetFileName(str);

                        if (dirPath.Length > 40) dirPath = dirPath.Substring(0, 40);

                        string status = "";

                        if (File.Exists(filePath))
                        {
                            string json = File.ReadAllText(filePath);
                            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                            try
                            {
                                if (!formatNull && formatChecked == formatCheck.Checked && jsonObj["pack"]["pack_format"] == packNumber)
                                    status = "already updated";
                                else
                                {
                                    if (jsonObj["pack"]["pack_format"] == packNumber)
                                        status = "reformatted";
                                    else
                                        status = "completed";

                                    jsonObj["pack"]["pack_format"] = packNumber;

                                    string output = "";
                                    if (formatCheck.Checked)
                                        output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                                    else
                                        output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj);

                                    File.WriteAllText(filePath, output);
                                }
                            }
                            catch (Exception e)
                            {
                                //Console.WriteLine(e.Message);
                                status = "skipped";
                            }
                            
                        }
                        else
                            status = "skipped";

                        logText.Text += String.Format(
                                    "{0,-40}{1,1}\r\n", dirPath, "\t... " + status);

                        logText.Refresh();
                    }
                    formatChecked = formatCheck.Checked;
                    formatNull = false;
                    logText.Text += "\r\nDone.";
                }
            }
            catch (FormatException)
            {
                logText.Text = "Error: Input is not a number.";
            }

            browseButton.Enabled = true;
            updateButton.Enabled = true;
            packFormat.Enabled = true;
            clearButton.Enabled = true;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            logText.Text = "";
        }
    }
}
