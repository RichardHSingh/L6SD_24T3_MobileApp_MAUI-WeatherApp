
namespace WeatherApp;

public partial class WelcomePage : ContentPage
{
	public WelcomePage()
	{
		InitializeComponent();
	}

    private void forecastBtn_Clicked(object sender, EventArgs e)
    {
		Navigation.PushModalAsync( new WeatherPage());
    }
}