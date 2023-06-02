using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XeMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cadastro : ContentPage
    {
        public string WebApiKey = "AIzaSyCJtd6ZalAdrELxM8jsbaTZqtB64zZ3SgI";
        public Cadastro()
        {
            InitializeComponent();
        }
        async void cadastrobtn_Clicked(System.Object sender, System.EventArgs e)
        {

            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebApiKey));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(novoemailentry.Text, novasenhaentry.Text);
                string gettoken = auth.FirebaseToken;
                await App.Current.MainPage.DisplayAlert("Aviso", "Cadastro efetuado!", "OK");
                await Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alerta", $"Erro de cadastro, ensira os dados corretos! \n\nDETALHES:\n\n{ex.Message}", "OK");
            }
        }
    }
}