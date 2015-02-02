using Autodesk.Revit.UI;
using System;
namespace DWFExport
{
	public class MainData
	{
		private ExternalCommandData m_commandData;
		private bool m_is3DView;
		public ExternalCommandData CommandData
		{
			get
			{
				return this.m_commandData;
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
		public MainData(ExternalCommandData commandData)
		{
			this.m_commandData = commandData;
			if (commandData.Application.ActiveUIDocument.Document.ActiveView.ViewType == Autodesk.Revit.DB.ViewType.ThreeD)
			{
				this.m_is3DView = true;
				return;
			}
			this.m_is3DView = false;
		}
	}
}
