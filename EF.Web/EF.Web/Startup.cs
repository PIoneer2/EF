﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartup("WebConfiguration", typeof(EF.Web.WebStartup))]
namespace EF.Web
{
    public partial class WebStartup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
