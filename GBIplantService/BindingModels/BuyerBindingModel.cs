using System.Runtime.Serialization;

namespace GBIplantService.BindingModels
{
    [DataContract]
    public class BuyerBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Mail { get; set; }

        [DataMember]
        public string BuyerFIO { get; set; }
    }
}
