namespace DataFusionModule
{
    /// <summary>
    /// Base Frame of Data, given from Data fusion module
    /// </summary>
    public interface IObstacle
    {
        short Id { get; }

        ObjectType ObjectType { get; }

        float X { get; }

        float Y { get; }

        float Z { get; }

        double Width { get; }

        double Height { get; }
    }
}