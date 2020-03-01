using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using EpEren.DiziBox.Classes.Enums;
using EpEren.DiziBox.Classes.Modals.Modeller;
using EpEren.DiziBox.Classes.Modals.Sayfalar;
using EpEren.DiziBox.Helpers;

namespace EpEren.DiziBox.Classes
{
    public static class HtmlParser
    {
        public static List<ShortDiziBolum> GetBolumler(string HTML)
        {
            var Liste = new List<ShortDiziBolum>();

            HtmlAgilityPack.HtmlDocument Document = new HtmlAgilityPack.HtmlDocument();
            Document.LoadHtml(HTML);

            var Dokumentler= Document.DocumentNode.SelectNodes("/html/body/main/div[4]/section/div[2]/article");

            foreach (var Bolum in Dokumentler)
            {
                var Resim = DbxCore.Http.GetImage(Bolum.SelectSingleNode("figure/a[2]/img").Attributes["src"].Value);
                var Dizi = Bolum.SelectSingleNode("figure/figcaption/a/b[1]").InnerText;
                var Sezon= Bolum.SelectSingleNode("figure/figcaption/a/span").InnerText;
                var BBolum = Bolum.SelectSingleNode("figure/figcaption/a/b[2]").InnerText;
                var Tarih = Bolum.SelectSingleNode("figure/figcaption/div/div[2]").InnerText;
                var Dil = Bolum.SelectSingleNode("figure/figcaption/div/div[1]/img").Attributes["src"].Value;

                Diller DilSecenek;
                if (Dil == "https://www.dizibox.pw/altyazi.png")
                {
                    DilSecenek = Diller.Turkce;
                }else
                {
                    DilSecenek = Diller.Ingilizce;
                }

                Liste.Add(new ShortDiziBolum()
                {
                    Resim = Resim,
                    Isim = Dizi,
                    Sezon = Sezon,
                    Bolum = BBolum,
                    Sezon_Bolum = Sezon + " " + BBolum,
                    Dil = DilSecenek,
                    Tarih = Tarih
                });

            }

            return Liste;
        }

        public static Pagenation GetPagenation(string HTML)
        {
            var Pagenationx = new Pagenation();
            HtmlAgilityPack.HtmlDocument Document = new HtmlAgilityPack.HtmlDocument();
            Document.LoadHtml(HTML);

            var HtmlPagenation = Document.DocumentNode.SelectSingleNode("//*[@class=\"woca-pagination\"]");

            var Sayfalar = HtmlPagenation.SelectSingleNode("span[1]").InnerText;

            Pagenationx.bulunan = Convert.ToInt32((Sayfalar.Split('/')[0]).Trim());
            Pagenationx.max = Convert.ToInt32((Sayfalar.Split('/')[1]).Trim());
            Pagenationx.min = 1;

            return Pagenationx;
        }

        public static Takvim GetTakvim(string HTML)
        {
            var Pagenationx = new Pagenation();
            HtmlAgilityPack.HtmlDocument Document = new HtmlAgilityPack.HtmlDocument();
            Document.LoadHtml(HTML);

            var Table = Document.DocumentNode.SelectSingleNode("/html/body/main/div[4]/table");
            var Gunler = Table.SelectNodes("thead/tr/td");
            var Icerikler= Table.SelectNodes("tbody/tr/td");

            var Donut = new Takvim();
            Donut.TakvimListe = new List<TakvimBirGun>();
            for (int i = 0; i < Gunler.Count; i++)
            {
                var Gun = Gunler[i];
                var Icerik = Icerikler[i].SelectNodes("a");

                var TBirGun = new TakvimBirGun();
                TBirGun.Gün = Gun.InnerText;
                TBirGun.Diziler = new List<TakvimDizi>();

                foreach (var SIcerik in Icerik)
                {
                    TBirGun.Diziler.Add(new TakvimDizi() { Dizi = SIcerik.InnerText });
                }

                Donut.TakvimListe.Add(TBirGun);

            }

            return Donut;
        }

        public static List<ShortDizi> GetEfsaneler(string HTML)
        {
            HtmlAgilityPack.HtmlDocument Document = new HtmlAgilityPack.HtmlDocument();
            Document.LoadHtml(HTML);

            var Diziler = Document.DocumentNode.SelectNodes("/html/body/main/div[4]/section/div[2]/article");

            var Liste = new List<ShortDizi>();

            foreach (var Dizi in Diziler)
            {
                var Isim = Dizi.SelectSingleNode("figure/figcaption/a").InnerText;
                var Imdb = Dizi.SelectSingleNode("figure/figcaption/div/div[1]").InnerText.Trim();
                var Tarih = Dizi.SelectSingleNode("figure/figcaption/div/div[2]").InnerText.Trim();
                var Resim = DbxCore.Http.GetImage(Dizi.SelectSingleNode("figure/a[2]/img").Attributes["src"].Value);
                Liste.Add(new ShortDizi()
                {
                    Resim=Resim,
                    Isim = Isim.Replace("\n",string.Empty),
                    Imdb = Imdb,
                    Yil = Tarih
                }) ;
            }
            return Liste;
        }
        public static List<ArsivShortDizi> GetArsivDiziler(string HTML)
        {
            HtmlAgilityPack.HtmlDocument Document = new HtmlAgilityPack.HtmlDocument();
            Document.LoadHtml(HTML);

            var Diziler = Document.DocumentNode.SelectNodes("/html/body/main/div[4]/div[2]/article");

            var Liste = new List<ArsivShortDizi>();

            foreach (var Dizi in Diziler)
            {
                var Isim = Dizi.SelectSingleNode("div/h3/a").InnerText;
                var ImdbObje = Dizi.SelectSingleNode("div/div[1]/span[3]");
                var Imdb = "";
                if (ImdbObje != null)
                {
                    Imdb = ImdbObje.InnerText.Trim();
                }
                var Tur = Dizi.SelectSingleNode("div/div[1]/span[1]").InnerText.Split('&')[0].Trim();
                var TarihYer = Dizi.SelectSingleNode("div/div[1]/span[2]").InnerText.Replace("&nbsp;",string.Empty).Replace("|",string.Empty).Trim();
                var Resim = DbxCore.Http.GetImage(Dizi.SelectSingleNode("figure/a/img").Attributes["src"].Value);
                var Aciklama = Dizi.SelectSingleNode("div/div[2]").InnerText.Trim();
                Liste.Add(new ArsivShortDizi()
                {
                    Aciklama = Aciklama,
                    Yil = TarihYer.Split('-')[0].Trim(),
                    Ulke = TarihYer.Split('-')[1].Trim(),
                    Resim = Resim,
                    Isim = Isim.Replace("\n", string.Empty),
                    Imdb = Imdb,
                    Tur = Tur
                });
            }
            return Liste;
        }
        public static ArsivFiltreSayfa GetArsivFiltre(string HTML)
        {
            HtmlAgilityPack.HtmlDocument Document = new HtmlAgilityPack.HtmlDocument();
            Document.LoadHtml(HTML);
            var Liste = new ArsivFiltreSayfa();

            var FitreAlan = Document.DocumentNode.SelectSingleNode("/html/body/main/div[4]/div[1]/aside");

            var UlkeList = new List<string>();
            var TurList = new List<string>();

            var Turler = FitreAlan.SelectNodes("form/div[1]/ul/li");
            var Ulkeler = FitreAlan.SelectNodes("form/div[2]/ul/li");

            foreach (var item in Turler)
            {
                TurList.Add(item.InnerText);
            }

            foreach (var item in Ulkeler)
            {
                UlkeList.Add(item.InnerText);
            }

            Liste.Turler = TurList;
            Liste.Ulkeler = UlkeList;

            return Liste;
        }

        public static int GetArsivCount(string HTML)
        {
            HtmlAgilityPack.HtmlDocument Document = new HtmlAgilityPack.HtmlDocument();
            Document.LoadHtml(HTML);

            return Convert.ToInt32(Document.DocumentNode.SelectSingleNode("/html/body/main/div[4]/div[2]/div[1]/h1/a/small").InnerText.Split(' ')[0].Trim());
        }
        public static List<ArsivShortDizi> GetAraDiziler(string HTML)
        {
            HtmlAgilityPack.HtmlDocument Document = new HtmlAgilityPack.HtmlDocument();
            Document.LoadHtml(HTML);

            var Diziler = Document.DocumentNode.SelectNodes("/html/body/main/section/div[2]/article");

            var Liste = new List<ArsivShortDizi>();

            if (Diziler == null) return Liste;

            foreach (var Dizi in Diziler)
            {
               
                var Isim = Dizi.SelectSingleNode("div/h3/a").InnerText;
                var ImdbObje = Dizi.SelectSingleNode("div/div[1]/span[3]/b");
                var Imdb = "";
                if (ImdbObje != null)
                {
                    Imdb = ImdbObje.InnerText.Trim();
                }
                var Tur = Dizi.SelectSingleNode("div/div[1]/span[1]").InnerText.Split('&')[0].Trim();
                string TarihYer = "";
                if (Tur == "")
                {
                    TarihYer = Dizi.SelectSingleNode("div/div[1]/span[1]").InnerText.Replace("&nbsp;", string.Empty).Replace("|", string.Empty).Trim();
                }
                else
                {
                    TarihYer = Dizi.SelectSingleNode("div/div[1]/span[2]").InnerText.Replace("&nbsp;", string.Empty).Replace("|", string.Empty).Trim();
                }

                if(Tur=="" && ImdbObje == null)
                {
                    Imdb= Dizi.SelectSingleNode("div/div[1]/span[2]/b").InnerText.Trim();
                }

                var Resim = DbxCore.Http.GetImage(Dizi.SelectSingleNode("figure/a/img").Attributes["src"].Value);
                var Aciklama = Dizi.SelectSingleNode("div/div[2]").InnerText.Trim();
                Liste.Add(new ArsivShortDizi()
                {
                    Aciklama = Aciklama,
                    Yil = TarihYer.Split('-')[0].Trim(),
                    Ulke = TarihYer.Split('-')[1].Trim(),
                    Resim = Resim,
                    Isim = Isim.Replace("\n", string.Empty),
                    Imdb = Imdb,
                    Tur = Tur
                });
            }
            return Liste;
        }
        public static BolumSayfasi GetBolum(string HTML,string CurrentUrl)
        {
            HtmlAgilityPack.HtmlDocument Document = new HtmlAgilityPack.HtmlDocument();
            Document.LoadHtml(HTML);

            var BolumAdi = Document.DocumentNode.SelectSingleNode("/html/body/main/div[4]/div[1]/h1/span[1]/span").InnerText.Trim();
            BolumAdi += " " + Document.DocumentNode.SelectSingleNode("/html/body/main/div[4]/div[1]/h1/span[2]").InnerText.Trim();
            BolumAdi += " " + Document.DocumentNode.SelectSingleNode("/html/body/main/div[4]/div[1]/h1/small").InnerText.Trim();

            var PlayerList = new List<Player>();

            Player AktifPlayer = null;

            var Playerlar = Document.DocumentNode.SelectNodes("/html/body/main/div[4]/div[2]/div[1]/section/div[1]/select/option");
            var sira = 1;
            foreach (var item in Playerlar)
            {
                try
                {
                    if (item.Attributes["selected"].Value == "selected")
                    {
                        AktifPlayer = new Player() { Sira = sira, Isim = item.InnerText.Trim() };
                    }
                }
                catch { }
                PlayerList.Add(new Player() { Sira = sira, Isim = item.InnerText.Trim() });
                sira++;
            }

            var Iframe = Document.DocumentNode.SelectSingleNode("//iframe").Attributes["src"].Value;
            var IkiIframeVeri = DbxCore.Http.KillTheProtocol(CurrentUrl, Iframe);

            HtmlAgilityPack.HtmlDocument DocumentIkı = new HtmlAgilityPack.HtmlDocument();
            DocumentIkı.LoadHtml(IkiIframeVeri);

            var IframeUc = DocumentIkı.DocumentNode.SelectSingleNode("//iframe");
            var AktifIframe = "";
            
            if (IframeUc != null)
            {
                AktifIframe = IframeUc.Attributes["src"].Value;
            }
            else if (new Regex("file:\"(.*?)\"").IsMatch(IkiIframeVeri))
            {
                var Bulunan= new Regex("file:\"(.*?)\"").Match(IkiIframeVeri);
                AktifIframe = Bulunan.Groups[1].Value;
            }
            else if (new Regex("file: \"(.*?)\"").IsMatch(IkiIframeVeri))
            {
                var Bulunan = new Regex("file: \"(.*?)\"").Match(IkiIframeVeri);
                AktifIframe = Bulunan.Groups[1].Value;
            }else if (new Regex("atob").IsMatch(IkiIframeVeri))
            {
                var AtobVeri = new Regex(@"unescape\((.*?)\)").Match(IkiIframeVeri);
                var AtobKod = Encoding.UTF8.GetString(Convert.FromBase64String(HttpUtility.HtmlEncode(HttpUtility.UrlDecode(AtobVeri.Groups[1].Value)).Replace("&quot;", string.Empty)));
                
                HtmlAgilityPack.HtmlDocument DocumentUc = new HtmlAgilityPack.HtmlDocument();
                DocumentUc.LoadHtml(AtobKod);

                var IframeDort = DocumentUc.DocumentNode.SelectSingleNode("//iframe");
                if (IframeDort != null)
                {
                    AktifIframe = IframeDort.Attributes["src"].Value;
                }
            }

            var Sayfa = new BolumSayfasi();
            Sayfa.Yorumlar = null;
            Sayfa.Playerlar = PlayerList;
            Sayfa.AktifPlayer = AktifPlayer;
            Sayfa.Bolum = AktifIframe;
            Sayfa.Title = BolumAdi;
            return Sayfa;
        }
    }
}
