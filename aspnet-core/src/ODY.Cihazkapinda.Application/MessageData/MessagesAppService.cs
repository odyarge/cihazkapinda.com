using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ODY.Cihazkapinda.MessageData
{
    public class MessagesAppService : CrudAppService<Messages, MessagesDto, Guid, PagedAndSortedResultRequestDto, MessagesCreateUpdateDto, MessagesCreateUpdateDto>, IMessagesAppService
    {
        public MessagesAppService(IRepository<Messages, Guid> repository) : base(repository)
        {
        }

        public async Task GetLastMessageByUserRead(string input)
        {
            var list = await Repository.GetListAsync();
            var filter = list.FindAll(x => x.Receive == input || x.Sender == input);
            var message = filter.OrderByDescending(x => x.CreationTime).FirstOrDefault();
            message.Status = 1;
            var update = ObjectMapper.Map<Messages, MessagesCreateUpdateDto>(message);
            await UpdateAsync(message.Id, update);
        }
        public async Task<List<MessagesDto>> GetMessageByUser(string input)
        {
            var list = await Repository.GetListAsync();
            var filter = list.FindAll(x => x.Receive == input || x.Sender == input);

            return ObjectMapper.Map<List<Messages>, List<MessagesDto>>(filter.OrderBy(o => o.CreationTime).ToList());
        }

        public async Task<List<MessagesDto>> GetMessageInCustomers()
        {
            var list = await Repository.GetListAsync();
            var filter = list.GroupBy(x => new { x.Receive, x.Sender }).Select(y => y.Last()).ToList();
            return ObjectMapper.Map<List<Messages>, List<MessagesDto>>(filter.OrderByDescending(o => o.CreationTime).ToList());
        }
        public async Task DeleteMessages(string input)
        {
            var list = await Repository.GetListAsync();
            var filter = list.FindAll(x => x.Sender == input || x.Receive == input);
            foreach (var item in filter)
            {
                await Repository.DeleteAsync(item.Id);
            }
        }
    }
}
