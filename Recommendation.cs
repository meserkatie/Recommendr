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
using SQLite;

namespace Recommendr
{
    public class Recommendation
    {
        [PrimaryKey, AutoIncrement, Column("_Id")]
        public int id { get; set; }
        [MaxLength(25)]

        public string recommendationType { get; set; }

        public string title { get; set; }

        public string genre { get; set; }

        public string rating { get; set; }

        public string additionalComments { get; set; }

        public string author { get; set; }

        public string recAuthor { get; set; }

        public DateTime postDate { get; set; }

        public Boolean toDo { get; set; }

    }
}