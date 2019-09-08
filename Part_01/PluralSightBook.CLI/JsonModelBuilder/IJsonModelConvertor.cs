using PluralSightBook.Core.DTOs;
using System.Collections.Generic;

namespace PluralSightBook.CLI.JsonModelBuilder
{
    public interface IJsonModelConvertor
    {
        IEnumerable<FriendsViewModelDTO> Convert(string input);
    }
}