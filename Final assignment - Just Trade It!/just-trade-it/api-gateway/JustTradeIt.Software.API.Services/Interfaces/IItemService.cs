using JustTradeIt.Software.API.Models;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Models.Dtos;

namespace JustTradeIt.Software.API.Services.Interfaces
{
    public interface IItemService
    {
        Envelope<ItemDto> GetItems(int pageSize, int pageNumber, bool ascendingSortOrder);
        ItemDetailsDto GetItemByIdentifier(string identifier);
        string AddNewItem(string email, ItemInputModel item);
        void RemoveItem(string email, string itemIdentifier);
    }
}