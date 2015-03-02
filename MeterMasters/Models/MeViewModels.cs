using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MMUserContext.Entities;

namespace MeterMasters.Models
{
    // Models returned by MeController actions.
    public class GetViewModel
    {
        public string UserId { get; set; }
        public int ClientId { get; set; }
        public string Hometown { get; set; }
        public string Message { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }

        public List<MixRequest> Requests { get; set; } 

    }
}