using IncidentCS;
using System;

namespace TestVisualization
{
	[Serializable]
	class Human
	{
		public string Name { get; set; }
		public int Age { get; set; }
		public double Height { get; set; }

		public Human()
		{
			Name = Incident.Human.FullName;
			Age = Incident.Human.Age(HumanAgeCategory.Adult);
			Height = Math.Round(Incident.Primitive.DoubleBetween(120, 200), 2);
		}
	}
}
