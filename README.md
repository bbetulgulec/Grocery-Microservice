# 🥬 Mevsim Manav

Konya'dan İstanbul Eyüp'e uzanan 60 yıllık bir manav geleneği için geliştirilmiş, **mikroservis mimarisi** üzerine
kurulu web uygulaması. Ziyaretçiler ürünleri, iletişim bilgilerini ve çalışma saatlerini görür; işletme sahibi her şeyi
admin panelinden yönetir. Proje AWS üzerinde Docker konteynerleri ile yayına alınmıştır.

![Ana Sayfa](docs/screenshots/home.png)

## ✨ Öne Çıkanlar

- Bağımsız çalışan, ayrı ayrı yayınlanabilen **mikroservisler**
- Tek giriş noktası olarak **Ocelot API Gateway**
- **IdentityServer + JWT** ile merkezi kimlik doğrulama ve yetkilendirme
- **CQRS + Mediator** ile okuma ve yazma işlemlerinin ayrılması
- **RabbitMQ** ile servisler arası asenkron haberleşme
- **Redis** ile hızlı önbellekleme
- **Docker + Portainer** ile konteyner yönetimi
- **AWS (EC2, S3, VPC, IAM)** üzerinde canlı yayın
- Çoklu dil desteği (Localization)

## 📸 Ekran Görüntüleri

### 🏠 Ana Sayfa

| | |
|---|---|
| ![Ana Sayfa](docs/screenshots/home.png)<br>**Ana Sayfa** | ![Menü](docs/screenshots/menu.png)<br>**Menü** |
| ![Öne Çıkanlar](docs/screenshots/highlight.png)<br>**Öne Çıkanlar** | ![Neden Biz](docs/screenshots/choose.png)<br>**Neden Biz** |
| ![Kalite](docs/screenshots/quality.png)<br>**Kalite** | ![Sertifikalar](docs/screenshots/certification.png)<br>**Sertifikalar** |
| ![Video](docs/screenshots/video.png)<br>**Video** | ![Galeri](docs/screenshots/gallery.png)<br>**Galeri** |
| ![Konum](docs/screenshots/location.png)<br>**Konum** | ![Footer](docs/screenshots/footer.png)<br>**Footer** |

### 🛒 Ürünler ve Sipariş

| | |
|---|---|
| ![Ürün Listesi](docs/screenshots/productList.png)<br>**Ürün Listesi** | ![Filtreleme](docs/screenshots/filterList.png)<br>**Filtreleme** |
| ![Ürün Detayı](docs/screenshots/productDetail.png)<br>**Ürün Detayı** | ![Sepet](docs/screenshots/basket.png)<br>**Sepet** |
| ![Sipariş](docs/screenshots/cardOrder.png)<br>**Sipariş** | ![Sipariş Tamamlandı](docs/screenshots/succesOrder.png)<br>**Sipariş Tamamlandı** |

### 💬 Yorumlar ve İletişim

| | |
|---|---|
| ![Yorumlar](docs/screenshots/comment.png)<br>**Yorumlar** | ![Yorum Ekle](docs/screenshots/addComment.png)<br>**Yorum Ekle** |
| ![Yorum Gönder](docs/screenshots/sendComment.png)<br>**Yorum Gönder** | ![Mesaj](docs/screenshots/message.png)<br>**Mesaj** |

### 👤 Üyelik

| | |
|---|---|
| ![Giriş](docs/screenshots/login.png)<br>**Giriş** | ![Kayıt](docs/screenshots/register.png)<br>**Kayıt** |

### 🛠️ Admin Paneli

<details>
<summary>Admin ekran görüntülerini göster</summary>

| | |
|---|---|
| ![Admin Giriş](docs/screenshots/adminLogib.png)<br>**Admin Giriş** | ![Hakkımızda](docs/screenshots/adminAbout.png)<br>**Hakkımızda** |
| ![Kategoriler](docs/screenshots/adminCategory.png)<br>**Kategoriler** | ![Ürünler](docs/screenshots/adminProduct.png)<br>**Ürünler** |
| ![Öne Çıkanlar](docs/screenshots/adminHighlight.png)<br>**Öne Çıkanlar** | ![Yorumlar](docs/screenshots/adminComment.png)<br>**Yorumlar** |
| ![Öneriler](docs/screenshots/adminSugsession.png)<br>**Öneriler** | ![Sepetler](docs/screenshots/adminBasket.png)<br>**Sepetler** |
| ![Kullanıcı](docs/screenshots/adminUser.png)<br>**Kullanıcı** | ![Kullanıcılar](docs/screenshots/adminUsers.png)<br>**Kullanıcılar** |
| ![Galeri](docs/screenshots/admiGalley.png)<br>**Galeri** | |

</details>

## 🏗️ Mimari Genel Bakış

```mermaid
flowchart LR
    Client[Web Arayüzü] --> Gateway[Ocelot API Gateway]
    Client -- giriş --> Identity[IdentityServer]
    Gateway --> S1[Category]
    Gateway --> S2[Order]
    Gateway --> S3[Payment]
    Gateway --> S4[Basket]
    S1 --> Mongo[(MongoDB)]
    S2 --> Postgres[(PostgreSQL)]
    S4 --> Redis[(Redis)]
    S1 <-->|olaylar| Rabbit{{RabbitMQ}}
    S2 <-->|olaylar| Rabbit
    Identity -. JWT .-> Gateway
```

İstek akışı: Kullanıcı arayüz üzerinden gelir, Gateway isteği doğru mikroservise yönlendirir. Korumalı uç noktalarda
IdentityServer'ın ürettiği JWT doğrulanır. Servisler kendi veritabanlarına sahiptir, birbirleriyle gerektiğinde RabbitMQ
üzerinden konuşur.

## 🛠️ Kullanılan Teknolojiler

### 🧩 Mimari ve Tasarım Desenleri

| Teknoloji | Ne İşe Yarar | Projedeki Kullanımı |
|-----------|--------------|---------------------|
| **Mikroservis Mimarisi** | Uygulamayı bağımsız küçük servislere böler | Category, Order, Payment ve Basket servisleri ayrı çalışır; her birinin kendi veritabanı ve Docker imajı var. Biri değişince diğerleri etkilenmez |
| **Onion Architecture** | Katmanlı, bağımlılıkları içe doğru akan yapı | Her serviste domain ortada, application onun etrafında, infrastructure ve API en dışta. İş kuralları veritabanından ve framework'ten bağımsız |
| **CQRS** | Okuma (Query) ve yazma (Command) işlemlerini ayırır | Veri okuyan ve değiştiren işlemler ayrı sınıflarda; her biri tek bir işten sorumlu |
| **Mediator Deseni** | Nesneler arası iletişimi tek noktadan yönetir | Controller'lar sadece isteği Mediator'a gönderir, doğru handler'ı o çalıştırır. Controller'lar ince kalır |

### 🗄️ Veritabanı, Önbellek ve Mesajlaşma

| Teknoloji | Tür | Ne İşe Yarar | Projedeki Kullanımı |
|-----------|-----|--------------|---------------------|
| **MongoDB** | NoSQL (doküman) | Esnek şemalı veri saklama | Category servisinin verileri |
| **PostgreSQL** | İlişkisel veritabanı | Tablolar arası ilişkili, tutarlı veri | Order servisinin verileri |
| **Redis** | Bellek içi depo | Çok hızlı okuma, önbellekleme | Basket servisi, sık okunan veriler |
| **RabbitMQ** | Mesaj kuyruğu | Servisler arası asenkron haberleşme | Servisler olay yayınlar, ilgilenen servis kendi hızında tüketir. Biri kapalıysa mesaj kaybolmaz |

### 🔐 Kimlik Doğrulama ve Güvenlik

| Teknoloji | Ne İşe Yarar | Projedeki Kullanımı |
|-----------|--------------|---------------------|
| **IdentityServer** | Merkezi kimlik ve yetki sunucusu | Giriş ve token üretimi tek serviste. Diğer servisler şifre bilmez, sadece token'a güvenir |
| **JSON Web Token (JWT)** | İmzalı, kendi içinde bilgi taşıyan token | Her istekte token gönderilir, servisler imzayı doğrulayıp kullanıcıyı ve yetkilerini anlar. Sunucuda oturum tutulmaz |
| **Client Credentials** | Servisten servise (kullanıcısız) kimlik doğrulama akışı | Servisler birbirine istek atarken kendi kimlikleriyle token alır |
| **Token Handler** | Giden isteklere token'ı otomatik ekler | HttpClient DelegatingHandler olarak yazıldı; token'ı ekler, süresi dolunca yeniler. Her yerde elle token almak gerekmez |

### 🚪 API Gateway

| Teknoloji | Ne İşe Yarar | Projedeki Kullanımı |
|-----------|--------------|---------------------|
| **Ocelot** | Tek giriş noktası, yönlendirme | Arayüz servis adreslerini bilmez, hepsi Gateway'e gider. İstekleri doğru servise yönlendirir, JWT doğrulamasını merkezde yapar, servislerin doğrudan dışarı açılmasını engeller |

### 🎨 Arayüz

| Teknoloji | Ne İşe Yarar | Projedeki Kullanımı |
|-----------|--------------|---------------------|
| **Hazır Template** | Hazır tasarım iskeleti | Arayüzü sıfırdan çizmek yerine template'i projeye uyarladım; sayfalar, footer ve admin ekranları üzerinde düzenlendi |
| **Localization** | Çoklu dil desteği | Metinler kod içine gömülmedi, kaynak dosyalarından `IStringLocalizer` ile çekilir. Yeni dil = yeni kaynak dosyası |
| **Razor** | Sunucu taraflı sayfa üretimi | `@Model` ve `@L[...]` ile dinamik içerik |
| **Bootstrap 5** | Responsive tasarım | Grid sistemi ile telefon, tablet ve masaüstü uyumu |
| **Font Awesome** | İkon kütüphanesi | Marka, iletişim ve ödeme ikonları |
| **JavaScript** | İstemci tarafı etkileşim | Küçük dinamik davranışlar |

### ☁️ Altyapı ve Yayın (DevOps)

| Teknoloji | Ne İşe Yarar | Projedeki Kullanımı |
|-----------|--------------|---------------------|
| **Docker** | Uygulamayı konteyner olarak paketler | Her mikroservis kendi imajında. Aynı imaj geliştirme ortamında da sunucuda da aynı çalışır |
| **Portainer** | Docker için web yönetim paneli | Konteynerleri arayüzden başlatma, durdurma ve log izleme |
| **AWS EC2** | Sanal sunucu | Konteynerlerin çalıştığı canlı sunucu |
| **AWS S3** | Nesne depolama | Görsel gibi dosyaların saklanması; sunucu değişse bile dosyalar kaybolmaz |
| **AWS VPC** | Özel sanal ağ | Hangi servislerin internete açık, hangilerinin sadece iç ağda olacağının belirlenmesi. Veritabanları dışarıdan erişilemez |
| **AWS IAM** | Erişim ve yetki yönetimi | En az yetki ilkesi; örneğin uygulama sadece ilgili S3 bucket'ına erişebilir |

## 🚀 Kurulum (Yerel Ortam)

**Gereksinimler:** .NET SDK [sürüm], Docker ve Docker Compose

```bash
# 1. Projeyi klonla
git clone https://github.com/[kullanici-adi]/[repo-adi].git
cd [repo-adi]

# 2. Ortam değişkenlerini ayarla (aşağıya bak)

# 3. Tüm servisleri ayağa kaldır
docker compose up -d
```
### ⚙️ Yapılandırma

Bağlantı bilgileri ve gizli anahtarlar koda yazılmadı, ortam değişkenleri ile verilir:

```
ConnectionStrings__MongoDb=[...]
ConnectionStrings__PostgreSql=[...]
Redis__Host=[...]
RabbitMq__Host=[...]
IdentityServer__Authority=[...]
AWS__AccessKey / AWS__SecretKey / AWS__BucketName=[...]
```

> ⚠️ Gerçek şifreleri ve AWS anahtarlarını GitHub'a yükleme. `.env` dosyasını `.gitignore`'a ekle.

## 🗺️ Yol Haritası

-  Mikroservis yapısı ve API Gateway
-  IdentityServer ile kimlik doğrulama
-  Docker ile paketleme ve AWS'ye yayın
-  Çoklu dil desteği
-  CI: GitHub Actions ile otomatik build ve test
-  CD: Otomatik deploy (GitHub Actions ile AWS EC2'ye)
-  Entity Framework Core entegrasyonu
-  Unit ve entegrasyon testlerinin artırılması

## 🌐 Web Sitesi

🔗 **[www.mevsimmanav.com.tr](https://www.mevsimmanav.com.tr/)**


## 📄 Lisans

Bu proje MIT lisansı ile paylaşılmaktadır.