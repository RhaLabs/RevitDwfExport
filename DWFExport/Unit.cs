using Autodesk.Revit.DB;
using System;
namespace DWFExport
{
	internal static class Unit
	{
		public static double CovertFromAPI(DisplayUnitType to, double value)
		{
			return value *= Unit.ImperialDutRatio(to);
		}
		public static double CovertToAPI(double value, DisplayUnitType from)
		{
			return value /= Unit.ImperialDutRatio(from);
		}
		private static double ImperialDutRatio(DisplayUnitType dut)
		{
			switch (dut)
			{
			case DisplayUnitType.DUT_METERS:
				return 0.3048;
			case DisplayUnitType.DUT_CENTIMETERS:
				return 30.48;
			case DisplayUnitType.DUT_MILLIMETERS:
				return 304.8;
			case DisplayUnitType.DUT_DECIMAL_FEET:
				return 1.0;
			case DisplayUnitType.DUT_FEET_FRACTIONAL_INCHES:
				return 1.0;
			case DisplayUnitType.DUT_FRACTIONAL_INCHES:
				return 12.0;
			case DisplayUnitType.DUT_DECIMAL_INCHES:
				return 12.0;
			case DisplayUnitType.DUT_METERS_CENTIMETERS:
				return 0.3048;
			}
			return 1.0;
		}
	}
}
