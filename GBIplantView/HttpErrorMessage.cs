using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GBIplantView
{
    [DataContract]
    class HttpErrorMessage
    {
        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string ExceptionMessage { get; set; }

        [DataMember]
        public string MessageDetail { get; set; }
    }
}
