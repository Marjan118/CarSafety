using RosBridgeClient.BaseMessages;
using UnityEngine;

namespace RosClient.Msgs
{
    public class GlobalPosition : Message
    {
        public const string RosMessageName = "safety/GlobalPosition";

        public double Alfa;
        public double Beta;
        public double Gamma;

        public double X;
        public double Y;
        public double Z;

        public GlobalPosition(Transform transform)
        {
            var euler = transform.eulerAngles;

            Alfa = euler.x;
            Beta = euler.y;
            Gamma = euler.z;

            var position = transform.position;

            X = position.x;
            Y = position.y;
            Z = position.z;
        }
    }
}