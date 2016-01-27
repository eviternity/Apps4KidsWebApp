use Apps4Kids
go

if not OBJECT_ID('sp_RegisterUser') is null
	drop procedure sp_RegisterUser
go

create procedure sp_RegisterUser
	@UserName nvarchar(50),
	@Password nvarchar(50),
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Children nvarchar(255),
	@CountryOfOriginID int,
	@AuthentificationCode varchar(30)
as
begin
	-- check wheater UserName allready exists
	declare @userExists bit;

	Select @userExists = iif(Count(id) < 1, 0, 1)
	from [User]
	where EMail = @UserName;

	if @userExists = 1
	begin
		RAISERROR ('User allready exists',10,10, 'sp_RegisterUser');
	end
	else
	begin
		declare @hashedPassword varbinary(512);
		declare @newID int;
		set @hashedPassword = HASHBYTES('SHA2_512', @Password);

		insert into [User](EMail,FirstName,LastName,Children,CountryOfOriginID,[Password])
		values (@UserName,@FirstName,@LastName,@Children,@CountryOfOriginID,@hashedPassword);

		set @newID = SCOPE_IDENTITY();

		insert into AuthentificationCode (UserID, Code)
		values (@newID, @AuthentificationCode);

	end


end
go

if not OBJECT_ID('sp_Login') is null
	drop procedure sp_Login
go

create procedure sp_Login
	@UserName nvarchar(50),
	@Password nvarchar(50)
as
begin
	select
		u.ID,
		u.FirstName,
		u.LastName,
		u.Children,
		c.Designation as CountryOfOrigin,
		cast(iif( isnull(a.UserID, 0) > 0, 1, 0) as bit) as IsAdmin
	from 
		[User] as u
		join CountryOfOrigin as c
			on u.CountryOfOriginID = c.ID
		left join UserLock as ul
			on  u.ID = ul.UserID
		left join [Admin] as a
			on u.ID = a.UserID
		left join AuthentificationCode as ac
			on u.ID = ac.UserID
	where 
		u.EMail = @UserName and
		u.[Password] = HASHBYTES('SHA2_512', @Password) and
		ul.UserID is null and
		ac.UserID is null;
end
go

if not OBJECT_ID('sp_Authentificate') is null
	drop procedure sp_Authentificate
go

create procedure sp_Authentificate
	@UserID int,
	@AuthentificationCode varchar(30)
as
begin
	delete
		AuthentificationCode
	where 
		UserID = @UserID and
		Code = @AuthentificationCode;

	select
		u.ID,
		u.FirstName,
		u.LastName,
		u.Children,
		c.Designation as CountryOfOrigin,
		cast(iif( isnull(a.UserID, 0) > 0, 1, 0) as bit) as IsAdmin
	from 
		[User] as u
		join CountryOfOrigin as c
			on u.CountryOfOriginID = c.ID
		left join UserLock as ul
			on  u.ID = ul.UserID
		left join [Admin] as a
			on u.ID = a.UserID
		left join AuthentificationCode as ac
			on u.ID = ac.UserID
	where 
		u.ID = @UserID and
		ul.UserID is null and
		ac.UserID is null;
end
go

if not OBJECT_ID('sp_changePassword') is null
	drop procedure sp_changePassword
go

create procedure sp_changePassword
	@UserId int,
	@Password nvarchar(50),
	@ChangePasswordCode varchar(30)
as
begin


	update [User]
	set [Password] = Hashbytes('SHA2_512',@Password)
	where exists
				(
					select * 
					from ChangePasswordCode 
					where 
							UserID = @UserId and
							Code = @ChangePasswordCode
				);
	delete ChangePasswordCode
	where UserID = @UserId;	

	select
		u.ID,
		u.FirstName,
		u.LastName,
		u.Children,
		c.Designation as CountryOfOrigin,
		cast(iif( isnull(a.UserID, 0) > 0, 1, 0) as bit) as IsAdmin
	from 
		[User] as u
		join CountryOfOrigin as c
			on u.CountryOfOriginID = c.ID
		left join UserLock as ul
			on  u.ID = ul.UserID
		left join [Admin] as a
			on u.ID = a.UserID		
	where 
		u.ID = @UserID and
		ul.UserID is null and
		u.[Password] = Hashbytes('SHA2_512',@Password);
end
go

if not OBJECT_ID('sp_changeUserPassword') is null
	drop procedure sp_changeUserPassword
go


create procedure sp_changeUserPassword
	@UserID int,
	@NewPasswort nvarchar(50)
as
begin
	update [User]
	set [Password] = HASHBYTES('SHA2_512', @NewPasswort)
	where ID = @UserID;
end
go


if not OBJECT_ID('sp_GetProducerID') is null
	drop procedure sp_GetProducerID
go

create procedure sp_GetProducerID
	@ProducerName nvarchar(50),
	@ID int output
as
begin
	declare @result int;

	select @result = ID
	from Producer
	where Designation = @ProducerName;

	if(@result is null)
	begin
		insert into Producer (Designation)
		values (@ProducerName);
		set @result = SCOPE_IDENTITY();
	end

	set @ID = @result;	
end
go


use master
go

