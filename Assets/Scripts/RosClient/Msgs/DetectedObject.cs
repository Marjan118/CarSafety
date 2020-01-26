using RosBridgeClient.BaseMessages;
using Simulate;
using UnityEngine;

namespace RosClient.Msgs
{
    public class DetectedObject : Message
    {
        public const string RosMessageName = "safety/DetectedObject";

        public double Height;

        public int Id;

        public int ObjectType;

        public double Width;

        public double X;
        public double Y;
        public double Z;

        public DetectedObject()
        {
        }

        public DetectedObject(Vector3 distance, Obstacle describe)
        {
            Id = describe.GetInstanceID();
            ObjectType = (int) describe.objectType;

            X = distance.x;
            Y = distance.y;
            Z = distance.z;
        }
    }
}