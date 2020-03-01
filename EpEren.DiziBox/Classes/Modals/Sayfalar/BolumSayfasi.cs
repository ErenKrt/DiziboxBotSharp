using EpEren.DiziBox.Classes.Modals.Modeller;
using System;
using System.Collections.Generic;
using System.Text;

namespace EpEren.DiziBox.Classes.Modals.Sayfalar
{
    public class BolumSayfasi : Sayfa
    {
        public string Bolum { get; set; }
        public Player AktifPlayer { get; set; }
        public List<Player> Playerlar { get; set; }
        public List<Yorum> Yorumlar { get; set; }
    }
}
