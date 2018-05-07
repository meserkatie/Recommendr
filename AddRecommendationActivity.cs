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
using Newtonsoft.Json;

namespace Recommendr.Activities
{
    [Activity(Label = "AddRecommendationActivity")]
    public class AddRecommendationActivity : Activity
    {
        ImageView IVlogo;
        ImageButton btnProfile;
        ImageButton btnAdd;
        ImageButton btnHome;
        ImageButton btnSearch;
        ImageButton btnLogout;
        Spinner spnrType;
        Spinner spnrRating;
        EditText txtTitleInput;
        EditText txtAuthorInput;
        EditText txtGenreInput;
        EditText txtComment;
        Button btnClear;
        Button btnSubmit;
        LoginTable currentUser;

        string typeSelected = string.Empty;
        string ratingSelected = string.Empty;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            //Step 1--Set our view from the "main" layout resource
            SetContentView(Resource.Layout.AddRecommendation);
            ActionBar.Hide();
            currentUser = JsonConvert.DeserializeObject<LoginTable>(Intent.GetStringExtra("CurrentUser"));

            // Step 3--Find Controls
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

            spnrType = FindViewById<Spinner>(Resource.Id.spnrType);
            ArrayAdapter adapter = ArrayAdapter.CreateFromResource(this,
                Resource.Array.dropdown_type, Resource.Layout.spinner_item);
            adapter.SetDropDownViewResource(Resource.Layout.spinner_dropdown_item);
            spnrType.Adapter = adapter;

            spnrType.ItemSelected += SpnrType_ItemSelected;
           

            spnrRating = FindViewById<Spinner>(Resource.Id.spnrRating);

            ArrayAdapter ratingAdpt = ArrayAdapter.CreateFromResource(this,
                Resource.Array.dropdown_rating, Resource.Layout.spinner_item);
            ratingAdpt.SetDropDownViewResource(Resource.Layout.spinner_dropdown_item);
            spnrRating.Adapter = ratingAdpt;

            spnrRating.ItemSelected += SpnrRating_ItemSelected;

            txtTitleInput = FindViewById<EditText>(Resource.Id.txtTitleInput);
            txtAuthorInput = FindViewById<EditText>(Resource.Id.txtAuthorInput);
            txtGenreInput = FindViewById<EditText>(Resource.Id.txtGenreInput);
            txtComment = FindViewById<EditText>(Resource.Id.txtComment);
            btnClear = FindViewById<Button>(Resource.Id.btnClear);
            btnSubmit = FindViewById<Button>(Resource.Id.btnSubmit);

            //Step 4--Event Handler(s)
            btnSearch.Click += BtnSearch_Click;
            btnAdd.Click += BtnAdd_Click;
            btnHome.Click += BtnHome_Click;
            btnLogout.Click += BtnLogout_Click;
            btnProfile.Click += BtnProfile_Click;
            btnSubmit.Click += BtnSubmit_Click;
            btnClear.Click += BtnClear_Click;
        }

        private void SpnrRating_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            ratingSelected = spinner.GetItemAtPosition(e.Position).ToString();
            //Toast.MakeText(this, typeSelected, ToastLength.Short).Show();
        }

        private void SpnrType_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            typeSelected = spinner.GetItemAtPosition(e.Position).ToString();
            //Toast.MakeText(this, typeSelected, ToastLength.Short).Show();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtTitleInput.Text = string.Empty;
            txtAuthorInput.Text = string.Empty;
            txtGenreInput.Text = string.Empty;
            txtComment.Text = string.Empty;
            spnrRating.SetSelection(0);
            spnrType.SetSelection(0);
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            //Check that all required fields have values
            if(txtTitleInput.Text == string.Empty || txtGenreInput.Text == string.Empty)
            {
                Toast.MakeText(this, "Missing Required Field", ToastLength.Short).Show();
                return;
            }

            if(spnrType.SelectedItemPosition == 0)
            {
                Toast.MakeText(this, "Please Select a Type", ToastLength.Short).Show();
                return;
            }

            if (spnrRating.SelectedItemPosition == 0)
            {
                Toast.MakeText(this, "Please Select a Rating", ToastLength.Short).Show();
                return;
            }

            if(spnrType.SelectedItemPosition == 1 || spnrType.SelectedItemPosition == 3)
            {
                if(txtAuthorInput.Text == string.Empty)
                {
                    Toast.MakeText(this, "Please Enter an Author/Artist", ToastLength.Short).Show();
                    return;
                }
            }

            Recommendation newRec = new Recommendation();

            newRec.title = txtTitleInput.Text;
            newRec.author = txtAuthorInput.Text;
            newRec.recommendationType = typeSelected;
            newRec.genre = txtGenreInput.Text;
            newRec.rating = ratingSelected;
            newRec.additionalComments = txtComment.Text;
            newRec.recAuthor = currentUser.username;
            newRec.toDo = false;
            newRec.postDate = DateTime.Now;

            String test = string.Empty;
            currentUser.AddMyRecommendations(newRec);
            Toast.MakeText(this, "Recommendation Successfully Added", ToastLength.Short).Show();

            Intent intent = new Intent(this, typeof(HomeActivity));
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