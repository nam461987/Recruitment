using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace NhaKhoaAdmin.Common
{
    public class Strings
    {
        public static string UploadFolderRoot = ConfigurationManager.AppSettings["UploadNewsFolderRoot"];
    }
}