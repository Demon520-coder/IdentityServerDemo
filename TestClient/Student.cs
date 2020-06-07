using ProtoBuf;
using System;
namespace TestClient
{
    [ProtoContract]
    public class Student
    {  
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public int Age { get; set; }
        [ProtoMember(3)]
        public char Gender { get; set; }
    }
}
