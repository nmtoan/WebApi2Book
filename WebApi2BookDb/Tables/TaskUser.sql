create table [dbo].[TaskUser] (
	[TaskId] bigint not null,
	[UserId] bigint not null,
	[ts] rowversion not null,
	primary key ([TaskId], [UserId]),
	foreign key ([TaskId]) references [dbo].[Task] ([TaskId]),
	foreign key ([UserId]) references [dbo].[User] ([UserId])
);
go

create index ix_TaskUser_UserId on TaskUser ([UserId]);