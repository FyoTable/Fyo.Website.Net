using Fyo.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Fyo.Models {
    public class DeviceSoftware {
		public long DeviceId { get; set; }
		public Device Device { get; set; }
		public long SoftwareId { get; set; }
		public Software Software { get; set; }
    }
}