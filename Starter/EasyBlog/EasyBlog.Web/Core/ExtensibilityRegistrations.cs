﻿using Autofac;
using Autofac.Features.ResolveAnything;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyBlog.Web.Core
{
    public class ExtensibilityRegistrations : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource( t => 
            {
                return t.GetInterfaces().FirstOrDefault(i => i.Name == "IEasyBlogModule") != null;
            }));
        }
    }
}