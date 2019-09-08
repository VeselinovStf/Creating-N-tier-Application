using Newtonsoft.Json;
using PluralSightBook.Core.DTOs;
using System.Collections.Generic;

namespace PluralSightBook.CLI.JsonModelBuilder
{
    public class JsonModelConvertor : IJsonModelConvertor
    {
        public IEnumerable<FriendsViewModelDTO> Convert(string input)
        {
            var jsonresult = JsonConvert.DeserializeObject<IEnumerable<FriendsViewModelDTO>>(input);

            return jsonresult;
        }
    }
}