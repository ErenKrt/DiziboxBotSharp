using System;
using System.Collections.Generic;
using System.Text;
using EpEren.DiziBox;
namespace EpEren.DiziBox
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            DbxCore Core = new DbxCore();
            var Ara = Core.Ara("Mr.Robot");

            var Arsiv = Core.Arsiv(new Classes.Modals.Sayfalar.ArsivFiltre());

            var ArsivFiltre = Core.Arsiv(1, new Classes.Modals.Sayfalar.ArsivFiltre()
            {
                Imdb="5",
                OrderBY=Classes.Modals.Sayfalar.Siralama.imdb,
                Tur="komedi",
                Ulke="amerika",
                Yil="2019"
            });

            var Efsaneler = Core.GetEfsaneler();
            var Populerler= Core.GetPopuler();
            var Takvim = Core.GetTakvim();
            var Yeniler = Core.GetYeniler();

            var BoumIzle = Core.Izle(Yeniler.Bolumler[0].Isim, Yeniler.Bolumler[0].Sezon, Yeniler.Bolumler[0].Bolum, 1);

            
        }
    }
}
