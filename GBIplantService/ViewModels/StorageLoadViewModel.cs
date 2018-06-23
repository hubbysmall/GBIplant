using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace GBIplantService.ViewModels
{
    [DataContract]
    public class StorageLoadViewModel
    {
        [DataMember]
        public string StorageName { get; set; }
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public List<StocksComponentLoadViewModel> GBIingridients { get; set; }
    }

    [DataContract]
    public class StocksComponentLoadViewModel
    {
        [DataMember]
        public string GBIingridientname { get; set; }

        [DataMember]
        public int Count { get; set; }
    }
}
