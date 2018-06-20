using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBIplantService.ViewModels
{
    public class BuyerZakazesViewModel
    {
        public string BuyerName { get; set; }

        public string DateCreate { get; set; }

        public string GBIpieceOfArtName { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public string Status { get; set; }
    }
}
