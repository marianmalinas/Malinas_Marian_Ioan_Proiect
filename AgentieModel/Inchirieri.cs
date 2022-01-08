namespace AgentieModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Inchirieri")]
    public partial class Inchirieri
    {
        public int Id { get; set; }

        public int? ApId { get; set; }

        public int? ClientId { get; set; }

        public int? AngajatId { get; set; }

        public virtual Angajati Angajati { get; set; }

        public virtual Apartamente Apartamente { get; set; }

        public virtual Clienti Clienti { get; set; }
    }
}
