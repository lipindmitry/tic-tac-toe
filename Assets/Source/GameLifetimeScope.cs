using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        //// 1. ����������� ��������
        //builder.Register<IPlayerService, PlayerService>(Lifetime.Singleton);
        //builder.Register<IAudioManager, AudioManager>(Lifetime.Scoped);

        //// 2. ����������� MonoBehaviour (���� ������ ��� �� �����)
        //builder.RegisterComponentInHierarchy<GameManager>();

        //// 3. ����������� ������� (��������, ������)
        //// builder.RegisterComponentInNewPrefab(playerPrefab, Lifetime.Scoped);
    }
}
