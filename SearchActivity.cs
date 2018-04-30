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

namespace Recommendr.Activities
{
    [Activity(Label = "Search")]
    public class SearchActivity : Activity
    {
        //Step 2--Declare Class Variables
        ImageView IVlogo;
        Toolbar toolbarNav;
        ImageButton btnProfile;
        ImageButton btnAdd;
        ImageButton btnHome;
        ImageButton btnSearch;
        ImageButton btnLogout;
        EditText txtFindUsers;
        Button btnFind;
        ListView lvResults;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Step 1--Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Search);
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

            btnFind = FindViewById<Button>(Resource.Id.btnFind);
            txtFindUsers = FindViewById<EditText>(Resource.Id.txtFindUsers);
            lvResults = FindViewById<ListView>(Resource.Id.lvResults);

          //  txtFindUsers.Alpha = 0;
           // txtFindUsers.TextChanged += txtFindUsers_TextChanged;

            //Step 4--Event Handler(s)
            btnSearch.Click += BtnSearch_Click;
            btnAdd.Click += BtnAdd_Click;
            btnHome.Click += BtnHome_Click;
            btnLogout.Click += BtnLogout_Click;
            btnProfile.Click += BtnProfile_Click;
            btnFind.Click += BtnFind_Click;

            MyListViewAdapter adapter = new MyListViewAdapter(this, Resource.Layout.ListView_FindUsers, PopulateListView());
            lvResults.Adapter = adapter;
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

        private void BtnFind_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(AddRecommendationActivity));
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SearchActivity));
        }

        

        private List<User> PopulateListView()
        {
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3"); //Call Database  
            var db = new SQLiteConnection(dpPath);

            var userList = db.Table<LoginTable>();
            List<User> allUsers = new List<User>();

            foreach(var user in userList)
            {
                allUsers.Add(new User { Username = user.Username, Email = user.Email, NoOfFollowers = user.NoOfFollowers, RecommendationNum = user.RecommendationNum});
            }

            return allUsers;
        }
    }
}