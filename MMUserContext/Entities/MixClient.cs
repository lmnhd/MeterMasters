using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMUserContext.Entities
{
    public class MixClient
    {
        public virtual int Id { get; set; }
        public virtual string UserId { get; set; }
        public virtual string Hometown { get; set; }

        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual List<int> Requests { get; set; }
        public virtual List<int> Mixs { get; set; }
        public virtual DateTime MemberSince { get; set; }
        public virtual List<int> DeclinedMixs { get; set; }
        public virtual bool UnderReview { get; set; }

    }
}
