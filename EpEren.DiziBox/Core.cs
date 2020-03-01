using EpEren.DiziBox.Classes;
using EpEren.DiziBox.Classes.Modals.Modeller;
using EpEren.DiziBox.Classes.Modals.Sayfalar;
using EpEren.DiziBox.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EpEren.DiziBox
{
    public class DbxCore
    {
        public static Http Http;
        public DbxCore()
        {
            Http = new Http("https://www.dizibox.pw/");
        }

        public BolumlerSayfasi GetYeniler(int Page=1)
        {
            var Veri = Http.Yeni(Page);

            var Bolumler= HtmlParser.GetBolumler(Veri);
            var Pagenation = HtmlParser.GetPagenation(Veri);

            BolumlerSayfasi Sayfa = new BolumlerSayfasi();

            Sayfa.Title = "Yeni Eklenen Bölümler";
            Sayfa.Sayfalama = Pagenation;
            Sayfa.Bolumler = Bolumler;

            return Sayfa;
        }
        public BolumlerSayfasi GetPopuler(int Page = 1)
        {
            var Veri = Http.Populer(Page);

            var Bolumler = HtmlParser.GetBolumler(Veri);
            var Pagenation = HtmlParser.GetPagenation(Veri);

            BolumlerSayfasi Sayfa = new BolumlerSayfasi();

            Sayfa.Title = "Popüler Dizilerden Son Bölümler";
            Sayfa.Sayfalama = Pagenation;
            Sayfa.Bolumler = Bolumler;

            return Sayfa;
        }
        public Takvim GetTakvim()
        {
            var Veri = Http.Takvim();
            return HtmlParser.GetTakvim(Veri);
        }
        public EfsanelerSayfasi GetEfsaneler(int Page=1)
        {
            var Veri = Http.Efsaneler(Page);

            var Diziler = HtmlParser.GetEfsaneler(Veri);
            var Pagenation = HtmlParser.GetPagenation(Veri);

            EfsanelerSayfasi Sayfa = new EfsanelerSayfasi();

            Sayfa.Title = "Efsaneler";
            Sayfa.Sayfalama = Pagenation;
            Sayfa.Diziler = Diziler;

            return Sayfa;
        }

        public ArsivSayfasi Arsiv(int Page,ArsivFiltre Filtre)
        {
            var Query = "";

            bool ilk = true;

            if (!string.IsNullOrEmpty(Filtre.Tur))
            {
                var Bol = Filtre.Tur.Split(',');
                var Qicerik = "";
                foreach (var item in Bol)
                {
                    if (ilk)
                    {
                        Qicerik += "?"+Uri.EscapeDataString("tur[]")+"="+item.ToLower();
                        ilk = false;
                    }
                    else
                    {
                        Qicerik += "&" + Uri.EscapeDataString("tur[]")+"="+ item.ToLower();
                    }
                }

                Query += Qicerik;
            }

            if (!string.IsNullOrEmpty(Filtre.Ulke))
            {
                var Qicerik = "";

                if (ilk)
                {
                    Qicerik += "?" + Uri.EscapeDataString("ulke[]") + "=" + Filtre.Ulke.ToLower();
                    ilk = false;
                }
                else
                {
                    Qicerik += "&" + Uri.EscapeDataString("ulke[]") + "=" + Filtre.Ulke.ToLower();
                }
                Query += Qicerik;
            }

            if (!string.IsNullOrEmpty(Filtre.Yil))
            {
                if (ilk)
                {
                    Query += "?" + Uri.EscapeDataString("yil") + "=" + Filtre.Yil.ToLower();
                    ilk = false;
                }
                else
                {
                    Query += "&" + Uri.EscapeDataString("yil") + "=" + Filtre.Yil.ToLower();
                }
            }


            if (!string.IsNullOrEmpty(Filtre.Imdb))
            {
                if (ilk)
                {
                    Query += "?" + Uri.EscapeDataString("imdb") + "=" + Filtre.Imdb.ToLower();
                    ilk = false;
                }
                else
                {
                    Query += "&" + Uri.EscapeDataString("imdb") + "=" + Filtre.Imdb.ToLower();
                }
            }
            if (Filtre.OrderBY!=Siralama.varsayilan)
            {
                if (ilk)
                {
                    Query += "?" + Uri.EscapeDataString("orderby") + "=" + Filtre.OrderBY.ToString();
                    ilk = false;
                }
                else
                {
                    Query += "&" + Uri.EscapeDataString("orderby") + "=" + Filtre.OrderBY.ToString();
                }
            }
            var Veri = Http.Arsiv(Page,Query);

            var Diziler = HtmlParser.GetArsivDiziler(Veri);
            var Pagenation = HtmlParser.GetPagenation(Veri);

            ArsivSayfasi Sayfa = new ArsivSayfasi();

            Sayfa.Title = "Arşiv";
            Sayfa.Filtre = HtmlParser.GetArsivFiltre(Veri);
            Sayfa.Sayfalama = Pagenation;
            Sayfa.Diziler = Diziler;
            Sayfa.Siralama = Filtre.OrderBY;
            Sayfa.ToplamDizi= HtmlParser.GetArsivCount(Veri);
            
            return Sayfa;

        }
        public ArsivSayfasi Arsiv(ArsivFiltre Filtre) {
            return Arsiv(1, Filtre);
        }
        public AramaSayfasi Ara(string arama,int Page=1)
        {
            var Veri = Http.Ara(arama,Page);

            var Diziler = HtmlParser.GetAraDiziler(Veri);
            var Pagenation = HtmlParser.GetPagenation(Veri);

            AramaSayfasi Sayfa = new AramaSayfasi();

            Sayfa.Title = arama+" | Arama";
            
            Sayfa.Sayfalama = Pagenation;
            Sayfa.Diziler = Diziler;
            
            return Sayfa;
        }
        public BolumSayfasi Izle(string dizi, string sezon, string bolum,int player)
        {
            var Veri = Http.Dizi(dizi,sezon, bolum,player);

            var Bolum = HtmlParser.GetBolum(Veri, Http.GetBaseUrl()+Extra.fnSeo(dizi) + "-" + Extra.fnSeo(sezon) + "-" + Extra.fnSeo(bolum) + "-izle");
            
            
            return Bolum;
        }
    }
}
