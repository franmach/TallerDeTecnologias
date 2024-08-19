using Android.App;
using Android.Content.PM;
using Android.OS;
using Plugin.Fingerprint;

namespace ObligatorioTT
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Configurar el resolver para la actividad actual
            CrossFingerprint.SetCurrentActivityResolver(() => this);
        }
    }
}
