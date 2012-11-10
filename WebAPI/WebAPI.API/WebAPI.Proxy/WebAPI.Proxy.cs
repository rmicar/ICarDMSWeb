using ServiceStack.ServiceHost;
using ServiceStack.Logging;
using ServiceStack.ServiceInterface.ServiceModel;
using ServiceStack.ServiceInterface;
using ICarDMS.Interface;
using ICarDMS.Proxy.PB;

namespace ICarDMS.Proxy.Net
{
    public class ICarDMSProxy : IPBProxy
    {

        ILog Log = LogManager.GetLogger(typeof(ICarDMSProxy));
        ILog LogBO = LogManager.GetLogger("ICarDMSBO");
        
        public ServiceStack.ServiceHost.IRequestContext requestContext { get; set; }
        public ServiceStack.ServiceHost.IHttpRequest httpRequest { get; set; }
        public ServiceStack.ServiceHost.IHttpResponse httpResponse { get; set; }
        public ServiceStack.ServiceInterface.Service Service { get; set; }

        public object requestDTO { get; set; }
        public object responseDTO { get; set; }
        public string entityID { get; set; }
/*
        public ICarDMSProxy () ServiceStack.ServiceHost.IRequestContext requestContext)
        {
                
                this.requestContext = requestContext;
                this.httpRequest = requestContext.Get<IHttpRequest>();
                this.httpResponse = requestContext.Get<IHttpResponse>();
               
        }
  */      
        public void Execute( ServiceStack.ServiceInterface.Service paramService, object paramRequest, object paramResponse)
        {
            this.Service = paramService;
            this.requestContext = this.Service.RequestContext;
            this.httpRequest = requestContext.Get<IHttpRequest>();
            this.httpResponse = requestContext.Get<IHttpResponse>();
            this.requestDTO = paramRequest;
            this.responseDTO = paramResponse;
            Log.Debug("ICarDMSProxy.ExecuteBO()");
            ICarDMS.Proxy.PB.n_webapiproxy pepe = new ICarDMS.Proxy.PB.n_webapiproxy();
            pepe.execute(this);
            return;
        }
        /*
        public object Execute(object request)
        {
            Log.Debug("ICarDMSProxy.ExecuteBO()");
            requestDTO = request;
            //         n_webapiproxy pepe = new n_webapiproxy();
   //         pepe.execute(this);
            return responseDTO;
        }
         */
         
        public void Debug (string message)
        {
            if (LogBO.IsDebugEnabled)
              {
                LogBO.Debug(message);
              }
        }
        public string HTTPMethod()
        {
            return httpRequest.HttpMethod;
        }
        public string DTOName()
        {
            return httpRequest.OperationName;
        }
        public string URL()
        {
            return httpRequest.PathInfo;
        }
        public string QueryString(string key)
        {
            return httpRequest.QueryString[key];
        }
        public void addErrorMessage (string message)
        {
            //DTOResponse.ResponseStatus.Message = message;
        }
        /*
        public object ExecuteService(object requestDto, EndpointAttributes endpointAttributes)
        {
            return EndpointHost.Config.ServiceController.Execute(requestDto,
                new HttpRequestContext(requestDto, endpointAttributes));
        } 
         * */
    }
}