using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Text.RegularExpressions;
namespace DWFExport
{
	public class ExportData
	{
		protected ExternalCommandData m_commandData;
		protected Document m_activeDoc;
		protected string m_exportFileName;
		protected string m_exportFolder;
		protected string m_activeDocName;
		protected string m_activeViewName;
		protected bool m_is3DView;
		protected ExportFormat m_exportFormat;
		protected string m_filter;
		protected string m_title;
		protected ProjectInfo info;
		protected string StoreNumber;
		public ExternalCommandData CommandData
		{
			get
			{
				return this.m_commandData;
			}
		}
		public ProjectInfo Info
		{
			get
			{
				return this.info;
			}
		}
		public Document ActiveDocument
		{
			get
			{
				return this.m_activeDoc;
			}
		}
		public string ExportFileName
		{
			get
			{
				return this.m_exportFileName;
			}
			set
			{
				this.m_exportFileName = value;
			}
		}
		public string ExportFolder
		{
			get
			{
				return this.m_exportFolder;
			}
			set
			{
				this.m_exportFolder = value;
			}
		}
		public string ActiveDocName
		{
			get
			{
				return this.m_activeDocName;
			}
			set
			{
				this.m_activeDocName = value;
			}
		}
		public string ActiveViewName
		{
			get
			{
				return this.m_activeViewName;
			}
			set
			{
				this.m_activeViewName = value;
			}
		}
		public bool Is3DView
		{
			get
			{
				return this.m_is3DView;
			}
			set
			{
				this.m_is3DView = value;
			}
		}
		public ExportFormat ExportFormat
		{
			get
			{
				return this.m_exportFormat;
			}
			set
			{
				this.m_exportFormat = value;
			}
		}
		public string Filter
		{
			get
			{
				return this.m_filter;
			}
		}
		public string Title
		{
			get
			{
				return this.m_title;
			}
		}
		public ExportData(ExternalCommandData commandData, ExportFormat exportFormat)
		{
			this.m_commandData = commandData;
			this.m_activeDoc = commandData.Application.ActiveUIDocument.Document;
			this.m_exportFormat = exportFormat;
			this.m_filter = string.Empty;
			this.Initialize();
		}
		private void Initialize()
		{
			this.m_exportFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			this.info = this.m_activeDoc.ProjectInformation;
			Regex regex = new Regex("\\d{4}");
			Match match = regex.Match(this.info.BuildingName);
			if (match.Success)
			{
				this.StoreNumber = match.Value;
			}
			else
			{
				this.StoreNumber = "XXXX";
			}
			this.m_activeDocName = this.m_activeDoc.Title;
			this.m_activeViewName = this.m_activeDoc.ActiveView.Name;
			this.m_activeDoc.ActiveView.ViewType.ToString();
			this.m_exportFileName = string.Concat(new string[]
			{
				this.m_activeViewName,
				" - ",
				this.StoreNumber,
				".",
				this.getExtension().ToString()
			});
			if (this.m_activeDoc.ActiveView.ViewType == ViewType.ThreeD)
			{
				this.m_is3DView = true;
				return;
			}
			this.m_is3DView = false;
		}
		private string getExtension()
		{
			string result = null;
			switch (this.m_exportFormat)
			{
			case ExportFormat.DWF:
				result = "dwf";
				break;
			case ExportFormat.DWFx:
				result = "dwfx";
				break;
			}
			return result;
		}
		public virtual bool Export()
		{
			if (this.m_exportFolder == null || this.m_exportFileName == null)
			{
				throw new NullReferenceException();
			}
			return true;
		}
	}
}
