using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using MultiSnake.Lib;
using MultiSnake.Server.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace MultiSnake.Server.Hubs
{
	public class GameHub : Hub
	{
		private readonly SnakeDbContext DB;
		private Random r;

		public GameHub(SnakeDbContext database)
		{
			DB = database;
			r = new Random();
		}


		public async override Task OnConnectedAsync()
        {
            var chatMessages = await DB.ChatMessages.ToListAsync();

            foreach (var message in chatMessages)
            {
                await Clients.Caller.SendAsync("ReceiveMessage", message.User, message.Message, message.Time);
            }

            await base.OnConnectedAsync();
        }

        public async void Sync(RemoteSnake snake)
        {
            await Clients.Others.SendAsync("RemoteSync", snake);
        }

        public async Task SendMessage(string user, string message, string time)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message, time);

            ChatMessage newMessage = new ChatMessage() { Message = message, User = user, Time = DateTime.Now.ToString("HH:mm:ss") };
            DB.ChatMessages.Add(newMessage);
            await DB.SaveChangesAsync();
        }

		public async Task SendFood(Point location)
		{
			await Clients.Others.SendAsync("ReciveFood", location);
		}

		public async Task SendEat(Point location)
		{
			await Clients.Others.SendAsync("ReciveEat", location);
		}
	}
}