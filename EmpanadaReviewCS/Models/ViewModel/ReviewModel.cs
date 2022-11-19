using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EmpanadaReviewCS.Models;

namespace EmpanadaReviewCS.Models.ViewModel {
    public class ReviewModel {

        [Required(ErrorMessage = "Must add an ID")]
        [Display(Name = "Review ID")]
        [CodeExists]
        public int idReview { get; set; }

        [Required(ErrorMessage = "Must add a User ID")]
        [Display(Name = "User ID")]
        public int idUser { get; set; }

        [Required(ErrorMessage = "Must add a Rating")]
        [Display(Name = "Rating")]
        public int idRating { get; set; }

        [Required(ErrorMessage = "Must add a Title")]
        [Display(Name = "Title")]
        [StringLength(30, ErrorMessage = "Title must be less than 30 characters")]
        public string title { get; set; }

        [Required(ErrorMessage = "Must add a Description")]
        [Display(Name = "Description")]
        [StringLength(1024, ErrorMessage = "Description must be less than 1024 characters")]
        public string description { get; set; }

        [Required(ErrorMessage = "Must add a Created At")]
        [Display(Name = "Created At")]
        public System.DateTime createdAt { get; set; }

        [Display(Name = "Updated At")]
        public Nullable<System.DateTime> updatedAt { get; set; }

        [Display(Name = "Image Source")]
        public string imageSrc { get; set; }

        [Display(Name = "Restaurant ID")]
        public Nullable<int> idRestaurant { get; set; }

        [Display(Name = "Likes")]
        public Nullable<int> likes { get; set; }
    }

    public class CodeExists : ValidationAttribute {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            var review = (ReviewModel)validationContext.ObjectInstance;
            var db = new EmpanadaReviewEntities();
            var reviewExists = db.Reviews.Any(r => r.idReview == review.idReview);
            if (reviewExists) {
                return new ValidationResult("Review ID already exists");
            }
            return ValidationResult.Success;
        }
    }
}
