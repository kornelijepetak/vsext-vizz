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

	public class VizzWindow : Form
	{
		private readonly Chart chart = new Chart();
		private readonly ToolStrip strip = new ToolStrip();
		private readonly ToolStripComboBox propertiesBox = new ToolStripComboBox();
		private readonly Dictionary<string, PropertyInfo> propInfos = new Dictionary<string, PropertyInfo>();
		private readonly List<object> values;

		private SeriesChartType type;

		public VizzWindow(List<object> values)
		{
			if(values == null)
				return;

			this.values = values;
			type = SeriesChartType.Column;

			initializeComponents();
			displayData();
		}

		private void displayData()
		{
			chart.Series.Clear();

			if(values == null)
				return;

			Series series = new Series()
			{
				ChartType = type,
				BorderWidth = 4
			};

			chart.Series.Add(series);

			string propertyName = propertiesBox.SelectedItem as string;

			for(int x = 0; x < values.Count; x++)
			{
				double? y = values[x].GetValue(propertyName);
				series.Points.AddXY(x, y);
			}

			chart.ResetAutoValues();
			chart.Invalidate();
		}

		private void initializeComponents()
		{
			SuspendLayout();

			Text = "Vizz";

			strip.Dock = DockStyle.Top;
			strip.Items.Add(propertiesBox);

			propertiesBox.SelectedIndexChanged += onPropertyChanged;

			List<string> allPropertyNames = values
				.Where(x => x != null)
				.Select(x => x.GetType())
				.Distinct()
				.SelectMany(t => t.GetProperties())
				.Where(p => p.IsNumeric())
				.Select(p => p.Name)
				.Distinct()
				.ToList();

			allPropertyNames.Insert(0, "");

			propertiesBox.Size = new Size(200, 0);
			propertiesBox.Items.AddRange(allPropertyNames.ToArray());

			Controls.Add(chart);
			Controls.Add(strip);
			chart.ChartAreas.Add(new ChartArea());
			chart.Palette = ChartColorPalette.Berry;
			chart.Dock = DockStyle.Fill;
			chart.Click += onChartClick;
			chart.DoubleClick += onChartClick;

			ResumeLayout();
		}

		private void onPropertyChanged(object sender, EventArgs e)
			=> displayData();

		private void onChartClick(object sender, EventArgs e)
		{
			type = type.NextType();
			displayData();
		}
	}
}
