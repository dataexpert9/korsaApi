using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppModel.DTOs
{
    public class SearchRoleListDTO
    {
        public SearchRoleListDTO()
        {
            Roles = new List<RoleDTO>();
        }
        public IEnumerable<RoleDTO> Roles { get; set; }
    }
    public class RoleDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public List<RoleScreenDTO> RoleScreen { get; set; }

    }

    public class RoleScreenDTO
    {

        public int Id { get; set; }

        public int Screen_Id { get; set; }

        public int Role_Id { get; set; }

        public bool AllowScreen { get; set; }

        public bool Fullaccess { get; set; }

    }
}
