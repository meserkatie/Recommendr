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
    [Activity(Label = "AddRecommendationActivity")]
    public class AddRecommendationActivity : Activity
    {
        ImageView IVlogo;
        Toolbar toolbarNav;
        ImageButton btnProfile;
        ImageButton btnAdd;
        ImageButton btnHome;
        ImageButton btnSearch;
        ImageButton btnLogout;
        Spinner spnrType;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            //Step 1--Set our view from the "main" layout resource
            SetContentView(Resource.Layout.AddRecommendation);
            ActionBar.Hide();

            // Step 3--Find Controls
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

            Spinner spnrType = FindViewById<Spinner>(Resource.Id.spnrType);

            ArrayAdapter adapter = ArrayAdapter.CreateFromResource(this,
                Resource.Array.dropdown_type, Resource.Layout.spinner_item);
            adapter.SetDropDownViewResource(Resource.Layout.spinner_dropdown_item);
            spnrType.Adapter = adapter;

            //Work on This Later
          //  spnrType.ItemSelected += delegate
          //  {
            //    string selected = spnrType.SelectedItem.ToString();
            //    if(selected.Equals)
         //   }
        }
    }
}