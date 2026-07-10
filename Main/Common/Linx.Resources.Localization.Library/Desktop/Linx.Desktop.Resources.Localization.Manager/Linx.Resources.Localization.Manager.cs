using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Linx.Resources.Localization.Manager
{
	public class StringManager
	{
		LinxDataSet.LinxStringsDataTable DataTable;
		FileStream FS;
		string XmlFile;

		public StringManager(string file)
		{
			try
			{
				XmlFile = file;
				DataTable = new LinxDataSet.LinxStringsDataTable();
				DataTable.ReadXml(XmlFile);
				BlockFileAccess();
			}
			catch (Exception exception)
			{
				throw new Exception(exception.InnerException != null ? exception.InnerException.Message : exception.Message);
			}
		}

		~StringManager()
		{
			if (FS != null)
				FS.Close();

			FS.Dispose();
		}

		private void BlockFileAccess()
		{
			if (FS != null)
				FS.Close();

			FS = new FileStream(XmlFile, FileMode.Open, FileAccess.Write, FileShare.None);
		}

		private void UnBlockFileAccess()
		{
			if (FS != null)
				FS.Close();
		}

		public void SaveChanges()
		{
			try
			{
				UnBlockFileAccess();
				DataTable.WriteXml(XmlFile);
				BlockFileAccess();
			}
			catch (Exception exception)
			{
				throw new Exception(exception.InnerException != null ? exception.InnerException.Message : exception.Message);
			}
		}

		public string StringValue(string key)
		{
			IEnumerable<LinxDataSet.LinxStringsRow> strings = DataTable.Where(i => i.Key == key).ToList();

			if (strings.Count() > 0)
				return strings.First().Key;
			else
				return string.Empty;
		}

		public void Add(string key, string value)
		{
			Add(key, value, string.Empty, string.Empty);
		}

		public void Add(string key, string value, string en, string es)
		{
			try
			{
				if (DataTable.Where(i => i.Key == key).Count() > 0)
					return;

				DataTable.AddLinxStringsRow(key, value, en, es);
			}
			catch (Exception exception)
			{
				throw new Exception(exception.InnerException != null ? exception.InnerException.Message : exception.Message);
			}
		}

		public void Remove(string key)
		{
			try
			{
				IEnumerable<LinxDataSet.LinxStringsRow> strings = DataTable.Where(i => i.Key == key).ToList();
				if (strings.Count() > 0)
				{
					strings.First().Delete();
				}
			}
			catch (Exception exception)
			{
				throw new Exception(exception.InnerException != null ? exception.InnerException.Message : exception.Message);
			}
		}

		public void Edit(string key, string value)
		{
			try
			{
				IEnumerable<LinxDataSet.LinxStringsRow> strings = DataTable.Where(i => i.Key == key).ToList();
				if (strings.Count() > 0)
				{
					strings.First().Value = value;
				}
			}
			catch (Exception exception)
			{
				throw new Exception(exception.InnerException != null ? exception.InnerException.Message : exception.Message);
			}
		}

		public int Count()
		{
			return DataTable.Count;
		}
	}
}
