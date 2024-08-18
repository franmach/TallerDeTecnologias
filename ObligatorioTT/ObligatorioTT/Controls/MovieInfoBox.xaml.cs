using ObligatorioTT.Models;
using System.Windows.Input;
namespace ObligatorioTT.Controls;

public partial class MovieInfoBox : ContentView
{
	public static readonly BindableProperty MediaProperty =
		BindableProperty.Create(nameof(Media), typeof(Media), typeof(MovieInfoBox), null);

	public event EventHandler Closed;
	public MovieInfoBox()
	{
		InitializeComponent();
		ClosedCommand = new Command(ExecuteCloseCommand);

    }
	public Media Media
	{
		get => (Media)GetValue(MovieInfoBox.MediaProperty);
		set => SetValue(MovieInfoBox.MediaProperty, value);
	}
    public ICommand ClosedCommand { get; private set; }
    private void ExecuteCloseCommand() => Closed?.Invoke(this,EventArgs.Empty);

	private void Button_Clicked(object sender, EventArgs e) =>
		Closed?.Invoke(this,EventArgs.Empty);
}