namespace notlcd;

public class LcdWebClient : ILcdClient
{
    public LcdWebClient()
    {
        
    }

    private string _baseUrl = "http://192.168.1.46/";
    private string _setLineEndpoint = "set";
    private string _toggleBacklightEndpoint = "backlight";

    private string _firstLineQuery = "firstline";
    private string _secondLineQuery = "secondline";

    public async Task SetFirstLine(string text, CancellationToken cancellationToken = default)
    {
        await SetLine(text, _firstLineQuery, cancellationToken);
    }

    public async Task SetSecondLine(string text, CancellationToken cancellationToken = default)
    {
        await SetLine(text, _secondLineQuery, cancellationToken);
    }

    public async Task SetLines(string firstLineText, string secondLineText, CancellationToken cancellationToken = default)
    {
        var httpClient = new HttpClient();

        var uriBuilder = new UriBuilder(_baseUrl + _setLineEndpoint);
        var query = $"{_firstLineQuery}={firstLineText}&{_secondLineQuery}={secondLineText}";
        uriBuilder.Query = query;

        try
        {
            var response = await httpClient.GetAsync(uriBuilder.Uri, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response Content:");
                Console.WriteLine(content);
            }
            else
            {
                Console.WriteLine($"Request failed with status code {response.StatusCode}.");
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error details: {errorContent}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unexpected error: {e.Message}");
        }
    }

    private async Task SetLine(string text, string lineKey, CancellationToken cancellationToken = default)
    {
        var httpClient = new HttpClient();

        var uriBuilder = new UriBuilder(_baseUrl + _setLineEndpoint);
        var query = $"{lineKey}={text}";
        uriBuilder.Query = query;

        try
        {
            var response = await httpClient.GetAsync(uriBuilder.Uri, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response Content:");
                Console.WriteLine(content);
            }
            else
            {
                Console.WriteLine($"Request failed with status code {response.StatusCode}.");
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error details: {errorContent}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unexpected error: {e.Message}");
        }
    }

    public async Task ToggleBacklight(CancellationToken cancellationToken = default)
    {
        var httpClient = new HttpClient();

        var uriBuilder = new UriBuilder(_baseUrl + _toggleBacklightEndpoint);

        try
        {
            var response = await httpClient.GetAsync(uriBuilder.Uri, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response Content:");
                Console.WriteLine(content);
            }
            else
            {
                Console.WriteLine($"Request failed with status code {response.StatusCode}.");
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error details: {errorContent}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unexpected error: {e.Message}");
        }
    }
}