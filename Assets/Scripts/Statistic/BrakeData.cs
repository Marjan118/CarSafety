using UnityEngine;

namespace Statistic
{
    public class BrakeData
    {
        public BrakeData(float velocity, Vector3 position)
        {
            Velocity = velocity;
            Position = position;
        }

        public float Velocity { get; }

        public Vector3 Position { get; }
    }
}