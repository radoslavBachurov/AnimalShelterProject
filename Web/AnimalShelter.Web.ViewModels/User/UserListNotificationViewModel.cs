namespace AnimalShelter.Web.ViewModels.User
{
    using System;
    using System.Collections.Generic;

    public class UserListNotificationViewModel : PagingViewModel
    {
        public IEnumerable<UserNotificationViewModel> Notifications { get; set; }
    }
}
