use Apps4Kids
go

insert into Category (Designation)
	select distinct kategorie
	from dbIT06_LAP.dbo.tblAppImport
go

insert into Producer (Designation)
	select distinct hersteller
	from dbIT06_LAP.dbo.tblAppImport
go

insert into AppOS (Designation)
	select distinct os
	from dbIT06_LAP.dbo.tblAppImport
go


insert into App (ProducerID, Designation, URL, Price, Prerequisites, [Description] )
	select p.ID, appImport.name, 'http://www.testUrl.com', appImport.preis, appImport.osVersion, appImport.beschreibung
	from 
		dbIT06_LAP.dbo.tblAppImport as appImport
		join Producer as p
			on p.Designation = appImport.hersteller
go

insert into AppCategory(AppID,CategoryID)
	select a.ID, c.ID
	from dbIT06_LAP.dbo.tblAppImport as ai
		join Category as c 
			on ai.Kategorie = c.Designation
		join App as a
			on ai.Name = a.Designation and ai.osVersion = a.Prerequisites
go

insert into OperatingSystems(AppID,AppOSID)
	select a.ID, aos.ID
	from
		dbIT06_LAP.dbo.tblAppImport as ai
		join App as a on ai.name = a.Designation and ai.osVersion = a.Prerequisites
		join AppOS as aos on ai.os = aos.Designation
go


create procedure sp_InsertAppPicture
	@AppID int,
	@PicturePath varchar(255)
as
begin
	declare @sql varchar(max);
	declare @newPictureId int;
	
	set @sql = 'insert into Picture (Data)
				select bulkcolumn
				from openrowset(bulk ''' + @PicturePath + ''' , Single_Blob) as picture;';
	exec (@sql);
	set @newPictureId = @@IDENTITY;

	insert into AppPicture (AppID, PictureID)
	values (@AppID, @newPictureId);
end
go

create procedure sp_Import_Pictures
	@rootPath varchar(max)
as
begin

	declare @Appid int;
	declare @Path varchar(max);

	declare picCursor cursor
	for

	select a.ID, @rootPath + bi.bildPfad
	from dbIT06_LAP.dbo.tblBildImport as bi
		join dbIT06_LAP.dbo.tblAppImport as ai
			on bi.idApp = ai.idApp
		join App as a
			on ai.Name = a.Designation and ai.osVersion = a.Prerequisites;
	open picCursor;

	fetch next from picCursor into @Appid, @Path;
	while (@@fetch_status <> -1)
	begin
		exec sp_InsertAppPicture @Appid,@Path;
		fetch next from picCursor into @Appid, @Path;
	end
	close picCursor;
	deallocate picCursor;
end
go

exec sp_Import_Pictures 'E:\LAP\Apps4KidsWeb\SQL\App Daten\bilder\'
go



drop procedure sp_Import_Pictures
go

drop procedure sp_InsertAppPicture
go



insert into CountryOfOrigin(Designation)
values 
	('Afghanistan'),
	('Albanien'),
	('Algerien'),
	('Andorra'),
	('Angola'),
	('Antigua and Barbuda'),
	('Argentinien'),
	('Armenien'),
	('Australien'),
	('Österreich'),
	('Aserbaidschan'),
	('Bahamas'),
	('Bahrain'),
	('Bangladesh'),
	('Barbados'),
	('Weißrussland'),
	('Belgien'),
	('Belize'),
	('Benin'),
	('Bhutan'),
	('Bolivien'),
	('Bosnien und Herzegovina'),
	('Botswana'),
	('Brasilien'),
	('Brunei Darussalam'),
	('Bulgarien'),
	('Burkina Faso'),
	('Burundi'),
	('Kambodscha'),
	('Kamerun'),
	('Kanada'),
	('Cape Verde'),
	('Zentralafrikanische Republik '),
	('Tschad'),
	('Chile'),
	('China'),
	('Kolumbien'),
	('Komoren'),
	('Kongo'),
	('Kongo, Demokratische Republik'),
	('Cook Islands'),
	('Costa Rica'),
	('Côte dIvoire'),
	('Kroatien'),
	('Kuba'),
	('Zypern'),
	('Tschechische Republik'),
	('Dänemark'),
	('Dschibuti'),
	('Dominica'),
	('Dominikanische Republik'),
	('Ecuador'),
	('Ägypten'),
	('El Salvador'),
	('Equatorial Guinea'),
	('Eritrea'),
	('Estland'),
	('Äthiopien'),
	('Fiji'),
	('Finnland'),
	('Frankreich'),
	('Gabon'),
	('Gambia'),
	('Georgien'),
	('Deutschland'),
	('Ghana'),
	('Griechenland'),
	('Grenada'),
	('Guatemala'),
	('Guinea'),
	('Guinea-Bissau'),
	('Guyana'),
	('Haiti'),
	('Vatikanstadt'),
	('Honduras'),
	('Ungarn'),
	('Island'),
	('Indien'),
	('Indonesien'),
	('Iran'),
	('Irak'),
	('Irland'),
	('Israel'),
	('Italien'),
	('Jamaica'),
	('Japan'),
	('Jordanien'),
	('Kasachstan'),
	('Kenia'),
	('Kiribati'),
	('Korea, Demokratische Volksrepublik'),
	('Korea, Republik'),
	('Kuwait'),
	('Kirgisistan'),
	('Laos'),
	('Lettland'),
	('Libanon'),
	('Lesotho'),
	('Liberien'),
	('Libyen'),
	('Liechtenstein'),
	('Litauen'),
	('Luxemburg'),
	('Madagaskar'),
	('Malawi'),
	('Malaysia'),
	('Malediven'),
	('Mali'),
	('Malta'),
	('Marschall Inseln'),
	('Mauritanien'),
	('Mauritius'),
	('Mexiko'),
	('Micronesien'),
	('Moldau'),
	('Monaco'),
	('Mongolien'),
	('Montenegro'),
	('Marokko'),
	('Mosambik'),
	('Myanmar'),
	('Namibien'),
	('Nauru'),
	('Nepal'),
	('Niederlande'),
	('Neu Seeland'),
	('Nicaragua'),
	('Niger'),
	('Nigeria'),
	('Niue'),
	('Norwegen'),
	('Palestinänsische Gebiete'),
	('Oman'),
	('Pakistan'),
	('Palau'),
	('Panama'),
	('Papua Neu Guinea'),
	('Paraguay'),
	('Peru'),
	('Philippinen'),
	('Polen'),
	('Portugal'),
	('Qatar'),
	('Rumänien'),
	('Russische Föderation'),
	('Ruanda'),
	('Saint Kitts and Nevis'),
	('Saint Lucia'),
	('Saint Vincent und die Grenadinen'),
	('Samoa'),
	('San Marino'),
	('Sao Tomé und Principe'),
	('Saudi Arabien'),
	('Senegal'),
	('Serbien‡'),
	('Seychellen'),
	('Sierra Leone'),
	('Singapur'),
	('Slowakei'),
	('Slowenien'),
	('Solomon Inseln'),
	('Somalia'),
	('Süd Africa'),
	('Spanien'),
	('Sri Lanka'),
	('Sudan'),
	('Suriname'),
	('Swaziland'),
	('Schweden'),
	('Schweiz'),
	('Syrien'),
	('Tadschikistan '),
	('Tanzania'),
	('Thailand'),
	('Makedonien'),
	('Timor-Leste'),
	('Togo'),
	('Tonga'),
	('Trinidad und Tobago'),
	('Tunesien'),
	('Türkei'),
	('Turkmenistan'),
	('Tuvalu'),
	('Uganda'),
	('Ukraine '),
	('Vereinigte Arabische Emirate'),
	('Vereinigtes Königreich'),
	('Vereinigte Staaten'),
	('Uruguay'),
	('Usbekistan'),
	('Vanuatu'),
	('Venezuela'),
	('Viet Nam'),
	('Yemen'),
	('Sambia'),
	('Simbabwe')
go

declare @oesterreich int;
select @oesterreich =  id from CountryOfOrigin where Designation = 'Österreich';



exec sp_RegisterUser N'flo.schmidinger@gmx.at','123','Florian','Schmidinger','1 Bub', @oesterreich, 'SOMECODE';

exec sp_Authentificate 1,'SOMECODE';

exec sp_RegisterUser N'susi.schmidinger@gmx.at','123','Susi','Schmidinger','1 Bub', @oesterreich, 'SOMEOTHERCODE';

exec sp_Authentificate 2,'SOMEOTHERCODE';

insert into [Admin] (UserID)
values(1);

insert into Recention (AppID,UserID,Comment,Rating)
values (32,1,N'Super App',5);

insert into Recention (AppID,UserID,Comment,Rating)
values (36,1,N'Super App',4);

insert into Recention (AppID,UserID,Comment,Rating)
values (37,1,N'Super App',3);

insert into Recention (AppID,UserID,Comment,Rating)
values (46,1,N'Super App',2);

insert into Recention (AppID,UserID,Comment,Rating)
values (48,1,N'Super App',1);

insert into Recention (AppID,UserID,Comment,Rating)
values (48,2,N'Find ich auch super',2)



insert into AppOS(Designation)
values ('Apple iOS');

use master
go