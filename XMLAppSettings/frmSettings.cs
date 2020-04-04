//    Copyright(C) 2020 Christopher Ryan Mackay

//    This program Is free software: you can redistribute it And/Or modify
//    it under the terms Of the GNU General Public License As published by
//    the Free Software Foundation, either version 3 Of the License, Or
//    (at your option) any later version.

//    This program Is distributed In the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty Of
//    MERCHANTABILITY Or FITNESS FOR A PARTICULAR PURPOSE. See the
//    GNU General Public License For more details.

//    You should have received a copy Of the GNU General Public License
//    along with this program. If Not, see <http: //www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

namespace XMLAppSettings
{
    public partial class frmSettings : Form
    {
        private static string AppSettingsFile = "Settings.xml";

        public frmSettings()
        {
            InitializeComponent();
        }

        public static void SetSettingsValue(string _Field, string _Value)
        {
            string file = string.Empty;
            file = AppSettingsFile;

            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            if (doc.SelectSingleNode(_Field) == null)
            {
                _Field = _Field.Replace("//Settings/", "");
                XmlNode field = doc.CreateElement(_Field);
                field.InnerText = _Value;
                doc.DocumentElement.AppendChild(field);
                doc.Save(file);
            }
            else
            {
                XmlNode node = null;
                node = doc.SelectSingleNode(_Field);
                node.InnerText = _Value;
                doc.Save(file);
            }

        }

        public static void SetToolStripForeColor(ToolStrip _ToolStrip, Color _Color)
        {
            ToolStripItemCollection col = _ToolStrip.Items;

            foreach (ToolStripItem item in col)
            {
                item.ForeColor = _Color;
            }
        }

        public static string ToHexValue(Color _Color)
        {
            return "#" + _Color.R.ToString("X2") +
                         _Color.G.ToString("X2") +
                         _Color.B.ToString("X2");
        }

        public static void SetTextColorBasedOnBrightness(Color _TestColor, Label _Label)
        {
            if (_TestColor.GetBrightness() > 0.5)
            {
                _Label.ForeColor = Color.Black;
            }
            else
            {
                _Label.ForeColor = Color.White;
            }
        }

        private void Color_TextChanged(object sender, EventArgs e)
        {
            string color = string.Empty;
            TextBox txt = sender as TextBox;
            color = txt.Text;

            string txtName = string.Empty;
            txtName = txt.Name;

            try
            {
                switch (txtName)
                {
                    case "AppBackgroundColor":
                        lblAppBackgroundColor.BackColor = ColorTranslator.FromHtml(color);
                       SetTextColorBasedOnBrightness(ColorTranslator.FromHtml(color), lblAppBackgroundColor);
                        break;
                    case "AppTextColor":
                        lblAppTextColor.BackColor = ColorTranslator.FromHtml(color);
                       SetTextColorBasedOnBrightness(ColorTranslator.FromHtml(color), lblAppTextColor);
                        break;
                    case "AppBorderColor":
                        lblAppBorderColor.BackColor = ColorTranslator.FromHtml(color);
                       SetTextColorBasedOnBrightness(ColorTranslator.FromHtml(color), lblAppBorderColor);
                        break;
                    case "AppToolBarColor":
                        lblAppToolBarColor.BackColor = ColorTranslator.FromHtml(color);
                       SetTextColorBasedOnBrightness(ColorTranslator.FromHtml(color), lblAppToolBarColor);
                        break;
                    case "AppGridViewCellBorderColor":
                        lblAppGridViewCellBorderColor.BackColor = ColorTranslator.FromHtml(color);
                       SetTextColorBasedOnBrightness(ColorTranslator.FromHtml(color), lblAppGridViewCellBorderColor);
                        break;
                    case "AppGridViewAlternatingRowColor":
                        lblAppGridViewAlternatingRowColor.BackColor = ColorTranslator.FromHtml(color);
                       SetTextColorBasedOnBrightness(ColorTranslator.FromHtml(color), lblAppGridViewAlternatingRowColor);
                        break;
                    case "AppGridViewRowSelectionColor":
                        lblAppGridViewRowSelectionColor.BackColor = ColorTranslator.FromHtml(color);
                       SetTextColorBasedOnBrightness(ColorTranslator.FromHtml(color), lblAppGridViewRowSelectionColor);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception)
            {
                return;
            }

            // Set test controls
            lblAppTest.BackColor = lblAppBackgroundColor.BackColor;
            lblAppTest.ForeColor = lblAppTextColor.BackColor;
            lblAppTestBorder.BackColor = lblAppBorderColor.BackColor;
            toolStrip1.BackColor = lblAppToolBarColor.BackColor;
            SetToolStripForeColor(toolStrip1, lblAppTextColor.BackColor);

            FormatDataGridView(dgvColors);
            FormatDataGridView(dgvGeneral);
            tpColors.Refresh();
            tpGeneral.Refresh();

            SetThemeIfMatch();
        }

        private void Label_Click(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            string lblName = lbl.Name;

            ColorDialog clrDlg = new ColorDialog();
            clrDlg.FullOpen = true;

            Color currentColor;

            currentColor = lbl.BackColor;
            clrDlg.Color = currentColor;

            if (clrDlg.ShowDialog() == DialogResult.OK)
            {
                Color color = clrDlg.Color;
                try
                {
                    switch (lblName)
                    {
                        case "lblAppBackgroundColor":
                            lblAppBackgroundColor.BackColor = color;
                            AppBackgroundColor.Text =ToHexValue(color);
                           SetTextColorBasedOnBrightness(color, lblAppBackgroundColor);
                            break;
                        case "lblAppTextColor":
                            lblAppTextColor.BackColor = color;
                            AppTextColor.Text =ToHexValue(color);
                           SetTextColorBasedOnBrightness(color, lblAppTextColor);
                            break;
                        case "lblAppBorderColor":
                            lblAppBorderColor.BackColor = color;
                            AppBorderColor.Text =ToHexValue(color);
                           SetTextColorBasedOnBrightness(color, lblAppBorderColor);
                            break;
                        case "lblAppToolBarColor":
                            lblAppToolBarColor.BackColor = color;
                            AppToolBarColor.Text =ToHexValue(color);
                           SetTextColorBasedOnBrightness(color, lblAppToolBarColor);
                            break;
                        case "lblAppGridViewCellBorderColor":
                            lblAppGridViewCellBorderColor.BackColor = color;
                            AppGridViewCellBorderColor.Text =ToHexValue(color);
                           SetTextColorBasedOnBrightness(color, lblAppGridViewCellBorderColor);
                            break;
                        case "lblAppGridViewAlternatingRowColor":
                            lblAppGridViewAlternatingRowColor.BackColor = color;
                            AppGridViewAlternatingRowColor.Text =ToHexValue(color);
                           SetTextColorBasedOnBrightness(color, lblAppGridViewAlternatingRowColor);
                            break;
                        case "lblAppGridViewRowSelectionColor":
                            lblAppGridViewRowSelectionColor.BackColor = color;
                            AppGridViewRowSelectionColor.Text =ToHexValue(color);
                           SetTextColorBasedOnBrightness(color, lblAppGridViewRowSelectionColor);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception)
                {
                    return;
                }

                // Set test controls
                lblAppTest.BackColor = lblAppBackgroundColor.BackColor;
                lblAppTest.ForeColor = lblAppTextColor.BackColor;
                lblAppTestBorder.BackColor = lblAppBorderColor.BackColor;
                toolStrip1.BackColor = lblAppToolBarColor.BackColor;
                SetToolStripForeColor(toolStrip1, lblAppTextColor.BackColor);

                FormatDataGridView(dgvColors);
                FormatDataGridView(dgvGeneral);
                tpColors.Refresh();
                tpGeneral.Refresh();

                SetThemeIfMatch();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // General
            SetSettingsValue(ApplicationSettings.ShowGridLines, cbxShowGridLines.Checked.ToString());
            SetSettingsValue(ApplicationSettings.ShowCellBorders, rbShowCellBorders.Checked.ToString());
            SetSettingsValue(ApplicationSettings.ShowRowGridLines, rbShowRowGridLines.Checked.ToString());
            SetSettingsValue(ApplicationSettings.ShowColumnGridLines, rbShowColumnGridLines.Checked.ToString());
            SetSettingsValue(ApplicationSettings.SearchAsYouType, cbxSearch.Checked.ToString());

            if (cbThemes.Text != "<Custom>")
            {
                SetSettingsValue(ApplicationSettings.AppColorTheme, cbThemes.SelectedItem.ToString());
            }
            else
            {
                SetSettingsValue(ApplicationSettings.AppColorTheme, "<Custom>");
            }

            // Colors
            foreach (Control control in tpColors.Controls)
            {
                if (control is TextBox)
                {
                    string setting = string.Empty;
                    setting = control.Name;

                    string value = string.Empty;
                    value = control.Text;
                    SetSettingsValue("//Settings/" + setting, value);
                }
            }

            MessageBox.Show("Settings saved.");

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateAppSettings_SetDefaults();

            MySystemRenderer renderer = new MySystemRenderer();
            toolStrip1.Renderer = renderer;

            // General
            cbxShowGridLines.Checked = bool.Parse(GetSettingsValue(ApplicationSettings.ShowGridLines));
            cbxSearch.Checked = bool.Parse(GetSettingsValue(ApplicationSettings.SearchAsYouType));

            if (cbxShowGridLines.Checked)
            {
                rbShowCellBorders.Checked = bool.Parse(GetSettingsValue(ApplicationSettings.ShowCellBorders));
                rbShowRowGridLines.Checked = bool.Parse(GetSettingsValue(ApplicationSettings.ShowRowGridLines));
                rbShowColumnGridLines.Checked = bool.Parse(GetSettingsValue(ApplicationSettings.ShowColumnGridLines));

                rbShowCellBorders.Enabled = true;
                rbShowRowGridLines.Enabled = true;
                rbShowColumnGridLines.Enabled = true;
            }
            else
            {
                rbShowCellBorders.Checked = false;
                rbShowRowGridLines.Checked = false;
                rbShowColumnGridLines.Checked = false;

                rbShowCellBorders.Enabled = false;
                rbShowRowGridLines.Enabled = false;
                rbShowColumnGridLines.Enabled = false;
            }

            // Colors
            foreach (Control control in tpColors.Controls)
                if (control is TextBox)
                {
                    string setting = string.Empty;
                    setting = control.Name;

                    control.Text =GetSettingsValue("//Settings/" + control.Name);
                }

            if (GetSettingsValue(ApplicationSettings.AppColorTheme) == "<Custom>")
            {
                cbThemes.Text = "<Custom>";
            }
            else
            {
                cbThemes.SelectedIndex = cbThemes.FindStringExact(GetSettingsValue(ApplicationSettings.AppColorTheme));
            }

            // Application test label
            lblAppTest.BackColor = lblAppBackgroundColor.BackColor;
            lblAppTest.ForeColor = lblAppTextColor.BackColor;
            lblAppTestBorder.BackColor = lblAppBorderColor.BackColor;
            toolStrip1.BackColor = lblAppToolBarColor.BackColor;
            SetToolStripForeColor(toolStrip1, lblAppTextColor.BackColor);

            // DataGridView test
            dgvColors.Rows.Add("Value", "Value", "Value");
            dgvColors.Rows.Add("Value", "Value", "Value");
            dgvColors.Rows.Add("Value", "Value", "Value");
            FormatDataGridView(dgvColors);

            dgvGeneral.Rows.Add("Value", "Value", "Value");
            dgvGeneral.Rows.Add("Value", "Value", "Value");
            dgvGeneral.Rows.Add("Value", "Value", "Value");
            FormatDataGridView(dgvGeneral);

            tpColors.Refresh();
            tpGeneral.Refresh();

            SetThemeIfMatch();
        }

        private void SetDefaultAppColors()
        {
            AppBackgroundColor.Text = "#ECEEF0";
            AppTextColor.Text = "#15428B";
            AppBorderColor.Text = "#729DCE";
            AppToolBarColor.Text = "#D3E2F4";

            lblAppTest.BackColor = ColorTranslator.FromHtml(AppBackgroundColor.Text);
            lblAppTest.ForeColor = ColorTranslator.FromHtml(AppTextColor.Text);
            lblAppTestBorder.BackColor = ColorTranslator.FromHtml(AppBorderColor.Text);

            SetThemeIfMatch();
        }

        private void SetDefaultDataGridViewColors()
        {
            AppGridViewCellBorderColor.Text = "#DCDCDC";
            AppGridViewAlternatingRowColor.Text = "#EEEEFF";
            AppGridViewRowSelectionColor.Text = "#0078D7";

            FormatDataGridView(dgvColors);
            FormatDataGridView(dgvGeneral);

            SetThemeIfMatch();
        }

        private void btnAppDefault_Click(object sender, EventArgs e)
        {
            SetDefaultAppColors();
        }

        private void btnGridViewDefault_Click(object sender, EventArgs e)
        {
            SetDefaultDataGridViewColors();
        }

        private void cbxShowGridLines_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxShowGridLines.Checked)
            {
                rbShowCellBorders.Checked = true;
                rbShowRowGridLines.Checked = false;
                rbShowColumnGridLines.Checked = false;

                rbShowCellBorders.Enabled = true;
                rbShowRowGridLines.Enabled = true;
                rbShowColumnGridLines.Enabled = true;
            }
            else
            {
                rbShowCellBorders.Checked = false;
                rbShowRowGridLines.Checked = false;
                rbShowColumnGridLines.Checked = false;

                rbShowCellBorders.Enabled = false;
                rbShowRowGridLines.Enabled = false;
                rbShowColumnGridLines.Enabled = false;
            }

            FormatDataGridView(dgvColors);
            FormatDataGridView(dgvGeneral);
        }

        private void FormatDataGridView(DataGridView _DataGridView)
        {
            try
            {
                _DataGridView.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml(AppGridViewRowSelectionColor.Text);
                _DataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
                _DataGridView.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml(AppGridViewAlternatingRowColor.Text);
                _DataGridView.GridColor = ColorTranslator.FromHtml(AppGridViewCellBorderColor.Text);

                if (!cbxShowGridLines.Checked)
                {
                    _DataGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;
                }
                else if (rbShowCellBorders.Checked)
                {
                    _DataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                }
                else if (rbShowRowGridLines.Checked)
                {
                    _DataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                }
                else
                {
                    _DataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
                }

                _DataGridView.ClearSelection();
            }
            catch (Exception)
            {
                return;
            }
        }

        private void rbGrids_CheckedChanged(object sender, EventArgs e)
        {
            FormatDataGridView(dgvColors);
            FormatDataGridView(dgvGeneral);
        }

        private void tpColors_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect1 = new Rectangle(dgvColors.Location.X, dgvColors.Location.Y, dgvColors.ClientSize.Width, dgvColors.ClientSize.Height);
            rect1.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, rect1, lblAppBorderColor.BackColor, ButtonBorderStyle.Solid);
        }

        private void tpGeneral_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect1 = new Rectangle(dgvGeneral.Location.X, dgvGeneral.Location.Y, dgvGeneral.ClientSize.Width, dgvGeneral.ClientSize.Height);
            rect1.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, rect1, lblAppBorderColor.BackColor, ButtonBorderStyle.Solid);
        }

        private void SetTheme(string _SelectedTheme)
        {
           DrawingControl.SetDoubleBuffered(tpColors);
           DrawingControl.SuspendDrawing(tpColors);

            switch (_SelectedTheme)
            {
                case "Arctic":
                    AppBackgroundColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#FFFFFF"));
                    AppTextColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#000000"));
                    AppBorderColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#000000"));
                    AppToolBarColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#F3F3F3"));
                    AppGridViewCellBorderColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#C0C0C0"));
                    AppGridViewAlternatingRowColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#F3F3F3"));
                    AppGridViewRowSelectionColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#79BCFF"));
                    break;
                case "Classic OS":
                    AppBackgroundColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#F0F0F0"));
                    AppTextColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#000000"));
                    AppBorderColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#396CA5"));
                    AppToolBarColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#F0F0F0"));
                    AppGridViewCellBorderColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#DCDCDC"));
                    AppGridViewAlternatingRowColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#F0F0F0"));
                    AppGridViewRowSelectionColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#77A2D0"));
                    break;
                case "Soft Purple":
                    AppBackgroundColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#F5F5F5"));
                    AppTextColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#333333"));
                    AppBorderColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#705697"));
                    AppToolBarColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#D7CFE2"));
                    AppGridViewCellBorderColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#DCDCDC"));
                    AppGridViewAlternatingRowColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#EEEEFF"));
                    AppGridViewRowSelectionColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#0078D7"));
                    break;
                case "Retrobright":
                    AppBackgroundColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#FDF6E3"));
                    AppTextColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#237893"));
                    AppBorderColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#8D8464"));
                    AppToolBarColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#EEE8D5"));
                    AppGridViewCellBorderColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#8D8464"));
                    AppGridViewAlternatingRowColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#EEE8D5"));
                    AppGridViewRowSelectionColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#C0BCB0"));
                    break;
                case "Application Default":
                    AppBackgroundColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#ECEEF0"));
                    AppTextColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#15428B"));
                    AppBorderColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#729DCE"));
                    AppToolBarColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#D3E2F4"));
                    AppGridViewCellBorderColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#DCDCDC"));
                    AppGridViewAlternatingRowColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#EEEEFF"));
                    AppGridViewRowSelectionColor.Text = ColorTranslator.ToHtml(ColorTranslator.FromHtml("#0078D7"));
                    break;
                default:
                    break;
            }

            DrawingControl.ResumeDrawing(tpColors);
        }

        private void cbThemes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbThemes.Text != "<Custom>")
                SetTheme(cbThemes.SelectedItem.ToString());
        }

        private void SetThemeIfMatch()
        {
            Dictionary<string, Color> ctDefault = new Dictionary<string, Color>();
            ctDefault.Add("AppBackgroundColor", ColorTranslator.FromHtml("#ECEEF0"));
            ctDefault.Add("AppTextColor", ColorTranslator.FromHtml("#15428B"));
            ctDefault.Add("AppBorderColor", ColorTranslator.FromHtml("#729DCE"));
            ctDefault.Add("AppToolBarColor", ColorTranslator.FromHtml("#D3E2F4"));
            ctDefault.Add("AppGridViewCellBorderColor", ColorTranslator.FromHtml("#DCDCDC"));
            ctDefault.Add("AppGridViewAlternatingRowColor", ColorTranslator.FromHtml("#EEEEFF"));
            ctDefault.Add("AppGridViewRowSelectionColor", ColorTranslator.FromHtml("#0078D7"));

            Dictionary<string, Color> classicOS = new Dictionary<string, Color>();
            classicOS.Add("AppBackgroundColor", ColorTranslator.FromHtml("#F0F0F0"));
            classicOS.Add("AppTextColor", ColorTranslator.FromHtml("#000000"));
            classicOS.Add("AppBorderColor", ColorTranslator.FromHtml("#396CA5"));
            classicOS.Add("AppToolBarColor", ColorTranslator.FromHtml("#F0F0F0"));
            classicOS.Add("AppGridViewCellBorderColor", ColorTranslator.FromHtml("#DCDCDC"));
            classicOS.Add("AppGridViewAlternatingRowColor", ColorTranslator.FromHtml("#F0F0F0"));
            classicOS.Add("AppGridViewRowSelectionColor", ColorTranslator.FromHtml("#77A2D0"));

            Dictionary<string, Color> arctic = new Dictionary<string, Color>();
            arctic.Add("AppBackgroundColor", ColorTranslator.FromHtml("#FFFFFF"));
            arctic.Add("AppTextColor", ColorTranslator.FromHtml("#000000"));
            arctic.Add("AppBorderColor", ColorTranslator.FromHtml("#000000"));
            arctic.Add("AppToolBarColor", ColorTranslator.FromHtml("#F3F3F3"));
            arctic.Add("AppGridViewCellBorderColor", ColorTranslator.FromHtml("#C0C0C0"));
            arctic.Add("AppGridViewAlternatingRowColor", ColorTranslator.FromHtml("#F3F3F3"));
            arctic.Add("AppGridViewRowSelectionColor", ColorTranslator.FromHtml("#79BCFF"));

            Dictionary<string, Color> softPurple = new Dictionary<string, Color>();
            softPurple.Add("AppBackgroundColor", ColorTranslator.FromHtml("#F5F5F5"));
            softPurple.Add("AppTextColor", ColorTranslator.FromHtml("#333333"));
            softPurple.Add("AppBorderColor", ColorTranslator.FromHtml("#705697"));
            softPurple.Add("AppToolBarColor", ColorTranslator.FromHtml("#D7CFE2"));
            softPurple.Add("AppGridViewCellBorderColor", ColorTranslator.FromHtml("#DCDCDC"));
            softPurple.Add("AppGridViewAlternatingRowColor", ColorTranslator.FromHtml("#EEEEFF"));
            softPurple.Add("AppGridViewRowSelectionColor", ColorTranslator.FromHtml("#0078D7"));

            Dictionary<string, Color> retroBright = new Dictionary<string, Color>();
            retroBright.Add("AppBackgroundColor", ColorTranslator.FromHtml("#FDF6E3"));
            retroBright.Add("AppTextColor", ColorTranslator.FromHtml("#237893"));
            retroBright.Add("AppBorderColor", ColorTranslator.FromHtml("#8D8464"));
            retroBright.Add("AppToolBarColor", ColorTranslator.FromHtml("#EEE8D5"));
            retroBright.Add("AppGridViewCellBorderColor", ColorTranslator.FromHtml("#8D8464"));
            retroBright.Add("AppGridViewAlternatingRowColor", ColorTranslator.FromHtml("#EEE8D5"));
            retroBright.Add("AppGridViewRowSelectionColor", ColorTranslator.FromHtml("#C0BCB0"));

            Dictionary<string, Color> currentColors = new Dictionary<string, Color>();

            // Check if all the current color settings match to a default theme
            foreach (Control control in tpColors.Controls)
                if (control is TextBox)
                {
                    string setting = string.Empty;
                    setting = control.Name;
                    currentColors.Add(control.Name, ColorTranslator.FromHtml(control.Text));
                }

            bool equalsArctic = currentColors.OrderBy(kvp => kvp.Key).SequenceEqual(arctic.OrderBy(kvp => kvp.Key));
            bool equalsDefault = currentColors.OrderBy(kvp => kvp.Key).SequenceEqual(ctDefault.OrderBy(kvp => kvp.Key));
            bool equalsClassicOS = currentColors.OrderBy(kvp => kvp.Key).SequenceEqual(classicOS.OrderBy(kvp => kvp.Key));
            bool equalsSoftPurple = currentColors.OrderBy(kvp => kvp.Key).SequenceEqual(softPurple.OrderBy(kvp => kvp.Key));
            bool equalsRetroBright = currentColors.OrderBy(kvp => kvp.Key).SequenceEqual(retroBright.OrderBy(kvp => kvp.Key));

            string themeMatch = string.Empty;

            if (equalsArctic)
                themeMatch = "Arctic";
            else if (equalsDefault)
                themeMatch = "Application Default";
            else if (equalsClassicOS)
                themeMatch = "Classic OS";
            else if (equalsSoftPurple)
                themeMatch = "Soft Purple";
            else if (equalsRetroBright)
                themeMatch = "Retrobright";
            else
                themeMatch = string.Empty;
            
            
            if (themeMatch != string.Empty)
                cbThemes.SelectedIndex = cbThemes.FindStringExact(themeMatch);
            else
                cbThemes.Text = "<Custom>";
        }

        public static void CreateAppSettings_SetDefaults()
        {
            StringCollection appSettings = new StringCollection();

            string settingsFile = string.Empty;
            settingsFile = AppSettingsFile;

            // General
            appSettings.Add("DefaultBoxSize,9x9");
            appSettings.Add("SearchAsYouType,True");

            // Grid settings
            appSettings.Add("ShowGridLines,True");
            appSettings.Add("ShowCellBorders,True");
            appSettings.Add("ShowRowGridLines,False");
            appSettings.Add("ShowColumnGridLines,False");
            appSettings.Add("ShowAlternatingRowColor,True");

            // Application colors
            appSettings.Add("AppBackgroundColor,#ECEEF0");
            appSettings.Add("AppTextColor,#15428B");
            appSettings.Add("AppBorderColor,#729DCE");
            appSettings.Add("AppToolBarColor,#D3E2F4");
            appSettings.Add("AppGridViewCellBorderColor,#DCDCDC");
            appSettings.Add("AppGridViewAlternatingRowColor,#EEEEFF");
            appSettings.Add("AppGridViewRowSelectionColor,#0078D7");
            appSettings.Add("AppColorTheme,Application Default");

            if (!SettingsFileExists())
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                XmlWriter XmlWrt = XmlWriter.Create(settingsFile, settings);

                {
                    var withBlock = XmlWrt;
                    withBlock.WriteStartDocument();

                    withBlock.WriteComment("Application Settings");
                    withBlock.WriteStartElement("Settings");

                    string[] arr;

                    foreach (string setting in appSettings)
                    {
                        arr = setting.Split(',');

                        string settingName = arr[0];
                        string defaultValue = arr[1];

                        withBlock.WriteStartElement(settingName);
                        withBlock.WriteString(defaultValue);
                        withBlock.WriteEndElement();
                    }

                    withBlock.WriteEndDocument();
                    withBlock.Close();
                }

                XmlWrt = null;
            }
            else
            {
                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.Load(settingsFile);
                XmlElement elm = xmlDoc.DocumentElement;
                XmlNodeList lstSettings = elm.ChildNodes;
                string[] arr;
                StringCollection nodeNames = new StringCollection();

                foreach (XmlNode node in lstSettings)
                {
                    nodeNames.Add(node.Name);
                }

                foreach (string setting in appSettings)
                {
                    arr = setting.Split(',');

                    string settingName = arr[0];
                    string defaultValue = arr[1];

                    if (!nodeNames.Contains(settingName))
                    {
                        XmlNode newSetting = xmlDoc.CreateElement(settingName);
                        newSetting.InnerText = defaultValue;
                        xmlDoc.DocumentElement.AppendChild(newSetting);
                        xmlDoc.Save(settingsFile);
                    }
                }
            }
        }
        
        public sealed class ApplicationSettings
        {
            // General
            public const string SearchAsYouType = "//Settings/SearchAsYouType";

            // Grid settings
            public const string ShowGridLines = "//Settings/ShowGridLines";
            public const string ShowCellBorders = "//Settings/ShowCellBorders";
            public const string ShowRowGridLines = "//Settings/ShowRowGridLines";
            public const string ShowColumnGridLines = "//Settings/ShowColumnGridLines";
            public const string ShowAlternatingRowColor = "//Settings/ShowAlternatingRowColor";

            // Application colors
            public const string AppBackgroundColor = "//Settings/AppBackgroundColor";
            public const string AppTextColor = "//Settings/AppTextColor";
            public const string AppBorderColor = "//Settings/AppBorderColor";
            public const string AppToolBarColor = "//Settings/AppToolBarColor";
            public const string AppGridViewCellBorderColor = "//Settings/AppGridViewCellBorderColor";
            public const string AppGridViewAlternatingRowColor = "//Settings/AppGridViewAlternatingRowColor";
            public const string AppGridViewRowSelectionColor = "//Settings/AppGridViewRowSelectionColor";
            public const string AppColorTheme = "//Settings/AppColorTheme";
        }

        public static bool SettingsFileExists()
        {
            bool flag = false;

            string settingsFile = string.Empty;
            settingsFile = AppSettingsFile;

            if (File.Exists(settingsFile))
                flag = true;
            else
                flag = false;

            return flag;
        }
        
        public static string GetSettingsValue(string _Field)
        {
            string file = string.Empty;
            file = AppSettingsFile;

            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            XmlNode node = null;
            node = doc.SelectSingleNode(_Field);

            string value = string.Empty;

            if (node == null)
                value = string.Empty;
            else
                value = node.InnerText;

            return value;
        }
        
        public class MySystemRenderer : ToolStripSystemRenderer
        {
            protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
            {
                if (!e.Item.Selected)
                {
                    base.OnRenderButtonBackground(e);
                }
                else
                {
                    Rectangle rc = new Rectangle(0, 0, e.Item.Width, e.Item.Height);
                    SolidBrush fillBrush = new SolidBrush(Color.FromArgb(255, 211, 84));
                    LinearGradientBrush gradBrush = new LinearGradientBrush(rc, Color.FromArgb(255, 251, 214), Color.FromArgb(255, 211, 84), LinearGradientMode.Vertical);
                    e.Graphics.FillRectangle(gradBrush, rc);
                }
            }
        }

        public static class DrawingControl
        {
            [DllImport("user32.dll")]
            public static extern int SendMessage(IntPtr _hWnd, Int32 _wMsg, bool _wParam, Int32 _lParam);

            private const int WM_SETREDRAW = 11;

            public static void SetDoubleBuffered(Control _ctrl)
            {
                if (!SystemInformation.TerminalServerSession)
                {
                    typeof(System.Windows.Forms.Control).InvokeMember("DoubleBuffered", (System.Reflection.BindingFlags.SetProperty
                                    | (System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)), null, _ctrl, new object[] {
                            true});
                }
            }

            public static void SuspendDrawing(Control _ctrl)
            {
                SendMessage(_ctrl.Handle, WM_SETREDRAW, false, 0);
            }

            public static void ResumeDrawing(Control _ctrl)
            {
                SendMessage(_ctrl.Handle, WM_SETREDRAW, true, 0);
                _ctrl.Refresh();
            }
        }
    }
}
