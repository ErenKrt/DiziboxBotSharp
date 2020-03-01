using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using EpEren.DiziBox.Classes;
using EpEren.DiziBox.Classes.Modals.Sayfalar;
using RestSharp;

namespace EpEren.DiziBox.Helpers
{
    public class Http
    {
        private RestClient _Client { get; set; }
        public Http(string BaseUrl)
        {
            _Client = new RestClient(BaseUrl);
            _Client.CookieContainer = new System.Net.CookieContainer();
            _Client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36 OPR/66.0.3515.72";
            _Client.FollowRedirects = true;
            
        }

        private string GetPage(string uri,string RefUrl="")
        {
            RestRequest Page = new RestRequest(uri, Method.GET);
            Page.SetHeaders();
            if (RefUrl != "")
            {
                Page.AddHeader("Referer", RefUrl);
            }
            var Istek = _Client.Execute(Page);
            if (Istek.StatusCode == System.Net.HttpStatusCode.OK)
            {
              return Istek.Content;
            }
            else
            {
            return "";
            }
           
        }
        public string GetBaseUrl()
        {
            return _Client.BaseUrl.ToString();
        }

        public string GetImage(string uri)
        {
            RestRequest Page = new RestRequest(uri, Method.GET);
            Page.SetHeaders();
            var Istek = _Client.Execute(Page);
            
            if (Istek.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Convert.ToBase64String(Istek.RawBytes, 0, Istek.RawBytes.Length);
            }
            else
            {
                return "";
            }
        }

        public string Populer(int Page=1)
        {
            return GetPage("tum-bolumler/page/" + Page.ToString() + "/?tip=populer");
        }

        public string Yeni(int Page = 1)
        {
            return GetPage("tum-bolumler/page/" + Page.ToString());
        }
        public string Takvim()
        {
            return GetPage("dizi-takvimi/");
        }
        public string Efsaneler(int Page=1)
        {
            return GetPage("efsane-diziler/page/"+Page.ToString()+"/");
        }
        public string Ara(string Kelime,int Page=1)
        {
            return GetPage("page/"+Page.ToString()+"/?s=" + Uri.EscapeUriString(Kelime));
        }
        public string Dizi(string dizi,string sezon,string bolum,int player=1)
        {
            return GetPage(Extra.fnSeo(dizi) + "-" + Extra.fnSeo(sezon) + "-" + Extra.fnSeo(bolum) + "-izle/"+player.ToString()+"/");
        }
       
        public string Arsiv(int Page,string query)
        {
            return GetPage("arsiv/page/"+Page.ToString()+"/" + query);
        }

        public string KillTheProtocol(string RefUrl,string Url)
        {
            return GetPage(Url, RefUrl);
        }
    }
}
