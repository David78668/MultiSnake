using MultiSnake.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MultiSnake.Client.Game
{
    public class Snake
    {
        public Guid Id { get; set; }
		public bool IsAlive { get; private set; } = true;

		private readonly List<ISnakePart> _parts;
        private SnakeDirection _direction = SnakeDirection.Down;

        public delegate void RenderRequest(Guid id);
        public event RenderRequest OnRenderRequest;
		public event Action<Guid> OnSnakeDeath;

		public SnakeHead _head => (SnakeHead)_parts.First();

        private List<Snake> _snakes;

		public Snake(Guid id, List<Snake> snakes)
		{
			_parts = new List<ISnakePart>();
			Id = id;
			_snakes = snakes;

			CreateSnake();
		}


		private const int DEFAULT_SNAKE_START_SIZE = 3;
        private const int UPDATE_INTERVAL = 150;

        private void CreateSnake()
        {
            _parts.Add(new SnakeHead()
            {
                Point = new Point(10, 10)
            });

            for (int i = 0; i < DEFAULT_SNAKE_START_SIZE; i++)
            {
                _parts.Add(new SnakePart()
                {
                    Point = new Point(10, 10 +
                    (i * i == 0 ? SnakeHead.HEAD_SIZE : SnakePart.PART_SIZE)),
                });
            }

            SetupSnake();

            Init();
        }

        private void SetupSnake()
        {
            for (int i = 0; i < _parts.Count - 1; i++)
            {

                _parts[i].Next = _parts[i + 1];
            }
        }

        public void AddPart()
        {
            var part = new SnakePart()
            {
                Point = _parts.Last().Point,
            };

            _parts.Last().Next = part;

            _parts.Add(part);
        }

        public int GetParts()
        {
            return _parts.Count;
        }

        private System.Timers.Timer _timer;

        private void Init()
        {
            _timer = new System.Timers.Timer();
            _timer.Interval = UPDATE_INTERVAL;

            _timer.Elapsed += Elapsed;

            _timer.Start();

        }

        private const int DEFAULT_MOVE_SPEED = 24;

		private void Elapsed(object? sender, ElapsedEventArgs e)
		{
			Point newLocation =
				new Point(_head.Point.X, _head.Point.Y);

			switch (_direction)
			{
				case SnakeDirection.Up:
					newLocation = new Point(_head.Point.X, _head.Point.Y - DEFAULT_MOVE_SPEED);
					break;
				case SnakeDirection.Down:
					newLocation = new Point(_head.Point.X, _head.Point.Y + DEFAULT_MOVE_SPEED);
					break;
				case SnakeDirection.Left:
					newLocation = new Point(_head.Point.X - DEFAULT_MOVE_SPEED, _head.Point.Y);
					break;
				case SnakeDirection.Right:
					newLocation = new Point(_head.Point.X + DEFAULT_MOVE_SPEED, _head.Point.Y);
					break;
			}

			if (newLocation.X < 0)
			{
				newLocation.X = 700 - DEFAULT_MOVE_SPEED;
			}
			else if (newLocation.X >= 700)
			{
				newLocation.X = 0;
			}

			if (newLocation.Y < 0)
			{
				newLocation.Y = 525 - DEFAULT_MOVE_SPEED;
			}
			else if (newLocation.Y >= 525)
			{
				newLocation.Y = 0;
			}

			_head.Move(newLocation);

			OnRenderRequest.Invoke(Id);

			if (CheckCollision(_snakes))
			{
				IsAlive = false;
				_snakes.Remove(this);
				OnSnakeDeath?.Invoke(this.Id);
			}
		}


		public void Control(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D:
                    _direction = SnakeDirection.Right;
                    break;
                case Keys.A:
                    _direction = SnakeDirection.Left;
                    break;
                case Keys.W:
                    _direction = SnakeDirection.Up;
                    break;
                case Keys.S:
                    _direction = SnakeDirection.Down;
                    break;
            }
        }

        public Snake(RemoteSnake snake)
        {
            Id = snake.Id;
            _parts = new();

            RemoteToSnake(snake.Parts);
        }

        private void RemoteToSnake(Point[] points)
        {
            for(int i = 0; i < points.Length; i++)
            {
                if (i == 0)
                {
                    _parts.Add(new SnakeHead()
                    {
                        Point = points[i],
                    });
                }
                else
                {
                    _parts.Add(new SnakePart()
                    {
                        Point = points[i],
                    });
                }
            }

            SetupSnake();
        }


        public RemoteSnake GetRemoteSnake()
        {
            return new RemoteSnake()
            {
                Id = Id,
                Parts = _parts.Select(p => p.Point).ToArray(),
            };
        }


		public void Render(Graphics g)
		{
			if (!IsAlive)
				return;

			_head.Render(g);
		}

		public bool CheckCollision(List<Snake> snakes)
		{
            if (!IsAlive) return false;

            int headSize = SnakeHead.HEAD_SIZE;
            int bodySize = SnakePart.PART_SIZE;

            foreach (var snake in snakes)
            {
                if (snake.Id != Id && snake.IsAlive)
                {
                    foreach (var part in snake._parts)
                    {
                        Rectangle headCollisionBounds = new Rectangle(_head.Point.X, _head.Point.Y, headSize, headSize);
                        Rectangle bodyCollisionBounds = new Rectangle(part.Point.X, part.Point.Y, bodySize, bodySize);

                        if (headCollisionBounds.IntersectsWith(bodyCollisionBounds))
                        {
                            IsAlive = false;
                            return true;
                        }
                    }
                }
            }

            return false;
		}

	}
}
