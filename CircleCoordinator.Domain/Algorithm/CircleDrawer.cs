using CircleCoordinator.Domain.Models.Algorithm;

namespace CircleCoordinator.Domain.Algorithm;

internal interface ICircleDrawer
{
    Circle DrawCircle(int x, int y);
}
internal class CircleDrawer : ICircleDrawer
{
    private Random random;

    public CircleDrawer()
    {
        random = new Random();
    }

    public Circle DrawCircle(int x, int y)
    {
        int diameter = random.Next(10, 100); // Random diameter
        int radius = diameter / 2;

        // Calculate the top-left corner coordinates of the rectangle
        int topLeftX = x - radius;
        int topLeftY = y - radius;

        // Generate random color
        string randomColor = $"#{random.Next(0x1000000):X6}";

        return new Circle
        {
            X = topLeftX,
            Y = topLeftY,
            Diameter = diameter,
            Color = randomColor
        };
    }
}
