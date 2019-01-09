using System;
using System.Collections.Generic;
using System.Text;

namespace AppModel.DTOs
{
    public class SettingsDTO
    {
        public SettingsDTO()
        {
            English = new SettingsMLsDTO();
            Arabic = new SettingsMLsDTO();
        }
        public int Id { get; set; }
        public string CurrencySymbol { get; set; }
        public float InvitationBonus { get; set; }
        public double CurrencyToUSDRatio { get; set; }

        public double RideTax { get; set; }
        public SettingsMLsDTO English { get; set; }
        public SettingsMLsDTO Arabic { get; set; }

    }
    public class SettingsMLsDTO
    {
        public string AboutUs { get; set; }
        public string PrivacyPolicy { get; set; }
        public string Currency { get; set; }
        public string TermsOfUse { get; set; }
    }

}
