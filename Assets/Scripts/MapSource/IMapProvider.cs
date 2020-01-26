using DataFusionModule;

namespace MapSource
{
    /// <summary>
    /// Source of Map
    /// </summary>
    public interface IMapProvider
    {
        bool ObstacleExist(IObstacle obstacle);
    }
}