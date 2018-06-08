using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace GBIplantService.BindingModels
{
    [DataContract]
    public class GBIpieceOfArtBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string GBIpieceOfArtName { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public List<GBIpieceofArt__ingridientBindingModel> GBIpieceofArt__ingridients{ get; set; }
    }
}
