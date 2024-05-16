using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Infrastructure.Interfaces
{
    public  interface ISnackbar
    {
        Task Add(string  message, NotificationSeverity notificationSeverity);
    }
}
