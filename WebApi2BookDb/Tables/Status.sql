create table [dbo].[Status] (
	[StatusId] bigint identity (1, 1) not null,
	[Name] nvarchar(100) not null,
	[Ordinal] int not null,
	[ts] rowversion not null,
	primary key clustered ([StatusId] asc)
);