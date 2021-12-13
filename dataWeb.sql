use master
go

create database WebBanHangNongSan
go

use WebBanHangNongSan
go
---********************************
create table Admin(
  UserName  nvarchar(10) primary key,
  PassWord   nvarchar(20),
)
go
--********************************
create table Customer
(
  Username  nvarchar(10),
  PassWord nvarchar(20),
  Phone   nvarchar(10) primary key,
  Email   nvarchar(20),
  Address  nvarchar(50),
  RoleID   char(15)
)
go
--********************************
create table Catalogy
(
  ID        nvarchar(5)   primary key,
  CataName  nvarchar(20)
)
go
---********************************
create table Product
(
  ProID   nvarchar(5)   primary key,
  ID    nvarchar(5)   foreign key (ID) references Catalogy(ID) on update cascade on delete cascade,
  ProName   nvarchar(30),
  Quantity   int,
  Price    float,
  ProImage   char(10),
  DateOfImport   nvarchar(30),
  ProDescription  nvarchar(100)
)
go
---********************************
create table KhuyenMai
(
  MaKhuyenMai    nvarchar(10)  primary key,
  GiaTri       float
)

insert into KhuyenMai
values ('KM01', 0.2),
       ('KM02', 0.3),
	   ('KM03', 0.5)

select * from KhuyenMai
--********************************
create table GioHang
(
   Phone   nvarchar(10) foreign key(Phone)  references Customer(Phone) on update cascade on delete cascade,
   ProID   nvarchar(5) foreign key(ProID)  references Product(ProID) on update cascade on delete cascade,
   primary key (Phone,ProID)
)

insert into GioHang
values('011111', 'SP01'),
      ('022222', 'SP02'),
	  ('033333', 'SP03')
select * from GioHang
go
--********************************
create table HoaDon
(
  MaHD     nvarchar(10) primary key,
  Phone    nvarchar(10)   foreign key(Phone) references  Customer(Phone) on update cascade on delete cascade,
  ngayTao  date,
  MaKhuyenMai  nvarchar(10)   foreign key(MaKhuyenMai)  references KhuyenMai(MaKhuyenMai) on update cascade on delete cascade  
)
go
insert into HoaDon
values ('HD01', '011111','12/12/2021','KM01'),
       ('HD02', '022222','12/12/2021','KM02'),
	   ('HD03', '033333','12/12/2021','KM03')
go
select * from HoaDon
--********************************
create table ChiTietHoaDon
(
    MaHD   nvarchar(10)   foreign key (MaHD) references HoaDon(MaHD) on update cascade on delete cascade,
	ProID  nvarchar(5)   foreign key (ProID) references Product(ProID) on update cascade on delete cascade,
	SoLuongMua  int,
	ThanhTien   float,
	primary key (MaHD, ProID)
)
insert into ChiTietHoaDon
values('HD01', 'SP03',5,50000)
--********************************
----- Hóa Đơn: Mã, ten khách Hàng, ngày, Mã KM.
----- Chi tiết hóa đơn: Mã HD, Mã Hàng, Số lượng Mua, Thanh Tien (SoLuong*Gia)
----- Giỏ hàng: Ma KH, mã hàng.
----- Khuyễn Mại: Mã KM, Giá trị.
----- Thủ tục hiển thị hàng được mua nhiều.
----- Thủ tục tính Thanh Tien
----- Trigger update lại số lượng sản phẩm.
------------------
insert into Product
values('SP01', 'Loai1', N'Thịt bò', 30, 250000, 'h1.png', '11/12/2021', N'Thịt bò được nuôi sạch, quy trình làm thịt sạch sẽ, không chất bảo quản'),
('SP02', 'Loai2', N'Táo', 30, 35000, 'h2.png', '11/12/2021', N'Quy trình bào quản chất lượng, an toàn, không chất kích thích, không chất bảo quản'),
('SP03', 'Loai3', N'Rau cải', 30, 10000, 'h3.png', '11/12/2021', N'Rau sạch, không thuốc trừ sau, không chất kích thích')


insert into Admin 
values('admin1', 'abc123'),
      ('admin2', 'abc1234')
select * from Admin

insert into Customer
values ('customer1', 'abc123', '011111', 'customer1@gmail.com', 'HaNoi', 'Cap2'),
       ('customer2', 'abc123', '022222', 'customer2@gmail.com', 'NamDinh','Cap1'),
	   ('customer3', 'abc123', '033333', 'customer3@gmail.com', 'HaiDuong', 'Cap2'),
	   ('customer4', 'abc123', '044444', 'customer4@gmail.com', 'BacNinh','Cap1')

insert into Catalogy
values ('Loai1', N'Thịt Cá Dân Dã'),
       ('Loai2', N'Trái Cây'),
	   ('Loai3', N'Rau Hữu Cơ')





select * from Admin
go
select * from Customer
go
select * from Catalogy
go
select * from Product
go
select * from GioHang
go
select * from KhuyenMai
go 
select * from HoaDon
go
select * from ChiTietHoaDon
go

---******************************** Tìm kiếm admin để đăng nhập
create proc LoginAdmin (@UserName  nvarchar(10), @PassWord nvarchar(20))
as
   begin
       declare @count  int
	   declare @res bit
	   select @count = COUNT(*) from Admin where UserName = @UserName and PassWord = @PassWord
	   if @count > 0
	      set @res = 1
	   else set @res =0
	   select @res
   end
go
----
exec LoginAdmin 'Admin1', 'abc123'
go
--*************************************** Trigger update Quantity Product khi thêm Chi tiết hóa đơn 
create trigger Update_Quantity
on ChiTietHoaDon
for insert
as 
    begin
	    declare @SoLuongBanDau   int
		declare @SoLuongMua   int
		declare @MaSP   nvarchar(5)
		select @SoLuongMua = SoLuongMua from inserted
		select @MaSP = ProID from inserted
		select @SoLuongBanDau = Quantity from Product where ProID = @MaSP
		if @SoLuongMua > @SoLuongBanDau
		   begin
		        raiserror( N'Hang khong du ', 16,1)
                rollback tran
		        return
		   end
		else
		   begin
		         Update Product
				 set Product.Quantity = Product.Quantity - inserted.SoLuongMua
				 from Product inner join inserted
				 on Product.ProID = inserted.ProID
		   end
	end
go
--disable trigger  Update_Quantity on ChiTietHoaDon
-- Test:
select * from Product
go
select * from HoaDon
go
select * from ChiTietHoaDon
go
insert into ChiTietHoaDon 
values ('HD02', 'SP02', 5,175 )
--********************************** Thủ tục tính Thanh Tien trong ChiTietHoaDon
go
create proc ThanhTien_ChiTietHoaDon(@MaHD  nvarchar(10), @ProID  nvarchar(5), @SoLuongMua  int)
as
   begin
       declare @DonGia   int
	   select @DonGia = Price from Product where ProID = @ProID
	   insert into ChiTietHoaDon
	   values (@MaHD, @ProID, @SoLuongMua, @SoLuongMua*@DonGia)
   end
go
-- test
exec ThanhTien_ChiTietHoaDon 'HD03', 'SP03', 2
go
--************************************ Hiển thị 3 mặt hàng đk mua nhiều nhất
create view findProduct
as
(select COUNT(MaHD) as SoHoaDon, ProID 
 from ChiTietHoaDon
 Group by ChiTietHoaDon.ProID
 Having COUNT(MaHD) > 0
) 
go

select * from findProduct
go

select TOP(3) ProID 
from findProduct
where SoHoaDon > 0
order by SoHoaDon DESC 

go



                                       
							            
                       



