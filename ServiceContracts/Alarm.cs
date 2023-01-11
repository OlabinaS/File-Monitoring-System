using System;
using System.Runtime.Serialization;

namespace ServiceContracts
{
    [DataContract]
    public enum AuditEventTypes
    {
        [EnumMember] Critical = 0,
        [EnumMember] Information = 1,
        [EnumMember] Warning = 2
    }

    [DataContract]
    public class Alarm
    {
        [DataMember]
        public DateTime TimeStamp { get; set; }

        [DataMember]
        public string Path { get; set; }

        [DataMember]
        public string Filename { get; set; }

        [DataMember]
        public AuditEventTypes Risk { get; set; }

        public Alarm(DateTime timeStamp, string path, AuditEventTypes risk, string filename)
        {
            TimeStamp = timeStamp;
            Path = path;
            Risk = risk;
            Filename = filename;
        }
    }
}
