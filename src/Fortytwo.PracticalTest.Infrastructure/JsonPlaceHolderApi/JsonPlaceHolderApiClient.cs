using System.Net.Http.Json;

namespace Fortytwo.PracticalTest.Infrastructure.JsonPlaceHolderApi;

/// <summary>
/// Typed HTTP client for calling https://jsonplaceholder.typicode.com
/// </summary>
public class JsonPlaceHolderApiClient
{
    private readonly HttpClient _httpClient;

    public JsonPlaceHolderApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    /// <summary>
    /// Calls GET /posts/{id} and returns the deserialized post DTO, or null if not found.
    /// </summary>
    public async Task<JsonPlaceHolderPostDto?> GetPostById(int postId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"posts/{postId}", cancellationToken).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
            // return null for 404, throw for other errors
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();
        }

        var post = await response.Content.ReadFromJsonAsync<JsonPlaceHolderPostDto>(cancellationToken: cancellationToken).ConfigureAwait(false);
        return post;
    }
}
