using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MMUserContext.Entities;

namespace MMUserContext.Concrete
{
    public class MMDataWorkUnit : IDisposable
    {
        private UserContext userContext = new UserContext();
        private DbSet<MixClient> mixClients;
        private DbSet<MixRequest> mixRequests;
        private DbSet<Mix> mixs;

        public MMDataWorkUnit()
        {
            mixClients = userContext.MixClients;
            mixRequests = userContext.MixRequests;
            mixs = userContext.Mixs;
        }

        public int StoreClient(string userId,string email,string homeTown)
        {
            var client = new MixClient
            {
                UserId = userId,
                Name = email,
                Email = email,
                Hometown = homeTown,
                MemberSince = DateTime.Now
                


            };
            mixClients.Add(client);

            userContext.SaveChanges();
            return client.Id;
        }

        public MixRequest StoreMixRequest(MixRequest request)
        {
            var client = mixClients.FirstOrDefault(c => c.UserId == request.ClientUserId);
            if (client == null)
            {
                return request;
            }

            
            mixRequests.Add(request);
            
            userContext.SaveChanges();

            if (client.Requests == null)
            {
                client.Requests = new List<int>();
            }
            client.Requests.Add(request.Id);
            client.UnderReview = true;
            UpdateClient(client);
            return request;
        }

        public MixRequest GetMixRequest(int id)
        {
            return mixRequests.Find(id);

        }
        public bool UpdateClient(MixClient client)
        {
           
            userContext.Entry(client).State = EntityState.Modified;
            userContext.SaveChanges();
            return true;
        }
        public MixClient GetClient(string userId, bool loadRequests = false)
        {
            if (loadRequests)
            {
                var client = mixClients.FirstOrDefault(c => c.UserId.Equals(userId));

                if (client != null)
                {
                    client.Requests = GetMixRequestIds(client.UserId);
                }

                return client;

            }
            return mixClients.FirstOrDefault(c => c.UserId.Equals(userId));


        }

        public List<MixRequest> GetMixRequests(string userId)
        {
            return mixRequests.Where(m => m.ClientUserId.Equals(userId)).ToList();
        }

        public List<int> GetMixRequestIds(string userId)
        {
            var result = new List<int>();
            var requests = GetMixRequests(userId);
            if (requests.Count > 0)
            {
                result.AddRange(requests.Select(request => request.Id));
            }
            return result;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
