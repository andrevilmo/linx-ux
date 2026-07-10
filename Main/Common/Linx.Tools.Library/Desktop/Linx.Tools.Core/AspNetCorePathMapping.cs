using Microsoft.AspNetCore.Hosting;

namespace Linx.Tools
{
    public class HostEnvironment
    {
        private static IHostingEnvironment _hostingEnvironment;
        public static IHostingEnvironment HostingEnvironment { get { return _hostingEnvironment; } }

        public static void SetHostingEnvironment(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public static string GetWebRootPath()
        {
            return (_hostingEnvironment == null ? "" : _hostingEnvironment.WebRootPath);
        }

        public static string GetContentRootPath()
        {
            return (_hostingEnvironment == null ? "" : _hostingEnvironment.ContentRootPath);
        }        
    }
}