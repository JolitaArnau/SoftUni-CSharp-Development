using System;

public class Box
{
    private double length;
    private double width;
    private double height;

    public Box(double length, double widht, double height)
    {
        this.Lenght = length;
        this.Width = widht;
        this.Height = height;
    }

    public double Lenght
    {
        get { return this.length; }

        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Length cannot be zero or negative.");
            }

            this.length = value;
        }
    }

    public double Width
    {
        get { return this.width; }

        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Width cannot be zero or negative.");
            }

            this.width = value;
        }
    }

    public double Height
    {
        get { return this.height; }

        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Height cannot be zero or negative.");
            }

            this.height = value;
        }
    }

    public double CalculateSurfaceArea(double l, double w, double h)
    {
        var surface = 2 * (this.length * this.width) + 2 * (this.length * this.height) + 2 * (this.width * this.height);
        return surface;
    }

    public double CalculateLateralSurface(double l, double w, double h)
    {
        var lateralSurface = 2 * (this.length * this.height) + 2 * (this.width * this.height);
        return lateralSurface;
    }

    public double CalculateVolume(double l, double w, double h)
    {
        var volume = this.length * this.height * this.width;
        return volume;
    }
}