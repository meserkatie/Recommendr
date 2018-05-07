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
    [Activity(Label = "Search")]
    public class SearchActivity : Activity
    {
        //Step 2--Declare Class Variables
        ImageView IVlogo;
        ImageButton btnProfile;
        ImageButton btnAdd;
        ImageButton btnHome;
        ImageButton btnSearch;
        ImageButton btnLogout;
        EditText txtFindUsers;
        Button btnFind;
        ListView lvResults;
        LoginTable currentUser;
        List<LoginTable> searchedUsers = new List<LoginTable>();


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.Hide();
            currentUser = JsonConvert.DeserializeObject<LoginTable>(Intent.GetStringExtra("CurrentUser"));

            //Step 1--Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Search);

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

            btnFind = FindViewById<Button>(Resource.Id.btnFind);
            txtFindUsers = FindViewById<EditText>(Resource.Id.txtFindUsers);
            lvResults = FindViewById<ListView>(Resource.Id.lvResults);

            //Step 4--Event Handler(s)
            btnSearch.Click += BtnSearch_Click;
            btnAdd.Click += BtnAdd_Click;
            btnHome.Click += BtnHome_Click;
            btnLogout.Click += BtnLogout_Click;
            btnProfile.Click += BtnProfile_Click;
            btnFind.Click += BtnFind_Click;
            lvResults.ItemClick += LvResults_ItemClick;
        }

        private void LvResults_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //var obj = lvResults.Adapter.GetItem(e.Position);
            var selectedUser = searchedUsers[e.Position];
            Intent intent = new Intent(this, typeof(PreviewActivity));
            intent.PutExtra("SelectedUser", JsonConvert.SerializeObject(selectedUser));
            intent.PutExtra("CurrentUser", JsonConvert.SerializeObject(currentUser));
            this.StartActivity(intent);
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

        private void BtnFind_Click(object sender, EventArgs e)
        {
            searchedUsers = (from x in PopulateListView()
                                        where (x.username).ToLower().Contains(txtFindUsers.Text.ToLower())
                                        select x).ToList<LoginTable>();

            MyListViewAdapter adapter = new MyListViewAdapter(this, Resource.Layout.ListView_FindUsers, searchedUsers, currentUser);
            lvResults.Adapter = adapter;

            if(searchedUsers.Count == 0)
            {
                Toast.MakeText(this, "No Results", ToastLength.Short).Show();
            }
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



        private List<LoginTable> PopulateListView()
        {
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3"); //Call Database  
            var db = new SQLiteConnection(dpPath);

            var userList = db.Table<LoginTable>();
            var allUsers = new List<LoginTable>();

            foreach (var item in userList)
            {
                if(item.username != null)
                {
                    allUsers.Add(item);
                }
                
            }

            return allUsers;
        }
    }
}