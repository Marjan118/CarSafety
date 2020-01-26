using DataFusionModule;
using MapSource;
using UnityEngine;

namespace Simulate
{
    public class UnityMapProvider :MonoBehaviour,  IMapProvider
    {
        public bool ObstacleExist(IObstacle obstacle)
        {
            //TODO load map
            return false;
        }
    }
}