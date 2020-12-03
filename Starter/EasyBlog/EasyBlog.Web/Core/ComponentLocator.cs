using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyBlog.Web.Core
{
    public class ComponentLocator : IComponentLocator
    {
        public T ResolveComponent<T>()
        {
            throw new NotImplementedException();
        }
    }

    public interface IComponentLocator
    {
        T ResolveComponent<T>();
    }
}