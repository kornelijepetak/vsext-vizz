using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vizz
{
	using System.Reflection;
	using System.Windows.Forms;
	using System.Windows.Forms.DataVisualization.Charting;

	public static class Extensions
	{
		public static double? ToDouble(this object val)
		{
			try
			{
				return Convert.ToDouble(val);
			}
			catch
			{
				return null;
			}
		}

		private static HashSet<Type> types;

		public static bool IsNumeric(this PropertyInfo prop)
		{
			types = types ?? new HashSet<Type>() {
				typeof(byte),
				typeof(sbyte),
				typeof(short),
				typeof(ushort),
				typeof(int),
				typeof(uint),
				typeof(long),
				typeof(ulong),
				typeof(float),
				typeof(double),
				typeof(decimal)
			};

			return types.Contains(prop.PropertyType);
		}

		public static double? GetValue(this object obj, string propName)
		{
			if(obj == null)
				return null;

			if(string.IsNullOrWhiteSpace(propName))
				return obj.ToDouble();

			PropertyInfo propInfo = obj.GetType().GetProperty(propName);
			if(propInfo == null)
				return null;

			return propInfo.GetValue(obj).ToDouble();
		}

		public static SeriesChartType NextType(this SeriesChartType current)
		{
			switch(current)
			{
				case SeriesChartType.Column:
					return SeriesChartType.Line;
				case SeriesChartType.Line:
					return SeriesChartType.Bar;
				case SeriesChartType.Bar:
					return SeriesChartType.Point;
				default:
					return SeriesChartType.Column;
			}
		}
	}
}
