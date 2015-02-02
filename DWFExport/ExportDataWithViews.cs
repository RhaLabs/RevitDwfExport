using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
namespace DWFExport
{
	public class ExportDataWithViews : ExportData
	{
		protected SelectViewsData m_selectViewsData;
		private ViewSet m_exportViews;
		protected bool m_currentViewOnly;
		public SelectViewsData SelectViewsData
		{
			get
			{
				return this.m_selectViewsData;
			}
			set
			{
				this.m_selectViewsData = value;
			}
		}
		public ViewSet ExportViews
		{
			get
			{
				return this.m_exportViews;
			}
			set
			{
				this.m_exportViews = value;
			}
		}
		public bool CurrentViewOnly
		{
			get
			{
				return this.m_currentViewOnly;
			}
			set
			{
				this.m_currentViewOnly = value;
			}
		}
		public ExportDataWithViews(ExternalCommandData commandData, ExportFormat format) : base(commandData, format)
		{
			this.m_selectViewsData = new SelectViewsData(commandData);
			this.Initialize();
		}
		private void Initialize()
		{
			this.m_exportViews = new ViewSet();
		}
	}
}
