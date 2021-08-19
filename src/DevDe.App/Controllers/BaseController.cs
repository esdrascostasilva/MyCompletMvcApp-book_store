using DevDe.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevDe.App.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly INotifier _notifier;

        public BaseController(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected bool OperationValid()
        {
            return !_notifier.HasNotification();
        }

    }
}
