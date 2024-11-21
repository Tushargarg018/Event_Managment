using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Core.Helpers
{
    public static class URLHelper
    {
       
        public static string GetUrl(string baseUrl, string path)
        {
             return baseUrl + "/" + path;
        }
    }
}
