using DataFusionModule;
using PathSource;
using UnityEngine;

namespace Simulate
{
    public class UnityPathProvider : MonoBehaviour, IPathProvider
    {
        public bool OnPath(IObstacle obstacle)
        {
            return true;
        }
    }
}