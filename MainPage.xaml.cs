using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Popups;
using Newtonsoft.Json;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace TDBNP_FINAL
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<string> lstStr = new List<string>();
        public MainPage()
        {
            this.InitializeComponent();
            lstStr.Add("Yonaguni.mp4");
            lstStr.Add("Dakiti.mp4");
            lstStr.Add("Bts.mp4");
            lstStr.Add("Historia.mp4");
            lstStr.Add("Limon.mp4");
            lstStr.Add("Vuelta.mp4");
            lstStr.Add("Save.mp4");
            lstStr.Add("Musica.mp4");
            lstStr.Add("Eres.mp4");
            lstStr.Add("Lento.mp4");

            lstMedia.Items.Add("Yonaguni - Video");
            lstMedia.Items.Add("Dakiti - Video");
            lstMedia.Items.Add("Bts - Video");
            lstMedia.Items.Add("Historia de un amor - Video");
            lstMedia.Items.Add("Limon y sal - Video");
            lstMedia.Items.Add("La media Vuelta - Video");
            lstMedia.Items.Add("Save your tears - Video ");
            lstMedia.Items.Add("Musica ligera - Video");
            lstMedia.Items.Add("Eres - Video");
            lstMedia.Items.Add("Lento - Video");
        }

        private void btn_play_Tapped(object sender, TappedRoutedEventArgs e)
        {
            mdPlayer.Play();
            stb_play.Begin();
            stb_cd.Begin();
            stb_play.AutoReverse = true;
            stb_cd.RepeatBehavior = RepeatBehavior.Forever;
            stb_ondas.Begin();
        }

        private void btn_pause_Tapped(object sender, TappedRoutedEventArgs e)
        {
            mdPlayer.Pause();
            stb_pausa.Begin();
            stb_pausa.AutoReverse = true;
            stb_cd.Stop();
            stb_ondas.Stop();
        }

        private void btn_stop_Tapped(object sender, TappedRoutedEventArgs e)
        {
            mdPlayer.Stop();
            stb_detener.Begin();
            stb_detener.AutoReverse = true;
            stb_ondas.Stop();
            stb_cd.Stop();
        }

        async private void CargarMediaLocal(string archivo)
        {
            try
            {
                Uri ruta = new Uri("ms-appx:///Assets/Media/" + archivo);
                mdPlayer.Source = ruta;
                var msg = new MessageDialog("Cambio de archivo " + archivo);
                await msg.ShowAsync();
                mdPlayer.Play();
            }
            catch (Exception ex)
            {
                var msg = new MessageDialog("Hubo un error: " + ex.Message);
                await msg.ShowAsync();
            }
        }

        private void lstMedia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.stb_ondas.Stop();
            this.stb_cd.Stop();
            int index = this.lstMedia.SelectedIndex;
            CargarMediaLocal(lstStr[index]);
        }

        async private void btnTop_Click(object sender, RoutedEventArgs e)
        {
            Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();
            var headers = httpClient.DefaultRequestHeaders;
            string header = "ie";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Valor inválido: " + header);
            }
            Uri questUri = new Uri("http://ws.audioscrobbler.com/2.0/?method=chart.gettoptracks&api_key=2bd34b4d9e6922c9c6569d6477f9add2&format=json");
            Windows.Web.Http.HttpResponseMessage httpResponse = new Windows.Web.Http.HttpResponseMessage();
            string httpResponseBody = "";
            try
            {
                httpResponse = await httpClient.GetAsync(questUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }

            track music = new track();
            music = JsonConvert.DeserializeObject<track>(httpResponseBody);
            List<TracksData> list = music.Tracks.tracksList;

            foreach (TracksData x in list)
            {
                lst_top.Items.Add(x.name);
            }
        }
    }
}
