using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WsPortfolioExpress.Web.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? SurName { get; set; }

        public string? Document { get; set; }

        public string? DocumentType { get; set; }

        public string? Email { get; set; }

        public DateTime CreateDate { get; set; }

        public string? Imagen { get; set; }

        [DisplayName("Upload File")]
        public IFormFile? ImageFile { get; set; }
    }
}
