using System;

namespace Chill.Common
{
    public interface IDisposableContainer : IDisposable
    {
        void AddDisposableObj(IDisposable disposableObj);
    }
}
