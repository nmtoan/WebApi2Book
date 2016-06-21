﻿using NHibernate;
using System.Linq;
using WebApi2Book.Common;
using WebApi2Book.Common.Security;
using WebApi2Book.Data.Entities;
using WebApi2Book.Data.Exceptions;
using WebApi2Book.Data.QueryProcessors;

namespace WebApi2Book.Data.SqlServer.QueryProcessors
{
    public class AddTaskQueryProcessor : IAddTaskQueryProcessor
    {
        private readonly IDateTime _dateTime;
        private readonly ISession _session;
        private readonly IUserSession _userSession;

        public AddTaskQueryProcessor(IDateTime dateTime, ISession session, IUserSession userSession)
        {
            _dateTime = dateTime;
            _session = session;
            _userSession = userSession;
        }

        public void AddTask(Task task)
        {
            task.CreatedDate = _dateTime.UtcNow;
            task.Status = _session.QueryOver<Status>()
                .Where(x => x.Name == "Not Started")
                .SingleOrDefault();
            //task.CreatedBy = _session.QueryOver<User>()
            //    .Where(x => x.UserName == _userSession.Username)
            //    .SingleOrDefault();
            task.CreatedBy = _session.Get<User>(1L); // HACK: All tasks created by user 1 for now

            if (task.Users != null && task.Users.Any())
            {
                for (int i = 0; i < task.Users.Count; i++)
                {
                    var user = task.Users[i];
                    var persistedUser = _session.Get<User>(user.UserId);
                    if (persistedUser == null)
                    {
                        throw new ChildObjectNotFoundException("User not found");
                    }
                    task.Users[i] = persistedUser;
                }
            }

            _session.SaveOrUpdate(task);
        }
    }
}
