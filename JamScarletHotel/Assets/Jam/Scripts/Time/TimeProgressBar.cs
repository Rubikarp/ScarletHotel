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
        SetCurrentFill((timeManager.GameTime / timeManager.SeasonDuration));
    }

    void SetCurrentFill(float currentProgress)
    {
        Debug.Log(currentProgress);
        mask.fillAmount = currentProgress;
    }
}
