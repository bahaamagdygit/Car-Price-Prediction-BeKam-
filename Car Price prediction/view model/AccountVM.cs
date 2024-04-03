using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Car_Price_prediction.view_model
{
   
    public class Login_Vm
    {   
        [Required(ErrorMessage = "Please enter name"), MaxLength(30)]
        [Display(Name = "User Name")]
        public string User_Name{ get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password{ get; set; }
    }

    public class Registration_Vm
    {
        public string ID { get; set; }
        [Required(ErrorMessage = "Please enter name"), MaxLength(30)]
        [Display(Name = "User Name")]
        public string User_Name { get; set; }
        public string Name { get; set; } 
        public int  Age { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Confirm Password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string Confirm_Password { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter Email ID")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]

        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string phone_num { get; set; }
       /* public string userRole { get; set; }*/
    }
}