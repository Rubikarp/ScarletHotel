using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;


public class ButtonManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button fastButton;

    [Header("Visual Parameters")]
    [SerializeField] private Color inactiveColor;
    [SerializeField] private Color baseColor;
    [SerializeField] private float activeSize;

    void Start()
    {
        UpdateButtonVisuals();
    }

    public void UpdateButtonVisuals()
    {
        TimeManager timeManager = TimeManager.Instance;

        if (timeManager != null)
        {
            ChangeButtonVisualParam(pauseButton, inactiveColor, new Vector3(1, 1, 1));
            ChangeButtonVisualParam(playButton, inactiveColor, new Vector3(1, 1, 1));
            ChangeButtonVisualParam(fastButton, inactiveColor, new Vector3(1, 1, 1));

            if (timeManager.IsPaused)
            {
                ChangeButtonVisualParam(pauseButton, baseColor, new Vector3(activeSize, activeSize, activeSize));
            }
            else if ( timeManager.GameTimeSpeed == 1)
            {
                ChangeButtonVisualParam(playButton, baseColor, new Vector3(activeSize, activeSize, activeSize));
            }
            else if ( timeManager.GameTimeSpeed >1)
            {
                ChangeButtonVisualParam(fastButton, baseColor, new Vector3(activeSize, activeSize, activeSize));
            }
        }
        void ChangeButtonVisualParam(Button button, Color newColor, Vector3 newSize)
        {
            ColorBlock colorBlock =  button.colors;
            colorBlock.normalColor = newColor;
            button.colors = colorBlock;
            //print(newSize);
            button.transform.localScale = newSize;
        }
    }
}
