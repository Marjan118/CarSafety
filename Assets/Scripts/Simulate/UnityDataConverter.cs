using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataFusionModule;
using Simulate;
using UnityEngine;

namespace Assets.Scripts.Simulate
{
    public class UnityDataConverter : MonoBehaviour, IDataSource
    {
        public void Start()
        {
            RangeOfView = gameObject.AddComponent<SphereCollider>();
            RangeOfView.isTrigger = true;
            RangeOfView.radius = DistanceOfView;
        }


        #region Properties

        public float DistanceOfView => distanceOfView;

        public SphereCollider RangeOfView { get; private set; }

        #endregion

        #region Fields

        public float distanceOfView;

        private readonly List<Obstacle> ItemsInRangeOfView = new List<Obstacle>();

        #endregion

        #region Methods

        private void OnTriggerEnter(Collider other)
        {
            var obstacle = other.gameObject.GetComponent<Obstacle>();

            if (obstacle == null)
            {
                return;
            }

            ItemsInRangeOfView.Add(obstacle);
            NewObstacleDetected?.Invoke(this, obstacle);
        }

        private void OnTriggerExit(Collider other)
        {
            var obstacle = other.gameObject.GetComponent<Obstacle>();

            if (obstacle == null)
            {
                return;
            }

            ItemsInRangeOfView.Remove(obstacle);
        }

        private void FixedUpdate()
        {
            DetectedObstacles?.Invoke(this, ItemsInRangeOfView);
        }

        #endregion

        #region Events

        public event EventHandler<IEnumerable<IObstacle>> DetectedObstacles;
        public event EventHandler<IObstacle> NewObstacleDetected;

        #endregion
    }
}
