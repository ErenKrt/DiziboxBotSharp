using System;
using System.Collections.Generic;
using System.Text;

namespace EpEren.DiziBox.Classes.Modals.Modeller
{
    public class Takvim
    {
        public List<TakvimBirGun> TakvimListe { get; set; }
    }
    public class TakvimBirGun
    {
        public string Gün { get; set; }
        public List<TakvimDizi> Diziler { get; set; }
    }

    public class TakvimDizi
    {
        public string Dizi{ get; set; }
    }
    
}
