using UniRx;

public class BoardCell : IBoardCell
{
    public int Row { get; private set; }
    public int Column { get; private set; }
    public IReadOnlyReactiveProperty<CellContentType> ContentType => _contentType;

    private ReactiveProperty<CellContentType> _contentType = new();

    public BoardCell(int row, int column)
    {
        Row = row;
        Column = column;
        _contentType.Value = CellContentType.Empty;
    }

    public void SetContentType(CellContentType contentType)
    {
        if (_contentType.Value != CellContentType.Empty)
            throw new System.Exception("Только в пустую ячейку можно установить метку.");
        if (contentType != CellContentType.Zero && contentType != CellContentType.Cross)
            throw new System.Exception("Установить можно только крестик или нолик.");

        _contentType.Value = contentType;
    }

    public void Clear()
    {
        _contentType.Value = CellContentType.Empty;
    }
}

