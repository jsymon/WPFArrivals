using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace WPFArrivals.API
{
    [Serializable]
    [DataContract]
    public class Route
    {
        [DataMember(Name = "$type")]
        public string Type { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "modeName")]
        public string ModeName { get; set; }

        public DateTime Created { get; set; }

        [DataMember(Name = "created")]
        private string CreatedForSerialisation { get; set; }

        public DateTime Modified { get; set; }

        [DataMember(Name = "modified")]
        private string ModifiedForSerialisation { get; set; }

        [DataMember(Name = "routeSections")]
        public RouteSection[] RouteSections { get; set; }

        [OnDeserialized]
        void OnDeserialized(StreamingContext ctx)
        {
            this.Created = DateTime.Parse(this.CreatedForSerialisation);
            this.Modified = DateTime.Parse(this.ModifiedForSerialisation);
        }
    }

    [Serializable]
    [DataContract(Name = "routeSection")]
    public class RouteSection
    {
        [DataMember(Name = "$type")]
        public string Type { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "direction")]
        public string Direction { get; set; }

        [DataMember(Name = "originationName")]
        public string OriginationName { get; set; }

        [DataMember(Name = "destinationName")]
        public string DestinationName { get; set; }

        [DataMember(Name = "originator")]
        public string Originator { get; set; }

        [DataMember(Name = "destination")]
        public string Destination { get; set; }
    }
}
