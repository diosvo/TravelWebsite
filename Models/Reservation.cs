using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Web.Mvc;

namespace TravelWebsite.Models
{
    [Table("Reservation")]
    public class Reservation
    {
        [Key]
        public int ReID { get; set; }

        [Required]
        [MaxLength(40, ErrorMessage = "Max 40 char allowed")]
        public string Name { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Max 20 char allowed")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Wrong email pattern")]
        public string Email { get; set; }

        [Required]
        [MaxLength(40, ErrorMessage = "Max 40 char allowed")]
        public string Offers { get; set; }

        [Required]
        [MaxLength(40, ErrorMessage = "Max 40 char allowed")]
        public string Services { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "Max 10 char allowed")]
        public string phoneNumber { get; set; }

        [Required]
        [MaxLength(15, ErrorMessage = "Max 15 char allowed")]
        public string NIC { get; set; }
    }

    [Table("Payment")]
    public class Payment
    {
        public int ID { get; set; }
        public string paymentMode { get; set; }
        public virtual Reservation Reservation { get; set; }
        public int ReID { get; set; }
    }

    [Table("Package")]
    public class Package
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(40, ErrorMessage = "Max 40 char allowed")]
        public string Offer { get; set; }

        public string Image { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Max 100 char allowed")]
        public string Description { get; set; }
    }

    [Table("Services")]
    public class Services
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(40, ErrorMessage = "Max 40 char allowed")]
        public string Service { get; set; }

        [Required]
        [MaxLength(40, ErrorMessage = "Max 40 char allowed")]
        public string Image { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Max 100 char allowed")]
        public string Description { get; set; }
    }

    [Table("Login")]
    public class Login
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Max 20 char allowed")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Wrong email pattern")]
        public string Email { get; set; }

        [Required]
        [MaxLength(16, ErrorMessage = "Max 16 char allowed")]
        [MinLength(6, ErrorMessage = "Min 6 char allowed")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class cs : DbContext
    {
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Services> Services { get; set; }
    }
}