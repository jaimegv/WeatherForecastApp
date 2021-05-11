using System.Diagnostics;

namespace KickBase.DatabaseAccess.Model
{
    [DebuggerDisplay("{PlayerId}.{FirstName}.{LastName}.{NickName}.{MarketValue}.{Status}.{TeamId}.{Position}.{ShirtNumber}")]
    public class Player
    {
        public int PlayerId { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string NickName { get; set; }
        public double MarketValue { get; set; }
        public int Status { get; set; }
        public int TeamId { get; set; }
        public int Position { get; set; }
        public int ShirtNumber { get; set; }
    }
}
