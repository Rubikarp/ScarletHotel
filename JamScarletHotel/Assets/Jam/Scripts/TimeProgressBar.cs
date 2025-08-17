using UnityEngine;
using UnityEngine.UI;
using WG.Common;

public class TimeProgressBar : MonoBehaviour
{

    [SerializeField]
    Image mask;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TimeManager timeManager = TimeManager.Instance;
        SetCurrentFill((timeManager.gameTimer - timeManager.seasonDuration * (timeManager.seasonNumber-1)) / timeManager.seasonDuration);
    }
    void SetCurrentFill(float currentProgress)
    {
        Debug.Log(currentProgress);
        mask.fillAmount = currentProgress;
    }


}
