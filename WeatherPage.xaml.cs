using WeatherApp.Services;

namespace WeatherApp;

public partial class WeatherPage : ContentPage
{
    public List<Models.List> WeatherList;
    private double latitude;
    private double longitude;


	public WeatherPage()
	{
		InitializeComponent();
        WeatherList = new List<Models.List>();
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await GetLocation();
        var result = await ApiService.GetWeather(latitude, longitude);

        // loop for each item in resulting list
        foreach (var item in result.list)
        {
            // adding current item to weather list
            WeatherList.Add(item);
        }
        // setting the colelctionView item source to populate the weather list
        cvWeather.ItemsSource = WeatherList;

        // will display the city in accordance to location --> using x:name given in xaml file
        CityLbl.Text = result.city.name;

        // will display the weather dynamically in accoradance to given location
        WeatherDescriptionLbl.Text = result.list[0].weather[0].description;

        // display current temp in degree celcius -- currently temp is showing in Farenheit -- use &unit=metric in class
        TempLbl.Text = result.list[0].main.temperature + "°C";

        // will display the current humidity level
        HumidityLbl.Text = result.list[0].main.humidity + "%";

        // displays the given locations wind speed in kilometers
        WindLbl.Text = result.list[0].wind.speed + " km/h";

        // icon to be displayed as the main in accordance to cities current weather
        ImgWeatherIcon.Source = result.list[0].weather[0].customIcon;
    }



    public async Task GetLocation()
    {
        var location = await Geolocation.GetLocationAsync();
        latitude = location.Latitude;
        longitude = location.Longitude;
    }


}