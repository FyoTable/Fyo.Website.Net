using Fyo.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Fyo.Models {
    public class DeviceSoftwareVersion {
			public long DeviceId { get; set; }
			public Device Device { get; set; }
			public long SoftwareVersionId { get; set; }
			public SoftwareVersion SoftwareVersion { get; set; }
    }
}