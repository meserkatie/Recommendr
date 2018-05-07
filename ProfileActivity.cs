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
    [Activity(Label = "ProfileActivity")]
    public class ProfileActivity : Activity
    {
        //Step 2--Declare Class Variables
        ImageView IVlogo;
        ImageButton btnProfile;
        ImageButton btnAdd;
        ImageButton btnHome;
        ImageButton btnSearch;
        ImageButton btnLogout;
        TextView txtUsernameInput;
        TextView txtEmailInput;
        TextView txtDateInput;
        TextView txtRecNumInput;
        TextView txtEmpty;
        TextView txtFollowNum;
        Button btnFollowers;
        Button btnFollowing;
        Button btnChangePW;
        ListView lvToDo;
        LoginTable currentUser;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            //Step 1--Set our view from the "main" layout resource
            ActionBar.Hide();
            SetContentView(Resource.Layout.Profile);
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

            txtUsernameInput = FindViewById<TextView>(Resource.Id.txtUsernameInput);
            txtEmailInput = FindViewById<TextView>(Resource.Id.txtEmailInput);
            txtDateInput = FindViewById<TextView>(Resource.Id.txtDateInput);
            txtRecNumInput = FindViewById<TextView>(Resource.Id.txtRecNumInput);
            txtFollowNum = FindViewById<TextView>(Resource.Id.txtFollowNum);
            btnFollowers = FindViewById<Button>(Resource.Id.btnFollowers);
            btnFollowing = FindViewById<Button>(Resource.Id.btnFollowing);
            btnChangePW = FindViewById<Button>(Resource.Id.btnChangePW);
            txtEmpty = FindViewById<TextView>(Resource.Id.txtEmpty);
            lvToDo = FindViewById<ListView>(Resource.Id.lvToDo);

            txtUsernameInput.Text = currentUser.username;
            txtEmailInput.Text = currentUser.email;
            txtDateInput.Text = currentUser.membersince.ToShortDateString();
            txtRecNumInput.Text = currentUser.getRecommendationNum().ToString();

            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3"); //Call Database  
            var db = new SQLiteConnection(dpPath);

            currentUser.followers = (from x in db.Table<Friendship>()
                                     where (x.friendFrom).Contains(currentUser.username)
                                     select x).ToList<Friendship>();

            currentUser.noOfFollowers = currentUser.followers.Count;

            txtFollowNum.Text = currentUser.noOfFollowers.ToString();

            if (currentUser.GetToDo().Count == 0)
            {
                txtEmpty.Text = "No items in To Do List";
            }
            else
            {
                RecommendationAdapter adapter = new RecommendationAdapter(this, Resource.Layout.ListView_Recommendation, currentUser.GetToDo());
                lvToDo.Adapter = adapter;
            }
            
            //Step 4--Event Handler(s)
            btnSearch.Click += BtnSearch_Click;
            btnAdd.Click += BtnAdd_Click;
            btnHome.Click += BtnHome_Click;
            btnLogout.Click += BtnLogout_Click;
            btnProfile.Click += BtnProfile_Click;
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