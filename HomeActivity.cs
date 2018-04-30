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

namespace Recommendr.Activities
{
    [Activity(Label = "HomeActivity")]
    public class HomeActivity : Activity
    {
        //Step 2--Declare Class Variables
        ImageView IVlogo;
        Toolbar toolbarNav;
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
        Button btnApply;
        ListView lvFeed;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            //Step 1--Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Home);
            ActionBar.Hide();

            //Step 3--Find Controls
            IVlogo = FindViewById<ImageView>(Resource.Id.IVlogo);
            IVlogo.SetImageResource(Resource.Drawable.logo);
            toolbarNav = FindViewById<Toolbar>(Resource.Id.toolbarNav);

            btnProfile = FindViewById<ImageButton>(Resource.Id.btnProfile);
            btnProfile.SetImageResource(Resource.Mipmap.ic_action_person);

            btnAdd = FindViewById<ImageButton>(Resource.Id.btnAdd);
            btnAdd.SetImageResource(Resource.Mipmap.ic_action_add_box);

            btnHome = FindViewById<ImageButton>(Resource.Id.btnHome);
            btnHome.SetImageResource(Resource.Mipmap.ic_action_home);

            btnSearch = FindViewById<ImageButton>(Resource.Id.btnSearch);
            btnSearch.SetImageResource(Resource.Mipmap.ic_action_search);

            btnLogout = FindViewById<ImageButton>(Resource.Id.btnLogout);
            btnLogout.SetImageResource(Resource.Mipmap.ic_action_exit_to_app);

            cbBooks = FindViewById<CheckBox>(Resource.Id.cbBooks);
            cbMovies = FindViewById<CheckBox>(Resource.Id.cbMovies);
            cbMusic = FindViewById<CheckBox>(Resource.Id.cbMusic);
            cbTV = FindViewById<CheckBox>(Resource.Id.cbTV);
            cbOwnOption = FindViewById<CheckBox>(Resource.Id.cbOwnOption);
            btnApply = FindViewById<Button>(Resource.Id.btnApply);
            lvFeed = FindViewById<ListView>(Resource.Id.lvFeed);

            //Step 4--Event Handler(s)
            btnSearch.Click += BtnSearch_Click;
            btnAdd.Click += BtnAdd_Click;
            btnHome.Click += BtnHome_Click;
            btnLogout.Click += BtnLogout_Click;
            btnProfile.Click += BtnProfile_Click;
            btnApply.Click += BtnApply_Click;
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnProfile_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(ProfileActivity));
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(LoginActivity));
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(HomeActivity));
        }


        private void BtnAdd_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(AddRecommendationActivity));
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SearchActivity));
        }
    }
}