using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace GBIplantService.BindingModels
{
    [DataContract]
    public class ZakazBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int BuyerId { get; set; }
        [DataMember]
        public int GBIpieceOfArtId { get; set; }
        [DataMember]
        public int? ExecutorId { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public decimal Sum { get; set; }
    }
}
