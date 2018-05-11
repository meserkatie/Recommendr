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
    class MyListViewAdapter : BaseAdapter<LoginTable>
    {
        private List<LoginTable> users;
        private Context myContext;
        private int rowLayout;
        private TextView txtRowUN;
        private string currentUsername;
        private string friendUsername;
        private LoginTable currentUser;
        private int curPos;
        private int recomNum = 0;

        public MyListViewAdapter(Context context, int mRowLayout, List<LoginTable> items, LoginTable currentUser)
        {
            users = items;
            myContext = context;
            rowLayout = mRowLayout;
            currentUsername = currentUser.username;
            this.currentUser = currentUser;
        }
        public override int Count
        {
            get { return users.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }


        public override LoginTable this[int position]
        {
            get { return users[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if(row == null)
            {
                row = LayoutInflater.From(myContext).Inflate(Resource.Layout.ListView_FindUsers, null, false);
            }
            curPos = position;

            txtRowUN = row.FindViewById<TextView>(Resource.Id.txtRowUN);
            txtRowUN.Text = users[position].username;
            friendUsername = txtRowUN.Text;

            TextView txtNoRec = row.FindViewById<TextView>(Resource.Id.txtRowNoRec);
            // txtNoRec.Text = users[position].getRecommendationNum().ToString();

            //----------------Calculate RecomNum--------------------
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3"); //Call Database  
            var db = new SQLiteConnection(dpPath);
            var data = db.Table<Recommendation>(); //Call Table  
            recomNum = 0;
            foreach (var item in data)
            {
                if (item.recAuthor.Equals(users[position].username))
                {
                    recomNum++;
                }
            }
            txtNoRec.Text = recomNum.ToString();

            TextView txtRowFoll = row.FindViewById<TextView>(Resource.Id.txtRowFoll);
            txtRowFoll.Text = users[position].noOfFollowing.ToString();

         //   Button btnFollow = row.FindViewById<Button>(Resource.Id.btnFollow);
//btnFollow.Click += BtnFollow_Click;

            

         //   Button btnViewProfile = row.FindViewById<Button>(Resource.Id.btnViewProfile);
           // btnViewProfile.Click += BtnViewProfile_Click;

            return row;
        }

        //private void BtnViewProfile_Click(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        private void BtnFollow_Click(object sender, EventArgs e)
        {

        //    string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");
        //    var db = new SQLiteConnection(dpPath);
        //    db.CreateTable<Friendship>();
            
        //    Friendship newFriendship = new Friendship();
        //    newFriendship.friendFrom = currentUsername;
        //    newFriendship.friendTo = users[curPos].username;
        //    newFriendship.friendSince = DateTime.Now;

        //    db.Insert(newFriendship);

        ////    LoginTable tbl = new LoginTable();
        //  //  tbl.Id = currentUser.Id;
        //   // tbl.noOfFollowers = currentUser.noOfFollowers + 1;
        //   // db.Update(tbl);

        //    Toast.MakeText(myContext, "Successfully Followed", ToastLength.Short).Show();
        }
    }
}