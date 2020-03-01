# DiziboxBotSharp / DiziBox Bot / Yabancı Dizi Botu | .Net

Botun çalışma prensibi bir kullanıcı gibi  **dizibox.pw** adresine girerek **HtmlParser** mantığında alanları (**div**/**section**/**vb..**) parçalıyor ve size özel **object**ler olarak döndürüyor

---
- [Bilgilendirme](#bilgilendirme)
- [Kurulum](#kurulum)
---


# Bilgilendirme
> .NetCore >= 3.1

Sürümü kaynak kodlarını indirtikten sonra build ederek değişebilirsiniz.
# Kurulum
> Release kısmından güncel **dll**leri indirip projenize ekleyerek kullanabilirsiniz.
## Kod Kullanımları
```csharp
DbxCore Core = new DbxCore();

var Ara = Core.Ara("Mr.Robot"); //dizibox.pw/?s=*

var Arsiv = Core.Arsiv(new Classes.Modals.Sayfalar.ArsivFiltre()); //dizibox.pw/arsiv/

var ArsivFiltre = Core.Arsiv(1, new Classes.Modals.Sayfalar.ArsivFiltre() 
   {
      Imdb="5",
      OrderBY=Classes.Modals.Sayfalar.Siralama.imdb,
      Tur="komedi", //Tur="komedi,dram",
      Ulke="amerika",
      Yil="2019"
   });

var Efsaneler = Core.GetEfsaneler(); //dizibox.pw/efsane-diziler/
var Populerler= Core.GetPopuler(); //dizibox.pw/tum-bolumler/?tip=populer
var Takvim = Core.GetTakvim(); //dizibox.pw/dizi-takvimi/
var Yeniler = Core.GetYeniler(); //dizibox.pw//tum-bolumler/

var BoumIzle = Core.Izle(Yeniler.Bolumler[0].Isim, Yeniler.Bolumler[0].Sezon, Yeniler.Bolumler[0].Bolum, 1); 
```
---
> Not: **Dizibox.pw** sahipleri/yetkilileri güvenlik geliştirmek yerine mail atıp geliştirmeyi durdurmamı isteyebilirsiniz. Aksi takdirde iletişim haline girmeden update yaparak engellemeye çalışıriseniz ben de update yapacağım. xD

>Yakında reklamsız izlenmesi sağlamak için Extension yazılmasına başlanacaktır.
---
Geliştirci: &copy; [ErenKrt](https://www.instagram.com/ep.eren/)
