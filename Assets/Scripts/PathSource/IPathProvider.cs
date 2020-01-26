using DataFusionModule;

namespace PathSource
{
    /// <summary>
    /// Source of Path
    /// </summary>
    public interface IPathProvider
    {
        bool OnPath(IObstacle obstacle);
    }
}