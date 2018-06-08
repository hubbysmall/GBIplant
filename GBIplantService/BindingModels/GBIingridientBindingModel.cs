using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace GBIplantService.BindingModels
{
    [DataContract]
    public class GBIingridientBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string GBIingridient { get; set; }
    }
}
