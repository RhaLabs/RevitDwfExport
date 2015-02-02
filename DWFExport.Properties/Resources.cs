using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
namespace DWFExport.Properties
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
	internal class Resources
	{
		private static ResourceManager resourceMan;
		private static CultureInfo resourceCulture;
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("DWFExport.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}
		internal static string DUT_CENTIMETERS
		{
			get
			{
				return Resources.ResourceManager.GetString("DUT_CENTIMETERS", Resources.resourceCulture);
			}
		}
		internal static string DUT_DECIMAL_FEET
		{
			get
			{
				return Resources.ResourceManager.GetString("DUT_DECIMAL_FEET", Resources.resourceCulture);
			}
		}
		internal static string DUT_DECIMAL_INCHES
		{
			get
			{
				return Resources.ResourceManager.GetString("DUT_DECIMAL_INCHES", Resources.resourceCulture);
			}
		}
		internal static string DUT_FEET_FRACTIONAL_INCHES
		{
			get
			{
				return Resources.ResourceManager.GetString("DUT_FEET_FRACTIONAL_INCHES", Resources.resourceCulture);
			}
		}
		internal static string DUT_FRACTIONAL_INCHES
		{
			get
			{
				return Resources.ResourceManager.GetString("DUT_FRACTIONAL_INCHES", Resources.resourceCulture);
			}
		}
		internal static string DUT_METERS
		{
			get
			{
				return Resources.ResourceManager.GetString("DUT_METERS", Resources.resourceCulture);
			}
		}
		internal static string DUT_METERS_CENTIMETERS
		{
			get
			{
				return Resources.ResourceManager.GetString("DUT_METERS_CENTIMETERS", Resources.resourceCulture);
			}
		}
		internal static string DUT_MILLIMETERS
		{
			get
			{
				return Resources.ResourceManager.GetString("DUT_MILLIMETERS", Resources.resourceCulture);
			}
		}
		internal Resources()
		{
		}
	}
}
