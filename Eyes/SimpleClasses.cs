using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Eyes
{
    // Класс точки в 2 мерном пространстве
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point()
        {
            X = 0;
            Y = 0;
        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point(double x, double y)
        {
            X = Convert.ToInt32(Math.Round(x));
            Y = Convert.ToInt32(Math.Round(y));
        }

        public Point(Point p)
        {
            X = p.X;
            Y = p.Y;
        }

        /// <summary>
        /// Расчитывает расстояние от указанной точки до объекта
        /// </summary>
        /// <param name="ToPoint">Точка, от которой считаем расстояние</param>
        public double Distance(Point ToPoint)
        {
            return DistanceInner(ToPoint.X, ToPoint.Y);
        }

        /// <summary>
        /// Расчитывает расстояние от указанной точки до объекта
        /// </summary>
        /// <param name="x">Координата X, от которой считаем расстояние</param>
        /// <param name="y">Координата Y, от которой считаем расстояние</param>
        public double Distance(int x, int y)
        {
            return DistanceInner(x, y);
        }

        private double DistanceInner(int x, int y)
        {
            return Math.Sqrt(Math.Pow(x - X, 2) + Math.Pow(y - Y, 2));
        }

        /// <summary>
        /// Сдвигает точку на задонное значение
        /// 
        /// Выдаёт true, если сдвиг произошёл и false, если сдвига не было.
        /// </summary>
        /// <param name="Delta">Вектор, на который сдвигаем</param>
        public bool Move(Point Delta)
        {
            X += Delta.X;
            Y += Delta.Y;
            if (Delta.X != 0) return true;
            if (Delta.Y != 0) return true;
            return false;
        }

        /// <summary>
        /// Сдвигает точку на задонное значение.
        /// 
        /// Выдаёт true, если сдвиг произошёл и false, если сдвига не было.
        /// </summary>
        /// <param name="x">Сдвиг по оси абсцисс.</param>
        /// <param name="y">Сдвиг по оси ординат</param>
        public bool Move(int x, int y)
        {
            X += x;
            Y += y;
            if (x != 0) return true;
            if (y != 0) return true;
            return false;
        }
    }

    /// <summary>
    /// Осуществляет перемещение объекта
    /// </summary>
    public class DragObject
    {
        Control DragElement;
        Point Position;
        bool IsDrag = false;
        Point Limits;
        Point Size;

        public DragObject(Control Element = null)
        {
            DragElement = Element;
            Position = DragElement == null ? new Point() : new Point(DragElement.Left, DragElement.Top);
            Limits = DragElement == null ? new Point() : new Point(DragElement.Width, DragElement.Height);
        }

        /// <summary>
        /// Устанавливает начальные координаты привязки объекта
        /// </summary>
        /// <param name="Place">Координаты привязки, относительно которых осуществляется перемещение</param>
        public void SetPoint(Point Place)
        {
            Position = new Point(Place);
        }

        /// <summary>
        /// Устанавливает начальные координаты привязки объекта
        /// </summary>
        /// <param name="x">Координата X, относительно которой осуществляется перемещение</param>
        /// <param name="y">Координата Y, относительно которой осуществляется перемещение</param>
        public void SetPoint(int x, int y)
        {
            Position = new Point(x,y);
        }

        /// <summary>
        /// Устанавливает размеры перемещаемого объекта
        /// </summary>
        /// <param name="width">Ширина перемещаемого объекта</param>
        /// <param name="height">Высота перемещаемого объекта</param>
        public void SetObjectSize(int width, int height)
        {
            Limits = new Point(width, height);
        }

        /// <summary>
        /// Устанавливает размеры зоны перемещения объекта
        /// </summary>
        /// <param name="width">Ширина зоны перемещения</param>
        /// <param name="height">Высота зоны перемещения</param>
        public void SetOwnerSize(int width, int height)
        {
            Size = new Point(width, height);
        }

        /// <summary>
        /// Осуществляет перемещение объекта да разницу между вводными значениями и координатами привязки. 
        /// 
        /// Если объект меньше поля, по которому он перемецается, то он центрируется.
        /// 
        /// Обнуляет координаты привязки.
        /// </summary>
        /// <param name="x">Координата X, до которой осуществляется перемещение</param>
        /// <param name="y">Координата Y, до которой осуществляется перемещение</param>
        public Point MoveOuter(int x, int y)
        {
            if (!IsDrag) return new Point();
            int X = SetOuter(x, Position.X, Size.X, Limits.X);
            int Y = SetOuter(y, Position.Y, Size.Y, Limits.Y);
            SetPoint(x, y);
            return new Point(X, Y);
        }

        /// <summary>
        /// Осуществляет перемещение объекта да разницу между вводными значениями и координатами привязки. 
        /// 
        /// Если объект меньше поля, по которому он перемецается, то он центрируется.
        /// 
        /// Обнуляет координаты привязки.
        /// 
        /// Не требует включения
        /// </summary>
        /// <param name="x">Сдвиг по оси абцисс</param>
        /// <param name="y">Сдвиг по оси ординат</param>
        public Point MoveOnceOuter(int x, int y)
        {
            int X = SetOuter(x, 0, Size.X, Limits.X);
            int Y = SetOuter(y, 0, Size.Y, Limits.Y);
            SetPoint(X, Y);
            return new Point(X, Y);
        }

        /// <summary>
        /// Начинает перемещение
        /// </summary>
        /// <param name="Place">Координаты привязки, относительно которых осуществляется перемещение</param>
        public void StartDrag(Point Place)
        {
            SetPoint(Place);
            IsDrag = true;
        }

        /// <summary>
        /// Начинает перемещение
        /// </summary>
        /// <param name="x">Координата X, относительно которой осуществляется перемещение</param>
        /// <param name="y">Координата Y, относительно которой осуществляется перемещение</param>
        public void StartDrag(int x, int y)
        {
            SetPoint(x, y);
            IsDrag = true;
        }

        public void StopDrag()
        {
            IsDrag = false;
        }

        private int SetOuter(int x, int Pos, int size, int Limit)
        {
            int X = x - Pos;
            /*if (X < 0)
                X = 0;
            if (X > Limit - size)
                X = Limit - size;*/

            if (Limit < size)
                X = (size - Limit) / 2;
            return X;
        }
    }
}