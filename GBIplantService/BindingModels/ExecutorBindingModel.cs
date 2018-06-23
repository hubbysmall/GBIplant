using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace GBIplantService.BindingModels
{
    [DataContract]
    public class ExecutorBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string ExecutorFIO { get; set; }
    }
}
