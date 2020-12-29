namespace AnimalShelter.Data
{
    using Microsoft.AspNetCore.Identity;
    using System;

    public static class IdentityOptionsProvider
    {
        public static void GetIdentityOptions(IdentityOptions options)
        {
            //options.Password.RequireDigit = false;
            //options.Password.RequireLowercase = false;
            //options.Password.RequireUppercase = false;
            //options.Password.RequireNonAlphanumeric = false;
            //options.Password.RequiredLength = 6;
            //options.Lockout.DefaultLockoutTimeSpan = new TimeSpan(0, 15, 0);
            options.Lockout.MaxFailedAccessAttempts = 2;
            options.Lockout.AllowedForNewUsers = true;
            //options.User.RequireUniqueEmail = false;

            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6;
            options.SignIn.RequireConfirmedAccount = false;
        }
    }
}
