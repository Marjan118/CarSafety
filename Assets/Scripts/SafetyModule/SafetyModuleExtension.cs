using DataFusionModule;
using UnityEngine;
using VehiclePhysics;

namespace SafetyModule
{
    public static class SafetyModuleExtension
    {
        public static bool CheckSafetyDistanceOfObstacle(this SafetyModule safetyModule,
            VPVehicleController carController, IObstacle obstacle)
        {
            var distanceOfObstacle = carController.GetDistance(obstacle);
            var brakingDistance = safetyModule.BrakingDistance;
            //TODO dystans wyliczany powinnien być zgodnie z trajektorią ruchu nie po lini prostej
            return distanceOfObstacle.magnitude > brakingDistance + safetyModule.SafetyDistance;
        }

        public static Vector3 GetDistance(this VPVehicleController safetyModule, IObstacle obstacle)
        {
            var obstaclePosition = new Vector3(obstacle.X, obstacle.Y, obstacle.Z);

            return safetyModule.transform.position - obstaclePosition;
        }

        public static float CalculateBrakingDistance(this SafetyModule safetyModule, float speed)
        {
            return speed * speed / (2 * safetyModule.BrakingAccelerate);
        }


        public static void StopACar(this VPStandardInput input, VPVehicleController carController)
        {
            //TODO zrobić płynne hamowanie przez użycie PIDA
            input.externalBrake = carController.brakes.maxBrakeTorque;
            input.externalClutch = 1;
        }

        public static void ReleaseBrake(this VPStandardInput input)
        {
            input.externalBrake = 0;
            input.externalClutch = 0;
        }
    }
}