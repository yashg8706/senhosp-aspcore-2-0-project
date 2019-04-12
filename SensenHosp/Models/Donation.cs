using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensenHosp.Models
{
    public class Donation
    {
        public int ID { get; set; }

        public string DonorName { get; set; }

        public string DonorEmail { get; set; }

        public string Amount { get; set; }

        public string PayPalOrderId { get; set; }
    }
}
