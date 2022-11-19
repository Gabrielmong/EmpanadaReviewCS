using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmpanadaReviewCS.Models.ViewModel {
    public class RatingModel {
        public int idRating { get; set; }
        public int score { get; set; }
        public System.DateTime createdAt { get; set; }
        public Nullable<System.DateTime> updatedAt { get; set; }
        public Nullable<int> idRestaurant { get; set; }

    }
}