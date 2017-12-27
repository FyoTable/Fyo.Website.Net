using Fyo.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Fyo.Models {
    public class SoftwareVersion : BaseModel {
        public Software Software { get; set; }

		public string Version { get; set; }

		public string APK { get; set; }

		public string URL { get; set; }

		List<DeviceSoftwareVersion> DeviceSoftwareVersions { get; set; }
    }
}