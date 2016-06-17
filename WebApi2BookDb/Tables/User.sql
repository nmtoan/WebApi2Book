create table [dbo].[User] (
	[UserId] bigint identity (1, 1) not null,
	[FirstName] nvarchar(50) not null,
	[LastName] nvarchar(50) not null,
	[UserName] nvarchar(50) not null,
	[ts] rowversion not null,
	primary key clustered ([UserId] asc)
);