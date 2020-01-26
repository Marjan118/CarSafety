using RosBridgeClient;
using RosBridgeClient.Protocols;
using RosClient.Msgs;
using Simulate;
using UnityEngine;

namespace RosClient.General
{
    public class RosClient
    {
        public readonly string NewObjectDetectedPublicationId;

        public readonly string ObjectInViewPublicationId;

        public readonly RosSocket RosSocket;

        public RosClient(string address = RosCfg.ConnectionAddress)
        {
            RosSocket = new RosSocket(new WebSocketNetProtocol(address));
            NewObjectDetectedPublicationId = RosSocket.Advertise<DetectedObject>(RosCfg.NewObjectDetectedTopic);
            ObjectInViewPublicationId = RosSocket.Advertise<View>(RosCfg.ObjectInViewTopic);
        }

        public void NewObjectDetected(Vector3 distance, Obstacle gameObject)
        {
            var detectedObject = new DetectedObject(distance, gameObject);

            RosSocket.Publish(NewObjectDetectedPublicationId, detectedObject);
        }
    }
}