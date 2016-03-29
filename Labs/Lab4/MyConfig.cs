using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Lab4
{
	public class MyConfig : ConfigurationSection
	{
		[ConfigurationProperty("stringParam")]
		public string stringParam
		{
			get { return (string)base["stringParam"]; }
			set { base["stringParam"] = value; }
		}

		[ConfigurationProperty("intParam")]
		public int intParam
		{
			get { return Convert.ToInt32(base["intParam"]); }
			set { base["intParam"] = value; }
		}

		[ConfigurationProperty("doubleParam")]
		public double doubleParam
		{
			get { return Convert.ToDouble(base["doubleParam"]); }
			set { base["doubleParam"] = value; }
		}


	}
}