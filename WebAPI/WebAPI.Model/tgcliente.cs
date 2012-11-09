using System;
using ServiceStack.DataAnnotations;
using System.Runtime.Serialization;


//https://github.com/ServiceStack/ServiceStack.OrmLite/blob/master/src/ServiceStack.OrmLite.SqlServer/SqlServerOrmLiteDialectProvider.cs#L74
//https://github.com/ServiceStack/ServiceStack.OrmLite/blob/master/src/ServiceStack.OrmLite/OrmLiteDialectProviderBase.cs#L167
//https://github.com/ServiceStack/ServiceStack.OrmLite
//https://github.com/ServiceStack/ServiceStack.OrmLite/blob/master/tests/ServiceStack.OrmLite.Tests/UseCase/CustomerOrdersUseCase.cs


namespace ICarDMS.Model
{
    [DataContract]
    public class tgcliente
    {
        string _nombre    = string.Empty;
        string _apellido1 = string.Empty;
        string _apellido2 = string.Empty;
        string _apellidosearch = string.Empty;
        string _direccioneditada = string.Empty;

       
        //[Required]
        //[Alias("ShipperTypes")]
        //[AutoIncrement] 
        //[Index(Unique = true)]
        //[References(typeof(Customer))] 
        //[StringLength(24)]
        //[PrimaryKey]
        //[IgnoreDataMember]
        //[Sequence]
        //[Schema]
        [DataMember]
        public int codigo { get; set; }
        [DataMember]
        public string nombre { get { return _nombre.TrimEnd(); } set { _nombre = (value != null) ? value : string.Empty; } }
        [DataMember]
        public string apellido1 { get { return _apellido1.TrimEnd(); } set { _apellido1 = (value != null) ? value : string.Empty; } }
        [DataMember]
        public string apellido2 { get { return _apellido2.TrimEnd(); } set { _apellido2 = (value != null) ? value : string.Empty; } }
        [DataMember]
        public string direccioneditada { get { return _direccioneditada.TrimEnd(); } set { _direccioneditada = (value != null) ? value : string.Empty; } }
        [DataMember]
        public DateTime altafec { get; set; }              
        [IgnoreDataMember]
        public string apellidosearch { get; set; } 
    }
}
