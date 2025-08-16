using UniRx;

public class PlayersOptions
{
    public ReactiveProperty<string> PlayerName { get; } = new(GetRandomName());
    public ReactiveProperty<string> PlayerId { get; } = new();
    public ReactiveProperty<string> OpponentName { get; } = new();
    public ReactiveProperty<string> OpponentId { get; } = new();

    public static string GetRandomName()
    {
        int randomValue = (int)(UnityEngine.Random.value * 1000 + 1000);
        return $"Player_{randomValue}";
    }
}

