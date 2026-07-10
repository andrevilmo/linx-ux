using System;
using System.Windows;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Resources;
using System.Collections.Generic;

namespace Linx.Tools
{

	public struct StorageSpaces
	{
		public const System.Int64 Space25MB = 26214400;
		public const System.Int64 Space50MB = 52428800;
		public const System.Int64 Space100MB = 104857600;
		public const System.Int64 Space500MB = 524288000;
		public const System.Int64 Space1GB = 1048576000;
		public const System.Int64 Space10GB = 10485760000;
	}

	public static class StorageManager
	{

		public static IsolatedStorageFile GetUserStoreForApplication()
		{
			return IsolatedStorageFile.GetUserStoreForApplication();
		}

		#region Create directory
		public static void CreateDirectory(IsolatedStorageFile store, string fileName)
		{
			if (!store.FileExists(fileName))
			{
				string directory = System.IO.Path.GetDirectoryName(fileName);

				// Verify if exists a directory in file name.
				if (directory.Length > 0)
				{
					// Create directory if not exists.
					if (!store.DirectoryExists(directory))
						store.CreateDirectory(directory);
				}
			}
		}
		#endregion

		public static void ClearStorage()
		{
			using (IsolatedStorageFile appStore = StorageManager.GetUserStoreForApplication())
			{
				List<string> files = ShowFilesAndDirectories("", "*", true, EReturnType.Files);
				foreach (string f in files)
					appStore.DeleteFile(f);

				List<string> dir = ShowFilesAndDirectories("", "*", true, EReturnType.Directory);
				for (int i = dir.Count - 1; i >= 0; i--)
					appStore.DeleteDirectory(dir[i]);
			}
		}

		public static List<string> ShowFiles(bool recursive)
		{
			return ShowFilesAndDirectories("", "*", recursive, EReturnType.Files);
		}

		public static List<string> ShowFiles(bool recursive, string rootPath)
		{
			return ShowFilesAndDirectories(rootPath, "*", recursive, EReturnType.Files);
		}

		public static List<string> ShowFiles(bool recursive, string rootPath, string filter)
		{
			return ShowFilesAndDirectories(rootPath, filter, recursive, EReturnType.Directory);
		}


		public static List<string> ShowDirectories(bool recursive)
		{
			return ShowFilesAndDirectories("", "*", recursive, EReturnType.Directory);
		}

		public static List<string> ShowDirectories(bool recursive, string rootPath)
		{
			return ShowFilesAndDirectories(rootPath, "*", recursive, EReturnType.Directory);
		}

		private enum EReturnType
		{
			Directory, Files
		}

		private static List<string> ShowFilesAndDirectories(string rootPath, string filter, bool recursive, EReturnType typeOut)
		{
			// verificar ser rootPath tem \ no final
			List<string> Diretorios;
			string[] dir;
			using (IsolatedStorageFile store = StorageManager.GetUserStoreForApplication())
			{
				// verifica se o diretorio passado existe
				if (rootPath.Length > 0)
				{
					if (store.DirectoryExists(rootPath) == false)
						return new List<string>();

					dir = store.GetDirectoryNames(rootPath + "*");
				}
				else
					dir = store.GetDirectoryNames();

				// verifica se tem diretorios
				if (dir.Length == 0)
					return new List<string>();

				Diretorios = new List<string>(dir.Length);
				foreach (string d in dir)
				{
					string fullPath = rootPath + d + @"\";

					if (recursive == true)
					{
						if (typeOut == EReturnType.Directory)
							Diretorios.Add(fullPath);
						else
						{
							// lista os arquivos do diretorio
							string[] dirFiles = store.GetFileNames(fullPath + filter);
							foreach (string f in dirFiles)
								Diretorios.Add(fullPath + f);
						}

						Diretorios.AddRange(ShowFilesAndDirectories(fullPath, filter, recursive, typeOut));

					}
					else
					{
						if (typeOut == EReturnType.Directory)
							Diretorios.Add(fullPath);
					}
				}


				// lista os arquivos do diretorio ROOT
				if (rootPath.Length == 0)
				{
					if (typeOut == EReturnType.Files)
					{
						string[] dirFiles = store.GetFileNames(rootPath + filter);
						foreach (string f in dirFiles)
							Diretorios.Add(f);
					}
				}
			}

			return Diretorios;
		}


		public static bool FileExists(string fileName)
		{
			bool exists = false;
			using (IsolatedStorageFile store = StorageManager.GetUserStoreForApplication())
			{
				exists = store.FileExists(fileName);
			}
			return exists;
		}

		public static string[] GetFileNames(string searchPattern)
		{
			string[] dirFiles = new string[] { };
			using (IsolatedStorageFile store = StorageManager.GetUserStoreForApplication())
			{
				try
				{
					if (store.DirectoryExists(Path.GetDirectoryName(searchPattern)) == true)
					{
						dirFiles = store.GetFileNames(searchPattern);
					}
				}
				catch { }
			}
			return dirFiles;
		}

		public static void SaveFile(Stream stream, string fileName)
		{
			try
			{
				using (IsolatedStorageFile store = StorageManager.GetUserStoreForApplication())
				{
					//Creating directory if necessary.
					CreateDirectory(store, fileName);

					using (IsolatedStorageFileStream strm =
						new IsolatedStorageFileStream(fileName,
							 FileMode.Create, store))
					{
						byte[] buffer = new byte[(int)stream.Length];
						stream.Read(buffer, 0, buffer.Length);
						strm.Write(buffer, 0, buffer.Length);
						using (StreamWriter sw = new StreamWriter(strm))
						{
						}
					}
				}

			}
			catch (Exception ex)
			{
				throw new Exception(
					"Could not save to " + fileName + "\n" + ex.ToString(),
					ex);
			}

		}

		public static StreamReader OpenFile(string fileName)
		{
			try
			{
				StreamReader fileStream = null;

				using (IsolatedStorageFile appStore = StorageManager.GetUserStoreForApplication())
				{
					if (appStore.FileExists(fileName))
					{
						fileStream = new StreamReader(appStore.OpenFile(fileName, FileMode.Open, FileAccess.Read, FileShare.Read));
					}
				}

				return fileStream;
			}
			catch (Exception ex)
			{
				throw new Exception(
					"Could not read from " + fileName + "\n" + ex.ToString(),
					ex);
			}
		}

		public static StreamReader OpenFile(string fileName, string contentFileName)
		{
			StreamReader fileStream = null;
			using (StreamReader resource = OpenFile(fileName))
			{
				if (resource != null)
				{
					StreamResourceInfo resourceInfo = IsolatedStorageFileExtension.GetStreamResourceInfo(resource.BaseStream, contentFileName);
					if (resourceInfo != null)
						fileStream = new StreamReader(resourceInfo.Stream);
				}
			}

			return fileStream;
		}

		public static void DeleteFiles(string searchPattenFiles)
		{
			try
			{
				using (IsolatedStorageFile appStore = StorageManager.GetUserStoreForApplication())
				{
					string directoryPath = Path.GetDirectoryName(searchPattenFiles);
					string fullPath = string.Empty;

					if (!appStore.DirectoryExists(directoryPath))
						return;


					string[] dirFiles = appStore.GetFileNames(searchPattenFiles);
					foreach (string f in dirFiles)
					{
						fullPath = Path.Combine(directoryPath, f);

						if (appStore.FileExists(fullPath))
							appStore.DeleteFile(fullPath);
					}
				}

			}
			catch (Exception ex)
			{
				throw new Exception(
					"Could not delete files\n" + ex.ToString(),
					ex);
			}
		}

		public static void DeleteFile(string fileName)
		{
            try
            {
                using (IsolatedStorageFile appStore = StorageManager.GetUserStoreForApplication())
                {
                    if (appStore.FileExists(fileName))
                    {
                        appStore.DeleteFile(fileName);
                    }
                }

            }
            catch { }
			
		}

		public static bool GetMoreSpace()
		{
			return GetMoreSpace(StorageSpaces.Space50MB);
		}

		public static bool GetMoreSpace(Int64 spaceNeeded)
		{
			using (IsolatedStorageFile appStore = StorageManager.GetUserStoreForApplication())
			{
				if (spaceNeeded > appStore.Quota())
				{
					if (!appStore.IncreaseQuotaTo(spaceNeeded))
					{
						return false;
					}
					return true;
				}
			}

			return false;
		}

		public static bool GetMoreSpacePercentege(double freeSpacePercentege, Int64 spaceNeeded)
		{
			using (IsolatedStorageFile appStore = StorageManager.GetUserStoreForApplication())
			{
				double currentSpacePercentege = ((double)appStore.AvailableFreeSpace() / (double)appStore.Quota()) * 100;
				if (freeSpacePercentege > currentSpacePercentege)
				{
					if (!appStore.IncreaseQuotaTo(appStore.Quota() + spaceNeeded))
					{
						return false;
					}
					return true;
				}
			}

			return false;
		}

		public static long FreeSpace()
		{
			using (IsolatedStorageFile appStore = StorageManager.GetUserStoreForApplication())
			{
				return appStore.AvailableFreeSpace();
			}
		}

		public static long Quota()
		{
			using (IsolatedStorageFile appStore = StorageManager.GetUserStoreForApplication())
			{
				return appStore.Quota();
			}
		}
	}

}
