using System;
using System.Collections.Generic;
using System.IO;
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

        public string username { get; set; }
        [MaxLength(15)]

        public string email { get; set; }
        [MaxLength(25)]

        public string password { get; set; }
        [MaxLength(30)]

        public DateTime membersince { get; set; }

        public int noOfFollowers { get; set; }

        public int noOfFollowing { get; set; }

        public List<Friendship> followers = new List<Friendship>();

        public List<Friendship> following = new List<Friendship>();

        public List<Recommendation> toDo = new List<Recommendation>();

        public List<Recommendation> myRecommendations = new List<Recommendation>();

        public void AddFollower(Friendship newFollower)
        {
            followers.Add(newFollower);
        }

        public List<Friendship> GetFollowers()
        {
            return followers;
        }

        public void AddFollowing(Friendship newFollowing)
        {
            following.Add(newFollowing);
        }

        public List<Friendship> GetFollowing()
        {
            return following;
        }

        public void AddToDo(Recommendation newRec)
        {
            toDo.Add(newRec);
        }

        public List<Recommendation> GetToDo()
        {
            return toDo;
        }

        public void AddMyRecommendations(Recommendation newRec)
        {
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");
            var db = new SQLiteConnection(dpPath);
            db.CreateTable<Recommendation>();

            db.Insert(newRec);
        }

        public int getRecommendationNum()
        {
            return myRecommendations.Count;
        }

        
    }
}