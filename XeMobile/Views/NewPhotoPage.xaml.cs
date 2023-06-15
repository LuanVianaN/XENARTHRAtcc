using XeMobile.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Firebase.Auth;

namespace XeMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewPhotoPage : ContentPage
    {
        public static readonly BindableProperty PhotoProperty = BindableProperty.Create(
            "FotoPath",
            typeof(string),
            typeof(NewPhotoPage)
            );
        
        public string FotoPath 
        {
            get { return (string)GetValue(PhotoProperty); }
            set { SetValue(PhotoProperty,value); }
        }

        FileResult SelectedPhoto;

        public NewPhotoPage()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var result = await DisplayActionSheet("Nova foto", "Cancelar", null, new string[] { "Galeria", "Câmera" });

            if (result == "Galeria")
            {
                await PickPhotoAsync();              
            }
            else if (result == "Câmera")
            {
                await TakePhotoAsync();
            }
        }

        //Tirar foto
        async Task TakePhotoAsync()
        {
            try
            {
                var foto = await MediaPicker.CapturePhotoAsync();
                await LoadPhotoAsync(foto);
                Console.WriteLine($"CapturePhotoAsync COMPLETED: {FotoPath}");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Erro", "Feature is not supported on the device", "Ok");
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Erro","Permissions not granted","Ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }
        //Selecionar foto
        async Task PickPhotoAsync()
        {
            try
            {
                var foto = await MediaPicker.PickPhotoAsync();
                await LoadPhotoAsync(foto);
                Console.WriteLine($"CapturePhotoAsync COMPLETED: {FotoPath}");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Erro", "Feature is not supported on the device", "Ok");
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Erro", "Permissions not granted", "Ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }

        async Task LoadPhotoAsync(FileResult foto)
        {
            // cancelar
            if (foto == null)
            {
                FotoPath = null;
                return;
            }
            // salva arquivo local
            var novoarq = Path.Combine(FileSystem.CacheDirectory, foto.FileName);
            using (var stream = await foto.OpenReadAsync())
            using (var newStream = File.OpenWrite(novoarq))
                await stream.CopyToAsync(newStream);

            FotoPath = novoarq;
        }
        private async Task<bool> CheckAndRequestLocationPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

                if (status != PermissionStatus.Granted)
                {
                    // Permissão de localização não concedida, você pode exibir uma mensagem de erro ou tomar alguma ação apropriada.
                    await DisplayAlert("Permissão Negada", "Você precisa conceder permissão de localização para continuar.", "OK");
                    return false;
                }
            }

            return true;
        }

        private async void BtnSalvar_Clicked(object sender, EventArgs e)
        {
            // Verifica e solicita a permissão de localização
            bool hasLocationPermission = await CheckAndRequestLocationPermission();

            if (!hasLocationPermission)
                return;

            if (FotoPath == null)
            {
                await DisplayAlert("Erro", "Nenhuma foto selecionada", "Ok");
                return;
            }

            else if (ComentarioEnt.Text=="")
            {
                await DisplayAlert("Erro", "O comentário não pode estar vazio", "Ok");
                return;
            }
            var location = await Geolocation.GetLocationAsync();
            var foto = new Foto()
                {
                    Comentario = ComentarioEnt.Text,
                    Data = DateTime.Now,
                    FotoPath = FotoPath,
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };

                try
                {
                    await App.Database.SavePhotoAsync(foto);
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erro", ex.Message, "Ok");
                }

            await Navigation.PopAsync();
        }
    }
}