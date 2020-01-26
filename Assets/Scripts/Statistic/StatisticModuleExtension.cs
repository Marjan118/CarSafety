namespace Statistic
{
    public static class StatisticModuleExtension
    {
        public static float CalculateBrakeAccelerate(this BrakeData startBrakeData, BrakeData stopBrakeData)
        {
            var brakeDistance = startBrakeData.Position - stopBrakeData.Position;

            var velocity = startBrakeData.Velocity * startBrakeData.Velocity -
                           stopBrakeData.Velocity * stopBrakeData.Velocity;

            return velocity / (2 * brakeDistance.magnitude);
        }
    }
}