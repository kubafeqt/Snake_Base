using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms01
{
    internal class Game
    {
        public static Point panelMargin = new(10, 10);

        public static int foodNumber = 5;

        public static int sizeX, sizeY;
        public static int width, height; //of array
        public static int[,] snakeArr; //snakes
        public static int[,] foodArr; //food types
        public static int[,] blockArr; //block types
        //later enum for food types and block types, ... ;


        public static List<Point> foodPointList = new List<Point>();

        public static bool gameover = false;

        public static Font font = new Font("Consolas", 25.0f);

        public static Random random = new Random();


        public static void InitBase()
        {
            snakeArr = new int[width, height];
            foodArr = new int[width, height];
            blockArr = new int[width, height];
        }


        #region game-control
        public static void NewGame()
        {
            ResetGame();
            Snake.playerSnake = new(width / 2, height / 2, 10);
            for (int i = 0; i < foodNumber; i++)
            {
            spawnNewFood:
                Point fPoint = new Point(random.Next(width), random.Next(height));
                if (snakeArr[fPoint.X, fPoint.Y] > 0 || foodArr[fPoint.X, fPoint.Y] != 0) //food in snake || food in block/food
                { goto spawnNewFood; }
                foodPointList.Add(fPoint);
                foodArr[fPoint.X, fPoint.Y] = 1; //food
            }
            Form1.StartTimer();
            gameover = false;
        }

        private static void ResetGame()
        {
            Snake.snakeList.Clear();
            Array.Clear(snakeArr, 0, snakeArr.Length);
            Array.Clear(foodArr, 0, foodArr.Length);
            foodPointList.Clear();
        }

        #endregion

    }
}
