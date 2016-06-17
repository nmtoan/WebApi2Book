declare @statusId int, @taskId int, @userId int

if not exists(select * from [User] where Username = 'bhogg')
	insert into [dbo].[User]([FirstName], [LastName], [UserName])
	values (N'Boss', N'Hogg', N'bhogg')

if not exists(select * from [User] where Username = 'jbob')
	insert into [dbo].[User]([FirstName], [LastName], [UserName])
	values (N'Jim', N'Bob', N'jbob')

if not exists(select * from [User] where Username = 'jdoe')
	insert into [dbo].[User]([FirstName], [LastName], [UserName])
	values (N'John', N'Doe', N'jdoe')

if not exists(select * from dbo.Task where Username = 'bhogg')
begin
	select top 1 @statusId = StatusId from Status order by StatusId;
	select top 1 @userId = UserId from [User] order by UserId;

	insert into dbo.Task(Subject, StartDate, StatusId, CreatedDate, CreatedUserId)
	values (N'Test Task', getdate(), @statusId, getdate(), @userId);

	set @taskId = SCOPE_IDENTITY();

	insert into [dbo].[TaskUser]([TaskId],[UserId])
	values (@taskId, @userId)
end
	