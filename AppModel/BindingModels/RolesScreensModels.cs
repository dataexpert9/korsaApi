using System;
using System.Collections.Generic;
using System.Text;

namespace AppModel.BindingModels
{
    public class RoleScreenModel
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public System.Collections.Generic.List<AddRoleScreenBindingModel> Roles { get; set; }
    }

    public class AddRoleScreenBindingModel
    {
        public int Id { get; set; }

        public int ScreenId { get; set; }

        public bool AllowScreen { get; set; }

        public bool Fullaccess { get; set; }
    }
}
