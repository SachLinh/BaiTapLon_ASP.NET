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
  Username  nvarchar(20),
  PassWord nvarchar(20),
  Phone   nvarchar(10) primary key,
  Email   nvarchar(30),
  Address  nvarchar(50),
  RoleID   char(15)
)
go
--********************************
create table Catalogy
(
  ID        nvarchar(10)   primary key,
  CataName  nvarchar(20)
)
go
---********************************

create table Product
(
  ProID   nvarchar(10)   primary key,
  ID    nvarchar(10)   foreign key (ID) references Catalogy(ID) on update cascade on delete cascade,
  ProName   nvarchar(30),
  Quantity   int,
  Price    int,
  ProImage    nvarchar(100),
  DateOfImport   nvarchar(30),
  ProDescription  nvarchar(100)
)


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
insert into KhuyenMai
values ('KM00', 0)


select * from KhuyenMai
--********************************

--********************************

create table HoaDon
(
  MaHD     int IDENTITY(1,1) primary key,
  Phone    nvarchar(10)   foreign key(Phone) references  Customer(Phone) on update cascade on delete cascade,
  ngayDayHang  date,
  MaKhuyenMai  nvarchar(10)   foreign key(MaKhuyenMai)  references KhuyenMai(MaKhuyenMai) on update cascade on delete cascade,
  NoiNhanHang   nvarchar(200),
  ThanhToan    bit,
  GiaoHang   bit,
)


--********************************
create table ChiTietHoaDon
(
    MaHD   int   foreign key (MaHD) references HoaDon(MaHD) on update cascade on delete cascade,
	ProID  nvarchar(10)   foreign key (ProID) references Product(ProID) on update cascade on delete cascade,
	SoLuongMua  int,
	Price   int,
	primary key (MaHD, ProID)
)

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
values
('SP01', 'Loai1', N'Thịt bò', 30, 250000, 'ThitSuon.jpeg', '11/12/2021', N'Thịt bò được nuôi sạch, quy trình làm thịt sạch sẽ, không chất bảo quản'),
('SP02', 'Loai3', N'Rau dền', 30, 35000, 'RauDen.jpeg', '11/12/2021', N'Quy trình bào quản chất lượng, an toàn, không chất kích thích, không chất bảo quản'),
('SP03', 'Loai3', N'Rau cải chíp', 30, 10000, 'RauCaiChip.jpeg', '11/12/2021', N'Rau sạch, không thuốc trừ sau, không chất kích thích')

insert into Product
values('SP04', 'Loai1', N'Bắp Bò', 50, 160000, 'BapBo.jpg', '12/12/2021', N'Thịt bò được nuôi sạch, quy trình làm thịt sạch sẽ, không chất bảo quản'),
('SP05', 'Loai3', N'Bắp Cải', 10, 15000, 'BapCai.jpg', '12/12/2021', N'Quy trình bào quản chất lượng, an toàn, không chất kích thích, không chất bảo quản'),
('SP06', 'Loai3', N'Bầu Sao', 30, 10000, 'BauSao.jpg', '11/12/2021', N'Rau sạch, không thuốc trừ sau, không chất kích thích'),
('SP07', 'Loai3', N'Bí', 50, 16000, 'Bi.jpg', '12/12/2021', N'Rau sạch, không phun thuốc trừ sâu, không chất kích thích'),
('SP09', 'Loai1', N'Cá Chép', 30, 44000, 'CaChep.jpg', '11/12/2021', N'Thịt được nuôi sạch, quy trình làm thịt sạch sẽ, không chất bảo quản'),
('SP11', 'Loai2', N'Cam', 10, 15000, 'Cam.jpg', '12/12/2021', N'Quy trình bào quản chất lượng, an toàn, không chất kích thích, không chất bảo quản'),
('SP12', 'Loai2', N'Cà rốt', 30, 10000, 'Carot.jpg', '11/12/2021',  N'Quy trình bào quản chất lượng, an toàn, không chất kích thích, không chất bảo quản'),
('SP14', 'Loai3', N'Đậu Bắp', 10, 15000, 'DauBap.png', '12/12/2021', N'Quy trình bào quản chất lượng, an toàn, không chất kích thích, không chất bảo quản'),
('SP15', 'Loai3', N'Đậu Trạch', 30, 10000, 'DauTrach.jpeg', '11/12/2021', N'Rau sạch, không thuốc trừ sau, không chất kích thích'),
('SP16', 'Loai2', N'Dừa', 50, 56000, 'Dua.png', '12/12/2021',  N'Quy trình bào quản chất lượng, an toàn, không chất kích thích, không chất bảo quản'),
('SP17', 'Loai2', N'Dưa Lê', 10, 15000, 'DuaLe.jpg', '12/12/2021', N'Quy trình bào quản chất lượng, an toàn, không chất kích thích, không chất bảo quản'),
('SP18', 'Loai2', N'Dừa Xiêm', 30, 40000, 'DuaXiem.jpg', '11/12/2021',  N'Quy trình bào quản chất lượng, an toàn, không chất kích thích, không chất bảo quản'),
('SP19', 'Loai1', N'Gà', 50, 160000, 'Ga.jpeg', '12/12/2021', N'Thịt được nuôi sạch, quy trình làm thịt sạch sẽ, không chất bảo quản'),
('SP20', 'Loai1', N'Hàu Đá', 10, 150000, 'HauDa.jpeg', '12/12/2021', N'Thịt được nuôi sạch, quy trình làm thịt sạch sẽ, không chất bảo quản'),
('SP21', 'Loai3', N'Khổ Qua', 30, 10000, 'KhoQua.png', '11/12/2021', N'Rau sạch, không thuốc trừ sau, không chất kích thích'),
('SP22', 'Loai2', N'Măng Cầu', 50, 60000, 'MangCau.png', '12/12/2021',  N'Quy trình bào quản chất lượng, an toàn, không chất kích thích, không chất bảo quản'),
('SP23', 'Loai2', N'Măng Tây', 10, 50000, 'MangTay.png', '12/12/2021', N'Quy trình bào quản chất lượng, an toàn, không chất kích thích, không chất bảo quản'),
('SP24', 'Loai3', N'Mướp', 30, 10000, 'Muop.png', '11/12/2021', N'Rau sạch, không thuốc trừ sau, không chất kích thích'),
('SP25', 'Loai3', N'Rau Ngót', 50, 16000, 'RauNgot.jpg', '12/12/2021', N'Rau sạch, không thuốc trừ sau, không chất kích thích'),
('SP26', 'Loai1', N'Thăn Bò', 10, 150000, 'ThanBo.jpg', '12/12/2021', N'Thịt được nuôi sạch, quy trình làm thịt sạch sẽ, không chất bảo quản'),
('SP27', 'Loai1', N'Trứng Gà Ta', 30, 33000, 'TrungGaTa.jpg', '11/12/2021', N'Thịt được nuôi sạch, quy trình làm thịt sạch sẽ, không chất bảo quản'),
('SP28', 'Loai1', N'Trứng Cút', 20, 10000, 'TrungCut.jpg', '11/12/2021', N'Thịt được nuôi sạch, quy trình làm thịt sạch sẽ, không chất bảo quản'),
('SP29', 'Loai1', N'Trứng Vịt', 30, 30000, 'TrungVit.jpg', '11/12/2021', N'Thịt được nuôi sạch, quy trình làm thịt sạch sẽ, không chất bảo quản')


insert into Product
values('SP10', 'Loai1', N'Cá Lăng', 5, 100000, 'CaLang.jpeg', '12/12/2021', N'Thịt được nuôi sạch, quy trình làm thịt sạch sẽ, không chất bảo quản')

go

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
go

select * from HoaDon




select * from Admin
go
select * from Customer
go
select * from Catalogy
go
select * from Product
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
--************************************ Hiển thị 3 mặt hàng đk mua nhiều nhất



                                       
							            
                       



