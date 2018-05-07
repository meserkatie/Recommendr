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
    class RecommendationAdapter : BaseAdapter<Recommendation>
    {
        private List<Recommendation> recommendations;
        private Context myContext;
        private int rowLayout;

        public RecommendationAdapter(Context context, int mRowLayout, List<Recommendation> items)
        {
            recommendations = items;
            myContext = context;
            rowLayout = mRowLayout;
        }
        public override int Count
        {
            get { return recommendations.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }


        public override Recommendation this[int position]
        {
            get { return recommendations[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(myContext).Inflate(Resource.Layout.ListView_Recommendation, null, false);
            }

            TextView txtRowUsername = row.FindViewById<TextView>(Resource.Id.txtRowUsername);
            txtRowUsername.Text = recommendations[position].recAuthor;

            TextView txtRowTitleInput = row.FindViewById<TextView>(Resource.Id.txtRowTitleInput);
            txtRowTitleInput.Text = recommendations[position].title;

            TextView txtRowAuthorInput = row.FindViewById<TextView>(Resource.Id.txtRowAuthorInput);
            txtRowAuthorInput.Text = recommendations[position].author;

            TextView txtRowGenreInput = row.FindViewById<TextView>(Resource.Id.txtRowGenreInput);
            txtRowGenreInput.Text = recommendations[position].genre;

            TextView txtRowComments = row.FindViewById<TextView>(Resource.Id.txtRowComments);
            txtRowComments.Text = recommendations[position].additionalComments;

            TextView txtRowRating = row.FindViewById<TextView>(Resource.Id.txtRowRating);
            if (recommendations[position].rating.Equals("Highly Recommend"))
            {
                txtRowRating.Text = "Highly Recommend";
                txtRowRating.SetTextColor(Android.Graphics.Color.Blue);

            }
            else if (recommendations[position].rating.Equals("Recommend"))
            {
                txtRowRating.Text = "Recommend";
                txtRowRating.SetTextColor(Android.Graphics.Color.Green);
            }
            else if (recommendations[position].rating.Equals("Meh"))
            {
                txtRowRating.Text = "Meh";
                txtRowRating.SetTextColor(Android.Graphics.Color.Orange);
            }
            else
            {
                txtRowRating.Text = "Do Not Recommend";
                txtRowRating.SetTextColor(Android.Graphics.Color.Red);
            }

            ImageView ivIcon = row.FindViewById<ImageView>(Resource.Id.ivIcon);

            if (recommendations[position].recommendationType.Equals("Book"))
            {
                ivIcon.SetImageResource(Resource.Drawable.book);
            }
                
            else if (recommendations[position].recommendationType.Equals("Movie"))
            {
                ivIcon.SetImageResource(Resource.Drawable.movie);
            }

            else if (recommendations[position].recommendationType.Equals("Music"))
            {
                ivIcon.SetImageResource(Resource.Drawable.music);
            }
            else
            {
                ivIcon.SetImageResource(Resource.Drawable.tv);
            }

            Button btnToDo = row.FindViewById<Button>(Resource.Id.btnToDo);

            btnToDo.Click += BtnToDo_Click;


            return row;
        }

        private void BtnToDo_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}