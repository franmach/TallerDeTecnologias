using Microsoft.Maui.Controls;
using SQLitePCL;

namespace ObligatorioTT.Pages
{
    public partial class Video : ContentPage
    {
        public Video()
        {

            InitializeComponent();
            Routing.RegisterRoute(nameof(Login), typeof(Login));
            // Ruta relativa al archivo de video
            string videoPath = "file:///android_asset/presentacion.mp4";

            var htmlSource = new HtmlWebViewSource
            {
                Html = $@"
                    <html>
                        <body style='margin:0;padding:0;'>
                            <video width='100%' height='100%' controls autoplay>
                                <source src='{videoPath}' type='video/mp4'>
                                Your browser does not support the video tag.
                            </video>
                        </body>
                    </html>"
            };

            videoWebView.Source = htmlSource;

            Task.Delay(3000).ContinueWith(t =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Shell.Current.GoToAsync($"//{nameof(Login)}");
                });
            });


        }
    }
}
