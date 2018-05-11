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
    [Activity(Label = "PreviewActivity")]

    public class PreviewActivity : Activity
    {
        ImageView IVlogo;
        //ImageButton btnProfile;
        //ImageButton btnAdd;
        //ImageButton btnHome;
        //ImageButton btnSearch;
        //ImageButton btnLogout;
        TextView txtUsernameInput;
        TextView txtEmailInput;
        TextView txtDateInput;
        TextView txtRecNumInput;
        TextView txtFollowerNum;
        TextView txtFollowNum;
        Button btnFollow;
        Button btnBack;
        LoginTable currentUser;
        LoginTable selectedUser;
        int recomNum = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //Step 1--Set our view from the "main" layout resource
            ActionBar.Hide();
            SetContentView(Resource.Layout.Preview);
            currentUser = JsonConvert.DeserializeObject<LoginTable>(Intent.GetStringExtra("CurrentUser"));
            selectedUser = JsonConvert.DeserializeObject<LoginTable>(Intent.GetStringExtra("SelectedUser"));

            //Step 3--Find Controls
            IVlogo = FindViewById<ImageView>(Resource.Id.IVlogo);
            IVlogo.SetImageResource(Resource.Drawable.logo);

            //btnProfile = FindViewById<ImageButton>(Resource.Id.btnProfile);
            //btnProfile.SetImageResource(Resource.Drawable.ic_action_person);

            //btnAdd = FindViewById<ImageButton>(Resource.Id.btnAdd);
            //btnAdd.SetImageResource(Resource.Drawable.ic_action_add_box);

            //btnHome = FindViewById<ImageButton>(Resource.Id.btnHome);
            //btnHome.SetImageResource(Resource.Drawable.ic_action_home);

            //btnSearch = FindViewById<ImageButton>(Resource.Id.btnSearch);
            //btnSearch.SetImageResource(Resource.Drawable.ic_action_search);

            //btnLogout = FindViewById<ImageButton>(Resource.Id.btnLogout);
            //btnLogout.SetImageResource(Resource.Drawable.ic_action_exit_to_app);

            txtUsernameInput = FindViewById<TextView>(Resource.Id.txtUsernameInput);
            txtEmailInput = FindViewById<TextView>(Resource.Id.txtEmailInput);
            txtDateInput = FindViewById<TextView>(Resource.Id.txtDateInput);

            txtRecNumInput = FindViewById<TextView>(Resource.Id.txtRecNumInput);
            txtFollowNum = FindViewById<TextView>(Resource.Id.txtFollowNum);
            txtFollowerNum = FindViewById<TextView>(Resource.Id.txtFollowerNum);
            btnFollow = FindViewById<Button>(Resource.Id.btnFollow);
            btnBack = FindViewById<Button>(Resource.Id.btnBack);

            txtUsernameInput.Text = selectedUser.username;
            txtEmailInput.Text = selectedUser.email;
            txtDateInput.Text = selectedUser.membersince.ToString();
            //txtRecNumInput.Text = selectedUser.getRecommendationNum().ToString();
            txtFollowNum.Text += selectedUser.noOfFollowers.ToString();
            txtFollowerNum.Text += selectedUser.noOfFollowing.ToString();

            //----------------Calculate RecomNum--------------------
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3"); //Call Database  
            var db = new SQLiteConnection(dpPath);
            var data = db.Table<Recommendation>(); //Call Table  
            recomNum = 0;
            foreach (var item in data)
            {
                if (item.recAuthor.Equals(selectedUser.username))
                {
                    recomNum++;
                }
            }
            txtRecNumInput.Text = recomNum.ToString();

            btnFollow.Click += BtnFollow_Click;
            btnBack.Click += BtnBack_Click;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SearchActivity));
            intent.PutExtra("CurrentUser", JsonConvert.SerializeObject(currentUser));
            this.StartActivity(intent);
        }

        private void BtnFollow_Click(object sender, EventArgs e)
        {
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");
            var db = new SQLiteConnection(dpPath);
            db.CreateTable<Friendship>();

            Friendship newFriendship = new Friendship();
            newFriendship.friendFrom = currentUser.username;
            newFriendship.friendTo = selectedUser.username;
            newFriendship.friendSince = DateTime.Now;

            db.Insert(newFriendship);

             LoginTable tbl = currentUser;
            tbl.noOfFollowers++;
             db.Update(tbl);

            LoginTable sTbl = selectedUser;
            if(sTbl.noOfFollowing.Equals(null))
            {
                sTbl.noOfFollowing = 1;
            }
            else
            {
                sTbl.noOfFollowing++;
            }
            
            db.Update(sTbl);

            Toast.MakeText(this, "Successfully Followed", ToastLength.Short).Show();
        }
    }
}