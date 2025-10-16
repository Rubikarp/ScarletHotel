using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class GameTimer : MonoBehaviour
{
    [field: SerializeField] public float TimerDuration { get; private set; } = 10f;
    [field: SerializeField, ProgressBar(0, "TimerDuration")] public float RemainingTime { get; private set; } = 10f;
    [field: SerializeField] public bool IsActive { get; private set; } = false;

    public UnityEvent OnTimerEnd = new UnityEvent();

    private void Start()
    {
        TimeManager.OnTimeTick += UpdateTimer;
        RemainingTime = TimerDuration = 10f;
        IsActive = false;
    }
    private void OnDestroy()
    {
        OnTimerEnd.RemoveAllListeners();

        TimeManager.OnTimeTick -= UpdateTimer;
        RemainingTime = TimerDuration = 0f;
        IsActive = false;
    }

    [Button]
    public void StartTimer(float duration = 10f)
    {
        RemainingTime = TimerDuration = duration;
        IsActive = true;
    }
    public void UpdateTimer(float deltaTime)
    {
        if (!IsActive) return;

        RemainingTime -= deltaTime;
        if (RemainingTime <= 0)
        {
            RemainingTime = 0;
            IsActive = false;
            OnTimerEnd?.Invoke();
        }
    }

    [Button] public void StopTimer() => IsActive = false;
    [Button] public void ContinueTimer() => IsActive = false;
}
