namespace ThAmCo.Web.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class Auth0ManagementApiClient
    {
        private readonly string _auth0Domain;
        private readonly string _clientCredentialsClientId;
        private readonly string _clientCredentialsClientSecret;

        public Auth0ManagementApiClient(string auth0Domain, string clientCredentialsClientId, string clientCredentialsClientSecret)
        {
            _auth0Domain = auth0Domain;
            _clientCredentialsClientId = clientCredentialsClientId;
            _clientCredentialsClientSecret = clientCredentialsClientSecret;
        }

        public async Task UpdateUserMetadata(string token, string userId, IDictionary<string, string> metadata)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"https://{_auth0Domain}");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var payload = new { user_metadata = metadata };
                var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

                var response = await httpClient.PatchAsync($"api/v2/users/{userId}", content);
                //response.EnsureSuccessStatusCode();
            }
        }

        public async Task<Dictionary<string, string>> GetUserMetadata(string token, string userId)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"https://{_auth0Domain}");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Use the Management API Access Token to authenticate the request
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Make a GET request to the Auth0 Management API to fetch user data by user ID
                var response = await httpClient.GetAsync($"api/v2/users/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var jsonDocument = JsonDocument.Parse(responseContent);
                    var rootElement = jsonDocument.RootElement;

                    // Retrieve the desired user metadata fields
                    var userMetadata = new Dictionary<string, string>();
                    var userMetadataElement = rootElement.GetProperty("user_metadata");

                    if (userMetadataElement.TryGetProperty("address", out var addressElement))
                    {
                        userMetadata["address"] = addressElement.GetString();
                    }

                    if (userMetadataElement.TryGetProperty("phone", out var phoneElement))
                    {
                        userMetadata["phone"] = phoneElement.GetString();
                    }

                    return userMetadata;
                }
                else
                {
                    // Handle the error case if the request is not successful
                    // For example, you can log the error or return an empty dictionary
                    return new Dictionary<string, string>();
                }
            }
        }

        public async Task<string> GetManagementApiAccessToken()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"https://{_auth0Domain}/oauth/token");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var requestBody = new Dictionary<string, string>
        {
            { "client_id", "UitV1jvJfh0mNZsXMZM1LW8zCmfaEltV" },
            { "client_secret", "-vUjvQ2eIZerBOPSa4LcUBC7Zl8KoODrnQQpxBaKstObpb-aBBYZ2a02Kp3c_AXT" },
            { "audience", $"https://{_auth0Domain}/api/v2/" },
            { "grant_type", "client_credentials" }
        };

                var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("/oauth/token", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonSerializer.Deserialize<Dictionary<string, object>>(responseContent);

                    // Convert the access token to string
                    var accessToken = tokenResponse["access_token"].ToString();
                    return accessToken;
                }
                else
                {
                    // Handle the error case here if needed
                    throw new Exception("Failed to obtain access token.");
                }
            }
        }
    }
}
