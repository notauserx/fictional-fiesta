namespace PixelSort.Domain
{
    public interface IPixelConfiguraiton
    {
        int Width { get; }

        int Height { get; }

        int PixelSize { get; }

        int Dpi { get; }
    }
}
