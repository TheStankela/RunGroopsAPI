using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RunGroops.Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace RunGroops.Domain.EFModels
{
    public class Friend
    {
        [ForeignKey("FromUser")]
        public string FromUserId { get; set; }
        [ForeignKey("ToUser")]
        public string ToUserId { get; set; }
        public virtual AppUser FromUser { get; set; }
        public virtual AppUser ToUser { get; set; }
        public FriendRequestStatus FriendRequestStatus { get; set; }
        [NotMapped]
        public bool Approved => FriendRequestStatus == FriendRequestStatus.Approved;
    }
}
