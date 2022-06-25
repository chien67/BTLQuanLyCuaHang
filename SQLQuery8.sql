use BTLT3
go
-- ThucAn
-- Ban
-- LoaiThucAn
-- TaiKhoan
-- HoaDon
-- TtHoaDon

create table TableFood
(
	id int identity primary key,
	name nvarchar(100) default N'Chưa đặt tên',
	status nvarchar(100) default  N'Trống'-- trống|| có người
)
go
create table Account
(
	UserName nvarchar(100) primary key,
	DisplayName nvarchar(100) not null,
	Password nvarchar(100) not null,
	Type int not null
)
go 
create table FoodCategory
(
	id int identity primary key,
	name nvarchar(100) not null default N' Chưa đặt tên '
)
go
create table Food
(
	id int identity primary key,
	name nvarchar(100) not null default N'Chưa đặt tên',
	idCategory int not null,
	price float not null default 0

	foreign key (idCategory) references dbo.FoodCategory(id)
)
go
create table Bill
(
	id int identity primary key,
	DateCheckIn Date not null default getdate(),
	DateCheckOut Date not null,
	idTable int not null,
	status int not null -- 1:da thanh toan||0: chua thanh toan
	foreign key (idTable) references dbo.TableFood(id)
)
go
create table BillInfo
(
	id int identity primary key,
	idBill int not null,
	idFood int not null,
	count int not null default 0

	foreign key (idBill) references dbo.Bill(id),
	foreign key (idFood) references dbo.Food(id),
)
go
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
)    insert into dbo.Account
(
	UserName,
	DisplayName,
	Password,
	Type
)
Values
(
N'Chien01',
N'Nguyễn Văn Chiến',
N'123',
1
)
    insert into dbo.Account
(
	UserName,
	DisplayName,
	Password,
	Type
)
Values
(
N'Duc01',
N'Phạm Việt Đức',
N'123',
1
)
go
    insert into dbo.Account
(
	UserName,
	DisplayName,
	Password,
	Type
)
Values
(
N'staff',
N'nhanvien1',
N'123',
0
)
go

create proc getInfo
@userName nvarchar(100)
AS
BEGIN
	select *from dbo.Account where UserName = @userName
End
go
exec dbo.getInfo @userName = N'Chien01'

go

create proc USP_login
@userName nvarchar(100), @passWord nvarchar(100)
as
begin
	select * from dbo.Account where UserName = @userName and Password = @passWord
end
go

declare @i int = 0

while @i <= 10
begin 
	insert dbo.TableFood (name) values (N'Bàn ' + CAST (@i as nvarchar(100)))
	set @i = @i + 1
end

go

create proc USP_GetTableList
as select * from dbo.TableFood
go

UPDATE dbo.TableFood set status = N'Có người' where id = 9

exec dbo.USP_GetTableList
go
-- loại thức ăn
insert dbo.FoodCategory(name)
--name- nvarchar(100)
values(N'Hải sản')
insert dbo.FoodCategory(name)
values(N'Nông sản')
insert dbo.FoodCategory(name)
values(N'Lâm Sản')
insert dbo.FoodCategory(name)
values(N'Nước')
select *from dbo.FoodCategory
delete from dbo.FoodCategory where id>4
-- thêm thức ăn
insert dbo.Food
--name nvarchar(100), idCategory- int, price-int
values(N'Vịt quay',2,100000)
insert dbo.Food
values(N'Ức gà',2,50000)
insert dbo.Food
values(N'Dê núi',3,120000)
insert dbo.Food
values(N'Cá rán',1,100000)
insert dbo.Food
values(N'sushi',1,50000)
insert dbo.Food
values(N'Canh ngao',1,50000)
insert dbo.Food
values(N'Đậu lướt ván',2,30000)
insert dbo.Food
values(N'7Up',4,10000)
insert dbo.Food
values(N'pepsi',4,10000)
insert dbo.Food
values(N'Volka',4,120000)
insert dbo.Food
values(N'Lợn quay',2,200000)
insert dbo.Food
values(N'Tôm',1,60000)

go

-- thêm bill
insert dbo.Bill
--checkin, checkout- date, idTable-int, status-int
values(GETDATE(),GETDATE(),2,0)
insert dbo.Bill
values(GETDATE(),null,1,0)
insert dbo.Bill
values(GETDATE(),null,1,1)

-- thêm billinfo
insert dbo.BillInfo
--idBill-int, idFood -int,count-int
values(4,2,1)
insert dbo.BillInfo
values(4,2,3)
insert dbo.BillInfo
values(2,1,3)
insert dbo.BillInfo
values(2,1,3)
insert dbo.BillInfo
values(2,1,3) 

go
create proc USP_InsertBill
@idTable int
as
begin
	insert dbo.Bill(DateCheckIn, DateCheckOut, idTable, status,discount)
	values (GETDATE(),GETDATE(), @idTable, 0,0)
end
go
alter proc USP_InsertBillInfo
@idBill int, @idFood int ,@count int
as
begin
	declare @isExitBillInfo int
	declare @foodCount int = 1

	select @isExitBillInfo = id, @foodCount = b.count 
	from dbo.BillInfo AS b 
	where idBill = @idBill and idFood = @idFood

	if (@isExitBillInfo > 0)
	begin
		declare @newCount int = @foodCount + @count
		if (@newCount > 0)
			
			UPDATE dbo.BillInfo SET count = @foodCount + @count WHERE idBill = @idBill AND idFood = @idFood﻿
		else
			delete dbo.BillInfo where idBill = @idBill and idFood= @idFood
	end
	else
	begin
	insert dbo.BillInfo(idBill, idFood, count)

	values (@idBill, @idFood, @count)
	end
end
go


DELETE dbo.BillInfo

DELETE dbo.Bill

alter TRIGGER UTG_UpdateBillInfo
ON dbo.BillInfo FOR INSERT, UPDATE
AS
BEGIN
	DECLARE @idBill INT
	
	SELECT @idBill = idBill FROM Inserted
	
	DECLARE @idTable INT
	
	SELECT @idTable = idTable FROM dbo.Bill WHERE id = @idBill AND status = 0

	declare @count int
	select @count = count(*) from dbo.BillInfo where idBill = @idBill
	
	if(@count > 0)
	begin

		print @idTable
		print @idBill
		print @count
		UPDATE dbo.TableFood SET status = N'Có người' WHERE id = @idTable

	end
	else
	begin

		print @idTable
		print @idBill
		print @count
		UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTable
	end
END
GO



alter TRIGGER UTG_UpdateBill
ON dbo.Bill FOR UPDATE
AS
BEGIN
	DECLARE @idBill INT
	
	SELECT @idBill = id FROM Inserted	
	
	DECLARE @idTable INT
	
	SELECT @idTable = idTable FROM dbo.Bill WHERE id = @idBill
	
	DECLARE @count int = 0
	
	SELECT @count = COUNT(*) FROM dbo.Bill WHERE idTable = @idTable AND status = 0
	
	IF (@count = 0)
		UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTable
END
GO

alter table dbo.Bill
add discount int

update dbo.Bill set discount = 0
go

alter proc USP_SwitchTable
@idTable1 int , @idTable2 int
as begin

	declare @idFirstBill int
	declare @idSeconrdBill int

	declare @isFirstTableEmty int = 1
	declare @isSeconrdTableEmty int = 1

	select @idFirstBill = id From dbo.Bill where idTable = @idTable1 and status = 0
	select @idSeconrdBill = id From dbo.Bill where idTable = @idTable2 and status = 0

	print @idFirstBill
	print @idSeconrdBill

	if (@idFirstBill is null)
	begin
		print '000001'
		insert dbo.Bill(DateCheckIn, DateCheckOut, idTable, status)
		--checkin, checkout- date, idTable-int, status-int
		values(GETDATE(),GETDATE(),@idTable1,0)

		select @idFirstBill = MAX(id) from dbo.Bill where idTable = @idTable1 and status = 0

	end

	select @isFirstTableEmty = count(*) from dbo.BillInfo where idBill = @idFirstBill

	print @idFirstBill
	print @idSeconrdBill

	if (@idSeconrdBill is null)
	begin
		print '000002'
		insert dbo.Bill(DateCheckIn, DateCheckOut, idTable, status)
		--checkin, checkout- date, idTable-int, status-int
		values(GETDATE(),GETDATE(),@idTable2,0)

		select @idSeconrdBill = MAX(id) from dbo.Bill where idTable = @idTable2 and status = 0

	end
	
	select @isSeconrdTableEmty = count(*) from dbo.BillInfo where idBill = @idSeconrdBill

	print @idFirstBill
	print @idSeconrdBill

	select id into IDBillInfoTable from dbo.BillInfo where idBill = @idSeconrdBill

	update dbo.Billinfo set idBill = @idSeconrdBill where idBill = @idFirstBill

	update dbo.BillInfo set idBill = @idFirstBill where id in (select * from IDBillInfoTable)
	drop Table IDBillInfoTable

	if (@isFirstTableEmty = 0)
		update dbo.TableFood set status = N'Trống' where id = @idTable2
	if (@isSeconrdTableEmty = 0)
		update dbo.TableFood set status = N'Trống' where id = @idTable1
end
go

exec dbo.USP_SwitchTable @idTable1 = 2,
	@idTable2 = 7
update dbo.TableFood set status = N'Trống'
go

alter table dbo.Bill add totalPrice float

delete  dbo.Bill
go

alter proc USP_GetListBillByDate
@checkIn date, @checkOut date
as
begin

select t.name as [Tên bàn], b.totalPrice as [Tổng tiền],DateCheckIn as [Ngày vào], DateCheckOut as [Ngày ra], discount as [Giảm giá]
from dbo.Bill as b,dbo.TableFood as t
where DateCheckIn >= @checkIn and DateCheckOut <= @checkOut and b.status = 1
and t.id = b.idTable
end
go
CREATE PROC USP_UpdateAccount
@userName NVARCHAR(100), @displayName NVARCHAR(100), @password NVARCHAR(100), @newPassword NVARCHAR(100)
AS
BEGIN
	DECLARE @isRightPass INT = 0
	
	SELECT @isRightPass = COUNT(*) FROM dbo.Account WHERE USERName = @userName AND PassWord = @password
	
	IF (@isRightPass = 1)
	BEGIN
		IF (@newPassword = NULL OR @newPassword = '')
		BEGIN
			UPDATE dbo.Account SET DisplayName = @displayName WHERE UserName = @userName
		END		
		ELSE
			UPDATE dbo.Account SET DisplayName = @displayName, PassWord = @newPassword WHERE UserName = @userName
	end
END
GO

create trigger UTG_DeleteBillInfo
on dbo.BillInfo for Delete
as
begin
	declare @idBillInfo int
	declare @idBill int
	select @idBillInfo = id, @idBill = Deleted.idBill from Deleted

	declare @idTable int
	select @idTable = idTable from dbo.Bill where id = @idBill

	declare @count int = 0 
	select @count = count(*) from dbo.BillInfo as bi, dbo.Bill as b where b.id = bi.idBill and b.id = @idBill and b.status = 0

	if (@count = 0)
		update dbo.TableFood set status = N'Trống' where id = @idTable
end
go