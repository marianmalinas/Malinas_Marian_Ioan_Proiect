namespace AgentieModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Apartamente")]
    public partial class Apartamente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Apartamente()
        {
            Inchirieris = new HashSet<Inchirieri>();
        }

        [Key]
        public int ApId { get; set; }

        [StringLength(70)]
        public string Adresa { get; set; }

        public int? Pret { get; set; }

        [StringLength(20)]
        public string DataPublicare { get; set; }

        [StringLength(50)]
        public string Cartier { get; set; }

        [StringLength(250)]
        public string Descriere { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inchirieri> Inchirieris { get; set; }
    }
}
