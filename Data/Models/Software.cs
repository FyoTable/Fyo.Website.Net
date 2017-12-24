using Fyo.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Fyo.Models {
    public class Software : BaseModel {

		public string UniqueID { get; set; }

        public string Name { get; set; }

		public string APK { get; set; }

		public string URL { get; set; }

		public string Version { get; set; }

		List<DeviceSoftware> DeviceSoftware { get; set; }
    }
}