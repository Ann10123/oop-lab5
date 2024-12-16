using System;
using System.Drawing;
using System.Threading;

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
            form.Size = new Size(800, 600);
            form.BackColor = Color.White;

            form.Paint += (sender, e) =>
            {
                Graphics g = e.Graphics;
                Circle circle = new Circle(100, 200, 50);
                Square square = new Square(300, 200, 100);
                Rhomb rhomb = new Rhomb(500, 200, 120, 80);

                circle.DrawBlack(g);
                square.DrawBlack(g);
                rhomb.DrawBlack(g);

                Thread.Sleep(1000);

                circle.MoveRight(g, form.BackColor, 10);
                square.MoveRight(g, form.BackColor, 10);
                rhomb.MoveRight(g, form.BackColor, 10);
            };

            Application.Run(form);
        }
    }
}
