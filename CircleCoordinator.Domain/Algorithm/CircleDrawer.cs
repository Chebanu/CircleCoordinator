using CircleCoordinator.Domain.Constants;
using CircleCoordinator.Domain.Models.Algorithm;

namespace CircleCoordinator.Domain.Algorithm;

internal interface ICircleDrawer
{
    Circle DrawCircle(int x, int y);
    Circle DrawCircle(Circle circle);
}

internal class CircleDrawer : ICircleDrawer
{
    private readonly Random random;

    public CircleDrawer()
    {
        random = new Random();
    }

    public Circle DrawCircle(int x, int y)
    {
        int maxDiameter = Math.Min(CanvasSize.CanvasWidth, CanvasSize.CanvasHeight);
        int diameter = random.Next(10, maxDiameter + 1);
        int radius = diameter / 2;

        int topLeftX = x - radius;
        int topLeftY = y - radius;

        string randomColor = $"#{random.Next(0x1000000):X6}";

        return new Circle
        {
            X = topLeftX,
            Y = topLeftY,
            Diameter = diameter,
            Color = randomColor
        };
    }

    public Circle DrawCircle(Circle circle)
    {
        int radius = circle.Diameter / 2;

        int topLeftX = circle.X - radius;
        int topLeftY = circle.Y - radius;

        return new Circle
        {
            X = topLeftX,
            Y = topLeftY,
            Diameter = circle.Diameter,
            Color = circle.Color
        };
    }
}
