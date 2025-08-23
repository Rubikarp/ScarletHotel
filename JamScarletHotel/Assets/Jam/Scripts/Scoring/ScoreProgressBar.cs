using UnityEngine;
using UnityEngine.UI;

public class ScoreProgressBar : MonoBehaviour
{
    [SerializeField] private Image mask;

    void Start()
    {

    }

    public void SetCurrentFill(float currentProgress)
    {
        Debug.Log(currentProgress);
        mask.fillAmount = currentProgress;
    }
}
