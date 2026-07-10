using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using System.Text;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Schema;
using System.Xml;
using System.Collections.ObjectModel;
using System.ServiceModel.Description;
using System.Web.Services.Discovery;
using System.Runtime.Serialization;
using System.Data.Objects.DataClasses;
using System.Xml.Linq;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows;
using System.IO.IsolatedStorage;
using System.Windows.Resources;
using System.Windows.Media.Imaging;
using System.Security.Cryptography;
using System.ServiceModel.Channels;
using System.ServiceModel;

namespace Linx.Tools
{

    public class ImageExtension
    {

        public static string CreateChecksum(byte[] ImageContent)
        { 
            MD5 m5 = MD5.Create();
            byte[] hash = m5.ComputeHash(ImageContent);
            return Convert.ToBase64String(hash);
        }

        public static byte[] ResizeImage(byte[] ImageContent, int Width, int Height)
        {
            MemoryStream ImageStream = new MemoryStream(ImageContent);
            MemoryStream imgStreamProc = null;

            int intWidth, intHeight = 0;

            intWidth = Width;
            intHeight = Height;

            if (ImageStream == null)
                return new byte[] { };

            if (ImageStream.Length <= 0)
                return new byte[] { };
            
            BitmapImage imgBitmap = new BitmapImage();
            ImageStream.Position = 0;

            try
            {
                imgBitmap.BeginInit();

                if (intWidth > 0) imgBitmap.DecodePixelWidth = intWidth;
                if (intHeight > 0) imgBitmap.DecodePixelHeight = intHeight;
                imgBitmap.CacheOption = BitmapCacheOption.OnLoad;
                imgBitmap.StreamSource = ImageStream;


                imgBitmap.EndInit();

                imgStreamProc = BitmapImageToStream(imgBitmap);

            }
            catch (System.NotSupportedException)
            {
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                imgBitmap.Freeze();

                ImageStream.Flush();
                ImageStream.Close();
                ImageStream = null;

            }

            return imgStreamProc.ToArray();
        }
        
        private static MemoryStream BitmapImageToStream(BitmapImage bmp)
        {
            if (bmp == null) return null;

            MemoryStream memtmp = new MemoryStream();
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            encoder.Save(memtmp);
            memtmp.Seek(0, SeekOrigin.Begin);

            encoder = null;

            return memtmp;
        }

    }

	/// <summary>
	/// AssemblyHelper complement.
	/// </summary>
	public static partial class AssemblyHelper
	{
		public static Assembly Load(Stream stream)
		{
			return Assembly.Load(ReadFully(stream));
		}

		public static byte[] ReadFully(Stream stream)
		{
			byte[] buffer = new byte[32768];
			using (MemoryStream ms = new MemoryStream())
			{
				while (true)
				{
					int read = stream.Read(buffer, 0, buffer.Length);
					if (read <= 0)
						return ms.ToArray();
					ms.Write(buffer, 0, read);
				}
			}
		}
	}

	#region Exeptions Suport
	public class ForeignKeyException : Exception
	{		
		public string SourceEntity { get; set; }
		public string TargetEntity { get; set; }

		public ForeignKeyException(string sourceEntity, string targetEntity, string message, Exception innerException)
			: base(message, innerException)
		{
			this.SourceEntity = sourceEntity;
			this.TargetEntity = targetEntity;
		}

		public ForeignKeyException(string sourceEntity, string targetEntity, string message)
			: this(sourceEntity, targetEntity, message, null) { }
	}
	#endregion

	#region Lookup Support
	public delegate void AfterLookUpMethod(int[] selection, System.Collections.IList dataList, object entityView, string fieldName, string validateRef, string relation, ToolbarStatus controlStatus);

	public partial class LookUpValidator
	{
		public static void Show(AfterLookUpMethod afterLookUp, System.Collections.IList dataList,
			object entityView, string fieldName, string validateRef, string relation, ToolbarStatus controlStatus)
		{
		}
	}
	#endregion 

	#region IsolatedStorage Extensions
	public static class IsolatedStorageFileExtension
	{
		public static FileStream OpenFile(this IsolatedStorageFile instance, string path, FileMode fileMode)
		{
			return instance.OpenFile(path, fileMode, FileAccess.Read, FileShare.None);
		}

		public static FileStream OpenFile(this IsolatedStorageFile instance, string path, FileMode fileMode, FileAccess fileAccess)
		{
			return instance.OpenFile(path, fileMode, fileAccess, FileShare.None);
		}

		public static FileStream OpenFile(this IsolatedStorageFile instance, string path, FileMode fileMode, FileAccess fileAccess, FileShare fileShare)
		{
			return new FileStream(path, fileMode, fileAccess, fileShare);
		}
		
		public static bool FileExists(this IsolatedStorageFile instance, string path)
		{
			return System.IO.File.Exists(path);
		}

		public static bool DirectoryExists(this IsolatedStorageFile instance, string path)
		{
			return System.IO.Directory.Exists(path);
		}

		public static string[] GetDirectoryNames(this IsolatedStorageFile instance)
		{
			return instance.GetDirectoryNames("*");
		}

		public static string[] GetDirectoryNames(this IsolatedStorageFile instance, string searchPattern)
		{
			System.IO.DirectoryInfo infoDir = new DirectoryInfo(System.IO.Path.GetDirectoryName(searchPattern));
			return infoDir.GetDirectories(Path.GetFileName(searchPattern)).Select(e => e.FullName).ToArray(); 
		}

		public static string[] GetFileNamesExt(this IsolatedStorageFile instance, string searchPattern)
		{
			System.IO.DirectoryInfo infoDir = new DirectoryInfo(System.IO.Path.GetDirectoryName(searchPattern));
			return infoDir.GetFiles(Path.GetFileName(searchPattern)).Select(e => e.FullName).ToArray();
		}

		public static bool IncreaseQuotaTo(this IsolatedStorageFile instance, long spaceNeeded)
		{
			return true;
		}

		public static long Quota(this IsolatedStorageFile instance)
		{
			return long.MaxValue;
		}

		public static long AvailableFreeSpace(this IsolatedStorageFile instance)
		{
			return long.MaxValue;
		}

		public static StreamResourceInfo GetStreamResourceInfo(Stream resource, string contentFileName)
		{
			return Application.GetResourceStream(new Uri(((FileStream)resource).Name + "\\" + contentFileName, UriKind.Relative));
		}

	}
	#endregion
    
}


