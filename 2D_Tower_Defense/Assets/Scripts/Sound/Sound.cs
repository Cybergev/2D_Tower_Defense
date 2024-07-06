public enum Sound
{
    None = 0,
    BGM1 = 1,
    BGM2 = 2,
    BGM3 = 3,
    Arrow = 4,
    ArrowHit = 5,
    EnemyDie = 6,
    EnemyWin = 7,
    PlayerWin = 8,
    PlayerLose = 9,
    Magic = 10,
    MagicHit = 11,
}
public static class SoundExtensios
{
    public static void Play(this Sound sound)
    {
        SoundController.Instance.Play(sound);
    }
}
