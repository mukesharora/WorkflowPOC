using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Contract
{
    [DataContract]
    public class AnnounceRequest
    {
        [DataMember]
        public int state
        {
            get;
            set;
        }
    }
}
