public interface IBoardCell
{
    int Column { get; }
    CellContentType ContentType { get; }
    int Row { get; }
}