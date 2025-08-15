using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        //// 1. Регистрация сервисов
        //builder.Register<IPlayerService, PlayerService>(Lifetime.Singleton);
        //builder.Register<IAudioManager, AudioManager>(Lifetime.Scoped);

        //// 2. Регистрация MonoBehaviour (если объект уже на сцене)
        //builder.RegisterComponentInHierarchy<GameManager>();

        //// 3. Регистрация префаба (например, игрока)
        //// builder.RegisterComponentInNewPrefab(playerPrefab, Lifetime.Scoped);
    }
}
