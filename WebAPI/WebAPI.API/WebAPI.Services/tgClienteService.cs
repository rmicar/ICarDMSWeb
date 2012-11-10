using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

using ServiceStack.OrmLite;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;
using ServiceStack.ServiceInterface.Cors;
using ServiceStack.Common.Web;
using ServiceStack.Logging;

using ICarDMS.Interface;
using ICarDMS.Model;
using ICarDMS.Proxy.Net;


namespace ICarDMS.Service
{
    public class tgClienteService : ServiceStack.ServiceInterface.Service
    {
        public IDbConnectionFactory DbFactory { get; set; }
        private ILog Log;
        private List<tgcliente> Results;
        private tgClienteDTOResponse responseDTO;
        public  ICarDMSProxy proxy;

        // Constructor
        public tgClienteService()
        {
            Log = LogManager.GetLogger(GetType());
            responseDTO = new tgClienteDTOResponse();
            proxy = new ICarDMSProxy();

        }
        // Debug
        public void Debug(string message)
        {
            if (Log.IsDebugEnabled)
            {
                Log.Debug(message);
            }
        }
        //Search
        public object Get(tgClienteSearchDTO request)
        {
            
            int ResultLimit = 20;
            int PageSize;
            int PageNumber;
            string QueryOrderby;
            string QueryFilter;
            string QuerySelect;
            string QueryApellido;
            string QueryFormat;

            
            Debug("ClienteService.Get/Search");

            SqlExpressionVisitor<tgcliente> ev = OrmLiteConfig.DialectProvider.ExpressionVisitor<tgcliente>();

            
          
            

            /*
            perPageAttribute: '$top',
            skipAttribute: '$skip',
            orderAttribute: 'orderBy',
            customAttribute1: '$inlinecount',
            queryAttribute: '$filter',
            formatAttribute: '$format',
            customAttribute2: '$callback',`
            */
                
            var httpReq   = base.RequestContext.Get<IHttpRequest>();
            
            PageNumber    = Convert.ToInt32(httpReq.QueryString["$skip"]);
            PageSize      = Convert.ToInt32(httpReq.QueryString["$take"]);
            QueryOrderby  = httpReq.QueryString["$orderby"];
            QueryFilter   = httpReq.QueryString["$filter"];
            QuerySelect   = httpReq.QueryString["$select"];
            QueryFormat   = httpReq.QueryString["$format"];
            QueryApellido = httpReq.QueryString["apellidosearch"];


   
            if (PageSize > 0)
                {
                if (PageSize > ResultLimit) {PageSize = ResultLimit;}
                }
            else
                {
                PageSize = ResultLimit;
                }
            if (PageNumber < 0) { PageNumber = 0; }                    
                    
            if (string.IsNullOrEmpty(QueryApellido))                        
            {
                //ListaClientes = DbFactory.Exec(dbCmd => dbCmd.Select<tgcliente>());   
                //<tgcliente> ev1 = OrmLiteConfig.DialectProvider.ExpressionVisitor<tgcliente>();
                ev.OrderBy (rn => rn.codigo);
                ev.Limit   (PageNumber * PageSize, PageSize);
                Results = DbFactory.Run(dbCmd => dbCmd.Select(ev));
            }
            else
            {
                QueryApellido.ToUpper();
                //https://github.com/ServiceStack/ServiceStack.OrmLite/blob/master/src/AllDialectsTest/Main.cs#L362
                //https://github.com/ServiceStack/ServiceStack.OrmLite/issues/49#issuecomment-5696437                 

                ev.Where(rn => rn.apellidosearch.Contains(QueryApellido.ToString()));                            
                ev.OrderBy (rn => rn.codigo);
                ev.Limit(PageNumber * PageSize, PageSize);
                //"SELECT * FROM TGCLIENTE WHERE APELLIDOSEARCH LIKE '%" + apellidosearch + "%'"
                   
                Results = DbFactory.Run(dbCmd => dbCmd.Select(ev));
            }

            Debug("Devuelvo ... " + Results.Count.ToString());
            responseDTO.Results = Results;            
            return responseDTO;
            /*
            using (var dbConn = DbFactory.OpenDbConnection())
			using (var dbCmd = dbConn.CreateCommand())
			{
				var orders = request.CustomerId.IsNullOrEmpty()
					? dbCmd.Select<Order>("ORDER BY OrderDate DESC LIMIT {0}, {1}", (request.Page.GetValueOrDefault(1) - 1) * PageCount, PageCount)
					: dbCmd.Select<Order>("CustomerId = {0}", request.CustomerId);

				if (orders.Count == 0) 
					return new OrdersResponse();

				var orderDetails = dbCmd.Select<OrderDetail>(
					"OrderId IN ({0})", new SqlInValues(orders.ConvertAll(x => x.Id)));

				var orderDetailsLookup = orderDetails.ToLookup(o => o.OrderId);

				var customerOrders = orders.ConvertAll(o =>
					new CustomerOrder
					{
						Order = o,
						OrderDetails = orderDetailsLookup[o.Id].ToList()
					});

				return new OrdersResponse { Results = customerOrders };
			}

            */

        }
        // GET
        public object Get(tgClienteDTO requestDTO)
        {
            
            Debug("tgClienteService.Get("+ requestDTO.codigo.ToString() +")");         
            //ClienteByID =DtoUtils.CreateResponseDto
            Debug("llamando a proxy.Execute()");
            proxy.Execute(this,requestDTO,responseDTO);
            return responseDTO;
        }      
        // UPDATE
        public object Put(tgClienteDTO requestDTO)
        {
            proxy.Execute(this,requestDTO, responseDTO);
            return responseDTO;
        }
        // PATCH
        public object Patch(tgClienteDTO requestDTO)
        {
            proxy.Execute(this,requestDTO, responseDTO);
            return responseDTO;
        }
        // INSERT
        public object Post(tgClienteDTO requestDTO)
        {
            proxy.Execute(this,requestDTO, responseDTO);
            return responseDTO;
        }
        // DELETE
        public object Delete(tgClienteDTO requestDTO)
        {
            proxy.Execute(this,requestDTO, responseDTO);
            return responseDTO;
        }
        // OPTIONS
        [EnableCors]
        public void Options(tgClienteDTO request) { }
    }
}