using System.Collections.Generic;

namespace AppModel.DTOs
{
    public class CancellationReasonDTO
    {
        public int Id { get; set; }
        public string Reason { get; set; }
    }

    public class CancellationReasonDTOList
    {
        public List<CancellationReasonDTO> CancellationReasons { get; set; }
    }
}
