using EuroImport.Exceptions;
using EuroImport.Properties;
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

namespace EuroImport
{
    public partial class FrmMain : Form
    {
        protected ExportCSV export;
        public FrmMain()
        {
            InitializeComponent();
            txtFilePath.Text = Settings.Default.FileFolderDefault;
            txtImagesFolder.Text = Settings.Default.ImagesFolderDefault;
            export = new EuroImport.ExportCSV();
        }

        private void btnExcelBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Settings.Default.FileFolderDefault;
            openFileDialog1.Filter = "excel (*.xlsx)|*.xlsx| excel 2003-2007 (*.xls)|*.xls";
            openFileDialog1.Title = "excel";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog1.FileName;
                Settings.Default.FileFolderDefault = txtFilePath.Text;
                Settings.Default.Save();
            }
        }

        private void btnImageFolderBrowse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = Settings.Default.ImagesFolderDefault;
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if(result == DialogResult.OK)
            {
                txtImagesFolder.Text = folderBrowserDialog1.SelectedPath;
                Settings.Default.ImagesFolderDefault = folderBrowserDialog1.SelectedPath;
                Settings.Default.Save();
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                ReadExcel read = new EuroImport.ReadExcel();
                IEnumerable<Inventory> inventories = read.ReadExcelToTable(txtFilePath.Text);
                ModelImages modelImagesControl = new EuroImport.ModelImages();
                List<string> missingImages = new List<string>();
                string csv = this.export.CreateCSV(inventories, modelImagesControl.GetModels(txtImagesFolder.Text), ref missingImages);
                if(missingImages.Count > 0)
                {
                    DialogResult result = MessageBox.Show(string.Format("התמונות הבאות חסרות, האם להמשיך ללא התמונות הללו?\r\nתמונות:\r\n{0}", 
                        string.Join("\r\n", missingImages.GroupBy(c=>c).Select(c=>c.First()).Take(20))), "שגיאה", 
                        MessageBoxButtons.YesNo, 
                        MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign);
                    if (result != DialogResult.Yes)
                        return;
                }
                Save(csv);
            }
            catch(ValueMissingException valueEX)
            {
                BuildMessageBox(GenerateExceptionMessage("ערך חסר", valueEX));
            }
            catch (ValueNumericException valueEX)
            {
                BuildMessageBox(GenerateExceptionMessage("ערך אינו מספר", valueEX));
            }
            catch (SlugsException valueEX)
            {
                BuildMessageBox(GenerateExceptionMessage("ערך לא חוקי", valueEX));
            }
            catch (WrongDelimeterException valueEX)
            {
                BuildMessageBox(GenerateExceptionMessage("תו מפריד חייב להיות " + Settings.Default.Delimeter, valueEX));
            }
            catch (ColumnMissingException columnEX)
            {
                BuildMessageBox(string.Format("חסרה עמודה '{0}' בקובץ אקסל", columnEX.Message));
            }
            catch (Exception ex)
            {
                BuildMessageBox("תקלת מערכת\r\n" + ex.ToString());
            }
        }

        private void Save(string csv)
        {
            saveFileDialog1.Filter = "CSV text file (*.csv)|*.csv";
            saveFileDialog1.FileName = DateTime.Now.Date.ToString("dd.MM.yyyy") + ".csv";
            saveFileDialog1.InitialDirectory = Settings.Default.SaveCSVFolderDefault;
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, csv, Encoding.UTF8);
                Settings.Default.SaveCSVFolderDefault = saveFileDialog1.FileName.Substring(0, saveFileDialog1.FileName.LastIndexOf('\\'));
                Settings.Default.Save();
            }
        }
        protected void BuildMessageBox(string message)
        {
            MessageBox.Show(message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
        }
        protected string GenerateExceptionMessage(string title, ValueException valueException)
        {
            return string.Format("{0} - '{1}' \r\nשגיאה בשורה: {2} עמודה: '{3}' מקט: {4}"
                , title,valueException.Value, valueException.Line, valueException.ColumnName, valueException.SKU);
        }
    }
}
