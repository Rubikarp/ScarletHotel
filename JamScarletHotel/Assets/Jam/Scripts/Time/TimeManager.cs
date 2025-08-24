using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public delegate void TimeTickEvent(float deltaTime);

public class TimeManager : Singleton<TimeManager>
{
    [Header("Settings")]
    [SerializeField, Expandable]
    private GameTimeSettings gameTimeSettings;
    public float SeasonDuration => gameTimeSettings.SeasonDuration;

    [field: Header("Time Info")]
    [field: SerializeField, ReadOnly] public int SeasonIndex { get; private set; } = 0;
    [field: SerializeField, ReadOnly, ProgressBar("SeasonDuration")] public float GameTime { get; private set; } = 0;

    [field: Header("Time Speed")]
    [field: SerializeField, Min(0)] public float GameTimeSpeed { get; private set; } = 1f;
    [field: SerializeField, ReadOnly] public bool IsPaused { get; private set; } = false;

    public TimeTickEvent OnTimeTick;

    public string SeaonNumber => (SeasonIndex + 1).ToString();
    public float DeltaSimulTime => IsPaused ? 0 : Time.deltaTime * GameTimeSpeed;
    public float SeasonProgress => GameTime / SeasonDuration;
    public bool IsFreeze => GameTimeSpeed <= 0;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        gameTimeSettings = GameTimeSettings.Instance;

        GameTime = 0;
        SeasonIndex = 0;
    }

    void FixedUpdate()
    {
        if (IsPaused) return;

        float delta = DeltaSimulTime;
        GameTime += delta;
        OnTimeTick?.Invoke(delta);
        if (GameTime > (SeasonDuration * SeasonIndex))
        {
            SeasonIndex++;
            GameTime %= SeasonDuration;

            Debug.Log($"Season n°{SeaonNumber} start");
        }
    }

    public void TooglePause()
    {
        IsPaused = !IsPaused;
    }
    public void SetPause(bool state)
    {
        IsPaused = state;
    }
    public void SetSpeed(float speed)
    {
        if (speed < 0)
        {
            Debug.LogError("Game time speed cannot be negative. Setting to 0.");
            speed = 0;
        }

        GameTimeSpeed = speed;
    }
}
