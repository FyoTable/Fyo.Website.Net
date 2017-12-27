using Fyo.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Fyo.Models {
    public class Device : BaseModel {
        
        //currently this is auth0 and comes in the format auth0|<userid>
        public Guid UniqueIdentifier { get; set; }

        public string Name { get; set; }
		
        public string WirelessAccessPoint { get; set; }
        public string WirelessAccessPointIP { get; set; }
        public string IPAddress { get; set; }

		public List<DeviceSoftwareVersion> DeviceSoftwareVersions { get; set; }
    }
}