using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RestService
{
    public class Serie
    {
        [DataMember]
        public int id;
        [DataMember]
        public string title;
        [DataMember]
        public string plot;
        [DataMember]
        public string genre;
        [DataMember]
        public string imdb;
        [DataMember]
        public string image;
        [DataMember]
        public string search;
    }
}