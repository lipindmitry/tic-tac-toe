using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

public class GameBoard : IGameBoard
{
    private const int MIN_BOARD_SIZE = 3;

    public IObservable<IBoardCell> LastMove => _lastMove;
    private ReactiveProperty<BoardCell> _lastMove = new();

    public IEnumerable<IBoardCell> Cells => _cells;
    private List<BoardCell> _cells = new();

    private readonly int _size;

    public GameBoard(GameBoardSettings gameBoardSettings)
    {
        if (gameBoardSettings.Size <= MIN_BOARD_SIZE)
            throw new ArgumentOutOfRangeException(nameof(gameBoardSettings.Size));

        _size = gameBoardSettings.Size;
        for (var row = 0; row < _size; row++)
        {
            for (var column = 0; column < _size; column++)
            {
                var newCell = new BoardCell(row, column);
                _cells.Add(newCell);
            }
        }
    }

    public bool IsEmpty(int row, int column)
    {
        BoardCell cell = GetCell(row, column);
        return cell.ContentType == CellContentType.Empty;
    }

    public void SetContent(int row, int column, CellContentType content)
    {
        BoardCell cell = GetCell(row, column);
        cell.SetContentType(content);
        _lastMove.Value = cell;
    }

    public bool IsFullLine(out CellContentType cellContentType)
    {
        for (int i = 0; i < _size; i++)
        {
            if (CheckLine(i, 0, 0, 1))
            {
                cellContentType = GetCell(i, 0).ContentType;
                return true;
            }

            if (CheckLine(0, i, 1, 0))
            {
                cellContentType = GetCell(0, i).ContentType;
                return true;
            }
        }

        if (CheckLine(0, 0, 1, 1))
        {
            cellContentType = GetCell(0, 0).ContentType;
            return true;
        }
        if (CheckLine(0, _size - 1, 1, -1))
        {
            cellContentType = GetCell(0, _size - 1).ContentType;
            return true;
        }

        cellContentType = CellContentType.None;
        return false;
    }

    private bool CheckLine(int startRow, int startColumn, int rowStep, int columnStep)
    {
        var firstCell = _cells.FirstOrDefault(c => c.Row == startRow && c.Column == startColumn);
        if (firstCell.ContentType == CellContentType.Empty)
            return false;

        for (int i = 1; i < _size; i++)
        {
            int row = startRow + i * rowStep;
            int col = startColumn + i * columnStep;

            var cell = _cells.FirstOrDefault(c => c.Row == row && c.Column == col);
            if (cell.ContentType != firstCell.ContentType)
                return false;
        }

        return true; // Все 3 ячейки совпадают
    }

    private BoardCell GetCell(int row, int column)
    {
        var cell = _cells.FirstOrDefault(x => x.Row == row && x.Column == column);
        if (cell == null)
            throw new ArgumentException($"Нет такой ячейки. Строка: {row}, столбец: {column}");
        return cell;
    }
}

