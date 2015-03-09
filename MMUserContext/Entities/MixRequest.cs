using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMUserContext.Entities
{
    public class MixRequest
    {
        public virtual int Id { get; set; }

        public enum VersionType
        {
            
            Instrumental,
            Acapella,
            Showversion,
            Special

        }

        public enum MixType
        {
            FullTracks,
            VocalsOnly,
            TrackOnly
        }

        public enum MixArchiveType
        {
            Session,
            Wave
        }

        public enum UsedDAW
        {
            Protools,
            Cubase,
            Cakewalk,
            Nuendo,
            Garageband,
            FLStudio
        }


        public virtual List<VersionType> VersionTypes { get; set; } 


        public virtual MixType MyMixType { get; set; }

        public virtual string ClientUserId { get; set; }
        public virtual int ClientId { get; set; }
        public virtual MixArchiveType ArchiveType { get; set; }
        public virtual UsedDAW DAW { get; set; }

        public virtual string Title { get; set; }
        public virtual string UploadLink { get; set; }
        public virtual bool CanModifySounds { get; set; }
        public virtual string ClientName { get; set; }
        public virtual string Genre { get; set; }
        public virtual string ModifyNotes { get; set; }
        public virtual string ClientNotes { get; set; }
        public virtual string EngineerNotes { get; set; }
        public virtual DateTime EntryTime { get; set; }
        public virtual DateTime AcceptanceTime { get; set; }
        public virtual bool Active { get; set; }
        public virtual bool MixCancelled { get; set; }
        public virtual bool MixComplete { get; set; }
    }
}
