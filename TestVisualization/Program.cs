using IncidentCS;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestVisualization
{
	class Program
	{
		static void Main(string[] args)
		{
			List<Human> people =
				Enumerable.Range(0, 30)
				.Select(_ => new Human())
				.ToList();

			List<Human> sortedByHeight = people
				.OrderBy(h => h.Height)
				.ToList();

			List<Human> sortedByAge = people
				.OrderBy(h => h.Age)
				.ToList();

			Debugger.Break();
		}
	}
}
