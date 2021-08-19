﻿using DevDe.Business.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevDe.Business.Interfaces
{
    public interface INotifier
    {
        bool HasNotification();

        List<Notification> GetNotifications();

        void Handle(Notification notification);

    }
}
