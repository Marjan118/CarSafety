using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VehiclePhysics;

namespace Statistic
{
    public class StatisticModule : MonoBehaviour
    {
        #region Config

        public float StopSpeed = 1;

        #endregion

        private void Start()
        {
            _carController = GetComponent<VPVehicleController>();
        }

        private void FixedUpdate()
        {
            CalculateBrakeDistance();
        }

        #region Method

        public void CalculateBrakeDistance()
        {
            var brakeKeyPressed = BrakeKeys.Any(Input.GetKey);

            if (brakeKeyPressed)
            {
                if (_carController.speed > StopSpeed && _StartBrakeData == null)
                {
                    //Start braking
                    _StartBrakeData = new BrakeData(_carController.speed, _carController.transform.position);
                }
            }

            if (_StartBrakeData != null && (_carController.speed <= StopSpeed || !brakeKeyPressed))
            {
                var stopBrakeData = new BrakeData(_carController.speed, _carController.transform.position);

                var brakeAccelerate = _StartBrakeData.CalculateBrakeAccelerate(stopBrakeData);

                Accelerates.Add(brakeAccelerate);

                AverageBrakeAccelerate = Accelerates.Average();

                _StartBrakeData = null;
            }
        }

        #endregion

        #region Fields

        private BrakeData _StartBrakeData;

        private VPVehicleController _carController;

        #endregion

        #region Properties

        public double AverageBrakeAccelerate;

        public List<double> Accelerates;

        public List<KeyCode> BrakeKeys = new List<KeyCode>
        {
            KeyCode.S,
            KeyCode.DownArrow
        };

        #endregion
    }
}