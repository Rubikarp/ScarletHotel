using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameTimer))]
public class GameTimerUI : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private GameTimer timer;

    [Header("UI")]
    [SerializeField] private Image circularPie;
    [SerializeField] private Image lockImage;
    private void Start()
    {
        timer = GetComponent<GameTimer>();
    }

    private void Update()
    {
        if (lockImage != null)
        {
            lockImage.gameObject.SetActive(timer.IsActive);
        }
        if (circularPie != null)
        {
            circularPie.fillAmount = timer.RemainingTime / timer.TimerDuration;
        }
    }
}
