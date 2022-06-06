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

-- thêm thức ăn
insert dbo.Food
--name nvarchar(100), idCategory- int, price-int
values(N'Vịt quay',1,100000)
insert dbo.Food
values(N'Ức gà',2,50000)
insert dbo.Food
values(N'Dê núi',3,120000)
insert dbo.Food
values(N'Cá rán',4,100000)
insert dbo.Food
values(N'sushi',5,50000)
insert dbo.Food
values(N'Canh ngao',9,50000)
insert dbo.Food
values(N'Đậu lướt ván',8,30000)
insert dbo.Food
values(N'7Up',6,10000)
insert dbo.Food
values(N'pepsi',7,10000)
insert dbo.Food
values(N'Volka',12,120000)
insert dbo.Food
values(N'Lợn quay',10,200000)
insert dbo.Food
values(N'Tôm',11,60000)

-- thêm bill
insert dbo.Bill
--checkin, checkout- date, idTable-int, status-int
values(GETDATE(),GETDATE(),2,1)
insert dbo.Bill
values(GETDATE(),GETDATE(),1,0)
insert dbo.Bill
values(GETDATE(),GETDATE(),1,1)

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

select f.name, bi.count, f.price, f.price*bi.count AS totalPrice from dbo.BillInfo AS bi, dbo.Bill ASb, dbo.Food AS f
where bi.idBill = b.id AND bi.idFood = f.id AND b.idTable =3

select * from dbo.BillInfo
select * from dbo.Food
select * from dbo.FoodCategory
