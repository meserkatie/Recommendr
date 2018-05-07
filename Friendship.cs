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
    public class Friendship
    {
        [PrimaryKey, AutoIncrement, Column("_Id")]
        public int friendshipId { get; set; }

        public string friendTo { get; set; }

        public string friendFrom { get; set; }

        public DateTime friendSince { get; set; }
    }
}