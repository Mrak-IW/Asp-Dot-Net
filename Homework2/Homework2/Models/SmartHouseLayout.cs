﻿using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework2.Models
{
	public class SmartHouseLayout
	{
		public ISmartHouse SmartHouse { get; set; }
		public IList<DevTypesDescription> TypesAvailable { get; set; }
	}
}