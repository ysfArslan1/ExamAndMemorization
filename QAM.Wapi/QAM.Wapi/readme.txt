

Projede veri taban� olarak Microsoft SQL Server kullan�ld�. Migration eklemek i�in kullan�lan komut:
    dotnet ef migrations add mig1 --project QAM.Data --startup-project QAM.Wapi
Eklenen Migration'lar�n uygulanmas� i�in kullan�lan komut ise �u �ekildedir:
       dotnet ef database update --project "./QAM.Data" --startup-project "./QAM.Wapi"