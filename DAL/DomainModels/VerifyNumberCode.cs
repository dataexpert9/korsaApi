using Component.Utility;

namespace DAL.DomainModels
{
    public class VerifyNumberCode : BaseModel
    {
        public int Id { get; set; }

        public int Code { get; set; }
        public UserTypes UserType { get; set; }
        public string Phone { get; set; }
        //public bool isVerified { get; set; }

    }
}
