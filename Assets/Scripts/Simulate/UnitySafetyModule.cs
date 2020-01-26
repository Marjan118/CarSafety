using Assets.Scripts.Simulate;
using UnityEngine;
using VehiclePhysics;

namespace Simulate
{
    public class UnitySafetyModule : MonoBehaviour
    {
        private void Start()
        {
            var carController = GetComponent<VPVehicleController>();
            var input = GetComponent<VPStandardInput>();
            var dataSource = GetComponent<UnityDataConverter>();
            var mapProvider = GetComponent<UnityMapProvider>();
            var pathProvider = GetComponent<UnityPathProvider>();

            _safetyModule = new SafetyModule.SafetyModule(dataSource, mapProvider, pathProvider, carController, input)
            {
                BrakingAccelerate = BrakingAccelerate,
                SafetyDistance = SafetyDistance
            };
        }

        private void FixedUpdate()
        {
            _safetyModule.Update();

            BrakingDistance = _safetyModule.BrakingDistance;

        }

        private void OnApplicationQuit()
        {
            _safetyModule.Dispose();
        }


        #region Fields

        #region Config

        //fo unity output
        public float BrakingDistance;

        public float BrakingAccelerate = 5;

        public float SafetyDistance = 10;

        #endregion

        private SafetyModule.SafetyModule _safetyModule;

        #endregion
    }
}