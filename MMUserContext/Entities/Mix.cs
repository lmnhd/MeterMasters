using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMUserContext.Entities
{
    public class Mix
    {
        public struct CostItemization
        {
            public MixRequest.MixType MixType { get; set; }
            public int NumVersions { get; set; }
        }
        public virtual int Id { get; set; }

        public virtual int RequestId { get; set; }

        public virtual string ClientUserId { get; set; }
        public virtual int ClientId { get; set; }
        public virtual List<MixRequest.VersionType> Versions { get; set; }
        public virtual string ReviewMixUrl { get; set; }
        public virtual string MasterArchiveUrl { get; set; }

        public virtual string ClientComments
        {
            get; set;
            
        }

        public virtual decimal Price { get; set; }

    }
}
