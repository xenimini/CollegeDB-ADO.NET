# CollegeDB — ADO.NET + Dapper

Консольное приложение для работы с базой данных колледжа (студенты и группы).  
Используется SQL Server Express + ADO.NET / Dapper.

## Что нужно установить

1. [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
2. [SQL Server Management Studio (SSMS)](https://aka.ms/ssmsfullsetup)
3. [Visual Studio](https://visualstudio.microsoft.com/ru/downloads/) с рабочей нагрузкой «Разработка классических приложений .NET»

## Как запустить

### 1. База данных

1. Открой SSMS и подключись к `localhost\SQLEXPRESS` (Windows Authentication).
2. Создай базу `CollegeDB`.
3. Выполни скрипт создания таблиц (лежит в папке `Database` или создай вручную):

```sql
USE CollegeDB;
GO

CREATE TABLE Groups (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);

CREATE TABLE Students (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Age INT NULL,
    GroupId INT NULL,
    CONSTRAINT FK_Students_Groups FOREIGN KEY (GroupId) REFERENCES Groups(Id)
);
