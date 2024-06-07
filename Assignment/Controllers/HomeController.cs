using Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Diagnostics;

namespace Assignment.Controllers
{
    public class HomeController : Controller
    {
        //Connection String
        private readonly string _connectionString = "Data Source=labG9AEB3;Initial Catalog=Assignment_DB;Integrated Security=True;Trust Server Certificate=True";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(SurveyFormData formData)
        {
            // Insert the form data into the database
            InsertSurveyData(formData);

            // Set ViewBag message
            ViewBag.Message = "Survey data saved successfully.";

            // Redirect to a success page or display a success message
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Survey_Result()
        {
            // Fetch data from the database and pass it to the view
            SurveyResultData surveyResultData = GetSurveyResultData();
            return View(surveyResultData);
        }

        private void InsertSurveyData(SurveyFormData formData)
        {
            //Using Sql Connecting to connect with our database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                //Insert value into the data
                string query = @"INSERT INTO SurveyData (FullName, Email, DateOfBirth, ContactNumber, FavoriteFoods, Movies, Radio, Eat_out, TV) 
                                 VALUES (@FullName, @Email, @DateOfBirth, @ContactNumber, @FavoriteFoods, @movies, @radio, @eat_out, @tv)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    //Inserting survey data into the table
                    command.Parameters.AddWithValue("@FullName", formData.FullName);
                    command.Parameters.AddWithValue("@Email", formData.Email);
                    command.Parameters.AddWithValue("@DateOfBirth", formData.DateOfBirth);
                    command.Parameters.AddWithValue("@ContactNumber", formData.ContactNumber);
                    command.Parameters.AddWithValue("@FavoriteFoods", formData.FavoriteFoods);
                    command.Parameters.AddWithValue("@movies", formData.movies);
                    command.Parameters.AddWithValue("@radio", formData.radio);
                    command.Parameters.AddWithValue("@eat_out", formData.eat_out);
                    command.Parameters.AddWithValue("@tv", formData.tv);
                    command.ExecuteNonQuery();
                }
            }
        }

        //Get survey data
        private SurveyResultData GetSurveyResultData()
        {
            SurveyResultData resultData = new SurveyResultData();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Count total number of surveys
                string countQuery = "SELECT COUNT(*) FROM SurveyData";
                using (SqlCommand command = new SqlCommand(countQuery, connection))
                {
                    resultData.TotalSurveys = (int)command.ExecuteScalar();
                }

                // Calculate average age
                string avgAgeQuery = "SELECT AVG(DATEDIFF(year, DateOfBirth, GETDATE())) FROM SurveyData";
                using (SqlCommand command = new SqlCommand(avgAgeQuery, connection))
                {
                    resultData.AverageAge = (int)command.ExecuteScalar();
                }

                // Find oldest person
                string oldestQuery = "SELECT MAX(DATEDIFF(year, DateOfBirth, GETDATE())) FROM SurveyData";
                using (SqlCommand command = new SqlCommand(oldestQuery, connection))
                {
                    resultData.OldestAge = (int)command.ExecuteScalar();
                }

                // Find youngest person
                string youngestQuery = "SELECT MIN(DATEDIFF(year, DateOfBirth, GETDATE())) FROM SurveyData";
                using (SqlCommand command = new SqlCommand(youngestQuery, connection))
                {
                    resultData.YoungestAge = (int)command.ExecuteScalar();
                }

                // Calculate percentage of people who like Pizza
                string pizzaPercentageQuery = "SELECT ((COUNT(*) * 100.0) / (SELECT COUNT(*) FROM SurveyData)) FROM SurveyData WHERE FavoriteFoods = 'pizza'";
                using (SqlCommand command = new SqlCommand(pizzaPercentageQuery, connection))
                {
                    resultData.PizzaPercentage = Convert.ToDouble(command.ExecuteScalar());
                }

                // Calculate percentage of people who like Pasta
                string pastaPercentageQuery = "SELECT ((COUNT(*) * 100.0) / (SELECT COUNT(*) FROM SurveyData)) FROM SurveyData WHERE FavoriteFoods = 'pasta'";
                using (SqlCommand command = new SqlCommand(pastaPercentageQuery, connection))
                {
                    resultData.PastaPercentage = Convert.ToDouble(command.ExecuteScalar());
                }

                // Calculate percentage of people who like Pap and Wors
                string papAndWorsPercentageQuery = "SELECT ((COUNT(*) * 100.0) / (SELECT COUNT(*) FROM SurveyData)) FROM SurveyData WHERE FavoriteFoods = 'pap_and_wors'";
                using (SqlCommand command = new SqlCommand(papAndWorsPercentageQuery, connection))
                {
                    resultData.PapAndWorsPercentage = Convert.ToDouble(command.ExecuteScalar());
                }

                // Count people who like to watch movies
                string moviesCountQuery = "SELECT COUNT(*) FROM SurveyData WHERE Movies = 1";
                using (SqlCommand command = new SqlCommand(moviesCountQuery, connection))
                {
                    resultData.MoviesFans = (int)command.ExecuteScalar();
                }

                // Calculate average rating for movies
                string moviesAvgRatingQuery = "SELECT AVG(Movies) FROM SurveyData";
                using (SqlCommand command = new SqlCommand(moviesAvgRatingQuery, connection))
                {
                    resultData.MoviesAverageRating = Convert.ToDouble(command.ExecuteScalar());
                }

                // Count people who like to listen to radio
                string radioCountQuery = "SELECT COUNT(*) FROM SurveyData WHERE Radio = 1";
                using (SqlCommand command = new SqlCommand(radioCountQuery, connection))
                {
                    resultData.RadioFans = (int)command.ExecuteScalar();
                }

                // Calculate average rating for radio
                string radioAvgRatingQuery = "SELECT AVG(Radio) FROM SurveyData";
                using (SqlCommand command = new SqlCommand(radioAvgRatingQuery, connection))
                {
                    resultData.RadioAverageRating = Convert.ToDouble(command.ExecuteScalar());
                }

                // Count people who like to eat out
                string eatOutCountQuery = "SELECT COUNT(*) FROM SurveyData WHERE Eat_out = 1";
                using (SqlCommand command = new SqlCommand(eatOutCountQuery, connection))
                {
                    resultData.EatOutFans = (int)command.ExecuteScalar();
                }

                // Calculate average rating for eating out
                string eatOutAvgRatingQuery = "SELECT AVG(Eat_out) FROM SurveyData";
                using (SqlCommand command = new SqlCommand(eatOutAvgRatingQuery, connection))
                {
                    resultData.EatOutAverageRating = Convert.ToDouble(command.ExecuteScalar());
                }

                // Count people who like to watch TV
                string tvCountQuery = "SELECT COUNT(*) FROM SurveyData WHERE TV = 1";
                using (SqlCommand command = new SqlCommand(tvCountQuery, connection))
                {
                    resultData.TvFans = (int)command.ExecuteScalar();
                }

                // Calculate average rating for watching TV
                string tvAvgRatingQuery = "SELECT AVG(TV) FROM SurveyData";
                using (SqlCommand command = new SqlCommand(tvAvgRatingQuery, connection))
                {
                    resultData.TvAverageRating = Convert.ToDouble(command.ExecuteScalar());
                }
            }

            return resultData;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
