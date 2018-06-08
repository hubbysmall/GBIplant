using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace GBIplantService.ViewModels
{
    [DataContract]
    public class Storage__GBIingridientViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int StorageId { get; set; }
        [DataMember]
        public int GBIingridientId { get; set; }
        [DataMember]
        public string GBIingridientName { get; set; }
        [DataMember]
        public int Count { get; set; }
    }
}
