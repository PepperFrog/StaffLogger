using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StaffLogger
{
    public class EventsHandler
    {
        private static readonly HttpClient HttpClient = new HttpClient();
        private bool IsLeaving = false;
        // This method is triggered when a player is verified (connected and authenticated)
        public async void OnVerified(VerifiedEventArgs ev)
        {
            if (ev.Player.RemoteAdminAccess) // Check if player has RemoteAdmin access
            {
                string userId = ev.Player.UserId;
                string nickname = ev.Player.Nickname;

                // Send data to the server asynchronously
                await ConnectionToServer(userId, nickname);
            }
        }

        public async void PlayerLeft(LeftEventArgs ev)
        {
            if (ev.Player.RemoteAdminAccess)
            {
                IsLeaving = true;
                await ConnectionToServer(ev.Player.UserId, ev.Player.Nickname);
            }
        }

        // Sends a POST request to the specified URL with player data
        private async Task ConnectionToServer(string userId, string nickname)
        {
            // Define the URL for the POST request
            string url = StaffLogger.Instance.Config.Url;
            string action = "sessions";
            if (IsLeaving)
            {
                action = "updatesession";
            }

            // Create the POST data payload
            var postData = new Dictionary<string, string>
            {
                { "steamid", userId },
                { "username", nickname },
                { "action", action }
            };

            // Encode the data as form-urlencoded content
            var content = new FormUrlEncodedContent(postData);

            try
            {
                // Send POST request
                HttpResponseMessage response = await HttpClient.PostAsync(url, content);

                // Read the response content as a string
                string responseContent = await response.Content.ReadAsStringAsync();

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Optionally log success
                    Log.Info($"Data sent successfully to the server: {responseContent}.");
                    IsLeaving = false;
                }
                else
                {
                    // Optionally log failure
                    Log.Warn($"Failed to send data: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Log any exceptions
                Log.Error($"Exception occurred: {ex.Message}");
            }
        }
    }
}
