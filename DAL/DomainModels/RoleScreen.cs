namespace DAL.DomainModels
{
    public partial class RoleScreen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RoleScreen()
        {
            
        }

        public int Id { get; set; }

        
        public int Screen_Id { get; set; }   
            
        public int Role_Id { get; set; }

        public bool AllowScreen { get; set; }

        public bool Fullaccess { get; set; }

        public Role Roles { get; set; }
        public Screen Screens { get; set; }

    }
}
