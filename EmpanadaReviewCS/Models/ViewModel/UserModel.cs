using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EmpanadaReviewCS.Models;

namespace EmpanadaReviewCS.Models.ViewModel
{
    public class UserModel
    {
        [Required(ErrorMessage = "Must add a User ID")]
        [Display(Name = "User ID")]
        [CodeExists]
        public int idUser { get; set; }

        [Required(ErrorMessage = "Must add a Password")]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Required(ErrorMessage = "Must add a First Name")]
        [Display(Name = "First Name")]
        [StringLength(15, ErrorMessage = "Title must be less than 15 characters")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Must add a Last Name")]
        [Display(Name = "Last Name")]
        [StringLength(15, ErrorMessage = "Title must be less than 15 characters")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Must add a Bio")]
        [Display(Name = "Bio")]
        [StringLength(500, ErrorMessage = "Description must be less than 500 characters")]
        public string bio { get; set; }

        [Required(ErrorMessage = "Must add a Created At")]
        [Display(Name = "Created At")]
        public System.DateTime createdAt { get; set; }

        [Display(Name = "Updated At")]
        public Nullable<System.DateTime> updatedAt { get; set; }

        [Display(Name = "Image Source")]
        public string imageSrc { get; set; }

        [Required(ErrorMessage = "Must add a Phone Number")]
        [Display(Name = "Phone Number")]
        [Range(10000000, 99999999, ErrorMessage = "Phone number must be an 8 digit number")]
        public int phoneNumber { get; set; }

        [Required(ErrorMessage = "Must add an Email")]     
        [Display(Name = "Email")]
        [EmailAddress]
        [StringLength(320, ErrorMessage = "Email must be less than 320 characters")]
        public string email { get; set; }
      
        [Display(Name = "Gender")]
        [StringLength(20, ErrorMessage = "Description must be less than 20 characters")]
        public string gender { get; set; }

        [Display(Name = "Review Quantity")]
        public Nullable<int> reviews { get; set; }

    }
}