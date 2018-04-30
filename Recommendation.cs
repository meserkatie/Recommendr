using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Recommendr
{
    class Recommendation
    {
        public int id { get; set; }

        public string recommendationType { get; set; }

        public string title { get; set; }

        public List<Genre> genre = new List<Genre>();

        public String rating { get; set; }

        public String additionalComments { get; set; }

        public void AddGenre(Genre newGenre)
        {
            genre.Add(newGenre);
        }

        public void RemoveGenre(Genre remGenre)
        {
            genre.Remove(remGenre);
        }

        public List<Genre> GetGenres()
        {
            return genre;
        }

    }
}