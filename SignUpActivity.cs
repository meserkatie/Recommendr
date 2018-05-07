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
            ActionBar.Hide();

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
            btnSignUp.Click += BtnSignUp_Click;

            CreateDB();
        }

        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            //Check to make sure that all fields have values
            if (txtEmail.Text == String.Empty || txtConfirmPassword.Text == String.Empty || txtPassword.Text == String.Empty || txtUsername.Text == String.Empty)
            {
                Toast.MakeText(this, "Please enter values into all required fields.", ToastLength.Short).Show();
                return;
            }

            //Check to make sure that all fields have at least 5 characters
            //email
            if (CheckLength(txtEmail.Text.Length) == false)
            {
                Toast.MakeText(this, "Invalid Length. Please enter an email address with at least 5 characters.", ToastLength.Short).Show();
                return;
            }

            //username
            if (CheckLength(txtUsername.Text.Length) == false)
            {
                Toast.MakeText(this, "Invalid Length. Please enter a username with at least 5 characters.", ToastLength.Short).Show();
                return;
            }

            //password
            if (CheckLength(txtPassword.Text.Length) == false)
            {
                Toast.MakeText(this, "Invalid Length. Please enter a password with at least 5 characters.", ToastLength.Short).Show();
                return;
            }

            //confirm password
            if (CheckLength(txtConfirmPassword.Text.Length) == false)
            {
                Toast.MakeText(this, "Invalid Length. Please enter a password with at least 5 characters.", ToastLength.Short).Show();
                return;
            }

            //Check that email contains an "@" and a "."
            if (txtEmail.Text.Contains("@") == false || txtEmail.Text.Contains(".") == false)
            {
                Toast.MakeText(this, "Please enter a valid email address", ToastLength.Short).Show();
                return;
            }

            //Check that password and confirm password match
            if (txtConfirmPassword.Text.Equals(txtPassword.Text) == false)
            {
                Toast.MakeText(this, "Password and Confirm Password do not match, please try again.", ToastLength.Short).Show();
                txtConfirmPassword.Text = string.Empty;
                txtPassword.Text = string.Empty;
                return;
            }


            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");
                var db = new SQLiteConnection(dpPath);
                db.CreateTable<LoginTable>();
                LoginTable tbl = new LoginTable();

                db.CreateTable<Recommendation>();

                //Check to see if username is already in database
                var data = db.Table<LoginTable>(); //Call Table  
                var data1 = data.Where(x => x.username == txtUsername.Text).FirstOrDefault(); //Linq Query
                if(data1 != null)
                {
                    Toast.MakeText(this, "Username taken. Please choose another.", ToastLength.Short).Show();
                    return;
                }

                //Check to see if email is already in database
                data1 = data.Where(x => x.email == txtEmail.Text).FirstOrDefault(); //Linq Query
                if (data1 != null)
                {
                    Toast.MakeText(this, "Email already associated with an account.", ToastLength.Short).Show();
                    return;
                }

                //Add User to Database
                tbl.username = txtUsername.Text;
                tbl.password = txtPassword.Text;
                tbl.email = txtEmail.Text;
                tbl.noOfFollowers = 0;
                tbl.noOfFollowing = 0;
                tbl.membersince = DateTime.Now;
                //tbl.myRecommendations = new List<Recommendation>();
                //tbl.toDo = new List<Recommendation>();
                //tbl.followers = new List<LoginTable>();
                //tbl.following = new List<LoginTable>();
                

                db.Insert(tbl);

                Toast.MakeText(this, "User Added Successfully...,", ToastLength.Short).Show();
                StartActivity(typeof(LoginActivity));
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
            
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(LoginActivity));
        }

        private Boolean CheckLength(int length)
        {
            if (length < 5)
                return false;
            else
                return true;
        }

        public string CreateDB()
        {
            var output = "";
            output += "Creating Database if it does not exist";
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3"); //Create New Database  
            var db = new SQLiteConnection(dpPath);
            output += "\n Database Created....";
            return output;
        }
    }
}