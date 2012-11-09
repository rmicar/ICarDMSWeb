using System;
using System.Net;
using Funq;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Common.Web;
using ServiceStack.Logging;
using ServiceStack.Logging.Support.Logging;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using ServiceStack.Redis;
using ServiceStack.ServiceHost;
using ServiceStack.WebHost.Endpoints;
using ServiceStack.Configuration;
using System.Configuration;
using ServiceStack.Logging.EventLog;
using ServiceStack.Logging.Log4Net;

namespace ICarDMS.App
{  
     #region Aplicacion ICarDMSApp
    // Este es el objeto aplicacion. 
    // Es un "ServiceStack.NET" web service host
    public class ICarDMSApp : AppHostBase
    {
        #region variables del objeto aplicación
        private static ILog Log;
        const string ConnectionKey = "Autonet2008";
        #endregion

        #region Constructor del objeto aplicacioón
        // Indicar a "ServiceStack" donde encontrar los servicios web.
        public ICarDMSApp() : base("ICarDMSWebAPI App", typeof(ICarDMS.Service.tgClienteService).Assembly)
        {
            Configure_Log();
        }
        #endregion

        public override void Configure(Container container)
        {

            Configure_ServiceStack();
            Configure_CORS();
            Configure_ORM(container);            
            //Configure_RequestFilter_Auth(container);
            //Configure_RequestFilter_SSL(container);
            Configure_RequestFilter_QueryString(container);
            Configure_ResponseFilters(container);
            Configure_Caching(container);
            Configure_Rutas();
            Configure_JSON();    
            Log.Debug("ICarDMSWebAPIApp configurada: " + DateTime.Now);
        }

        #region ServiceStack
        void Configure_ServiceStack()
        {         
            
            SetConfig(new EndpointHostConfig
            {
                DebugMode = false, 
                DefaultRedirectPath = "mydefaultpage.htm",
                EnableFeatures = Feature.Csv | Feature.CustomFormat | Feature.Json | Feature.Markdown , //Feature.Html
                AllowJsonpRequests = true
            });
        }
        #endregion

        #region Rutas URL a los servicios
        void Configure_Rutas()
        {

         
        //Register user-defined REST Paths
        //Routes
        //  .Add<ICarDMSWebAPI.Service.ClienteRequest>("/cliente")
        //  .Add<ICarDMSWebAPI.Service.ClienteRequest>("/cliente/{codigo}");
        }
        #endregion

        #region Configuracion Logging
        public void Configure_Log()
        {
            //LogManager.LogFactory = new ConsoleLogFactory();
            //LogManager.LogFactory = new EventLogFactory("ICarDMSWeb", "Application");
            LogManager.LogFactory = new Log4NetFactory(true); //Also runs log4net.Config.XmlConfigurator.Configure()
            log4net.Config.XmlConfigurator.Configure();
            Log = LogManager.GetLogger(typeof(ICarDMSApp));
        }
        #endregion

        #region Configuración CORS
        public void Configure_CORS()
        {

            // Permitimos a los browser enviar cualquier peticion HTTP, incluida "OPTIONS"
            // Esto está ligado al problema "Cross-site" CORS
            // https://developer.mozilla.org/en/http_access_control

            base.SetConfig(new EndpointHostConfig
            {
                GlobalResponseHeaders =
					{
						{ "Access-Control-Allow-Origin", "*" },
						{ "Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS" },
                        { "Access-Control-Allow-Headers", "Content-Type" },
					},
            });
        }
        #endregion

        #region Configuración ORM SQLServer
        public void Configure_ORM(Container container)
        {
            #region connectionString
            string connectionString;
            //http://www.connectionstrings.com/sql-server-2005
            //connectionString = "Data Source=Serversources;Database=autonet;UserId=ramon;Password=autonet;";
            //connectionString = appConfig.GetString("ConnectionString");

            connectionString = ConfigurationManager.ConnectionStrings[ConnectionKey].ToString(); 
            if (connectionString == null)
            {
                connectionString = "Data Source=Serversources;Initial Catalog=autonet;User Id=webapi;Password=webapi;";
            }


            #endregion
            // Usando ORMLite SQL Server
            container.Register<IDbConnectionFactory>(new OrmLiteConnectionFactory(connectionString, SqlServerOrmLiteDialectProvider.Instance));
        }
        #endregion

        #region Configuración Caching
        public void Configure_Caching(Container container)
        {
            // Caching
            // https://github.com/ServiceStack/ServiceStack/wiki/Caching
            // https://github.com/ServiceStack/ServiceStack.Redis
            bool hasRedis;
            hasRedis = false;
            if (hasRedis)
                //Usando REDIS
                //container.Register<ICacheClient>(new PooledRedisClientManager());
                container.Register<ICacheClient>(new BasicRedisClientManager());
            else
                //Usando in-memory cache
                container.Register<ICacheClient>(new MemoryCacheClient());

            //container.Register<ICacheClient>(new MemoryCacheClient());
        }
        #endregion

        #region Request Filters Auth
        public void Configure_RequestFilter_Auth (Container container)
        {
            
            this.RequestFilters.Add((httpReq, httpResp, requestDto) =>
            {
                var sessionId = httpReq.GetCookieValue("ss-session");
                if (sessionId == null)
                {
                    httpResp.ReturnAuthRequired();
                }
            });

            this.RequestFilters.Add((httpReq, httpResp, requestDto) =>
            {

                const string AllowedUser = "user";
                const string AllowedPass = "p@55word";
                Guid currentSessionGuid;

                var userPass = httpReq.GetBasicAuthUserAndPassword();
                if (userPass == null)
                {
                    return;
                }
                var LogName = userPass.Value.Key;
                var LogPass = userPass.Value.Value;
                if (LogName == AllowedUser && LogPass == AllowedPass)
                {
                    currentSessionGuid = Guid.NewGuid();
                    var sessionKey = LogName + "/" + currentSessionGuid.ToString("N");

                    //set session for this request (as no cookies will be set on this request)
                    httpReq.Items["ss-session"] = sessionKey;
                    httpResp.SetPermanentCookie("ss-session", sessionKey);
                }
            });

        }
        #endregion

        #region Request Filters SSL
        public void Configure_RequestFilter_SSL(Container container)
        {
            this.RequestFilters.Add((httpReq, httpResp, requestDto) =>
            {
                if (!httpReq.IsSecureConnection)
                {
                    httpResp.StatusCode = (int)HttpStatusCode.Forbidden;
                    httpResp.Close(); 
                }              
            });
        }
        #endregion

        #region Request Filters QueryString
        public void Configure_RequestFilter_QueryString(Container container)
        {
            this.RequestFilters.Add((httpReq, httpResp, requestDto) =>
            {
                if (httpReq.ResponseContentType != ContentType.Json)                
                {
                    string QueryFormat;
                    QueryFormat = httpReq.QueryString["$format"];
                    if (QueryFormat != null)
                    {
                        if (QueryFormat.ToUpper() == "JSON")
                        {
                            //httpReq.ContentType = ContentType.Json;
                            // httpResp.ContentType = ContentType.Json;
                        }
                    }
                }

                //     //var httpReq = base.RequestContext.Get<IHttpRequest>();
                //     var page = httpReq.QueryString["page"]; //etc                
            });
        }
        #endregion

        #region Response Filters
        public void Configure_ResponseFilters(Container container)
        {
            /*
            this.ResponseFilters.Add((httpReq, httpResp, requestDto) =>
             {
                 if (httpReq.ResponseContentType == ContentType.Json) { }
                 else
                 {
                     string QueryFormat;
                     QueryFormat = httpReq.QueryString["$format"];
                     if (QueryFormat.ToUpper() == "JSON")
                     {
                         httpResp.ContentType = ContentType.Json;
                     }
                 }

                     //     //var httpReq = base.RequestContext.Get<IHttpRequest>();
            //     var page = httpReq.QueryString["page"]; //etc                
             });
             */
        }
        #endregion

        #region Configuración JSON
        public void Configure_JSON()
        {           
            // JSON por defecto
            SetConfig(new EndpointHostConfig 
            {
                DefaultContentType = ContentType.Json
            });
            //Set JSON web services to return idiomatic JSON camelCase properties
            ServiceStack.Text.JsConfig.EmitCamelCaseNames = true;
            ServiceStack.Text.JsConfig.IncludeNullValues = true;
            
        }
        #endregion
    }
    #endregion
}