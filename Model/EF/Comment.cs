namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comment")]
    public partial class Comment
    {
        public long ID { get; set; }

        public long? UserID { get; set; }

        public long? ProductID { get; set; }

        public string Content { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? DisplayOrder { get; set; }

        public bool? Status { get; set; }
    }
}
