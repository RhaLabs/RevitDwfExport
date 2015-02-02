using Autodesk.Revit.DB;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace DWFExport
{
	public class SelectViewsForm : System.Windows.Forms.Form
	{
		private SelectViewsData m_selectViewsData;
		private IContainer components;
		private GroupBox groupBoxShow;
		private CheckBox checkBoxViews;
		private CheckBox checkBoxSheets;
		private Button buttonCheckAll;
		private Button buttonCheckNone;
		private Button buttonOK;
		private Button buttonCancel;
		private CheckedListBox checkedListBoxViews;
		public SelectViewsForm(SelectViewsData selectViewsData)
		{
			this.InitializeComponent();
			this.m_selectViewsData = selectViewsData;
			this.InitializeControls();
		}
		private void InitializeControls()
		{
			this.UpdateViews();
		}
		private void buttonCheckAll_Click(object sender, EventArgs e)
		{
			checked
			{
				for (int i = 0; i < this.checkedListBoxViews.Items.Count; i++)
				{
					this.checkedListBoxViews.SetItemChecked(i, true);
				}
			}
		}
		private void buttonCheckNone_Click(object sender, EventArgs e)
		{
			checked
			{
				for (int i = 0; i < this.checkedListBoxViews.Items.Count; i++)
				{
					this.checkedListBoxViews.SetItemChecked(i, false);
				}
			}
		}
		private void checkBoxSheets_CheckedChanged(object sender, EventArgs e)
		{
			this.UpdateViews();
		}
		private void checkBoxViews_CheckedChanged(object sender, EventArgs e)
		{
			this.UpdateViews();
		}
		private void UpdateViews()
		{
			this.checkedListBoxViews.Items.Clear();
			if (this.checkBoxViews.Checked)
			{
				foreach (Autodesk.Revit.DB.View view in this.m_selectViewsData.PrintableViews)
				{
					this.checkedListBoxViews.Items.Add(view.ViewType.ToString() + ": " + view.ViewName);
				}
			}
			if (this.checkBoxSheets.Checked)
			{
				foreach (ViewSheet viewSheet in this.m_selectViewsData.PrintableSheets)
				{
					this.checkedListBoxViews.Items.Add("Drawing Sheet: " + viewSheet.SheetNumber + " - " + viewSheet.ViewName);
				}
			}
			this.checkedListBoxViews.Sorted = true;
		}
		private void buttonOK_Click(object sender, EventArgs e)
		{
			this.GetSelectedViews();
			base.Close();
		}
		private void GetSelectedViews()
		{
			this.m_selectViewsData.Contain3DView = false;
			checked
			{
				foreach (int index in this.checkedListBoxViews.CheckedIndices)
				{
					string text = this.checkedListBoxViews.Items[index].ToString();
					string text2 = "Drawing Sheet: ";
					if (text.StartsWith(text2))
					{
						text = text.Substring(text2.Length);
						string b = text.Substring(0, text.IndexOf(" - "));
						string b2 = text.Substring(text.IndexOf(" - ") + 3);
						IEnumerator enumerator2 = this.m_selectViewsData.PrintableSheets.GetEnumerator();
						try
						{
							while (enumerator2.MoveNext())
							{
								ViewSheet viewSheet = (ViewSheet)enumerator2.Current;
								if (viewSheet.SheetNumber == b && viewSheet.ViewName == b2)
								{
									this.m_selectViewsData.SelectedViews.Insert(viewSheet);
									break;
								}
							}
							continue;
						}
						finally
						{
							IDisposable disposable = enumerator2 as IDisposable;
							if (disposable != null)
							{
								disposable.Dispose();
							}
						}
					}
					string a = text.Substring(0, text.IndexOf(": "));
					string a2 = text.Substring(text.IndexOf(": ") + 2);
					foreach (Autodesk.Revit.DB.View view in this.m_selectViewsData.PrintableViews)
					{
						ViewType viewType = view.ViewType;
						if (a == viewType.ToString() && a2 == view.ViewName)
						{
							this.m_selectViewsData.SelectedViews.Insert(view);
							if (viewType == ViewType.ThreeD)
							{
								this.m_selectViewsData.Contain3DView = true;
								break;
							}
							break;
						}
					}
				}
			}
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
			this.groupBoxShow = new GroupBox();
			this.checkBoxViews = new CheckBox();
			this.checkBoxSheets = new CheckBox();
			this.buttonCheckAll = new Button();
			this.buttonCheckNone = new Button();
			this.buttonOK = new Button();
			this.buttonCancel = new Button();
			this.checkedListBoxViews = new CheckedListBox();
			this.groupBoxShow.SuspendLayout();
			base.SuspendLayout();
			this.groupBoxShow.Controls.Add(this.checkBoxViews);
			this.groupBoxShow.Controls.Add(this.checkBoxSheets);
			this.groupBoxShow.Location = new System.Drawing.Point(12, 288);
			this.groupBoxShow.Name = "groupBoxShow";
			this.groupBoxShow.Size = new Size(311, 41);
			this.groupBoxShow.TabIndex = 1;
			this.groupBoxShow.TabStop = false;
			this.groupBoxShow.Text = "Show";
			this.checkBoxViews.AutoSize = true;
			this.checkBoxViews.Checked = true;
			this.checkBoxViews.CheckState = CheckState.Checked;
			this.checkBoxViews.Location = new System.Drawing.Point(148, 18);
			this.checkBoxViews.Name = "checkBoxViews";
			this.checkBoxViews.Size = new Size(54, 17);
			this.checkBoxViews.TabIndex = 1;
			this.checkBoxViews.Text = "Views";
			this.checkBoxViews.UseVisualStyleBackColor = true;
			this.checkBoxViews.CheckedChanged += new EventHandler(this.checkBoxViews_CheckedChanged);
			this.checkBoxSheets.AutoSize = true;
			this.checkBoxSheets.Checked = true;
			this.checkBoxSheets.CheckState = CheckState.Checked;
			this.checkBoxSheets.Location = new System.Drawing.Point(7, 19);
			this.checkBoxSheets.Name = "checkBoxSheets";
			this.checkBoxSheets.Size = new Size(59, 17);
			this.checkBoxSheets.TabIndex = 0;
			this.checkBoxSheets.Text = "Sheets";
			this.checkBoxSheets.UseVisualStyleBackColor = true;
			this.checkBoxSheets.CheckedChanged += new EventHandler(this.checkBoxSheets_CheckedChanged);
			this.buttonCheckAll.Location = new System.Drawing.Point(333, 24);
			this.buttonCheckAll.Name = "buttonCheckAll";
			this.buttonCheckAll.Size = new Size(85, 23);
			this.buttonCheckAll.TabIndex = 0;
			this.buttonCheckAll.Text = "Check &All";
			this.buttonCheckAll.UseVisualStyleBackColor = true;
			this.buttonCheckAll.Click += new EventHandler(this.buttonCheckAll_Click);
			this.buttonCheckNone.Location = new System.Drawing.Point(333, 65);
			this.buttonCheckNone.Name = "buttonCheckNone";
			this.buttonCheckNone.Size = new Size(85, 23);
			this.buttonCheckNone.TabIndex = 1;
			this.buttonCheckNone.Text = "Check &None";
			this.buttonCheckNone.UseVisualStyleBackColor = true;
			this.buttonCheckNone.Click += new EventHandler(this.buttonCheckNone_Click);
			this.buttonOK.DialogResult = DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(238, 361);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(85, 23);
			this.buttonOK.TabIndex = 2;
			this.buttonOK.Text = "&OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
			this.buttonCancel.DialogResult = DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(333, 361);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new Size(85, 23);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "&Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.checkedListBoxViews.CheckOnClick = true;
			this.checkedListBoxViews.FormattingEnabled = true;
			this.checkedListBoxViews.Location = new System.Drawing.Point(12, 12);
			this.checkedListBoxViews.Name = "checkedListBoxViews";
			this.checkedListBoxViews.Size = new Size(311, 274);
			this.checkedListBoxViews.TabIndex = 3;
			base.AcceptButton = this.buttonOK;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.buttonCancel;
			base.ClientSize = new Size(432, 394);
			base.Controls.Add(this.checkedListBoxViews);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.buttonCheckNone);
			base.Controls.Add(this.buttonCheckAll);
			base.Controls.Add(this.groupBoxShow);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SelectViewsForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "View/Sheet Set";
			this.groupBoxShow.ResumeLayout(false);
			this.groupBoxShow.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
