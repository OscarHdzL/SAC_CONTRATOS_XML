using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
	public class Crudresponse
	{
		public string cod { get; set; }
		public string msg { get; set; }
	}

	public class CrudresponseIdentificador
	{
		public String cod { get; set; }
		public String msg { get; set; }
		public Guid id { get; set; }
	}
	public class CrudresponseNum
	{
		public String cod { get; set; }
	}
}
