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
    class User
    { 
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime Membersince { get; set; }

        public int RecommendationNum { get; set; }

        public int NoOfFollowers { get; set; }

        public List<User> Followers = new List<User>();

        public List<User> Following = new List<User>();

        public List<Recommendation> ToDo = new List<Recommendation>();

        public List<Recommendation> MyRecommendations = new List<Recommendation>();

        public void AddFollower(User newFollower)
        {
            Followers.Add(newFollower);
        }

        public List<User> GetFollowers()
        {
            return Followers;
        }

        public void AddFollowing(User newFollowing)
        {
            Following.Add(newFollowing);
        }

        public List<User> GetFollowing()
        {
            return Following;
        }

        public void AddToDo(Recommendation newRec)
        {
            ToDo.Add(newRec);
        }

        public void AddMyRecommendations(Recommendation newRec)
        {
            MyRecommendations.Add(newRec);
            RecommendationNum++;
        }
    }
}