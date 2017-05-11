using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml.Media.Imaging;

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

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace busqueda13
{
    public sealed partial class MainPage : Page
    {
        private SpeechSynthesizer synthesizer;

        public MainPage()
        {
            this.InitializeComponent();
            InicializaHabla();
        }

        private async void BtnEmpieza_Click(object sender, RoutedEventArgs e)
        {
            await Dime("Hola, Cris; soy Alya. Me dijo Leidi Bag que estás buscando el código secreto que muestra el plano de Joc Moz. ¡Sé dónde lo guarda!: lo escondió encima del teclado de papá. Llévalo al ordenador de Joc Moz, y escanea el código para que te muestre el plano.");
        }

        private void InicializaHabla()
        {
            //lanza el habla
            synthesizer = new SpeechSynthesizer();

            VoiceInformation voiceInfo =
         (
           from voice in SpeechSynthesizer.AllVoices
           where voice.Gender == VoiceGender.Female
           select voice
         ).FirstOrDefault() ?? SpeechSynthesizer.DefaultVoice;

            synthesizer.Voice = voiceInfo;
        }

        private async Task Dime(String szTexto)
        {
            try
            {
                // crear el flujo desde el texto
                SpeechSynthesisStream synthesisStream = await synthesizer.SynthesizeTextToStreamAsync(szTexto);

                // ...y lo dice
                media.AutoPlay = true;
                media.SetSource(synthesisStream, synthesisStream.ContentType);
                media.Play();
            }
            catch (Exception e)
            {
                var msg = new Windows.UI.Popups.MessageDialog(e.Message, "Error hablando:");
                await msg.ShowAsync();
            }
        }

    }
}
