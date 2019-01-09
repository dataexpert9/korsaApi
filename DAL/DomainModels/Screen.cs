namespace DAL.DomainModels
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Screen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Screen()
        {

            RoleScreen = new HashSet<RoleScreen>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }   
            
        public string Name_Ar { get; set; }

        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<RoleScreen> RoleScreen { get; set; }
    }
}
