namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public long ID { get; set; }

        public DateTime? CreateDate { get; set; }

        public long? CustomerID { get; set; }

        public short? PctDiscount { get; set; }

        public decimal? Total { get; set; }

        public decimal? TotalDiscount { get; set; }

        [StringLength(100)]
        public string ShipName { get; set; }

        [StringLength(50)]
        public string ShipMobile { get; set; }

        [StringLength(100)]
        public string ShipAddress { get; set; }

        [StringLength(100)]
        public string ShipEmail { get; set; }

        public int? Status { get; set; }
    }
}
