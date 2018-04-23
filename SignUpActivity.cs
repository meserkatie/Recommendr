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
    [Activity(Label = "SignUpActivity")]
    public class SignUpActivity : Activity
    {
        //Step 2--Declare Class Variables
        ImageView IVlogo;
        ImageView IVsignup;
        EditText txtUsername;
        EditText txtEmail;
        EditText txtPassword;
        EditText txtConfirmPassword;
        Button btnSignUp;
        Button btnLogin;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Step 1--Set our view from the "main" layout resource
            SetContentView(Resource.Layout.SignUp);

            //Step 3--Find Controls
            IVlogo = FindViewById<ImageView>(Resource.Id.IVlogo);
            IVlogo.SetImageResource(Resource.Drawable.logo);
            IVsignup = FindViewById<ImageView>(Resource.Id.IVSignUp);
            IVsignup.SetImageResource(Resource.Drawable.createaccount2);
            txtUsername = FindViewById<EditText>(Resource.Id.txtUsername);
            txtEmail = FindViewById<EditText>(Resource.Id.txtEmail);
            txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
            txtConfirmPassword = FindViewById<EditText>(Resource.Id.txtConfirmPassword);
            btnSignUp = FindViewById<Button>(Resource.Id.btnSignUp);
            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);

            //Step 4--Event Handler(s)
            btnLogin.Click += BtnLogin_Click;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(LoginActivity));
        }
    }
}