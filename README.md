
# TransPortathon Hackathon

bu proje TransPortathon Hackathon için oluşturulmuştur.Backend ve Frontend olarak ikiye ayrılmaktadır


## Varsayılan Kullanıcı bilgileri
### Taşımacı Hesabı
email : sirket@sirket.com

password : 1234567

### Kullanıcı Hesabı
email : normal@normal.com

password : 1234567

## Kullanılan  teklonojiler ve sürümleri
Angular : 16.0.0

.NET : 6.0


## Gereklilikler
    1. .NET 6
    1. Angular
    1. Yarn
    1. Cloudinary Hesabı
## Local'de çalıştırmak için yapılması gerekenler

proje yerel dizine çekilir

```bash
  git clone https://link-to-project
```

### Backend
**Uyarı** : 

    1. database'e erişmek için WebAPI altındaki appsettings.json'daki ConnectionString'i kendi postgre connection url'inize göre değirmeniz gerekmektedir

    2. projede herhangi bir sorun olmaması için Cloudinary hesap bilgilerini appsettings.json'daki ilgili yerlere girilmelidir

Backend projesinin içindeki 'Persistance' dosyasının içinde girilip aşağıdaki kod yazılır

```bash
  dotnet ef database update -s ../WebAPI
```


projenin backend'i çalışmaya hazırdır 🚀

### Frontend

Frontend projesinin içine girilir ve aşağıdaki kod yazılır

```bash
  yarn install
```

işlem tamamlandıktan sonra aşağıdaki kod yazılır
```bash
  ng serve --o
```
