namespace Alarms
{
    /// <summary>
    /// Base Alarm class
    /// </summary>
    public class Alarm
    {
        public Alarm(string message, AlarmType type)
        {
            //TODO add multiLanguage
            Message = message;
            Type = type;
        }

        public string Message { get; }

        public AlarmType Type { get; }
    }
}