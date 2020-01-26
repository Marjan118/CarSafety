using System;
using System.Collections.Generic;

namespace DataFusionModule
{
    /// <summary>
    /// Data Fusion Module
    /// </summary>
    public interface IDataSource
    {
        event EventHandler<IEnumerable<IObstacle>> DetectedObstacles;

        event EventHandler<IObstacle> NewObstacleDetected;
    }
}