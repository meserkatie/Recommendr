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
    public class LoginTable
    {
        [PrimaryKey, AutoIncrement, Column("_Id")]
        public int Id { get; set; }
        [MaxLength(25)]

        public string Username { get; set; }
        [MaxLength(15)]

        public string Email { get; set; }
        [MaxLength(25)]

        public string Password { get; set; }
        [MaxLength(30)]

        public DateTime Membersince { get; set; }

        public int RecommendationNum { get; set; }

        public int NoOfFollowers { get; set; }

    }
}