using Fyo.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Fyo.Models {
    public class Code : BaseModel {
        public long DeviceId { get; set; }
        public Device Device { get; set; }

        public string Digits { get; set; }
        public int Player { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}