using System;
using System.Collections.Generic;
using System.Text;

namespace xam.Server.RequestModel.InteractiveAccessObjects
{
	public class InteractiveObject
	{
		public int ID { get; set; }
		// Наименование объекта
		public string Name { get; set; }
		// Наименование типа объекта
		public string ObjectTypeName { get; set; }
	}
}
