using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvReader = LumenWorks.Framework.IO.Csv.CsvReader;

namespace ReviewsHandler
{
    class Program
    {
        static List<InvalidWord> AllInvalidWords = new List<InvalidWord>();
        static List<Review> AllReviews = new List<Review>();
        static List<Review> AllValidReviewsWithNoRating = new List<Review>();
        static List<Review> ReviewsToBePublished = new List<Review>();
        static void ReadWordsCsvFile()
        {
            var csvTable = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(@"C:\Users\itay.m\Downloads\Reviews\ST_SystemReport_Get Feedback Word Validation_12.9.2020_7.32 AM.csv")), true))
            {
                csvTable.Load(csvReader);
            }
            var csv = new StringBuilder();

            for (int i = 0; i < csvTable.Rows.Count; i++)
            {
                AllInvalidWords.Add(new InvalidWord
                {
                    wordId = Convert.ToInt32(csvTable.Rows[i][0]),
                    word = csvTable.Rows[i][1].ToString(),
                });          

                Console.WriteLine(AllInvalidWords[i]);
                var first = AllInvalidWords[i].wordId;
                var second = AllInvalidWords[i].word;          
            }
        }

        static void ReadReviewsCsvFile()
        {
            var csvTable = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(@"C:\Users\itay.m\Downloads\Reviews\ST_SystemReport_Get Feedback Product History_12.9.2020_7.33 AM.csv")), true))
            {
                csvTable.Load(csvReader);
            }

            var csv = new StringBuilder();

            for (int i = 0; i < csvTable.Rows.Count; i++)
            {
                // adding all the reviews to a list
                AllReviews.Add(new Review
                {
                    reviewID = Convert.ToInt32(csvTable.Rows[i][0]),
                    orderID = Convert.ToInt32(csvTable.Rows[i][1]),
                    ProductOverallRating = Convert.ToInt32(csvTable.Rows[i][2]),
                    RecommendToFriend = (csvTable.Rows[i][3].ToString()),
                    YourReview = (csvTable.Rows[i][4]).ToString(),
                    CustomerName = (csvTable.Rows[i][5]).ToString(),
                    CustomerID = Convert.ToInt32(csvTable.Rows[i][6]),
                    ProductID = Convert.ToInt32(csvTable.Rows[i][7]),
                    Approved = (csvTable.Rows[i][8]).ToString(),
                    SKU = (csvTable.Rows[i][9]).ToString(),
                    ShowFeedback = (csvTable.Rows[i][10]).ToString(),

                });
                Console.WriteLine(AllReviews[i]);
                if (AllReviews[i].Approved == "FALSE" && AllReviews[i].YourReview == null && AllReviews[i].ProductOverallRating == 5)
                {
                    AllReviews[i].Approved = "TRUE";
                    ReviewsToBePublished.Add(AllReviews[i]);
                }
                else if (AllReviews[i].Approved == "FALSE" && AllReviews[i].YourReview == null && AllReviews[i].ProductOverallRating == 4)
                {
                    AllReviews[i].Approved = "TRUE";
                    ReviewsToBePublished.Add(AllReviews[i]);
                }
                else if (AllReviews[i].Approved == "FALSE" && AllReviews[i].YourReview == null && AllReviews[i].ProductOverallRating == 3)
                {
                    AllReviews[i].Approved = "TRUE";
                    ReviewsToBePublished.Add(AllReviews[i]);
                }

                // running on invalid word list
                int wordCounter = 0;
                for (int j = 0; j < AllInvalidWords.Count; j++)
                {
                    
                    
                    // checking for invalid words in A SINGLE REVIEW
                    if (AllReviews[i].YourReview.Contains(AllInvalidWords[j].word))
                    {
                        wordCounter++;
                        Console.WriteLine($"{AllReviews[i]} has {AllInvalidWords[j].word} in the review");
                       
                        
                    }
                    //printing total number of invalid words in A SINGLE REVIEW
                    
                }
                Console.WriteLine($"{AllReviews[i]} has {wordCounter} invalid words, and has rating of {AllReviews[i].ProductOverallRating}");
               
                if (AllReviews[i].ProductOverallRating == 0 && wordCounter == 0 && AllReviews[i].RecommendToFriend == "TRUE")
                {
                    AllValidReviewsWithNoRating.Add(AllReviews[i]);
                }
            }





        }

        static void ApplyStarsToReviews(List<Review> AllValidReviewsWithNoRating)
        {
            int reviewCounter = 0;
            for (int i = 0; i < AllValidReviewsWithNoRating.Count; i++)
            {
                reviewCounter++;
                int CheckRemdr = reviewCounter % 5;
                int checkmerd = 6 / 5;
                double dws = 6 / 5f;
                double aa = dws - checkmerd;
            
                if (reviewCounter <= 4)
                {
                    AllValidReviewsWithNoRating[i].ProductOverallRating = 5;
                }
                else if (CheckRemdr == 0)
                {
                    AllValidReviewsWithNoRating[i].ProductOverallRating = 4;
                }
                else if (aa < 1)
                {
                    AllValidReviewsWithNoRating[i].ProductOverallRating = 5;
                }
                Console.WriteLine(AllValidReviewsWithNoRating[i].ToString());
                ReviewsToBePublished.Add(AllValidReviewsWithNoRating[i]);

            }
           
        }
    
        static void Main(string[] args)
        {
          //  PrintReport();
            var csvTable = new DataTable();
            var csv = new StringBuilder();

            ReadWordsCsvFile();
            ReadReviewsCsvFile();
            ApplyStarsToReviews(AllValidReviewsWithNoRating);

            
            foreach (Review review in ReviewsToBePublished)
            {
                review.Approved = "TRUE";
                Console.WriteLine(review);
                var first = review.reviewID;
                var second = review.orderID;
                var third = review.ProductOverallRating;
                var fourth = review.RecommendToFriend;
                var fifth = review.YourReview;
                var sixth = review.CustomerName;
                var seventh = review.CustomerID;
                var eight = review.ProductID;
                var nine = review.Approved;
                var ten = review.SKU;
                var eleven = review.ShowFeedback;
                var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", first, second, third, fourth, fifth, sixth, seventh, eight, nine, ten, eleven);
                csv.Append(newLine + Environment.NewLine);
                File.WriteAllText(@"C:\Users\itay.m\Downloads\Reviews\ST_SystemReport_Get Feedback Product History_12.9.2020_7.33 AM - NEW.csv", csv.ToString());
            }

            Console.ReadLine();
        }



    }
    
}
