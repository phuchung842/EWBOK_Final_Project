namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public long ID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Code { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(250)]
        public string Tag { get; set; }

        [Column(TypeName = "ntext")]
        public string Decription { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImage { get; set; }

        public decimal? Price { get; set; }

        public decimal? PromotionPrice { get; set; }

        [StringLength(250)]
        public string Translator { get; set; }

        public int? Weight { get; set; }

        public int? Pages { get; set; }

        [StringLength(50)]
        public string Size { get; set; }

        [StringLength(100)]
        public string Form { get; set; }

        public bool? IncludeVAT { get; set; }

        public int Quantity { get; set; }

        public int? PublishYear { get; set; }

        public long? PublisherID { get; set; }

        public long? AuthorID { get; set; }

        public long? CategoryID { get; set; }

        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        public int? Warranty { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [StringLength(250)]
        public string MetaKeywords { get; set; }

        [StringLength(250)]
        public string MetaDecription { get; set; }

        public bool? Status { get; set; }

        public DateTime? TopHot { get; set; }

        public long? ViewCount { get; set; }

        public long? SellerCount { get; set; }

        public long? WishCount { get; set; }

        public decimal? ProductStatus { get; set; }

        public int? Star { get; set; }
    }
}
