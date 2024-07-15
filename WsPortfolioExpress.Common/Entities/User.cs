using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WsPortfolioExpress.Common.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required]
        [StringLength(100)]
        public string? Email { get; set; }

        [Required]
        [StringLength(200)]
        public string? Password { get; set; }

        public bool Restore { get; set; }

        public bool Confirmed { get; set; }

        [Required]
        [StringLength(200)]
        public string? Token { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
