﻿using KingLemurJulian.Core.Events;
using MediatR;
using System.Threading.Tasks;

namespace KingLemurJulian.Core
{
    public class BotRunner : IBotRunner
    {
        private readonly IChatClient chatClient;
        private readonly IChannelRepository channelRepository;
        private readonly IMediator mediator;

        public BotRunner(IChatClient chatClient, IChannelRepository channelRepository, IMediator mediator)
        {
            this.chatClient = chatClient;
            this.channelRepository = channelRepository;
            this.mediator = mediator;

            chatClient.OnCommandReceived += ChatClient_OnCommandReceived;
            chatClient.OnMessageReceived += ChatClient_OnMessageReceived;
        }

        private Task ChatClient_OnMessageReceived(ChatMessageEvent chatMessageEvent)
        {
            return Task.CompletedTask;
        }

        private async Task ChatClient_OnCommandReceived(CommandRequest commandRequest)
        {
            await mediator.Send(commandRequest).ConfigureAwait(false);
        }

        public async Task InitializeAsync()
        {
            chatClient.Initialize();
            await chatClient.ConnectAsync().ConfigureAwait(false);
        }

        public Task JoinChannels()
        {
            foreach (var channelName in channelRepository.GetChannelsToJoin())
                chatClient.JoinChannel(channelName);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            chatClient.Dispose();
        }
    }
}