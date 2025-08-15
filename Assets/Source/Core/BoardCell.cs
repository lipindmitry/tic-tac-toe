public class BoardCell : IBoardCell
{
    public int Row { get; private set; }
    public int Column { get; private set; }
    public CellContentType ContentType { get; private set; }

    public BoardCell(int row, int column)
    {
        Row = row;
        Column = column;
        ContentType = CellContentType.Empty;
    }

    public void SetContentType(CellContentType contentType)
    {
        if (contentType != CellContentType.Empty)
            throw new System.Exception("Только в пустую ячейку можно установить метку.");
        if (contentType != CellContentType.Zero && contentType != CellContentType.Cross)
            throw new System.Exception("Установить можно только крестик или нолик.");

        ContentType = contentType;
    }
}

