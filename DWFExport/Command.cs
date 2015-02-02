using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Windows.Forms;
namespace DWFExport
{
	[Journaling(JournalingMode.NoCommandData), Regeneration(RegenerationOption.Manual), Transaction(TransactionMode.Manual)]
	public class Command : IExternalCommand
	{
		public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
		{
			try
			{
				if (commandData.Application.ActiveUIDocument.Document == null)
				{
					message = "Active view is null.";
					Result result = Result.Failed;
					return result;
				}
				MainData maindata = new MainData(commandData);
				using (SelectionForm selectionForm = new SelectionForm(maindata))
				{
					if (selectionForm.ShowDialog() == DialogResult.Cancel)
					{
						Result result = Result.Succeeded;
						return result;
					}
				}
			}
			catch (Exception ex)
			{
				message = ex.ToString();
				Result result = Result.Failed;
				return result;
			}
			return 0;
		}
	}


	[Journaling(JournalingMode.NoCommandData), Regeneration(RegenerationOption.Manual), Transaction(TransactionMode.Manual)]
	public class CreateNarrativeCommand : IExternalCommand
	{
		public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
		{
			try
			{
				if (commandData.Application.ActiveUIDocument.Document == null)
				{
					message = "Active view is null.";
					Result result = Result.Failed;
					return result;
				}
				MainData maindata = new MainData(commandData);
				/*using (SelectionForm selectionForm = new SelectionForm(maindata))
				{
					if (selectionForm.ShowDialog() == DialogResult.Cancel)
					{
						Result result = Result.Succeeded;
						return result;
					}
				} */
			}
			catch (Exception ex)
			{
				message = ex.ToString();
				Result result = Result.Failed;
				return result;
			}
			return 0;
		}
	}
}
