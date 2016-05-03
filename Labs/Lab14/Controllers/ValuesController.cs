﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Web.Hosting;

namespace Lab14.Controllers
{
	public class ValuesController : ApiController
	{
		private string xmlfilename = "storage.xml";
		private string XmlFileName
		{
			get
			{
				return HostingEnvironment.MapPath("~/App_Data/" + xmlfilename); ;
			}
		}

		// GET api/values
		public List<string> Get()
		{
			List<string> storage = LoadStorage(XmlFileName);
			return storage;
		}

		// GET api/values/5
		public string Get(int? id)
		{
			string result = "Ничего не выбрано";
			int index = (id != null ? (int)id : -1);
			List<string> storage = LoadStorage(XmlFileName);
			try
			{
				result = storage[index];
			}
			catch { }

			return result;
		}

		// POST api/values
		public HttpResponseMessage Post([FromBody]string value)
		{
			if (value != null)
			{
				List<string> storage = LoadStorage(XmlFileName);
				int newId = storage.Count + 1;

				storage.Add(value);

				SaveStorage(XmlFileName, storage);

				return new HttpResponseMessage(HttpStatusCode.Created);
			}
			return new HttpResponseMessage(HttpStatusCode.BadRequest);
		}

		// PUT api/values/5
		public HttpResponseMessage Put(int id, [FromBody]string value)
		{
			HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest);

			if (value != null && id >= 0)
			{
				List<string> storage = LoadStorage(XmlFileName);
				if (id < storage.Count)
				{
					storage[id] = value;
					response = new HttpResponseMessage(HttpStatusCode.OK);
				}
				else
				{
					storage.Add(value);
					response = new HttpResponseMessage(HttpStatusCode.Created);
				}
				SaveStorage(XmlFileName, storage);
			}

			return response;
		}

		// DELETE api/values/5
		public HttpResponseMessage Delete(int id)
		{
			HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest);

			if (id >= 0)
			{
				List<string> storage = LoadStorage(XmlFileName);

				if (id < storage.Count)
				{
					storage.RemoveAt(id);
					SaveStorage(XmlFileName, storage);
					response = new HttpResponseMessage(HttpStatusCode.OK);
				}
				else
				{
					response = new HttpResponseMessage(HttpStatusCode.NotFound);
				}
			}

			return response;
		}

		private List<string> LoadStorage(string filename)
		{
			FileInfo fi = new FileInfo(filename);
			List<string> storage = null;
			XmlSerializer xs = new XmlSerializer(typeof(List<string>));

			if (fi.Exists)
			{
				using (FileStream fs = new FileStream(filename, FileMode.Open))
				{
					storage = xs.Deserialize(fs) as List<string>;
				}
			}
			else
			{
				storage = new List<string>();
				storage.Add("Одинокая строка");
				SaveStorage(filename, storage);
			}

			return storage;
		}

		private bool SaveStorage(string filename, List<string> storage)
		{
			bool result = false;
			XmlSerializer xs = new XmlSerializer(typeof(List<string>));

			FileInfo fi = new FileInfo(filename);
			if (fi.Exists)
			{
				fi.Delete();
			}

			using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
			{
				if (fs != null)
				{
					
					xs.Serialize(fs, storage);
					fs.Flush();
					result = true;
				}
			}

			return result;
		}
	}
}
