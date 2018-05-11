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
    [Activity(Label = "Recommendr", MainLauncher = true, Icon = "@mipmap/ic_action_r")]
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
            ActionBar.Hide();
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
            btnLogin.Click += BtnLogin_Click;
            CreateDB();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3"); //Call Database  
                var db = new SQLiteConnection(dpPath);
                var data = db.Table<LoginTable>(); //Call Table  
                var data1 = data.Where(x => x.username == txtUsername.Text && x.password == txtPassword.Text).FirstOrDefault(); //Linq Query  
                if (data1 != null)
                {
                    Toast.MakeText(this, "Login Success", ToastLength.Short).Show();

                    LoginTable currentUser = new LoginTable();

                    var userList = db.Table<LoginTable>();
                    foreach (var user in userList)
                    {
                        if(user.username != null)
                        {
                            if (user.username.Equals(txtUsername.Text))
                            {
                                currentUser = user;
                                break;
                            }
                        }
                        
                    }

                    Intent intent = new Intent(this, typeof(HomeActivity));
                    intent.PutExtra("CurrentUser", JsonConvert.SerializeObject(currentUser));
                    this.StartActivity(intent);

                }
                else
                {
                    Toast.MakeText(this, "Invalid Credentials", ToastLength.Short).Show();
                    txtUsername.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }

        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SignUpActivity));
        }

        public string CreateDB()
        {

            var output = "";
            output += "Creating Database if it does not exist";
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3"); //Create New Database  
            var db = new SQLiteConnection(dpPath);
            output += "\n Database Created....";

           // db.DeleteAll<LoginTable>();
            //db.DeleteAll<Friendship>();
           // db.DeleteAll<Recommendation>();
            return output;
        }
    }
}