

using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using WG.Common;


public class ButtonManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("Buttons")]
    [SerializeField]
    Button playButton;
    [SerializeField]
    Button pauseButton;
    [SerializeField]
    Button fastButton;

    [Header("Visual Parameters")]
    [SerializeField]
    Color inactiveColor;
    [SerializeField]
    Color baseColor;
    [SerializeField]
    float activeSize;


    void Start()
    {
        UpdateButtonVisuals();
    }

    // Update is called once per frame
    public void UpdateButtonVisuals()
    {
        TimeManager timeManager = TimeManager.Instance;

        if (timeManager != null)
        {
            ChangeButtonVisualParam(pauseButton, inactiveColor, new Vector3(1, 1, 1));
            ChangeButtonVisualParam(playButton, inactiveColor, new Vector3(1, 1, 1));
            ChangeButtonVisualParam(fastButton, inactiveColor, new Vector3(1, 1, 1));

            if (timeManager.isPaused)
            {
                ChangeButtonVisualParam(pauseButton, baseColor, new Vector3(activeSize, activeSize, activeSize));
            }
            else if (!timeManager.isPaused && timeManager.gameTimeSpeed == 1)
            {
                ChangeButtonVisualParam(playButton, baseColor, new Vector3(activeSize, activeSize, activeSize));
            }
            else if (timeManager.isPaused && timeManager.gameTimeSpeed < 1)
            {
                ChangeButtonVisualParam(fastButton, baseColor, new Vector3(activeSize, activeSize, activeSize));
            }
        }
        void ChangeButtonVisualParam(Button button, Color newColor, Vector3 newSize)
        {
            ColorBlock colorBlock =  button.colors;
            colorBlock.normalColor = newColor;
            button.colors = colorBlock;
            button.transform.localScale = newSize;
        }
    }
}
