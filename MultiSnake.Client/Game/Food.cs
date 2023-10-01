using MultiSnake.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiSnake.Client.Game
{
	public class Food
	{
		public int Id { get; set; }
		public Point Location { get; set; }
		public bool IsEaten { get; set; } = false;
		public Snake LocalSnake { get; set; }

		private readonly CommunicationService _comunication;

		Random r = new Random();

		public Food(Snake localSnake)
		{
			Location = new Point(r.Next(0, 100), r.Next(0, 100));
			LocalSnake = localSnake;

			_comunication = new CommunicationService();

			_comunication.Connect();
		}

		public void Render(Graphics g, SnakeHead head)
		{
			Rectangle headCollisionBounds = new Rectangle(head.Point.X, head.Point.Y, 24, 24);

			Rectangle foodCollisionBounds = new Rectangle(Location.X, Location.Y, 24, 24);

			if (headCollisionBounds.IntersectsWith(foodCollisionBounds))
			{
				if(IsEaten == false)
				{
					LocalSnake.AddPart();
					_comunication.SendEat(Location);
				}

				FoodIsEaten(g);
			}
			else
			{
				g.FillRectangle(Brushes.Blue, new Rectangle() { Height = 24, Width = 24, Location = Location });
			}
		}

		public void FoodIsEaten(Graphics g)
		{
			Color newColor = Color.FromArgb(0, Color.White);

			IsEaten = true;

			try
			{
				g.FillRectangle(new SolidBrush(newColor), new Rectangle() { Height = 24, Width = 24, Location = new Point(100, 10) });
			}
			catch (Exception)
			{

			}
		}
	}
}
