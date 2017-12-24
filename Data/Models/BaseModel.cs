using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fyo.Models {
    public class BaseModel {
        [Key]
        public long ID { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime ModifiedDate { get; set; }

        public BaseModel()
        {
            IsDeleted = false;
        }
    }

    public class BaseModelModifiedBy : BaseModel {
        public long? ModifiedByUserID { get; set; }
        public User ModifiedByUser { get; set; }
    }
}