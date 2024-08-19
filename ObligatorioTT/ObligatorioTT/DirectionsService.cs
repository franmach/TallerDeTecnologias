using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Maps;
using Newtonsoft.Json.Linq;
using Microsoft.Maui.Graphics;

public class DirectionsService
{
    private readonly string _apiKey;

    public DirectionsService(string apiKey)
    {
        _apiKey = apiKey;
    }

    public async Task<Polyline> GetRouteAsync(Location origen, Location destino)
    {
        string url = $"https://maps.googleapis.com/maps/api/directions/json?origin={origen.Latitude},{origen.Longitude}&destination={destino.Latitude},{destino.Longitude}&key={_apiKey}";

        using (HttpClient client = new HttpClient())
        {
            string json = await client.GetStringAsync(url);
            var jsonObject = JObject.Parse(json);

            if (jsonObject["status"].ToString() == "OK")
            {
                var puntosCodificados = jsonObject["routes"][0]["overview_polyline"]["points"].ToString();
                var polyline = DecodePolyline(puntosCodificados);
                return polyline;
            }
            else
            {
                throw new Exception($"Error en la API de Directions: {jsonObject["status"]}");
            }
        }
    }

    private Polyline DecodePolyline(string encodedPoints)
    {
        if (string.IsNullOrEmpty(encodedPoints))
            return null;

        var polyline = new Polyline
        {
            StrokeColor = Colors.Blue,
            StrokeWidth = 5
        };

        var polylineChars = encodedPoints.ToCharArray();
        int index = 0;

        int currentLat = 0;
        int currentLng = 0;

        while (index < polylineChars.Length)
        {
            int sum = 0;
            int shifter = 0;
            int next5Bits;

            do
            {
                next5Bits = polylineChars[index++] - 63;
                sum |= (next5Bits & 31) << shifter;
                shifter += 5;
            } while (next5Bits >= 32 && index < polylineChars.Length);

            if (sum != 0)
            {
                currentLat += (sum & 1) != 0 ? ~(sum >> 1) : (sum >> 1);
            }

            sum = 0;
            shifter = 0;

            do
            {
                next5Bits = polylineChars[index++] - 63;
                sum |= (next5Bits & 31) << shifter;
                shifter += 5;
            } while (next5Bits >= 32 && index < polylineChars.Length);

            if (sum != 0)
            {
                currentLng += (sum & 1) != 0 ? ~(sum >> 1) : (sum >> 1);
            }

            var nextPosition = new Location(Convert.ToDouble(currentLat) / 1E5, Convert.ToDouble(currentLng) / 1E5);
            polyline.Geopath.Add(nextPosition);
        }

        return polyline;
    }
}
