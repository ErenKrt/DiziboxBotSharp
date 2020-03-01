using System;
using System.Collections.Generic;
using System.Text;
using EpEren.DiziBox.Classes.Enums;

namespace EpEren.DiziBox.Classes.Modals.Modeller
{
    public class ShortDiziBolum
    {
        public string Isim{ get; set; }
        public string Sezon { get; set; }
        public string Bolum { get; set; }
        public string Sezon_Bolum { get; set; }
        public string Resim { get; set; }
        public string Tarih { get; set; }
        public Diller Dil{ get; set; }
    }
}
