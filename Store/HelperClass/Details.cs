﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.HelperClass
{
    public class Details
    {
        [JsonProperty("petId")]
        public int PetId { get; set; }
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
        [JsonProperty("shipDate")]
        public string ShipDate { get; set; }
        [JsonProperty("status")]
        public string status { get; set; }
        [JsonProperty("complete")]
        public bool Complete { get; set; }

    }
}
