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
        await GetWeatherDataByLocation(latitude, longitude);
    }



    public async Task GetLocation()
    {
        var location = await Geolocation.GetLocationAsync();
        latitude = location.Latitude;
        longitude = location.Longitude;
    }

    // event for changing back to ones location after location is set elsewhere
    private async void TapLocation_Tapped(object sender, TappedEventArgs e)
    {
        await GetLocation();
        await GetWeatherDataByLocation(latitude, longitude);
    }

    public async Task GetWeatherDataByLocation(double latitude, double longitude)
    {
        var result = await ApiService.GetWeather(latitude, longitude);       
        UpdateUI(result);
    }

    // search icon for entering specfic city
    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        var response = await DisplayPromptAsync(title: "", message: "Search weather by City", placeholder: "Enter city name", accept: "Search", cancel: "Cancel");
        
        if (response != null)
        {
            await GetWeatherDataByCity(response);        }

    }

    // code for getting weather by city name user enters
    public async Task GetWeatherDataByCity(string city)
    {
        var result = await ApiService.GetWeatherByCity(city);
        UpdateUI(result);
    }

    public void UpdateUI(dynamic result)
    {
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
}