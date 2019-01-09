using System;
using System.Collections.Generic;
using System.Text;

namespace AppModel.DTOs
{
    public class InvitedFriendDTO : BaseModelDTO
    {
        public InvitedFriendDTO()
        {
            InvitedUser = new UserDTO();
            Referrer = new UserDTO();
        }
        public int Id { get; set; }

        public int InvitedUser_Id { get; set; }
        public UserDTO InvitedUser { get; set; }

        public int Referrer_Id { get; set; }
        public virtual UserDTO Referrer { get; set; }
    }

    public class InvitedFriendDTOList
    {
        public InvitedFriendDTOList()
        {
            InvitedFriends = new List<InvitedFriendDTO>();
        }
        public List<InvitedFriendDTO> InvitedFriends { get; set; }
    }
}