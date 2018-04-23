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
    [Activity(Label = "Login to Account", MainLauncher = true, Icon = "@mipmap/ic_action_r")]
    public class LoginActivity : Activity
    {
        //Step 2--Declare Class Variables
        ImageView IVlogo;
        ImageView IVlogin;
        EditText txtUsername;
        EditText txtPassword;
        Button btnLogin;
        Button btnSignUp;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //Step 1--Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Login);

            //Step 3--Find Controls
            IVlogo = FindViewById<ImageView>(Resource.Id.IVlogo);
            IVlogo.SetImageResource(Resource.Drawable.logo);
            IVlogin = FindViewById<ImageView>(Resource.Id.IVlogin);
            IVlogin.SetImageResource(Resource.Drawable.login5);
            txtUsername = FindViewById<EditText>(Resource.Id.txtUsername);
            txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            btnSignUp = FindViewById<Button>(Resource.Id.btnSignUp);

            //Step 4--Event Handler(s)
            btnSignUp.Click += BtnSignUp_Click;
        }

        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SignUpActivity));
        }
    }
}