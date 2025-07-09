using System;
using System.Drawing.Printing;

namespace WinForms01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitGamePanel();
            Game.InitBase();
            InitTimer();
        }

        private void InitGamePanel()
        {
            Game.width = 120; Game.height = 60;
            gamepanel.Location = new(Game.panelMargin.X, Game.panelMargin.Y);
            GamePanelSize();
            GameSize();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            GamePanelSize();
            GameSize();
        }

        private void GamePanelSize() => gamepanel.Size = new Size(ClientSize.Width - Game.panelMargin.X * 2, ClientSize.Height - Game.panelMargin.Y * 2);

        private void GameSize()
        {
            Game.sizeX = gamepanel.Width / Game.width;
            Game.sizeY = gamepanel.Height / Game.height;
        }

        private static System.Windows.Forms.Timer? timer;
        public static int timerInterval = 60;

        public void InitTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = timerInterval;
            timer.Tick += new EventHandler(timer_tick);
        }

        private void timer_tick(object s, EventArgs e)
        {
            Snake.playerSnake.direction = Snake.playerSnake.directionKeyDown;
            foreach (Snake snake in Snake.snakeList)
            {
                if (snake.direction != Snake.Direction.none)
                {
                    switch (snake.direction)
                    {
                        case Snake.Direction.left:
                            {
                                if (snake.x != 0) { snake.x--; } else { snake.x = Game.width - 1; }
                                break;
                            }
                        case Snake.Direction.right:
                            {
                                if (snake.x != Game.width - 1) { snake.x++; } else { snake.x = 0; } //border-across
                                break;
                            }
                        case Snake.Direction.up:
                            {
                                if (snake.y != 0) { snake.y--; } else { snake.y = Game.height - 1; }
                                break;
                            }
                        case Snake.Direction.down:
                            {
                                if (snake.y != Game.height - 1) { snake.y++; } else { snake.y = 0; }
                                break;
                            }
                        default: break;
                    }
                    if (Game.snakeArr[snake.x, snake.y] == 0) //snake movement
                    {
                        Game.snakeArr[snake.x, snake.y] = 1;
                        snake.snakePointQueue.Enqueue(new Point(snake.x, snake.y)); //queue for snake tail
                    }
                    else  //snake collision
                    {
                        GameOver();
                    } 
                    if (Game.foodArr[snake.x, snake.y] != 1 && snake.snakeLength >= snake.startSnakeLength) //food not eaten
                    {
                        Point deq = snake.snakePointQueue.Dequeue();
                        Game.snakeArr[deq.X, deq.Y] = 0;
                    }
                    else if (Game.foodArr[snake.x, snake.y] != 1 && snake.snakeLength < snake.startSnakeLength) //snake growth
                    { 
                        snake.snakeLength++;
                    }
                    else //food eaten
                    {
                        snake.snakeLength++;
                        Game.foodArr[snake.x, snake.y] = 0;
                        //new-food:
                        int i = Game.foodPointList.IndexOf(new Point(snake.x, snake.y));
                    newFoodPoint:
                        Point fPoint = new Point(Game.random.Next(Game.width - 1), Game.random.Next(Game.height - 1));
                        if (Game.snakeArr[fPoint.X, fPoint.Y] > 0 || Game.foodArr[fPoint.X, fPoint.Y] != 0) //food in snake || food in block/food
                        { goto newFoodPoint; }
                        Game.foodPointList[i] = fPoint;
                        Game.foodArr[fPoint.X, fPoint.Y] = 1;
                    }
                }
            }
            Refresh();
        }

        public static void StartTimer() => timer.Enabled = true;

        private void GameOver()
        {
            timer.Enabled = false;
            Snake.playerSnake.failPos = new Point(Snake.playerSnake.x, Snake.playerSnake.y);
            Game.gameover = true;
            Refresh();
        }

        private void gamepanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics gfx = e.Graphics;
            gfx.DrawRectangle(Pens.Black, 0, 0, gamepanel.Width - 1, gamepanel.Height - 1); //border
            foreach (Snake snake in Snake.snakeList)
            {
                foreach (Point p in snake.snakePointQueue.ToList()) //snakes
                {
                    gfx.FillRectangle(Brushes.Black, p.X * Game.sizeX, p.Y * Game.sizeY, Game.sizeX, Game.sizeY);
                }
                foreach (Point p in Game.foodPointList.ToList()) //foods
                {
                    gfx.FillRectangle(Brushes.DarkRed, p.X * Game.sizeX, p.Y * Game.sizeY, Game.sizeX, Game.sizeY);
                }
                if (Game.gameover) //gameover
                {
                    gfx.FillRectangle(Brushes.PaleVioletRed, snake.failPos.X * Game.sizeX, snake.failPos.Y * Game.sizeY, Game.sizeX, Game.sizeY);
                    gfx.DrawString($"GameOver! - SnakeLenght : {snake.snakeLength}", Game.font, Brushes.Black, Game.width / 2 - 50, Game.height / 2);
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Keys key = e.KeyCode;
            if ((key == Keys.W || key == Keys.Up) && (Snake.playerSnake.direction != Snake.Direction.down || Snake.playerSnake.snakeLength == 0)) { Snake.playerSnake.directionKeyDown = Snake.Direction.up; }
            if ((key == Keys.S || key == Keys.Down) && (Snake.playerSnake.direction != Snake.Direction.up || Snake.playerSnake.snakeLength == 0)) { Snake.playerSnake.directionKeyDown = Snake.Direction.down; }
            if ((key == Keys.A || key == Keys.Left) && (Snake.playerSnake.direction != Snake.Direction.right || Snake.playerSnake.snakeLength == 0)) { Snake.playerSnake.directionKeyDown = Snake.Direction.left; }
            if ((key == Keys.D || key == Keys.Right) && (Snake.playerSnake.direction != Snake.Direction.left || Snake.playerSnake.snakeLength == 0)) { Snake.playerSnake.directionKeyDown = Snake.Direction.right; }
            if (key == Keys.R) { Game.NewGame(); }
        }

        //double-buffering:
        protected override CreateParams CreateParams
        {
            get
            {
                var parm = base.CreateParams;
                parm.ExStyle &= ~0x02000000;  //Turn off WS_CLIPCHILDREN 
                parm.ExStyle |= 0x02000000; //Turn on WS_EX_COMPOSITED
                return parm;
            }
        }
    }
}
