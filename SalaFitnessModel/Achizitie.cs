namespace SalaFitnessModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Achizitie")]
    public partial class Achizitie
    {
        public int AchizitieId { get; set; }

        public int? ClientId { get; set; }

        public int? AbonamentId { get; set; }

        public virtual Abonament Abonament { get; set; }

        public virtual Client Client { get; set; }
    }
}
