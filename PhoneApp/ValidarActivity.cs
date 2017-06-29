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

namespace PhoneApp
{
    [Activity(Label = "Validar Actividad", Icon = "@drawable/icon", Theme = "@android:style/Theme.Holo")]
    public class ValidarActivity : Activity
    {
        TextView tvValidacion;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Validar);

            var etEmail = FindViewById<EditText>(Resource.Id.etEmail);
            var etPassword = FindViewById<EditText>(Resource.Id.etPassword);
            var btnValidarActividad = FindViewById<Button>(Resource.Id.btnValidarActividad);
            tvValidacion = FindViewById<TextView>(Resource.Id.tvValidacion);

            btnValidarActividad.Click += (sender, e) =>
            {
                string email = etEmail.Text;
                string password = etPassword.Text;
                string AndroidId = Android.Provider.Settings.Secure.GetString(ContentResolver,
                    Android.Provider.Settings.Secure.AndroidId);

                Validate(email, password, AndroidId);
            };
        }

        public async void Validate(string Email, string Password, string Device)
        {
            string res;

            var ServiceClient = new SALLab06.ServiceClient();
            var SvcResult = await ServiceClient.ValidateAsync(Email, Password, Device);

            res = $"{SvcResult.Status}\n{SvcResult.Fullname}\n{SvcResult.Token}";

            tvValidacion.Text = res;
        }
    }
}