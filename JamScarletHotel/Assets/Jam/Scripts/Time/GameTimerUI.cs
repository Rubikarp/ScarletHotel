using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameTimerUI : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private GameTimer timer;

    [Header("UI")]
    [SerializeField] private Image circularPie;
    [SerializeField] private Image lockImage;
    [SerializeField] private TextMeshProUGUI TimeArea;

    private void Start()
    {
        if (timer == null) timer = GetComponent<GameTimer>();
    }

    private void Update()
    {
        if (timer == null)
        {
            Debug.LogWarning($"GameTimer is not set on {gameObject.name}", this);
            return;
        }

        lockImage?.gameObject.SetActive(timer.IsActive);

        if (TimeArea != null)
        {
            TimeArea.text = timer.RemainingTime.ToString("F0");
        }
        if (circularPie != null)
        {
            circularPie.fillAmount = timer.RemainingTime / timer.TimerDuration;
        }
    }
}
