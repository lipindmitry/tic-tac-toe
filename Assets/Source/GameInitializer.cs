using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameInitializer : LifetimeScope
{
    [SerializeField] private GameBoardSettings _gameBoardSettings;
    [SerializeField] private UIMenu _uiMenu;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_gameBoardSettings);

        var gameBoard = new GameBoard(_gameBoardSettings);
        builder.RegisterInstance<IGameBoard>(gameBoard);

        builder.RegisterInstance(new Game(gameBoard));
        builder.Register<PlayersOptions>(Lifetime.Singleton);

        builder.RegisterInstance(_uiMenu);
    }
}
