using UnityEngine;
using WG.Common;
using UnityEngine.UI;

public class ScoreProgressBar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    Image mask;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame


    public void SetCurrentFill(float currentProgress)
    {
        Debug.Log(currentProgress);
        mask.fillAmount = currentProgress;
    }
}
