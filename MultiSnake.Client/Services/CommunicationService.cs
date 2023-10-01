using Microsoft.AspNetCore.SignalR.Client;
using MultiSnake.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiSnake.Client.Services
{
	public class CommunicationService
	{
		private readonly HubConnection _hub;

		public delegate void Sync(RemoteSnake model);
		public event Sync OnSync;

		public delegate void MessageReceived(string user, string message, string time);
		public event MessageReceived OnMessageReceived;

		public delegate void FoodRecived(Point location);
		public event FoodRecived OnFoodRecived;

		public delegate void EatRecived(Point location);
		public event EatRecived OnEatRecived;

		public CommunicationService()
		{
			var builder
				= new HubConnectionBuilder()
				.WithUrl("https://localhost:7144/gamehub");
			_hub = builder.Build();
		}

		public async void Connect()
		{
			_hub.On<RemoteSnake>("RemoteSync", RemoteSync);
			_hub.On<string, string, string>("ReceiveMessage", ReceiveMessage);
			_hub.On<Point>("ReciveFood", ReciveFood);
			_hub.On<Point>("ReciveEat", ReciveEat);

			await _hub.StartAsync();
		}

		public async void SyncLocal(RemoteSnake local)
		{
			await _hub.SendAsync("Sync", local);
		}

		private void RemoteSync(RemoteSnake remoteSnake)
		{
			OnSync?.Invoke(remoteSnake);
		}

		public async void SendMessage(string user, string message, string time)
		{
			await _hub.SendAsync("SendMessage", user, message, time);
		}

		private void ReceiveMessage(string user, string message, string time)
		{
			OnMessageReceived?.Invoke(user, message, time);
		}

		public async void SendFood(Point location)
		{
			await _hub.SendAsync("SendFood", location);
		}

		private void ReciveFood(Point location)
		{
			OnFoodRecived?.Invoke(location);
		}

		public async void SendEat(Point location)
		{
			await _hub.SendAsync("SendEat", location);
		}

		private void ReciveEat(Point location)
		{
			OnEatRecived?.Invoke(location);
		}
	}
}
