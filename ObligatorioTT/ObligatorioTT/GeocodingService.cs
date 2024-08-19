using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.Maui.Devices.Sensors;

public class GeocodingService
{
    private readonly string _apiKey;

    public GeocodingService(string apiKey)
    {
        _apiKey = apiKey;
    }

    public async Task<Location> GetCoordinatesFromAddressAsync(string address)
    {
        string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key={_apiKey}";

        using (HttpClient client = new HttpClient())
        {
            string json = await client.GetStringAsync(url);
            var jsonObject = JObject.Parse(json);

            if (jsonObject["status"].ToString() == "OK")
            {
                var location = jsonObject["results"][0]["geometry"]["location"];
                double lat = location["lat"].ToObject<double>();
                double lng = location["lng"].ToObject<double>();
                return new Location(lat, lng);
            }
            else
            {
                throw new Exception("No se pudo obtener la ubicación.");
            }
        }
    }
}
