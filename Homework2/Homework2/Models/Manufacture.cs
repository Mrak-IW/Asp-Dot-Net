using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
using System.Reflection;

namespace Homework2.Models
{
	public static class Manufacture
	{
		public static ISmartHouseCreator GetManufacture(Assembly smartHouseAssembly)
		{
			ISmartHouseCreator shc = null;
			Type shcType = null;

			var res = from t in smartHouseAssembly.GetTypes()
					  where t.GetInterfaces().Contains(typeof(ISmartHouseCreator))
					  select t;

			shcType = res.FirstOrDefault();

			if (shcType != null)
			{
				object[] constructorArgs = new object[1];
				constructorArgs[0] = smartHouseAssembly;

				shc = Activator.CreateInstance(shcType, constructorArgs) as ISmartHouseCreator;
			}

			return shc;
		}
	}
}