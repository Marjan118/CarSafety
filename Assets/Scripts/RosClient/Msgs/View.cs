using RosBridgeClient.BaseMessages;

namespace RosClient.Msgs
{
    public class View : Message
    {
        public const string RosMessageName = "safety/DetectedObjects";

        public readonly DetectedObject[] DetectedObjects;

        public View(DetectedObject[] carDetectedObjects)
        {
            DetectedObjects = carDetectedObjects;
        }
    }
}