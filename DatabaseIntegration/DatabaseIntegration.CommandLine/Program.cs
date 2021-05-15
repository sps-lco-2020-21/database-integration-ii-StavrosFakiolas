using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using DatabaseIntegration.Lib;
using System.Data;

namespace DatabaseIntegration.CommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionStringSettings cxnstring = ConfigurationManager.ConnectionStrings["sl"];

            MovieDatabase md = new MovieDatabase(cxnstring.ConnectionString);

            string movieName = Console.ReadLine();
            DataSet movieMatches = md.GetMeSomeData($"select id from movies where title = '{movieName}';");
            if(movieMatches.Tables[0].Rows.Count > 0)
            {
                long reviewerID;
                string reviewerName = Console.ReadLine();
                DataSet reviewerMatches = md.GetMeSomeData($"select id from Reviewers where name = '{reviewerName}';");
                if(reviewerMatches.Tables[0].Rows.Count > 0)
                {
                    var data = reviewerMatches.Tables[0].Rows[0]["id"];
                    reviewerID = (long)data;
                    Console.WriteLine(reviewerID);
                }
            }
            else
                Console.WriteLine("movie doesnt exist");
            Console.ReadKey();
            
            
            
            
            
            
            
            
            
            foreach (string s in md.MoviesByRating(5))
            {
                Console.WriteLine(s);
            }

            // the same results, but returned in a different way 
            string qry = string.Format(MovieDatabase.N_STAR_MOVIES, 5);
            DataSet ds = md.GetMeSomeData(qry);
            foreach(DataRow row in ds.Tables[0].Rows)
            {
                Console.WriteLine($"{row["id"]}: {row["title"]}");
            }


            Console.ReadKey();
        }
    }
}
