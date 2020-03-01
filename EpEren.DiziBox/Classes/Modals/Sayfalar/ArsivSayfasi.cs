using EpEren.DiziBox.Classes.Modals.Modeller;
using System;
using System.Collections.Generic;
using System.Text;

namespace EpEren.DiziBox.Classes.Modals.Sayfalar
{
    public enum Siralama { varsayilan, imdb, yorum };
    public class ArsivSayfasi : Sayfa
    {
        public int ToplamDizi { get; set; }
        public List<ArsivShortDizi> Diziler { get; set; }
        public Siralama Siralama { get; set; }
        public EpEren.DiziBox.Classes.Modals.Modeller.ArsivFiltreSayfa Filtre { get; set; }
    }
    public class ArsivFiltre
    {
        public string Tur{ get; set; }
        public string Ulke { get; set; }
        public string Yil { get; set; }
        public string Imdb{ get; set; }
        public Siralama OrderBY { get; set; }
    }
}
