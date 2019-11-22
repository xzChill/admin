using Castle.DynamicProxy;
using System;

namespace Chill.Common
{
    public abstract class BaseFilterAttribute : Attribute, IFilter
    {
        public abstract void OnActionExecuted(IInvocation invocation);
        public abstract void OnActionExecuting(IInvocation invocation);
    }
}
