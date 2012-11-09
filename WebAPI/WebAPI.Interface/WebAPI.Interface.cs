using ServiceStack.ServiceInterface;

namespace ICarDMS.Interface
{
        public interface IPBProxy
        {
            void Debug(string message);
            void Execute(ServiceStack.ServiceInterface.Service Service, object request, object response);
            string URL();
            string HTTPMethod();
            string DTOName();
            string QueryString(string key);
        }
  
}
