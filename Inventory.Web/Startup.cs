﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Inventory.Web.Startup))]
namespace Inventory.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
