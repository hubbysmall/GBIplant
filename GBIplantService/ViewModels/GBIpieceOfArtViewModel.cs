using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace GBIplantService.ViewModels
{
    [DataContract]
    public class GBIpieceOfArtViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string GBIpieceOfArtName { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public List<GBIpieceofArt__ingridientViewModel> GBIpieceofArt__ingridients { get; set; }
    }
}
