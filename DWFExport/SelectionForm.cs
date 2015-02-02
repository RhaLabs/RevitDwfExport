using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace DWFExport
{
	public class SelectionForm : Form
	{
		private MainData _maindata;
		private ExportDWFData _exportDWFData;
		private IContainer components;
		private TextBox textBoxSaveAs;
		private Label labelSaveAs;
		private Button buttonBrowser;
		private RadioButton radioButtonCurrentView;
		private RadioButton radioButtonSelectView;
		private Button buttonSelectViews;
		private GroupBox groupBoxRange;
		private Button buttonOptions;
		private Button buttonOK;
		private Button buttonCancel;
		private RadioButton radioButtonExport;
		private ComboBox comboBoxExport;
		private GroupBox groupBoxOperation;
		public SelectionForm(MainData maindata)
		{
			this._maindata = maindata;
			this.InitializeComponent();
			this.radioButtonExport.Checked = true;
			this.comboBoxExport.Enabled = true;
			this.InitializeFormats();
			this.InitializeControls();
		}
		public DialogResult Export(string selectedFormat)
		{
			SelectionForm.GetSelectedExportFormat(selectedFormat);
			DialogResult result = DialogResult.OK;
			if (this.ValidateExportFolder())
			{
				this._exportDWFData.CurrentViewOnly = !this.radioButtonSelectView.Checked;
				try
				{
					if (!this._exportDWFData.Export())
					{
						MessageBox.Show("This project cannot be exported to " + this._exportDWFData.ExportFileName + " in current settings.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString(), "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			return result;
		}
		private bool ValidateExportFolder()
		{
			string text = this.textBoxSaveAs.Text;
			if (string.IsNullOrEmpty(text))
			{
				MessageBox.Show("Please specify the folder and file name!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				this.textBoxSaveAs.Focus();
				return false;
			}
			if (!text.Contains("\\"))
			{
				MessageBox.Show("Please specify file name!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				this.textBoxSaveAs.Focus();
				return false;
			}
			string directoryName = Path.GetDirectoryName(text);
			if (!Directory.Exists(directoryName))
			{
				MessageBox.Show("The specified folder does not exist!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				this.textBoxSaveAs.Focus();
				return false;
			}
			this._exportDWFData.ExportFileName = Path.GetFileNameWithoutExtension(text);
			this._exportDWFData.ExportFolder = directoryName;
			return true;
		}
		public static DialogResult ShowSaveDialog(ExportData exportData, ref string returnFileName, ref int filterIndex)
		{
			DialogResult result;
			using (SaveFileDialog saveFileDialog = new SaveFileDialog())
			{
				saveFileDialog.Title = exportData.Title;
				saveFileDialog.InitialDirectory = exportData.ExportFolder;
				saveFileDialog.FileName = exportData.ExportFileName;
				saveFileDialog.Filter = exportData.Filter;
				saveFileDialog.FilterIndex = 1;
				saveFileDialog.RestoreDirectory = true;
				DialogResult dialogResult = saveFileDialog.ShowDialog();
				if (dialogResult != DialogResult.Cancel)
				{
					returnFileName = saveFileDialog.FileName;
					filterIndex = saveFileDialog.FilterIndex;
				}
				result = dialogResult;
			}
			return result;
		}
		private void InitializeFormats()
		{
			this.comboBoxExport.Items.Add("DWF");
			this.comboBoxExport.Items.Add("DWFx");
		}
		private void InitializeControls()
		{
			this.radioButtonCurrentView.Checked = true;
		}
		private void ButtonCancelClick(object sender, EventArgs e)
		{
			base.Close();
		}
		private void ButtonOKClick(object sender, EventArgs e)
		{
			string selectedFormat = this.comboBoxExport.SelectedItem.ToString();
			DialogResult dialogResult = this.Export(selectedFormat);
			if (dialogResult == DialogResult.OK)
			{
				base.Close();
			}
		}
		private static ExportFormat GetSelectedExportFormat(string selectedFormat)
		{
			ExportFormat result = ExportFormat.DWF;
			if (selectedFormat != null)
			{
				if (!(selectedFormat == "DWF"))
				{
					if (selectedFormat == "DWFx")
					{
						result = ExportFormat.DWFx;
					}
				}
				else
				{
					result = ExportFormat.DWF;
				}
			}
			return result;
		}
		private static DialogResult Export(ExportData data)
		{
			string empty = string.Empty;
			int num = -1;
			DialogResult dialogResult = SelectionForm.ShowSaveDialog(data, ref empty, ref num);
			if (dialogResult != DialogResult.Cancel)
			{
				data.ExportFileName = Path.GetFileName(empty);
				data.ExportFolder = Path.GetDirectoryName(empty);
				if (!data.Export())
				{
					MessageBox.Show("This project cannot be exported to " + data.ExportFileName + " in current settings.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			return dialogResult;
		}
		private void ButtonOptionsClick(object sender, EventArgs e)
		{
			using (ExportDWFOptionForm exportDWFOptionForm = new ExportDWFOptionForm(this._exportDWFData))
			{
				exportDWFOptionForm.ShowDialog();
			}
		}
		private void ButtonBrowserClick(object sender, EventArgs e)
		{
			string empty = string.Empty;
			int num = -1;
			DialogResult dialogResult = SelectionForm.ShowSaveDialog(this._exportDWFData, ref empty, ref num);
			if (dialogResult != DialogResult.Cancel)
			{
				this.textBoxSaveAs.Text = empty;
			}
		}
		private void ButtonSelectViewsClick(object sender, EventArgs e)
		{
			using (SelectViewsForm selectViewsForm = new SelectViewsForm(this._exportDWFData.SelectViewsData))
			{
				this._exportDWFData.SelectViewsData.SelectedViews.Clear();
				selectViewsForm.ShowDialog();
				if (this._exportDWFData.SelectViewsData.SelectedViews.Size == 0)
				{
					this.radioButtonCurrentView.Checked = true;
				}
				else
				{
					this.radioButtonCurrentView.Checked = false;
				}
			}
		}
		private void ComboBoxExportSelectedIndexChanged(object sender, EventArgs e)
		{
			string selectedFormat = this.comboBoxExport.SelectedIndex.ToString();
			this._exportDWFData = new ExportDWFData(this._maindata.CommandData, SelectionForm.GetSelectedExportFormat(selectedFormat));
			this.textBoxSaveAs.Text = this._exportDWFData.ExportFolder + "\\" + this._exportDWFData.ExportFileName;
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		private void InitializeComponent()
		{
			this.groupBoxOperation = new GroupBox();
			this.comboBoxExport = new ComboBox();
			this.buttonOptions = new Button();
			this.radioButtonExport = new RadioButton();
			this.buttonCancel = new Button();
			this.buttonOK = new Button();
			this.groupBoxRange = new GroupBox();
			this.buttonSelectViews = new Button();
			this.radioButtonSelectView = new RadioButton();
			this.radioButtonCurrentView = new RadioButton();
			this.buttonBrowser = new Button();
			this.labelSaveAs = new Label();
			this.textBoxSaveAs = new TextBox();
			this.groupBoxOperation.SuspendLayout();
			this.groupBoxRange.SuspendLayout();
			base.SuspendLayout();
			this.groupBoxOperation.Controls.Add(this.comboBoxExport);
			this.groupBoxOperation.Controls.Add(this.buttonOptions);
			this.groupBoxOperation.Controls.Add(this.radioButtonExport);
			this.groupBoxOperation.Location = new Point(12, 12);
			this.groupBoxOperation.Name = "groupBoxOperation";
			this.groupBoxOperation.Size = new Size(378, 81);
			this.groupBoxOperation.TabIndex = 3;
			this.groupBoxOperation.TabStop = false;
			this.groupBoxOperation.Text = "Operation";
			this.comboBoxExport.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxExport.FormattingEnabled = true;
			this.comboBoxExport.Location = new Point(90, 17);
			this.comboBoxExport.Name = "comboBoxExport";
			this.comboBoxExport.Size = new Size(161, 21);
			this.comboBoxExport.TabIndex = 2;
			this.comboBoxExport.SelectedIndexChanged += new EventHandler(this.ComboBoxExportSelectedIndexChanged);
			this.buttonOptions.Location = new Point(9, 44);
			this.buttonOptions.Name = "buttonOptions";
			this.buttonOptions.Size = new Size(75, 21);
			this.buttonOptions.TabIndex = 10;
			this.buttonOptions.Text = "&Options...";
			this.buttonOptions.UseVisualStyleBackColor = true;
			this.buttonOptions.Click += new EventHandler(this.ButtonOptionsClick);
			this.radioButtonExport.AutoSize = true;
			this.radioButtonExport.Checked = true;
			this.radioButtonExport.Location = new Point(9, 21);
			this.radioButtonExport.Name = "radioButtonExport";
			this.radioButtonExport.Size = new Size(58, 17);
			this.radioButtonExport.TabIndex = 0;
			this.radioButtonExport.TabStop = true;
			this.radioButtonExport.Text = "Export ";
			this.radioButtonExport.UseVisualStyleBackColor = true;
			this.buttonCancel.DialogResult = DialogResult.Cancel;
			this.buttonCancel.Location = new Point(211, 227);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new Size(75, 23);
			this.buttonCancel.TabIndex = 5;
			this.buttonCancel.Text = "&Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new EventHandler(this.ButtonCancelClick);
			this.buttonOK.Location = new Point(126, 227);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(75, 23);
			this.buttonOK.TabIndex = 4;
			this.buttonOK.Text = "&OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.ButtonOKClick);
			this.groupBoxRange.Controls.Add(this.buttonSelectViews);
			this.groupBoxRange.Controls.Add(this.radioButtonSelectView);
			this.groupBoxRange.Controls.Add(this.radioButtonCurrentView);
			this.groupBoxRange.Location = new Point(12, 130);
			this.groupBoxRange.Name = "groupBoxRange";
			this.groupBoxRange.Size = new Size(378, 73);
			this.groupBoxRange.TabIndex = 9;
			this.groupBoxRange.TabStop = false;
			this.groupBoxRange.Text = "Range";
			this.buttonSelectViews.Enabled = true;
			this.buttonSelectViews.Location = new Point(145, 42);
			this.buttonSelectViews.Name = "buttonSelectViews";
			this.buttonSelectViews.Size = new Size(27, 21);
			this.buttonSelectViews.TabIndex = 2;
			this.buttonSelectViews.Text = "...";
			this.buttonSelectViews.UseVisualStyleBackColor = true;
			this.buttonSelectViews.Click += new EventHandler(this.ButtonSelectViewsClick);
			this.radioButtonSelectView.AutoSize = true;
			this.radioButtonSelectView.Location = new Point(9, 44);
			this.radioButtonSelectView.Name = "radioButtonSelectView";
			this.radioButtonSelectView.Size = new Size(132, 17);
			this.radioButtonSelectView.TabIndex = 1;
			this.radioButtonSelectView.Text = "Selected views/sheets";
			this.radioButtonSelectView.UseVisualStyleBackColor = true;
			this.radioButtonCurrentView.AutoSize = true;
			this.radioButtonCurrentView.Checked = true;
			this.radioButtonCurrentView.Location = new Point(9, 20);
			this.radioButtonCurrentView.Name = "radioButtonCurrentView";
			this.radioButtonCurrentView.Size = new Size(87, 17);
			this.radioButtonCurrentView.TabIndex = 0;
			this.radioButtonCurrentView.TabStop = true;
			this.radioButtonCurrentView.Text = "Current view";
			this.radioButtonCurrentView.UseVisualStyleBackColor = true;
			this.buttonBrowser.Location = new Point(370, 103);
			this.buttonBrowser.Name = "buttonBrowser";
			this.buttonBrowser.Size = new Size(24, 20);
			this.buttonBrowser.TabIndex = 8;
			this.buttonBrowser.Text = "...";
			this.buttonBrowser.UseVisualStyleBackColor = true;
			this.buttonBrowser.Click += new EventHandler(this.ButtonBrowserClick);
			this.labelSaveAs.AutoSize = true;
			this.labelSaveAs.Location = new Point(12, 106);
			this.labelSaveAs.Name = "labelSaveAs";
			this.labelSaveAs.Size = new Size(50, 13);
			this.labelSaveAs.TabIndex = 6;
			this.labelSaveAs.Text = "Save As:";
			this.textBoxSaveAs.Location = new Point(62, 103);
			this.textBoxSaveAs.Name = "textBoxSaveAs";
			this.textBoxSaveAs.Size = new Size(308, 20);
			this.textBoxSaveAs.TabIndex = 7;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(410, 262);
			base.Controls.Add(this.groupBoxRange);
			base.Controls.Add(this.buttonBrowser);
			base.Controls.Add(this.labelSaveAs);
			base.Controls.Add(this.textBoxSaveAs);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.groupBoxOperation);
			base.Name = "SelectionForm";
			this.Text = "Export DWF";
			this.groupBoxOperation.ResumeLayout(false);
			this.groupBoxOperation.PerformLayout();
			this.groupBoxRange.ResumeLayout(false);
			this.groupBoxRange.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
