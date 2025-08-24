using UnityEngine;
using UnityEngine.UI;

public class TimeProgressBar : MonoBehaviour
{
    [SerializeField] private Image mask;
    private TimeManager timeManager;

    enum ETimeType
    {
        day,
        week,
        season
    }
    [SerializeField] 
    ETimeType timeType;

    void Start()
    {
        timeManager = TimeManager.Instance;
    }

    void FixedUpdate()
    {
        if (timeType == ETimeType.day)
        {
            SetCurrentFill((((timeManager.DayIndex) * timeManager.DayDuration +timeManager.GameTime) / timeManager.DayDuration));
        }
        if (timeType == ETimeType.week)
        {
            SetCurrentFill((((timeManager.DayIndex) * timeManager.DayDuration + timeManager.GameTime) / (timeManager.WeekDuration * timeManager.DayDuration)));
        }
        if (timeType == ETimeType.season)
        {
            SetCurrentFill((((timeManager.DayIndex) * timeManager.DayDuration + timeManager.GameTime) / (timeManager.SeasonDuration * timeManager.DayDuration)));
        }
    }

    void SetCurrentFill(float currentProgress)
    {
        //print(currentProgress);
        mask.fillAmount = currentProgress;
    }
}
