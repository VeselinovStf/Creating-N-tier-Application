using PluralSightBook.Core.DTOs;
using PluralSightBook.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PluralSightBook.CLI.WebAPIService
{
    public class WebApiFriendsService : IFriendService
    {
        private readonly HttpClient _client;

        public WebApiFriendsService(HttpClient client)
        {
            this._client = client;
        }

        public Task AddFriend(Guid currentUserId, string currentUserEmail, string friendEmail)
        {
            throw new NotImplementedException();
        }

        public void DeleteFriend(int friendId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FriendsViewModelDTO>> ListFriendsOf(Guid userId)
        {
            string request = String.Format("api/Friend/{0}", userId.ToString());

            using (_client)
            {
                HttpResponseMessage response = await _client.GetAsync(request); // blocking call

                if (response.IsSuccessStatusCode)
                {
                    var friends = response.Content.ReadAsAsync<IEnumerable<FriendsViewModelDTO>>().Result;

                    return friends;
                }
            }

            return null;
        }
    }
}