use master
go

if not DB_ID('Apps4Kids') is null
	drop database Apps4Kids
go

create database Apps4Kids
go

use Apps4Kids
go

create table Category
(
	-- KEYS
	ID int identity(1,1) not null,

	constraint pk_Category
	primary key (ID),

	-- ATTRIBUTES
	Designation nvarchar(50) not null
)
go

create table CountryOfOrigin
(
	-- KEYS
	ID int identity(1,1) not null,

	constraint pk_CountryOfOrigin
	primary key (ID),

	-- ATTRIBUTES
	Designation nvarchar(50) not null
)
go

create table Producer
(
	-- KEYS
	ID int identity(1,1) not null,

	constraint pk_Producer
	primary key(ID),

	--ATTRIBUTES
	Designation nvarchar(50) not null
)

create table Picture
(
	-- KEYS
	ID int identity(1,1) not null,

	constraint pk_Picture
	primary key (ID),

	-- ATTRIBUTES
	Data varbinary(max) not null
)
go

create table AppOS
(
	ID int identity(1,1) not null,

	constraint pk_AppOS
	primary key (ID),

	Designation nvarchar(50) not null
)
go

create table [User]
(
	-- KEYS
	ID int identity(1,1) not null,

	constraint pk_User
	primary key(id),

	CountryOfOriginID int not null,

	constraint fk_User_CountryOfOrigin
	foreign key (CountryOfOriginID)
	references CountryOfOrigin (ID),

	-- ATTRIBUTES
	FirstName nvarchar(50) not null,
	LastName nvarchar(50) not null,
	EMail nvarchar(50) not null,
	Children nvarchar(100) not null,
	[Password] varbinary (512) not null
)
go

create table [Admin]
(
	UserID int not null,

	constraint pk_Admin
	primary key (UserID),

	constraint fk_Admin_User
	foreign key (UserID)
	references [User] (ID)
)
go

create table UserLock
(
	UserID int not null,

	constraint pk_UserLock
	primary key (UserID),

	constraint fk_UserLock_User
	foreign key (UserID)
	references [User] (ID)
)
go

create table AuthentificationCode
(
	UserID int not null,

	constraint pk_AuthentificationCode
	primary key (UserID),

	constraint fk_AuthentificationCode_User
	foreign key (UserID)
	references [User] (ID),

	Code varchar(30) not null
)
go

create table ChangePasswordCode
(
	UserID int not null,

	constraint pk_ChangePasswordCode
	primary key (UserID),

	constraint fk_ChangePasswordCode_User
	foreign key (UserID)
	references [User] (ID),

	Code varchar(30) not null
)

create table App
(
	-- KEYS
	ID int identity(1,1) not null,

	constraint pk_App
	primary key(id),

	ProducerID int not null,

	constraint fk_App_Producer
	foreign key (ProducerID)
	references Producer (ID),

	-- ATTRIBUTES
	Designation nvarchar(50) not null,
	URL nvarchar(50)not null,
	Price decimal(5,2) not null,
	Prerequisites nvarchar(50) not null,
	[Description] nvarchar (max) not null
)
go

create table AppCategory
(
	-- KEYS
	AppID int not null,
	CategoryID int not null,

	constraint pk_AppCategory
	primary key (AppID, CategoryID),

	constraint fk_AppCategory_App
	foreign key (AppID)
	references App (ID),

	constraint fk_AppCategory_Category
	foreign key (CategoryID)
	references Category (ID)
)
go

create table OperatingSystems
(
	--Keys
	AppOSID int not null,
	AppID int not null,

	constraint pk_OperatingSystems
	primary key (AppOSID, AppID),

	constraint fk_OperatingSystems_AppOS
	foreign key (AppOSID)
	references AppOS(ID),

	constraint fk_OperatingSystems_App
	foreign key (AppID)
	references App (ID)
)

create table AppPicture
(
	-- KEYS
	AppID int not null,
	PictureID int not null,

	constraint pk_AppPictures
	primary key (AppID, PictureID),

	constraint fk_AppPicture_App
	foreign key (AppID)
	references App (ID),

	constraint fk_AppPicture_Picture
	foreign key (PictureID)
	references Picture (ID)
)
go

create table Recention
(
	ID int identity(1,1) not null,
	-- KEYS
	UserID int not null,
	AppID int not null,

	constraint pk_Recention
	primary key (ID),

	constraint fk_Recention_User
	foreign key (UserID)
	references [User] (ID),

	constraint fk_Recention_App
	foreign key (AppID)
	references App (ID),

	-- ATTRIBUTES

	Rating int not null,
	constraint ck_Recention_Rating
	check (Rating between 0 and 5),

	Comment nvarchar(255),
	[Date] DateTime not null default getdate()
)
go

create table Recommendation
(
	-- KEYS
	ID int identity(1,1) not null,
	UserID int not null,
	OSID int not null,
	
	constraint pk_Recommendation
	primary key (ID),

	constraint fk_Recommendation_User
	foreign key (UserID)
	references [User] (ID),

	constraint fk_Recommendation_OS
	foreign key (OSID)
	references AppOS (ID),

	-- ATTRIBUTES
	AppDesignation nvarchar(50) not null,
	[Description] nvarchar(max) not null,
	[Date] datetime not null default getdate()
)
go

create table Approved
(
	-- KEYS
	RecommendationID int not null,

	constraint pk_Approved
	primary key (RecommendationID),

	constraint fk_Approved_Recommendation
	foreign key (RecommendationID)
	references Recommendation (ID)
)
go

create table Denied
(
	-- KEYS
	RecommendationID int not null,

	constraint pk_Denied
	primary key (RecommendationID),

	constraint fk_Denied_Recommendation
	foreign key (RecommendationID)
	references Recommendation (ID)
)
go


use master
go