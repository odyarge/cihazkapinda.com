using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ODY.Cihazkapinda.MessageData
{
    public interface IMessagesAppService : ICrudAppService<MessagesDto, Guid, PagedAndSortedResultRequestDto, MessagesCreateUpdateDto, MessagesCreateUpdateDto>
    {
        Task<List<MessagesDto>> GetMessageInCustomers();
        Task<List<MessagesDto>> GetMessageByUser(string input);
        Task DeleteMessages(string input);
        Task GetLastMessageByUserRead(string input);
    }
}
