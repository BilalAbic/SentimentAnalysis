# Duygu Analizi (Sentiment Analysis)

Nesne Tabanli Programlama dersi odevi olarak hazirlanan, ML (Machine Learning) ve metin isleme konularinda alistirma yapmak amaciyla gelistirilmis bir Windows Forms uygulamasidir.

## Proje Hakkinda

Bu proje, Turkce metinlerin duygu analizini yapmak icin **ML.NET** ve **Naive Bayes** algoritmalarini kullanan hibrit bir yaklasim sunmaktadir. Kullanici tarafindan girilen metinler "Olumlu" veya "Olumsuz" olarak siniflandirilir.

### Kullanilan Teknolojiler
- .NET 8
- Windows Forms
- ML.NET (SDCA Logistic Regression)
- Naive Bayes Classifier

## Program Akisi

```
[Kullanici Metni Girer]
         |
         v
[TextPreprocessor - Metin Temizleme]
         |
         v
[Anahtar Kelime Analizi] + [ML.NET Tahmini]
         |
         v
[Hibrit Karar (Agirlikli Ortalama)]
         |
         v
[Sonuc: Olumlu / Olumsuz + Guven Skoru]
```

## Ekran Goruntuleri

| Ana Ekran | Olumlu Sonuc | Olumsuz Sonuc |
|-----------|--------------|---------------|
| ![Ana Ekran](screenshots/1.png) | ![Olumlu](screenshots/2.png) | ![Olumsuz](screenshots/3.png) |

## Referans

Bu proje gelistirilirken asagidaki kaynak referans alinmistir:

- [Duygu Analizi Sistemini Iyilestirme Plani](docs/Duygu%20Analizi%20Sistemini%20?yile?tirme%20Plan?.pdf) *(Gemini AI ile hazirlanmistir)*

## Indirme

Programi buradan indirebilirsiniz: [Releases](https://github.com/BilalAbic/SentimentAnalysis/releases)

## Lisans

Bu proje egitim amacli hazirlanmistir.
