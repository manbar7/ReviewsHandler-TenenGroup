using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewsHandler
{
    class Review
    {
        public int reviewID { get; set; }
        public int orderID { get; set; }
        public int ProductOverallRating { get; set; }
        public string RecommendToFriend { get; set; }
        public string YourReview { get; set; }
        public string CustomerName { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public string Approved { get; set; }
        public string SKU { get; set; }
        public string ShowFeedback { get; set; }

        public Review()
        {
        }

        public override string ToString()
        {
            return $"ID:{reviewID}|OrderID:{orderID}|ProductOverallRating:{ProductOverallRating}|RecommendtoAFriend:{RecommendToFriend}|Review:{YourReview}|Cus Name:{CustomerName}|Cus ID:{CustomerID}|ProductID:{ProductID}Approved:{Approved}|SKU:{SKU}|Feedback:{ShowFeedback}";
        }
    }
}
