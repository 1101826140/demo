using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebFormDemo.Models
{
    [Table("books")]
    public class Book
    {
        [Key]
        public int book_id { get; set; }
        public string book_name { get; set; }
        public decimal book_price { get; set; }
        public string book_auth { get; set; }
    }
}