/*
 * Created by SharpDevelop.
 * User: bcrawford
 * Date: 4/8/2013
 * Time: 12:31 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace DWFExport
{
	/// <summary>
	/// Description of RevisionData.
	/// </summary>
	public class RevisionData : ExportData
	{
		public RevisionData(ExternalCommandData commandData, ExportFormat exportFormat = ExportFormat.DWF):
			base(commandData, exportFormat)
		{
		}
		public string Revision
		{
			get;
			set;
		}
	}
}

