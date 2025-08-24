using UnityEngine;
using UnityEngine.UI;

public class TimeProgressBar : MonoBehaviour
{
    [SerializeField] private Image mask;
    private TimeManager timeManager;

    void Start()
    {
        timeManager = TimeManager.Instance;
    }

    void FixedUpdate()
    {
        SetCurrentFill(timeManager.SeasonProgress);
    }

    void SetCurrentFill(float currentProgress)
    {
        mask.fillAmount = currentProgress;
    }
}
