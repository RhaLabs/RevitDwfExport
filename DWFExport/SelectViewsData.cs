using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
namespace DWFExport
{
	public class SelectViewsData
	{
		private ExternalCommandData m_commandData;
		private ViewSet m_printableViews;
		private ViewSet m_printableSheets;
		private ViewSet m_selectedViews;
		private bool m_contain3DView;
		public ViewSet PrintableViews
		{
			get
			{
				return this.m_printableViews;
			}
			set
			{
				this.m_printableViews = value;
			}
		}
		public ViewSet PrintableSheets
		{
			get
			{
				return this.m_printableSheets;
			}
			set
			{
				this.m_printableSheets = value;
			}
		}
		public ViewSet SelectedViews
		{
			get
			{
				return this.m_selectedViews;
			}
			set
			{
				this.m_selectedViews = value;
			}
		}
		public bool Contain3DView
		{
			get
			{
				return this.m_contain3DView;
			}
			set
			{
				this.m_contain3DView = value;
			}
		}
		public SelectViewsData(ExternalCommandData commandData)
		{
			this.m_commandData = commandData;
			this.m_printableViews = new ViewSet();
			this.m_printableSheets = new ViewSet();
			this.m_selectedViews = new ViewSet();
			this.GetAllPrintableViews();
		}
		private void GetAllPrintableViews()
		{
			FilteredElementCollector filteredElementCollector = new FilteredElementCollector(this.m_commandData.Application.ActiveUIDocument.Document);
			FilteredElementIterator elementIterator = filteredElementCollector.OfClass(typeof(View)).GetElementIterator();
			elementIterator.Reset();
			this.m_printableViews.Clear();
			this.m_printableSheets.Clear();
			while (elementIterator.MoveNext())
			{
				View view = elementIterator.Current as View;
				if (view != null && !view.IsTemplate && view.CanBePrinted)
				{
					if (view.ViewType == ViewType.DrawingSheet)
					{
						this.m_printableSheets.Insert(view);
					}
					else
					{
						this.m_printableViews.Insert(view);
					}
				}
			}
		}
	}
}
