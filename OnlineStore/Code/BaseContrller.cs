using Microsoft.AspNetCore.Mvc;
using OnlineStore.Common.DTOs;
using OnlineStore.WebApp.Code;

namespace OnlineStore.Code
{
    public class BaseController : Controller
    {
        protected readonly CurrentUserDto CurrentUser;

        public BaseController(ControllerDependencies dependencies)
            : base()
        {
            CurrentUser = dependencies.CurrentUser;
        }
    }
}
