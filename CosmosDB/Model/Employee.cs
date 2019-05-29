using System;
using System.Runtime.Serialization;

namespace CosmosDB.Model
{
    [Serializable]
    [DataContract]
    
    public class Employee
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string City { get; set; }
    }
}
