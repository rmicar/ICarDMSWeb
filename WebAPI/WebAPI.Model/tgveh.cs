using System;
using ServiceStack.DataAnnotations;
using System.Runtime.Serialization;


namespace ICarDMS.Model
{
    [DataContract]
    public class tgveh
    {
        string _matric    = string.Empty;
        string _chasis = string.Empty;
        string _marca = string.Empty;
        
    
        [DataMember]
        public string chasis { get { return _chasis.TrimEnd(); } set { _chasis = (value != null) ? value : string.Empty; } }
        [DataMember]
        public string matric { get { return _matric.TrimEnd(); } set { _matric = (value != null) ? value : string.Empty; } }
        [DataMember]
        public string marca { get { return _marca.TrimEnd(); } set { _marca = (value != null) ? value : string.Empty; } }
     
    }
}
