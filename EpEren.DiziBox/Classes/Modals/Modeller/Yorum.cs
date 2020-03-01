using System;
using System.Collections.Generic;
using System.Text;

namespace EpEren.DiziBox.Classes.Modals.Modeller
{
    public class Yorum
    {
        public string Isım{ get; set; }
        public string Icerik { get; set; }
        public string Tarih { get; set; }
        public string Like { get; set; }
        public string Dislike { get; set; }

        public Pagenation Sayfalama { get; set; }
    }
}
