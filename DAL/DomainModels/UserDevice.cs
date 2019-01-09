using PushSharp.Apple;
using PushSharp.Google;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DomainModels
{
    public class UserDevice : BaseModel
    {
        public UserDevice()
        {

        }
        public int Id { get; set; }
        public string DeviceName { get; set; }
        public string UDID { get; set; }
        public bool Platform { get; set; }
        public int? User_Id { get; set; }
        public string AuthToken { get; set; }
        public bool IsActive { get; set; } 
        public virtual User User { get; set; }

        public int? Driver_Id { get; set; }
        public virtual Driver Driver { get; set; }

        [NotMapped]
        public GcmConfiguration AndroidPushConfiguration;
        [NotMapped]
        public ApnsConfiguration iOSPushConfiguration;

        public enum ApnsEnvironmentTypes
        {
            Sandbox,
            Production
        }

        public enum ApplicationTypes
        {
            PlayStore,
            Enterprise
        }
        /// <summary>
        /// Will be initialized against IOS devices(if enabled on client side)
        /// </summary>
        public ApnsEnvironmentTypes EnvironmentType { get; set; }
        /// <summary>
        /// Will be initialized against IOS devices(if enabled on client side)
        /// </summary>

        public ApplicationTypes ApplicationType { get; set; }








    }
}
