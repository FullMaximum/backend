using System;
using SimpleInjector;

namespace FlowersBEWebApi.Middleware;

public class MiddlewareFactory : IMiddlewareFactory
{
    private readonly Container _container = ObjectContainer._container;

    public MiddlewareFactory()
    {
        //_container = container;
    }

    public IMiddleware Create(Type middlewareType)
    {
        return _container.GetInstance<IMiddleware>();
    }

    public void Release(IMiddleware middleware)
    {
    }
}
