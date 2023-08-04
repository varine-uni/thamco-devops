namespace ThAmCo.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using ThAmCo.Web.Models;
    using ThAmCo.Web.Services;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text.Json;
    using System;

    [ApiController]
    [Route("api/[controller]")]
    public class UserProfileController : Controller
    {
        private readonly Auth0ManagementApiClient _auth0ManagementApiClient;

        public UserProfileController(Auth0ManagementApiClient auth0ManagementApiClient)
        {
            _auth0ManagementApiClient = auth0ManagementApiClient;
        }

        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            // Use the logged-in user's ID (subject claim) to fetch the user's data from Auth0
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userProfileData = await GetUserProfileDataFromAuth0(userId);

            var model = new UpdateProfileModel
            {
                Address = userProfileData.TryGetValue("address", out var address) ? address : "Default Address",
                PhoneNumber = userProfileData.TryGetValue("phone", out var phone) ? phone : "Default Phone Number",
                // Add other profile fields as needed
            };

            return View(model);
        }

        [HttpPatch("update-profile")]
        public async Task<IActionResult> UpdateUserProfileAsync([FromBody] UpdateProfileModel profileModel)
        {
            // Get the user's ID (subject claim) from the authenticated user
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                // Update the user metadata using the Auth0 Management API client
                var accessToken = await _auth0ManagementApiClient.GetManagementApiAccessToken();
                await _auth0ManagementApiClient.UpdateUserMetadata(accessToken, userId, new Dictionary<string, string>
        {
            { "address", profileModel.Address },
            { "phone", profileModel.PhoneNumber }
            // Add other profile fields as needed
        });

                // Profile update successful
                return RedirectToAction("EditProfile", "UserProfile");
            }
            catch (Exception ex)
            {
                // Log the exception or show an error message to the user.
                // Redirect to an error page or handle the error as needed.
                return View("Error");
            }
        }

        private async Task<Dictionary<string, string>> GetUserProfileDataFromAuth0(string userId)
        {
            var accessToken = await _auth0ManagementApiClient.GetManagementApiAccessToken();
            var metadata = await _auth0ManagementApiClient.GetUserMetadata(accessToken, userId);

            return metadata;
        }
    }
}
