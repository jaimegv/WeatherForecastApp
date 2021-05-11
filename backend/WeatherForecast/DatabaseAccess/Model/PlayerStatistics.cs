using System.Diagnostics;

namespace KickBase.DatabaseAccess.Model
{
    [DebuggerDisplay("{Id}.{Name}. Position: {Position}. Points: {Points}. Season points: {SeasonPoints}. Goals: {Goals}. Assists: {Assists}. RedCards: {RedCards}. YellowCards: {YellowCards}")]
    public class PlayerStatistics
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int Position { get; set; }
        public long? Points { get; set; }
        public long? SeasonPoints { get; set; }
        public int? Goals { get; set; }
        public int? Assists { get; set; }
        public int? RedCards { get; set; }
        public int? YellowCards { get; set; }
    }
}