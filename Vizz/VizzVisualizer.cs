using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.DebuggerVisualizers;

using System.Diagnostics;
using Vizz;

[assembly: DebuggerVisualizer(
	typeof(VizzVisualizer),
	typeof(VisualizerObjectSource),
	Target = typeof(List<>),
	Description = "Vizzualize this!")]

namespace Vizz
{
	public class VizzVisualizer : DialogDebuggerVisualizer
	{
		protected override void Show(
			IDialogVisualizerService windowService,
			IVisualizerObjectProvider objectProvider)
		{
			if (objectProvider.GetObject() is IEnumerable collection)
				windowService.ShowDialog(new VizzWindow(collection));
		}
	}
}
