﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.DomainModels
{

    public partial class Admin
    {

        public Admin()
        {
            ReceivedNotifications = new HashSet<AdminSubAdminNotifications>();
            SentNotifications = new HashSet<AdminNotifications>();
            AdminTokens = new HashSet<AdminTokens>();

        }

        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public string Phone { get; set; }

        [Required]
        public short Role { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public string AccountNo { get; set; }

        public int? Store_Id { get; set; }
        public int? Role_Id { get; set; }

        public short? Status { get; set; }

        public bool IsDeleted { get; set; }

        [NotMapped]
        public Token Token { get; set; }

        [NotMapped]
        public bool ImageDeletedOnEdit { get; set; }
        public string ImageUrl { get; set; }

        public virtual Role Roles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdminSubAdminNotifications> ReceivedNotifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdminNotifications> SentNotifications { get; set; }        

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdminTokens> AdminTokens { get; set; }
    }
}
