/*
 * Created by SharpDevelop.
 * User: bcrawford
 * Date: 4/8/2013
 * Time: 12:32 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DWFExport
{
	/// <summary>
	/// Description of RevisionSelectForm.
	/// </summary>
	public partial class RevisionSelectForm : Form
	{
		#region Private
		private MainData _maindata;
		private RevisionData _revisionData;
		#endregion
		public RevisionSelectForm(MainData maindata)
		{
			this._maindata = maindata;
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
		}
	}
}
