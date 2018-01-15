using Fyo.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Fyo.Models {
    public class Software : BaseModel {
        public string Name { get; set; }
		List<SoftwareVersion> SoftwareVersions { get; set; }
        public string Package { get; set; }
    }
}