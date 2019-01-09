namespace DAL.DomainModels
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Role
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Role()
        {
            RoleScreen = new HashSet<RoleScreen>();
            Admins = new HashSet<Admin>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }   

        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<RoleScreen> RoleScreen { get; set; }
        public virtual ICollection<Admin> Admins { get; set; }
    }
}
