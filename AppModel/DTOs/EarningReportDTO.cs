using System;
using System.Collections.Generic;
using System.Text;

namespace AppModel.DTOs
{
    public class EarningReportDTO
    {
        public EarningReportDTO()
        {
            OutFlows = new List<DriverPaymentDTO>();
            UserDeposits = new List<TripDTO>();
            DriverSubscriptions = new List<CashSubscriptionDTO>();
        }
        public List<DriverPaymentDTO> OutFlows { get; set; }
        public List<TripDTO> UserDeposits { get; set; }
        public List<CashSubscriptionDTO> DriverSubscriptions { get; set; }

    }
    public class EarningReportDTOList
    {
        public List<EarningReportDTO> Earnings { get; set; }
    }
}
