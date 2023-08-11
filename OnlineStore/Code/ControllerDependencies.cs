using Microsoft.AspNetCore.Mvc;
using OnlineStore.Common.DTOs;


namespace OnlineStore.WebApp.Code
{
    public class ControllerDependencies 
    {
        public CurrentUserDto CurrentUser { get; set; }

        public ControllerDependencies(CurrentUserDto currentUser)
        {
            this.CurrentUser = currentUser;
        }
    }
}
