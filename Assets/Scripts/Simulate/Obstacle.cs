using DataFusionModule;
using UnityEngine;

namespace Simulate
{
    public class Obstacle : MonoBehaviour, IObstacle
    {
        // Update is called once per frame
        private void Update()
        {
            //moveType.Render(transform);
        }

        #region Properties

        public short Id => (short) GetInstanceID();

        public ObjectType ObjectType => objectType;
        public float X => transform.position.x;
        public float Y => transform.position.y;
        public float Z => transform.position.z;

        //TODO szerokośc powinna byc przeliczana względem obserwatora czyli samochodu

        public double Width => width;
        public double Height => height;

        #endregion

        #region Fields

        public ObjectType objectType;

        public double width;

        public double height;

        #endregion
    }
}