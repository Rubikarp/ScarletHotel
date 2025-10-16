using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public delegate void TimeTickEvent(float deltaTime);
public delegate void WeekChangeEvent(int CurrentWeek);
public delegate void SeasonChangeEvent(int CurrentSeason);

public class TimeManager : Singleton<TimeManager>
{
    [Header("Settings")]
    [SerializeField] private GameTimeSettings gameTimeSettings;

    public int SeasonDuration => gameTimeSettings.SeasonDuration;
    public int WeekDuration => gameTimeSettings.WeekDuration;
    public float DayDuration => gameTimeSettings.DayDuration;

    [field: Header("Time Info")]
    [field: SerializeField, ReadOnly] public int SeasonIndex { get; private set; } = 0;
    [field: SerializeField, ReadOnly] public int WeekIndex { get; private set; } = 0;
    [field: SerializeField, ReadOnly] public int DayIndex { get; private set; } = 0;
    [field: SerializeField, ReadOnly] public float GameTime { get; private set; } = 0;

    [field: Header("Time Speed")]
    [field: SerializeField, Min(0)] public float GameTimeSpeed { get; private set; } = 1f;
    [field: SerializeField, ReadOnly] public bool IsPaused { get; private set; } = false;

    [Header("Event")]
    public UnityEvent OnWeekChange;
    public UnityEvent OnSeasonChange;
    public static TimeTickEvent OnTimeTick;
    public static WeekChangeEvent OnNewWeek;
    public static SeasonChangeEvent OnNewSeason;

    public float DeltaSimulTime => IsPaused ? 0 : Time.deltaTime * GameTimeSpeed;
    public float SeasonProgress => GameTime / SeasonDuration;
    public string SeasonNumber => (SeasonIndex + 1).ToString();
    public string WeekNumber => (WeekIndex + 1).ToString();
    public string DayNumber => (DayIndex + 1).ToString();

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        GameTime = 0;
        DayIndex = 0;
        WeekIndex = 0;
        SeasonIndex = 0;
    }

    void Update()
    {
        if (IsPaused) return;

        float delta = DeltaSimulTime;
        GameTime += delta;
        OnTimeTick?.Invoke(delta);
        if (GameTime > DayDuration)
        {
            OnNextDay();
        }
    }

    private void OnNextDay()
    {
        DayIndex++;
        GameTime %= DayDuration;
        Debug.Log($"Day n°{DayNumber} start");

        if (DayIndex >= WeekDuration + (WeekIndex * WeekDuration))
        {
            OnNextWeek();
        }
    }
    private void OnNextWeek()
    {
        WeekIndex++;
        GameTime %= DayDuration;
        Debug.Log($"Week n°{WeekNumber} start");

        OnNewWeek?.Invoke(WeekIndex);
        OnWeekChange?.Invoke();

        if (WeekIndex >= SeasonDuration + (SeasonIndex * SeasonDuration))
        {
            OnNextSeason();
        }
    }
    private void OnNextSeason()
    {
        SeasonIndex++;
        GameTime %= DayDuration;
        Debug.Log($"Season n°{WeekNumber} start");

        OnNewSeason?.Invoke(WeekIndex);
        OnSeasonChange?.Invoke();
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
