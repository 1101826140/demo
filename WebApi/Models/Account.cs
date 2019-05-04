using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{

    [Table("sun_Account")]
    public class Account
    {

        public int Id { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}