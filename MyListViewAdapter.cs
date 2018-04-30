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
    class MyListViewAdapter : BaseAdapter<User>
    {
        private List<User> users;
        private Context myContext;
        private int rowLayout;

        public MyListViewAdapter(Context context, int mRowLayout, List<User> items)
        {
            users = items;
            myContext = context;
            rowLayout = mRowLayout;
        }
        public override int Count
        {
            get { return users.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }


        public override User this[int position]
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

            TextView txtRowUN = row.FindViewById<TextView>(Resource.Id.txtRowUN);
            txtRowUN.Text = users[position].Username;

            TextView txtNoRec = row.FindViewById<TextView>(Resource.Id.txtRowNoRec);
            txtNoRec.Text = users[position].RecommendationNum.ToString();

            TextView txtRowFoll = row.FindViewById<TextView>(Resource.Id.txtRowFoll);
            txtRowFoll.Text = users[position].NoOfFollowers.ToString();


            return row;
        }
    }
}