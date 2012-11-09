using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.CacheAccess;

/*
//https://github.com/ServiceStack/ServiceStack/blob/master/tests/ServiceStack.WebHost.Endpoints.Tests/AttributeFiltersTest.cs

namespace ICarDMS.Filter
{
    public class FilterTestAttribute : Attribute, IHasRequestFilter
    {
        public ICacheClient Cache { get; set; }

        public int Priority { get; set; }

        public void RequestFilter(IHttpRequest req, IHttpResponse res, object requestDto)
        {
            var dto = requestDto as AttributeFiltered;
            dto.RequestFilterExecuted = true;
            dto.RequestFilterDependenyIsResolved = Cache != null;
        }
    }
}
*/

/*
http://stackoverflow.com/questions/10613153/custom-attributes-to-servicestack-methods
https://github.com/ServiceStack/ServiceStack/blob/master/src/ServiceStack.ServiceInterface/RequiredRoleAttribute.cs


[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class MyRequestFilterAttribute : RequestFilterAttribute
{

    public string Provider { get; set; }

    public MyRequestFilterAttribute(ApplyTo applyTo)  : base(applyTo)
    {
        this.Priority = (int)RequestFilterPriority.Authenticate;
    }

    public MyRequestFilterAttribute()  : this(ApplyTo.All)
    {
    }

    public MyRequestFilterAttribute(ApplyTo applyTo, string provider)   : this(applyTo)
    {
        this.Provider = provider;
    }

    public MyRequestFilterAttribute(string provider)  : this(ApplyTo.All)
    {
        this.Provider = provider;
    }

    public override void Execute(IHttpRequest req, IHttpResponse res, object requestDto)
    { }
}

[MyRequestFilter(ApplyTo.All)]
public class TodoService : RestServiceBase<Todo>
{
    public TodoRepository Repository { get; set; }
    public override object OnGet(Todo request)
    {
        if (request.Id == default(long))
            return Repository.GetAll();

        return Repository.GetById(request.Id);
    }
    public override object OnPost(Todo todo)
    {
        return Repository.Store(todo);
    }
    public override object OnPut(Todo todo)
    {
        return Repository.Store(todo);
    }
    [MyRequestFilter("Admin")]
    public override object OnDelete(Todo request)
    {
        Repository.DeleteById(request.Id);
        return null;
    }
    public object GetDetailsofALL()
    {
        return null;
    }
}
*/