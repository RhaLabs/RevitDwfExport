using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
namespace DWFExport
{
	public class ExportDWFData : ExportDataWithViews
	{
		private bool m_exportObjectData;
		private bool m_exportAreas;
		private bool m_exportMergeFiles;
		private List<DWFImageQuality> m_ImageQualities;
		private DWFImageQuality m_dwfImageQuality;
		private DWFImageFormat m_dwfImageFormat;
		public bool ExportObjectData
		{
			get	{
				return this.m_exportObjectData;
			}
			set	{
				this.m_exportObjectData = value;
			}
		}
		public bool ExportMergeFiles
		{
			get {
				return this.m_exportMergeFiles;
			}
			set	{
				this.m_exportMergeFiles = value;
			}
		}
		public bool ExportAreas
		{
			get	{
				return this.m_exportAreas;
			}
			set	{
				this.m_exportAreas = value;
			}
		}
		public DWFImageFormat DwfImageFormat
		{
			get	{
				return this.m_dwfImageFormat;
			}
			set	{
				this.m_dwfImageFormat = value;
			}
		}
		public DWFImageQuality DwfImageQuality
		{
			get	{
				return this.m_dwfImageQuality;
			}
			set	{
				this.m_dwfImageQuality = value;
			}
		}
		public List<DWFImageQuality> ImageQualities
		{
			get	{
				return this.m_ImageQualities;
			}
			set	{
				this.m_ImageQualities = value;
			}
		}
		public ExportDWFData(ExternalCommandData commandData, ExportFormat format) : base(commandData, format)
		{
			this.Initialize();
		}
		private void Initialize()
		{
			this.m_exportObjectData = true;
			this.m_exportAreas = false;
			this.m_exportMergeFiles = false;
			this.m_ImageQualities = new List<DWFImageQuality>();
			this.m_ImageQualities.Add(DWFImageQuality.Low);
			this.m_ImageQualities.Add(DWFImageQuality.Medium);
			this.m_ImageQualities.Add(DWFImageQuality.High);
			if (this.m_exportFormat == ExportFormat.DWF) {
				this.m_filter = "DWF Files |*.dwf";
				this.m_title = "Export DWF";
				return;
			}
			this.m_filter = "DWFx Files |*.dwfx";
			this.m_title = "Export DWFx";
		}
		public override bool Export()
		{
			Transaction transaction = new Transaction(this.m_activeDoc, "Export_To_DWF");
			transaction.Start();
			bool result = false;
			base.Export();
			ViewSet viewSet = new ViewSet();
			ViewSet viewSet2 = new ViewSet();
			if (this.m_currentViewOnly)	{
				viewSet.Insert(this.m_activeDoc.ActiveView);
			}	else {
				viewSet = this.m_selectViewsData.SelectedViews;
			}
			foreach (View view in viewSet) {
				ViewSheet viewSheet = (ViewSheet)view;
				viewSet2.Insert(viewSheet);
				Parameter parameter = viewSheet.get_Parameter("SEQUENCE#");
				if (this.m_exportFormat == ExportFormat.DWFx)	{
					this.m_exportFileName = string.Concat(new string[]
					                                      {
					                                      	viewSheet.SheetNumber,
					                                      	" - ",
					                                      	viewSheet.Name,
					                                      	" - ",
					                                      	this.StoreNumber,
					                                      	".dwfx"
					                                      });
					DWFXExportOptions dWFXExportOptions = new DWFXExportOptions();
					dWFXExportOptions.ExportObjectData = this.m_exportObjectData;
					dWFXExportOptions.ExportingAreas = this.m_exportAreas;
					dWFXExportOptions.MergedViews = this.m_exportMergeFiles;
					dWFXExportOptions.ImageFormat = this.m_dwfImageFormat;
					dWFXExportOptions.ImageQuality = this.m_dwfImageQuality;
					result = this.m_activeDoc.Export(this.m_exportFolder, this.m_exportFileName, viewSet2, dWFXExportOptions);
				}	else {
					this.m_exportFileName = string.Concat(new string[]
					                                      {
					                                      	parameter.AsString(),
					                                      	" - ",
					                                      	viewSheet.SheetNumber,
					                                      	" ",
					                                      	viewSheet.Name,
					                                      	" - ",
					                                      	this.StoreNumber,
					                                      	".dwf"
					                                      });
					DWFExportOptions dWFExportOptions = new DWFExportOptions();
					dWFExportOptions.ExportObjectData = this.m_exportObjectData;
					dWFExportOptions.ExportingAreas = this.m_exportAreas;
					dWFExportOptions.MergedViews = this.m_exportMergeFiles;
					dWFExportOptions.ImageFormat = this.m_dwfImageFormat;
					dWFExportOptions.ImageQuality = this.m_dwfImageQuality;
					result = this.m_activeDoc.Export(this.m_exportFolder, this.m_exportFileName, viewSet2, dWFExportOptions);
				}
				viewSet2.Erase(view);
			}
			transaction.Commit();
			return result;
		}
	}
}
