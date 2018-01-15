using Fyo.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Fyo.Models {
    public class SoftwareVersion : BaseModel {
        public long SoftwareId { get; set; }
        public Software Software { get; set; }

		public string Version { get; set; }

		public string Apk { get; set; }

		List<DeviceSoftwareVersion> DeviceSoftwareVersions { get; set; }
    }
}