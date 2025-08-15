using System;
using System.Collections.Generic;

public interface IGameBoard
{
    IEnumerable<IBoardCell> Cells { get; }
    IObservable<IBoardCell> LastMove { get; }
}