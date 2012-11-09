using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;
using ServiceStack.Common.Web;

using ICarDMS.Model;



namespace ICarDMS.Service
{
    // http://msdn.microsoft.com/es-es/library/system.runtime.serialization.datamemberattribute.aspx

    #region DTO
    // DTO Request  GET(search)
    [Description("Endpoint de clientes")]
    [Route("/tgclientes", "GET")]
    [Route("/tgcliente", "GET")]
    public class tgClienteSearchDTO
    {
        public int codigo { get; set; }
        public string apellidosearch { get; set; }
    }



    // DTO Request PUT,POST,PATCH,DELETE, GET{codigo}
    [Description("Endpoint de cliente")]
    [Route("/tgcliente/{codigo}")]
    public class tgClienteDTO
    {
        public int codigo { get; set; }
    }

    // DTO Response
    [DataContract]
    public class tgClienteDTOResponse : IHasResponseStatus
    {
        [DataMember(Name = "tgcliente")]
        public List<tgcliente> Results { get; set; }
        public ResponseStatus ResponseStatus { get; set; }

        public tgClienteDTOResponse()
        {
            this.ResponseStatus = new ResponseStatus();
            this.Results = new List<tgcliente>();
        }
    }

    #endregion
}