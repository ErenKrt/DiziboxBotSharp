using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace EpEren.DiziBox.Helpers
{
    public static class Extensions
    {
        public static void SetHeaders(this RestRequest Req)
        {
            Req.AddHeader("Connection", "keep-alive");
            Req.AddHeader("Host", "www.dizibox.pw");
            Req.AddHeader("Sec-Fetch-Mode", "navigate");
            Req.AddHeader("Sec-Fetch-Site", "none");
            Req.AddHeader("Sec-Fetch-User", "?1");
            Req.AddHeader("Upgrade-Insecure-Requests", "1");
            Req.AddCookie("dbxu", DateTimeOffset.Now.ToUnixTimeSeconds().ToString());
        }
       
    }
}
