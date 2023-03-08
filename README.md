# 2FAService ASP.NET CORE WEBAPI .NET 6
2 factory authentication Service

Welcome to the 2FAService wiki!

Before you start testing  this app please refer to the intructions above

* Step 1: Configurations  in **appsettings.json**

 "Service2FASetting":{
    "CodeValidityDuration": "00:00:05", 
    "CodeNumbersCount": 6,
    "NumberOfLettersInCode": 0,
    "NumberOfCodeRequestPerNumber": 5,
    "StoragePath": "C:\\2FASERVICE\\Codes.storage"
  }

1. **CodeValidityDuration** : The Code validity in TimeSpan
2. **CodeNumbersCount** -  the number of numbers in the generated code : 0,1,2,3,...6
3. **NumberOfLettersInCode** -  the number of the letters in the generated code: 0,1,2,3,...6
4. **NumberOfCodeRequestPerNumber** the limit of the requested code by Phone number
5. **StoragePath** -  The path where the data will be stored

after Launch the App  you can testing the 2 endpoints by first generating the code for a number and try authenticate the number with the generated code



![start](https://user-images.githubusercontent.com/9817325/223734511-7556c8d7-113f-4cde-aea9-82ec4c09ec7c.JPG)


