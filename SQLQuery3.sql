/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [UserName]
      ,[DisplayName]
      ,[Password]
      ,[Type]
  FROM [BTLT3].[dbo].[Account]
    insert into dbo.Account
(
	UserName,
	DisplayName,
	Password,
	Type
)
Values
(
N'Quynh01',
N'Vũ Thị Quỳnh',
N'123',
1
)