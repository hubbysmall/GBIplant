using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace GBIplantService.BindingModels
{
    [DataContract]
    public class GBIpieceofArt__ingridientBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int GBIpieceofArtId { get; set; }
        [DataMember]
        public int GBIingridientId { get; set; }
        [DataMember]
        public int Count { get; set; }
    }
}
