//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;

//namespace Recommendr
//{
//    public class User
//    { 
//        public string username { get; set; }

//        public string email { get; set; }

//        public string password { get; set; }

//        public DateTime membersince { get; set; }

//        public int recommendationNum { get; set; }

//        public int noOfFollowers { get; set; }

//        public List<User> followers = new List<User>();

//        public List<User> following = new List<User>();

//        public List<Recommendation> toDo = new List<Recommendation>();

//        public List<Recommendation> myRecommendations = new List<Recommendation>();

//        public void AddFollower(User newFollower)
//        {
//            followers.Add(newFollower);
//        }

//        public List<User> GetFollowers()
//        {
//            return followers;
//        }

//        public void AddFollowing(User newFollowing)
//        {
//            following.Add(newFollowing);
//        }

//        public List<User> GetFollowing()
//        {
//            return following;
//        }

//        public void AddToDo(Recommendation newRec)
//        {
//            toDo.Add(newRec);
//        }

//        public void AddMyRecommendations(Recommendation newRec)
//        {
//            myRecommendations.Add(newRec);
//            recommendationNum++;
//        }
//    }
//}