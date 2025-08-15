public class GameParameters
{
    public int PlayerNumber { get; }
    public CellContentType PlayerContentType { get; }
    public int OpponentNumber { get; }
    public CellContentType OpponentContentType { get; }

    public GameParameters(int playerNumber,
        CellContentType playerContentType,
        int opponentNumber,
        CellContentType opponentContentType)
    {
        PlayerNumber = playerNumber;
        PlayerContentType = playerContentType;
        OpponentNumber = opponentNumber;
        OpponentContentType = opponentContentType;
    }
}