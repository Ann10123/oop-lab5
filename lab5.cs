using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

abstract class Figure
{
    protected int X { get; set; }
    protected int Y { get; set; }
    protected Figure(int x, int y)
    {
        X = x;
        Y = y;
    }
    public abstract void DrawBlack(Graphics g);
    public abstract void HideDrawingBackGround(Graphics g, Color backgroundColor);
    public void MoveRight(Graphics g, Color backgroundColor, int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            HideDrawingBackGround(g, backgroundColor);
            X += 10;
            DrawBlack(g);
            Thread.Sleep(100);
        }
    }
}

class Circle : Figure
{
    private int Radius { get; set; }
    public Circle(int x, int y, int radius) : base(x, y)
    {
        Radius = radius;
    }
    public override void DrawBlack(Graphics g)
    {
        g.FillEllipse(Brushes.Black, X - Radius, Y - Radius, 2 * Radius, 2 * Radius);
    }
    public override void HideDrawingBackGround(Graphics g, Color backgroundColor)
    {
        g.FillEllipse(new SolidBrush(backgroundColor), X - Radius, Y - Radius, 2 * Radius, 2 * Radius);
    }
}

class Square : Figure
{
    private int SideLength { get; set; }

    public Square(int x, int y, int sideLength) : base(x, y)
    {
        SideLength = sideLength;
    }

    public override void DrawBlack(Graphics g)
    {
        g.FillRectangle(Brushes.Black, X - SideLength / 2, Y - SideLength / 2, SideLength, SideLength);
    }

    public override void HideDrawingBackGround(Graphics g, Color backgroundColor)
    {
        g.FillRectangle(new SolidBrush(backgroundColor), X - SideLength / 2, Y - SideLength / 2, SideLength, SideLength);
    }
}

class Rhomb : Figure
{
    private int HorDiagLen { get; set; }
    private int VertDiagLen { get; set; }

    public Rhomb(int x, int y, int horDiagLen, int vertDiagLen) : base(x, y)
    {
        HorDiagLen = horDiagLen;
        VertDiagLen = vertDiagLen;
    }

    public override void DrawBlack(Graphics g)
    {
        Point[] points = {
            new Point(X, Y - VertDiagLen / 2),
            new Point(X + HorDiagLen / 2, Y),
            new Point(X, Y + VertDiagLen / 2),
            new Point(X - HorDiagLen / 2, Y)
        };
        g.FillPolygon(Brushes.Black, points);
    }

    public override void HideDrawingBackGround(Graphics g, Color backgroundColor)
    {
        Point[] points = {
            new Point(X, Y - VertDiagLen / 2),
            new Point(X + HorDiagLen / 2, Y),
            new Point(X, Y + VertDiagLen / 2),
            new Point(X - HorDiagLen / 2, Y)
        };
        g.FillPolygon(new SolidBrush(backgroundColor), points);
    }
}

class Program
{
    static void Main()
    {
        using (var form = new Form())
        {
            form.Text = "Figures Movement";
            form.Size = new Size(500, 500);
            form.BackColor = Color.White;
            Random random = new Random();
            int formWidth = form.ClientSize.Width;
            int formHeight = form.ClientSize.Height;

            Circle circle = new Circle(random.Next(50, formWidth - 50), random.Next(50, formHeight - 50), 35);
            Square square = new Square(random.Next(50, formWidth - 50), random.Next(50, formHeight - 50), 75);
            Rhomb rhomb = new Rhomb(random.Next(50, formWidth - 50), random.Next(50, formHeight - 50), 75, 75);
            Button moveCircleButton = new Button
            {
                Text = "Move Circle",
                Size = new Size(100, 30),
                Location = new Point(40, 25)
            };

            Button moveSquareButton = new Button
            {
                Text = "Move Square",
                Size = new Size(100, 30),
                Location = new Point(190, 25)
            };

            Button moveRhombButton = new Button
            {
                Text = "Move Rhomb",
                Size = new Size(100, 30),
                Location = new Point(340, 25)
            };

            form.Controls.Add(moveCircleButton);
            form.Controls.Add(moveSquareButton);
            form.Controls.Add(moveRhombButton);

            moveCircleButton.Click += (sender, e) =>
            {
                using (Graphics g = form.CreateGraphics())
                {
                    circle.MoveRight(g, form.BackColor, 55);
                }
            };

            moveSquareButton.Click += (sender, e) =>
            {
                using (Graphics g = form.CreateGraphics())
                {
                    square.MoveRight(g, form.BackColor, 55);
                }
            };

            moveRhombButton.Click += (sender, e) =>
            {
                using (Graphics g = form.CreateGraphics())
                {
                    rhomb.MoveRight(g, form.BackColor, 55);
                }
            };
            Application.EnableVisualStyles();
            Application.Run(form);
        }
    }
}
