

Projede veri tabaný olarak Microsoft SQL Server kullanýldý. Migration eklemek için kullanýlan komut:
    dotnet ef migrations add mig1 --project QAM.Data --startup-project QAM.Wapi
Eklenen Migration'larýn uygulanmasý için kullanýlan komut ise þu þekildedir:
       dotnet ef database update --project "./QAM.Data" --startup-project "./QAM.Wapi"