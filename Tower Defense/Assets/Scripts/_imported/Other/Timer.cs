public class Timer
{
    public float CurrentTime { get; private set; }
    public bool IsFinished => CurrentTime <= 0;
    public Timer(float startTime)
    {
        StartTimer(startTime);
    }
    public void StartTimer(float startTime)
    {
        CurrentTime = startTime;
    }
    public void AddTime(float time)
    {
        if (CurrentTime < 0) 
            return;
        CurrentTime += time;
    }
    public void RemoveTime(float deltaTime)
    {
        if (CurrentTime < 0) 
            return;
        CurrentTime -= deltaTime;
    }
}
