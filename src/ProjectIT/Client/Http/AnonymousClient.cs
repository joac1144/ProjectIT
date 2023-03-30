namespace ProjectIT.Client.Http;

public class AnonymousClient
{
    public HttpClient Client;

    public AnonymousClient(HttpClient httpClient)
    {
        Client = httpClient;
    }
}