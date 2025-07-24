using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms01
{
   internal class Snake
   {
      public static Snake playerSnake;
      public static List<Snake> snakeList = new();

      public enum Direction
      {
         left,
         right,
         up,
         down,
         none
      }

      public Direction direction {  get; set; }
      public Direction directionKeyDown = Direction.none;

      public int snakeLength { get; set; }
      public int startSnakeLength { get; set; }


      public int x { get; set; }
      public int y { get; set; }

      public Queue<Point> snakePointQueue = new Queue<Point>();

      public Point failPos = new Point();

      public Snake(int startX, int startY, int startSnakeLength)
      {
         x = startX;
         y = startY;
         this.startSnakeLength = startSnakeLength;
         Game.snakeArr[x, y] = 1;
         snakePointQueue.Enqueue(new Point(x, y));
         snakeList.Add(this);
      }

   }
}
