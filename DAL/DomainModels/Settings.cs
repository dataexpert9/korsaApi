using Component.Utility;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DomainModels
{
    public class Settings : BaseModel
    {
        public int Id { get; set; }
        public string CurrencySymbol { get; set; }
        public double CurrencyToUSDRatio { get; set; }
        public float InvitationBonus { get; set; }
        public double MinimumRequestRadius { get; set; }
        public double MaximumRequestRadius { get; set; }
        public double RideTax { get; set; }
        public CultureType Culture { get; set; }
        public virtual ICollection<SettingsML> SettingsMLsList { get; set; }
    }

    public class SettingsML : BaseModel
    {
        public int Id { get; set; }
        public string AboutUs { get; set; }
        public string PrivacyPolicy { get; set; }
        public string Currency { get; set; }
        public string TermsOfUse { get; set; }
        public CultureType Culture { get; set; }
        [ForeignKey("Settings")]
        public int Settings_Id { get; set; }
        public Settings Settings { get; set; }
    }
}
