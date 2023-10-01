using MultiSnake.Client.Game;
using MultiSnake.Client.Services;
using MultiSnake.Lib;

namespace MultiSnake.Client
{
	public partial class Form1 : Form
	{
		public Graphics _g;

		private readonly CommunicationService _comunication;

		private readonly List<Snake> _snakes;

		private readonly List<Food> _food;

		private Snake _localSnake => _snakes.First(x => x.Id == _guid);

		private Guid _guid;

		private System.Windows.Forms.Timer _foodTimer;

		public Form1()
		{
			InitializeComponent();
			DoubleBuffered = true;

			usernameBox.Text = "DefaultUser";

			messageBox.TabStop = false;
			usernameBox.TabStop = false;
			chatBox.TabStop = false;
			sendButton.TabStop = false;

			_foodTimer = new System.Windows.Forms.Timer();
			_foodTimer.Interval = 10000;
			_foodTimer.Tick += _foodTimer_Tick;
			_foodTimer.Start();

			_snakes = new();
			_guid = Guid.NewGuid();

			_comunication = new CommunicationService();
			_comunication.OnSync += _comunication_OnSync;

			_comunication.Connect();

			_snakes.Add(new Snake(_guid, _snakes));

			_localSnake.OnRenderRequest += _localSnake_OnRenderRequest;

			_localSnake.OnSnakeDeath += _localSnake_OnSnakeDeath;

			_comunication.OnMessageReceived += _comunication_OnMessageReceived;

			_comunication.OnFoodRecived += _comunication_OnFoodRecived;

			_comunication.OnEatRecived += _comunication_OnEatRecived;

			_food = new List<Food>();
		}

		private void _comunication_OnFoodRecived(Point location)
		{
			Food recivedFood = new Food(_localSnake);
			recivedFood.Location = location;
			_food.Add(recivedFood);
		}

		private void _foodTimer_Tick(object? sender, EventArgs e)
		{
			Food newFood = new Food(_localSnake);
			_food.Add(newFood);

			_comunication.SendFood(newFood.Location);
		}

		private void _comunication_OnMessageReceived(string user, string message, string time)
		{
			Invoke(new Action(() => { chatBox.Items.Add($"<{time}>{user}: {message}"); }));
		}

		private void _comunication_OnSync(RemoteSnake model)
		{
			if (model.Id == _guid)
			{
				return;
			}

			var snake = _snakes.FirstOrDefault(x => x.Id == model.Id);

			if (snake != null)
			{
				var index = _snakes.IndexOf(snake);

				_snakes[index] = new Snake(model);
			}
			else
			{
				_snakes.Add(new Snake(model));
			}
		}

		private void _comunication_OnEatRecived(Point location)
		{
			Food eatenFood = new Food(_localSnake);
			eatenFood.Location = location;
			eatenFood.FoodIsEaten(_g);
		}

		private void _localSnake_OnRenderRequest(Guid id)
		{
			Invalidate();

			var snake = _snakes.FirstOrDefault(x => x.Id == id);

			if (snake != null)
			{
				_comunication.SyncLocal(snake.GetRemoteSnake());
			}
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			var g = e.Graphics;
			_g = e.Graphics;

			foreach (var snake in _snakes)
			{
				snake.Render(g);
			}

			foreach(Food food in _food)
			{
				if (!food.IsEaten)
				{
					food.Render(g, _localSnake._head);
				}
				else
				{
					food.FoodIsEaten(g);
				}
			}

			scoreBoardLB.Text = "";

			for (int i = 0; i < _snakes.Count; i++)
			{
				scoreBoardLB.Text += $" Snake{i + 1} = {_snakes[i].GetParts() - 4} |";
			}
		}



		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			_localSnake.Control(e);
		}

		private void SendMsg()
		{
			_comunication.SendMessage(usernameBox.Text, messageBox.Text, DateTime.Now.ToString("HH:mm:ss"));
			messageBox.Clear();
		}

		private void sendButton_Click(object sender, EventArgs e)
		{
			SendMsg();
		}

		private void _localSnake_OnSnakeDeath(Guid Id)
		{
			if (Id == _guid)
			{
				if (InvokeRequired)
				{
					Invoke(new Action(() => Close()));
				}
				else
				{
					Close();
				}
			}
		}
	}
}