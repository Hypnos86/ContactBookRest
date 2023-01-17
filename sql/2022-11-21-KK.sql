CREATE TABLE [dbo].[PhoneNumbers](
[Id_PhoneNumber] [int] IDENTITY(1,1) NOT NULL,
[PrefixNumber] CHAR(3) DEFAULT '+48', 
[Number] CHAR(9),
[Contact_Id] INT,
PRIMARY KEY (Id_PhoneNumber),
FOREIGN KEY(Contact_Id) REFERENCES Contacts(Id_Contact)
);