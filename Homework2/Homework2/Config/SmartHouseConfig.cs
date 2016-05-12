using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Homework2.Config
{
	public class SmartHouseConfig : ConfigurationSection
	{
		private const string path = "storageFilePath";
		private const string name = "storageFileName";
		private const string useSession = "useSession";

		public static string SectionName
		{
			get
			{
				return typeof(SmartHouseConfig).Name;
			}
		}

		[ConfigurationProperty(path)]
		public string StorageFilePath
		{
			get { return base[path] as string; }
			set { base[path] = value; }
		}

		[ConfigurationProperty(name)]
		public string StorageFileName
		{
			get { return base[name] as string; }
			set { base[name] = value; }
		}

		[ConfigurationProperty(useSession)]
		public bool UseSession
		{
			get { return (bool)base[useSession]; }
			set { base[useSession] = value.ToString(); }
		}
	}
}