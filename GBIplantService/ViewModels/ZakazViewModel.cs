using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace GBIplantService.ViewModels
{
    [DataContract]
    public class ZakazViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int BuyerId { get; set; }
        [DataMember]
        public string BuyerFIO { get; set; }
        [DataMember]
        public int GBIpieceOfArtId { get; set; }
        [DataMember]
        public string GBIpieceOfArtName { get; set; }
        [DataMember]
        public int? ExecutorId { get; set; }
        [DataMember]
        public string ExecutorName { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public decimal Sum { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string DateCreate { get; set; }
        [DataMember]
        public string DateExecute { get; set; }
    }
}
