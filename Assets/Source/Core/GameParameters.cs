public class GameParameters
{
    public string PlayerId { get; }
    public CellContentType PlayerContentType { get; }
    public string OpponentId { get; }
    public CellContentType OpponentContentType { get; }

    public GameParameters(string playerId,
        CellContentType playerContentType,
        string opponentId,
        CellContentType opponentContentType)
    {
        PlayerId = playerId;
        PlayerContentType = playerContentType;
        OpponentId = opponentId;
        OpponentContentType = opponentContentType;
    }
}