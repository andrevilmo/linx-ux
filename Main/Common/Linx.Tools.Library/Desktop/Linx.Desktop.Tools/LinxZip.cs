using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;

namespace Linx.Tools
{
    public class LinxZip
    {
        private Dictionary<string, string> StringContents = new Dictionary<string, string>();
        private Dictionary<string, byte[]> BytesContents = new Dictionary<string, byte[]>();
        private List<string> Files = new List<string>();

        public void AddFile(string fullPath)
        {
            Files.Add(fullPath);
        }

        public void AddStringContent(string fileName, string contents)
        {
            StringContents.Add(fileName, contents);
        }

        public void AddBytesContents(string fileName, byte[] contents)
        {
            BytesContents.Add(fileName, contents);
        }

        public byte[] GetZipBytes()
        {
            using (MemoryStream zipStream = new MemoryStream())
            {
                using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                {

                    foreach (var file in Files)
                        AddFileInZip(zip, file);
                    foreach (var strContent in StringContents)
                        AddStringContentInZip(zip, strContent.Key, strContent.Value);
                    foreach (var bytesCnt in BytesContents)
                        AddBytesContentsInZip(zip, bytesCnt.Key, bytesCnt.Value);
                }
                zipStream.Position = 0;
                byte[] data1 = new byte[zipStream.Length];
                zipStream.Read(data1, 0, data1.Length);

                return data1;
            }
        }


        public string GetZipFile(string filePath)
        {
            var bytes = GetZipBytes();

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                fileStream.Write(bytes, 0, bytes.Length);
            }

            return filePath;
        }


        private void AddFileInZip(ZipArchive zip, string fullPath)
        {
            var fi = new FileInfo(fullPath);
            var entry = zip.CreateEntry(fi.Name);
            var buffer = File.ReadAllBytes(fullPath);
            using (Stream st = entry.Open())
                st.Write(buffer, 0, buffer.Length);
        }

        private void AddStringContentInZip(ZipArchive zip, string fileName, string contents)
        {
            var entry = zip.CreateEntry(fileName);
            using (StreamWriter sw = new StreamWriter(entry.Open()))
            {
                sw.WriteLine(contents);
            }
        }

        private void AddBytesContentsInZip(ZipArchive zip, string fileName, byte[] contents)
        {
            var entry = zip.CreateEntry(fileName);
            using (Stream st = entry.Open())
                st.Write(contents, 0, contents.Length);
        }

    }
}
