using System;

namespace EmpanadaReviewCS.Models.ViewModel {
    public class MasterListModel {
        public int idReview { get; set; }
        public int idUser { get; set; }
        public int idRating { get; set; }

        public int idRestaurant { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime createdAt { get; set; }
        public string imageSrc { get; set; }
        public int? likes { get; set; }
        public int score { get; set; }
        public string name { get; set; }
        public string userName { get; set; }
    }
}