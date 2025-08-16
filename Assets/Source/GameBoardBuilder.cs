using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;

public class GameBoardBuilder : MonoBehaviour
{
    public event Action<IBoardCell> MoveRequested;

    [SerializeField] private CellView _cellPrefab;
    [SerializeField] private Transform _cellsParrent;

    [Inject] private Game _game;
    [Inject] private PlayersOptions _playersOptions;
    [Inject] private IGameBoard _gameBoard;

    private List<CellView> _cellsViews = new();

    private void Start()
    {
        foreach (var cell in _gameBoard.Cells
                                       .OrderBy(x => x.Row)
                                       .ThenBy(x => x.Column))
        {
            var cellView = Instantiate(_cellPrefab, _cellsParrent);
            cellView.Initialize(cell);
            _cellsViews.Add(cellView);
            cellView.MarkRequested += OnMarkRequested;
        }
    }

    private void OnDestroy()
    {
        foreach (var cellView in _cellsViews)
            cellView.MarkRequested -= OnMarkRequested;

        _cellsViews.Clear();
    }

    private void OnMarkRequested(IBoardCell boardCell)
    {
        _game.MakeMove(boardCell.Row, boardCell.Column, _playersOptions.PlayerId.Value);
    }
}

