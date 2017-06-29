using Android.App;
using Android.Widget;
using Android.OS;

namespace PhoneApp
{
    [Activity(Label = "PhoneApp", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.Holo")]
    public class MainActivity : Activity
    {
        static readonly System.Collections.Generic.List<string> PhoneNumbers =
            new System.Collections.Generic.List<string>();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var PhoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
            var TranslateButton = FindViewById<Button>(Resource.Id.TranslateButton);
            var CallButton = FindViewById<Button>(Resource.Id.CallButton);
            var CallHistoryButton = FindViewById<Button>(Resource.Id.CallHistoryButton);
            var btnValidar = FindViewById<Button>(Resource.Id.btnValidar);

            CallButton.Enabled = false;

            var TranslatedNumber = string.Empty;

            TranslateButton.Click += (object sender, System.EventArgs e) =>
            {
                var Translator = new PhoneTranslator();
                TranslatedNumber = Translator.ToNumber(PhoneNumberText.Text);
                if (string.IsNullOrWhiteSpace(TranslatedNumber))
                {
                    //No hay número a llamar.
                    CallButton.Text = "Llamar";
                    CallButton.Enabled = false;
                }
                else
                {
                    //Hay un posible número telefónico a llamar.
                    CallButton.Text = $"Llamar al {TranslatedNumber}";
                    CallButton.Enabled = true;
                }
            };

            CallButton.Click += (object sender, System.EventArgs e) =>
            {
                //Intentar marcar el número telefónico.
                var CallDialog = new AlertDialog.Builder(this);
                CallDialog.SetMessage($"Llamar al {TranslatedNumber}");
                CallDialog.SetNeutralButton("Llamar", delegate
                {
                    //Agregar el número marcado a la lista de números marcados.
                    PhoneNumbers.Add(TranslatedNumber);
                    //Habilitar el botón CallHistoryButton.
                    CallHistoryButton.Enabled = true;

                    //Crear un intento para marcar el número telefónico.
                    var CallIntent =
                        new Android.Content.Intent(Android.Content.Intent.ActionCall);

                    CallIntent.SetData(
                        Android.Net.Uri.Parse($"tel:{TranslatedNumber}"));

                    StartActivity(CallIntent);
                });

                CallDialog.SetNegativeButton("Cancelar", delegate { });

                //Mostrar el cuadro de diálogo y esperar una respuesta.
                CallDialog.Show();
            };

            CallHistoryButton.Click += (sender, e) =>
            {
                var Intent = new Android.Content.Intent(this,
                    typeof(CallHistoryActivity));
                Intent.PutStringArrayListExtra("phone_numbers", PhoneNumbers);
                StartActivity(Intent);
            };

            btnValidar.Click += (sender, e) =>
            {
                var Intent = new Android.Content.Intent(this, typeof(ValidarActivity));
                StartActivity(Intent);
            };
        }
    }
}

