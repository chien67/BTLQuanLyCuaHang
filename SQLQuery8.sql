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
values(N'Dê núi',1086,120000)
insert dbo.Food
values(N'Cá rán',1,100000)
insert dbo.Food
values(N'sushi',1,50000)
insert dbo.Food
values(N'Canh ngao',1,50000)
insert dbo.Food
values(N'Đậu lướt ván',2,30000)
insert dbo.Food
values(N'7Up',3,10000)
insert dbo.Food
values(N'pepsi',3,10000)
insert dbo.Food
values(N'Volka',3,120000)
insert dbo.Food
values(N'Lợn quay',2,200000)
insert dbo.Food
values(N'Tôm',1,60000)

go

-- thêm bill
insert dbo.Bill
--checkin, checkout- date, idTable-int, status-int
values(GETDATE(),null,2,1)
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
	insert dbo.Bill(DateCheckIn, DateCheckOut, idTable, status)
	values (GETDATE(),GETDATE(), @idTable, 0)
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
			
			update dbo.BillInfo set count = @foodCount + @count where idFood = @idFood
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

delete from dbo.TableFood where id>10


