using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace DWFExport
{
	public class ExportDWFOptionForm : Form
	{
		private ExportDWFData m_data;
		private IContainer components;
		private GroupBox groupBox1;
		private CheckBox checkBoxRoomsAndAreas;
		private CheckBox checkBoxModelElements;
		private Button buttonOK;
		private Button buttonCancel;
		private CheckBox checkBoxMergeViews;
		private GroupBox groupBoxGraphicsSetting;
		private ComboBox comboBoxImageQuality;
		private Label labelImageQuality;
		private RadioButton radioButtonCompressedFormat;
		private RadioButton radioButtonStandardFormat;
		public ExportDWFOptionForm(ExportDWFData data)
		{
			this.m_data = data;
			this.InitializeComponent();
			this.Initialize();
		}
		private void Initialize()
		{
			this.checkBoxModelElements.Checked = this.m_data.ExportObjectData;
			this.checkBoxRoomsAndAreas.Checked = this.m_data.ExportAreas;
			this.radioButtonStandardFormat.Checked = true;
			this.radioButtonCompressedFormat.Checked = false;
			this.labelImageQuality.Enabled = false;
			this.comboBoxImageQuality.Enabled = false;
			this.comboBoxImageQuality.DataSource = this.m_data.ImageQualities;
			this.checkBoxMergeViews.Enabled = !this.m_data.CurrentViewOnly;
			this.checkBoxMergeViews.Checked = !this.m_data.CurrentViewOnly;
			this.checkBoxMergeViews.Text = "Combine selected views and sheets into a single file";
		}
		private void buttonOK_Click(object sender, EventArgs e)
		{
			this.m_data.ExportObjectData = this.checkBoxModelElements.Checked;
			this.m_data.ExportAreas = (this.checkBoxRoomsAndAreas.Enabled && this.checkBoxRoomsAndAreas.Checked);
			this.m_data.ExportMergeFiles = this.checkBoxMergeViews.Checked;
			if (this.radioButtonStandardFormat.Checked)
			{
				this.m_data.DwfImageFormat = Autodesk.Revit.DB.DWFImageFormat.Lossless;
				this.m_data.DwfImageQuality = Autodesk.Revit.DB.DWFImageQuality.Default;
				return;
			}
			this.m_data.DwfImageFormat = Autodesk.Revit.DB.DWFImageFormat.Lossy;
			this.m_data.DwfImageQuality = this.m_data.ImageQualities[this.comboBoxImageQuality.SelectedIndex];
		}
		private void checkBoxModelElements_CheckedChanged(object sender, EventArgs e)
		{
			this.checkBoxRoomsAndAreas.Enabled = this.checkBoxModelElements.Checked;
		}
		private void radioButtonCompressedFormat_CheckedChanged(object sender, EventArgs e)
		{
			this.labelImageQuality.Enabled = this.radioButtonCompressedFormat.Checked;
			this.comboBoxImageQuality.Enabled = this.radioButtonCompressedFormat.Checked;
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
			this.groupBox1 = new GroupBox();
			this.checkBoxRoomsAndAreas = new CheckBox();
			this.checkBoxModelElements = new CheckBox();
			this.buttonOK = new Button();
			this.buttonCancel = new Button();
			this.checkBoxMergeViews = new CheckBox();
			this.groupBoxGraphicsSetting = new GroupBox();
			this.comboBoxImageQuality = new ComboBox();
			this.labelImageQuality = new Label();
			this.radioButtonCompressedFormat = new RadioButton();
			this.radioButtonStandardFormat = new RadioButton();
			this.groupBox1.SuspendLayout();
			this.groupBoxGraphicsSetting.SuspendLayout();
			base.SuspendLayout();
			this.groupBox1.Controls.Add(this.checkBoxRoomsAndAreas);
			this.groupBox1.Controls.Add(this.checkBoxModelElements);
			this.groupBox1.Location = new Point(13, 13);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(308, 68);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Export Object Data";
			this.checkBoxRoomsAndAreas.AutoSize = true;
			this.checkBoxRoomsAndAreas.Location = new Point(10, 43);
			this.checkBoxRoomsAndAreas.Name = "checkBoxRoomsAndAreas";
			this.checkBoxRoomsAndAreas.Size = new Size(110, 17);
			this.checkBoxRoomsAndAreas.TabIndex = 0;
			this.checkBoxRoomsAndAreas.Text = "Rooms and Areas";
			this.checkBoxRoomsAndAreas.UseVisualStyleBackColor = true;
			this.checkBoxModelElements.AutoSize = true;
			this.checkBoxModelElements.Location = new Point(10, 20);
			this.checkBoxModelElements.Name = "checkBoxModelElements";
			this.checkBoxModelElements.Size = new Size(100, 17);
			this.checkBoxModelElements.TabIndex = 0;
			this.checkBoxModelElements.Text = "Model Elements";
			this.checkBoxModelElements.UseVisualStyleBackColor = true;
			this.checkBoxModelElements.CheckedChanged += new EventHandler(this.checkBoxModelElements_CheckedChanged);
			this.buttonOK.DialogResult = DialogResult.OK;
			this.buttonOK.Location = new Point(114, 230);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(91, 23);
			this.buttonOK.TabIndex = 1;
			this.buttonOK.Text = "&OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
			this.buttonCancel.DialogResult = DialogResult.Cancel;
			this.buttonCancel.Location = new Point(220, 230);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new Size(101, 23);
			this.buttonCancel.TabIndex = 1;
			this.buttonCancel.Text = "&Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.checkBoxMergeViews.AutoSize = true;
			this.checkBoxMergeViews.Location = new Point(23, 195);
			this.checkBoxMergeViews.Name = "checkBoxMergeViews";
			this.checkBoxMergeViews.Size = new Size(226, 17);
			this.checkBoxMergeViews.TabIndex = 4;
			this.checkBoxMergeViews.Text = "Create separate files for each view/sheet";
			this.checkBoxMergeViews.UseVisualStyleBackColor = true;
			this.groupBoxGraphicsSetting.Controls.Add(this.comboBoxImageQuality);
			this.groupBoxGraphicsSetting.Controls.Add(this.labelImageQuality);
			this.groupBoxGraphicsSetting.Controls.Add(this.radioButtonCompressedFormat);
			this.groupBoxGraphicsSetting.Controls.Add(this.radioButtonStandardFormat);
			this.groupBoxGraphicsSetting.Location = new Point(13, 88);
			this.groupBoxGraphicsSetting.Name = "groupBoxGraphicsSetting";
			this.groupBoxGraphicsSetting.Size = new Size(308, 100);
			this.groupBoxGraphicsSetting.TabIndex = 5;
			this.groupBoxGraphicsSetting.TabStop = false;
			this.groupBoxGraphicsSetting.Text = "Graphics Settings";
			this.comboBoxImageQuality.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxImageQuality.FormattingEnabled = true;
			this.comboBoxImageQuality.Location = new Point(129, 66);
			this.comboBoxImageQuality.Name = "comboBoxImageQuality";
			this.comboBoxImageQuality.Size = new Size(163, 21);
			this.comboBoxImageQuality.TabIndex = 3;
			this.labelImageQuality.AutoSize = true;
			this.labelImageQuality.Location = new Point(54, 69);
			this.labelImageQuality.Name = "labelImageQuality";
			this.labelImageQuality.Size = new Size(78, 13);
			this.labelImageQuality.TabIndex = 2;
			this.labelImageQuality.Text = "Image Quality:";
			this.radioButtonCompressedFormat.AutoSize = true;
			this.radioButtonCompressedFormat.Location = new Point(10, 41);
			this.radioButtonCompressedFormat.Name = "radioButtonCompressedFormat";
			this.radioButtonCompressedFormat.Size = new Size(170, 17);
			this.radioButtonCompressedFormat.TabIndex = 0;
			this.radioButtonCompressedFormat.TabStop = true;
			this.radioButtonCompressedFormat.Text = "Use compressed raster format";
			this.radioButtonCompressedFormat.UseVisualStyleBackColor = true;
			this.radioButtonCompressedFormat.CheckedChanged += new EventHandler(this.radioButtonCompressedFormat_CheckedChanged);
			this.radioButtonStandardFormat.AutoSize = true;
			this.radioButtonStandardFormat.Location = new Point(10, 20);
			this.radioButtonStandardFormat.Name = "radioButtonStandardFormat";
			this.radioButtonStandardFormat.Size = new Size(124, 17);
			this.radioButtonStandardFormat.TabIndex = 0;
			this.radioButtonStandardFormat.TabStop = true;
			this.radioButtonStandardFormat.Text = "Use standard format";
			this.radioButtonStandardFormat.UseVisualStyleBackColor = true;
			base.AcceptButton = this.buttonOK;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.buttonCancel;
			base.ClientSize = new Size(335, 262);
			base.Controls.Add(this.groupBoxGraphicsSetting);
			base.Controls.Add(this.checkBoxMergeViews);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ExportDWFOptionForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Export DWF Options";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBoxGraphicsSetting.ResumeLayout(false);
			this.groupBoxGraphicsSetting.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
