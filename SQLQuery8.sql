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