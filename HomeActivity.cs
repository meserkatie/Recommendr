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
using Newtonsoft.Json;
using SQLite;

namespace Recommendr.Activities
{
    [Activity(Label = "HomeActivity")]
    public class HomeActivity : Activity
    {
        //Step 2--Declare Class Variables
        ImageView IVlogo;
        ImageButton btnProfile;
        ImageButton btnAdd;
        ImageButton btnHome;
        ImageButton btnSearch;
        ImageButton btnLogout;
        EditText txtFilter;
        CheckBox cbBooks;
        CheckBox cbMovies;
        CheckBox cbMusic;
        CheckBox cbTV;
        CheckBox cbOwnOption;
        CheckBox cbOnlyOption;
        Button btnApply;
        ListView lvFeed;
        LoginTable currentUser = new LoginTable();
        bool books = true;
        bool movies = true;
        bool shows = true;
        bool music = true;
        List<Recommendation> filteredRecom = new List<Recommendation>();

        //public void ckb_OnClick(View v)
        //{
        //    switch (v.Id)
        //    {
        //        case Resource.Id.cbBooks:
        //            if (books == true)
        //                books = false;
        //            else
        //                books = true;
        //            break;
        //        case Resource.Id.cbMovies:
        //            if (movies == true)
        //                movies = false;
        //            else
        //                movies = true;
        //            break;
        //        case Resource.Id.cbMusic:
        //            if (music == true)
        //                music = false;
        //            else
        //                music = true;
        //            break;
        //        case Resource.Id.cbTV:
        //            if (shows == true)
        //                shows = false;
        //            else
        //                shows = true;
        //            break;
        //    }
        //}

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            //Step 1--Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Home);
            ActionBar.Hide();
            currentUser = JsonConvert.DeserializeObject<LoginTable>(Intent.GetStringExtra("CurrentUser"));

            //Step 3--Find Controls
            IVlogo = FindViewById<ImageView>(Resource.Id.IVlogo);
            IVlogo.SetImageResource(Resource.Drawable.logo);

            btnProfile = FindViewById<ImageButton>(Resource.Id.btnProfile);
            btnProfile.SetImageResource(Resource.Drawable.ic_action_person);

            btnAdd = FindViewById<ImageButton>(Resource.Id.btnAdd);
            btnAdd.SetImageResource(Resource.Drawable.ic_action_add_box);

            btnHome = FindViewById<ImageButton>(Resource.Id.btnHome);
            btnHome.SetImageResource(Resource.Drawable.ic_action_home);

            btnSearch = FindViewById<ImageButton>(Resource.Id.btnSearch);
            btnSearch.SetImageResource(Resource.Drawable.ic_action_search);

            btnLogout = FindViewById<ImageButton>(Resource.Id.btnLogout);
            btnLogout.SetImageResource(Resource.Drawable.ic_action_exit_to_app);

            cbBooks = FindViewById<CheckBox>(Resource.Id.cbBooks);
            cbMovies = FindViewById<CheckBox>(Resource.Id.cbMovies);
            cbMusic = FindViewById<CheckBox>(Resource.Id.cbMusic);
            cbTV = FindViewById<CheckBox>(Resource.Id.cbTV);
            cbOwnOption = FindViewById<CheckBox>(Resource.Id.cbOwnOption);
            cbOnlyOption = FindViewById<CheckBox>(Resource.Id.cbOnlyOption);
            btnApply = FindViewById<Button>(Resource.Id.btnApply);
            lvFeed = FindViewById<ListView>(Resource.Id.lvFeed);

            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3"); //Call Database  
            var db = new SQLiteConnection(dpPath);
            var data = db.Table<Recommendation>(); //Call Table  
                                                   //  var data1 = data.Where(x => x.recAuthor == currentUser.username).FirstOrDefault(); //Linq Query

            currentUser.myRecommendations = new List<Recommendation>();
            currentUser.followers =  (from x in db.Table<Friendship>()
                                              where (x.friendFrom).Contains(currentUser.username)
                                              select x).ToList<Friendship>();

            currentUser.noOfFollowers = currentUser.followers.Count;

            if (data != null)
            {
                foreach (var item in data)
                {
                    currentUser.myRecommendations.Add(item);
                }
            }
            

            RecommendationAdapter adapter = new RecommendationAdapter(this, Resource.Layout.ListView_Recommendation, currentUser.myRecommendations);
            lvFeed.Adapter = adapter;

            if (currentUser.myRecommendations.Count == 0)
            {
                Toast.MakeText(this, "No Results", ToastLength.Short).Show();
            }

            //Step 4--Event Handler(s)
            btnSearch.Click += BtnSearch_Click;
            btnAdd.Click += BtnAdd_Click;
            btnHome.Click += BtnHome_Click;
            btnLogout.Click += BtnLogout_Click;
            btnProfile.Click += BtnProfile_Click;
           // btnApply.Click += BtnApply_Click;
        }


        private void BtnApply_Click(object sender, EventArgs e)
        {
            //filteredRecom.Clear();
            //filteredRecom = currentUser.myRecommendations;

            //Recommendation temp = new Recommendation();

            //if(books && music && movies && shows)
            //{
            //    RecommendationAdapter adapter = new RecommendationAdapter(this, Resource.Layout.ListView_Recommendation, currentUser.myRecommendations);
            //    lvFeed.Adapter = adapter;
            //    return;
            //}
            
            //if(books == false && filteredRecom.Count != 0)
            //{
            //    for(int i = filteredRecom.Count - 1; i >= 0; i--)
            //    {
            //        temp = filteredRecom.ElementAt(i);
            //        if (temp.recommendationType == "Book")
            //            filteredRecom.Remove(temp);
            //    }
            //}

            //if (music == false && filteredRecom.Count != 0)
            //{
            //    for (int i = filteredRecom.Count - 1; i >= 0; i--)
            //    {
            //        temp = filteredRecom.ElementAt(i);
            //        if (temp.recommendationType == "Music")
            //            filteredRecom.Remove(temp);
            //    }
            //}

            //if (shows == false && filteredRecom.Count != 0)
            //{
            //    for (int i = filteredRecom.Count - 1; i >= 0; i--)
            //    {
            //        temp = filteredRecom.ElementAt(i);
            //        if (temp.recommendationType == "TV Show")
            //            filteredRecom.Remove(temp);
            //    }
            //}

            //if (movies == false && filteredRecom.Count != 0)
            //{
            //    for (int i = filteredRecom.Count - 1; i >= 0; i--)
            //    {
            //        temp = filteredRecom.ElementAt(i);
            //        if (temp.recommendationType == "Movie")
            //            filteredRecom.Remove(temp);
            //    }
            //}

            //RecommendationAdapter adapter2 = new RecommendationAdapter(this, Resource.Layout.ListView_Recommendation, filteredRecom);
            //lvFeed.Adapter = adapter2;

            //if (filteredRecom.Count == 0)
            //{
            //    Toast.MakeText(this, "No Results", ToastLength.Short).Show();
            //}
            //return;

        }

        private void BtnProfile_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ProfileActivity));
            intent.PutExtra("CurrentUser", JsonConvert.SerializeObject(currentUser));
            this.StartActivity(intent);
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(LoginActivity));
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(HomeActivity));
            intent.PutExtra("CurrentUser", JsonConvert.SerializeObject(currentUser));
            this.StartActivity(intent);
        }


        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(AddRecommendationActivity));
            intent.PutExtra("CurrentUser", JsonConvert.SerializeObject(currentUser));
            this.StartActivity(intent);
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SearchActivity));
            intent.PutExtra("CurrentUser", JsonConvert.SerializeObject(currentUser));
            this.StartActivity(intent);
        }
    }
}