using System.Collections.Generic;
using System.Threading.Tasks;

namespace KingLemurJulian.Core
{
    public interface IChannelRepository
    {
        List<string> GetChannelsToJoin();
        Task SaveChannel(string channelName);
        Task DeleteChannel(string channelName);
    }
}
