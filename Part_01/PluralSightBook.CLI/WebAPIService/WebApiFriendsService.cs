using PluralSightBook.CLI.JsonModelBuilder;
using PluralSightBook.Core.DTOs;
using PluralSightBook.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace PluralSightBook.CLI.WebAPIService
{
    public class WebApiFriendsService : IFriendService
    {
        private readonly WebClient _client;
        private readonly IJsonModelConvertor jsonConvertor;

        public WebApiFriendsService(WebClient client, IJsonModelConvertor jsonConvertor)
        {
            this._client = client;
            this.jsonConvertor = jsonConvertor;
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

            using (_client) //WebClient
            {
                var result = await _client.DownloadStringTaskAsync(request); //URI

                var jsonresult = this.jsonConvertor.Convert(result);

                return jsonresult;
            }
        }
    }
}