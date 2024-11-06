using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkPractice.Models
{
     class Detail
    {
        [Key]
        public int ID {  get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public String Address { get; set; }
        public DateTime DOB { get; set; }

    }
}
