using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WsPortfolioExpress.Common.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        [Required]
        [StringLength(50)]
        public string? SurName { get; set; }

        [Required]
        [StringLength(25)]
        public string? Document { get; set; }

        [Required]
        [StringLength(3)]
        public string? DocumentType { get; set; }

        [Required]
        [StringLength(100)]
        public string? Email { get; set; }

        public DateTime CreateDate { get; set; }

        [StringLength(500)]
        public string? Imagen { get; set; }
    }
}
