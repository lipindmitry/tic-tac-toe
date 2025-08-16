using UniRx;

public interface IBoardCell
{
    int Column { get; }
    int Row { get; }
    IReadOnlyReactiveProperty<CellContentType> ContentType { get; }
}