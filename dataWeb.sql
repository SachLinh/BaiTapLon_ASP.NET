use master
go

create database WebBanHangNongSan
go

use WebBanHangNongSan
go

create table Admin(
  UserName  nvarchar(10) primary key,
  PassWord   nvarchar(20),
)
------------------
insert into Admin 
values('Admin1', 'abc123'),
      ('Admin2', 'abc123')
select * from Admin
go
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
----
exec LoginAdmin 'Admin1', 'abc123'