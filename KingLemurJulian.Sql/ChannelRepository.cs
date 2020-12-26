using KingLemurJulian.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KingLemurJulian.Sql
{
    public class ChannelRepository : IChannelRepository
    {
        private readonly ISqlAccess sql;

        public ChannelRepository(string connectionString)
        {
            sql = SqlAccessBase.Create(connectionString);
        }

        public List<string> GetChannelsToJoin()
        {
            return new List<string>
            {
                "Tranquiliza"
            };
        }

        public Task DeleteChannel(string channelName)
        {
            return Task.CompletedTask;
        }

        public Task SaveChannel(string channelName)
        {
            return Task.CompletedTask;
        }
    }
}
