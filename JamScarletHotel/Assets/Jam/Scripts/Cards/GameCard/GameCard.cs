using AYellowpaper;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class GameCard : MonoBehaviour
{
    [SerializeField, RequireInterface(typeof(ICardData))]
    protected ScriptableObject currentData;
    private ScriptableObject previousData;
    public ICardData CardData => (ICardData)currentData;

    public GameTimer Timer;

    public void TryLoadData(ICardData newData)
    {
        currentData = (ScriptableObject)newData;
        if (IsValidCardData())
        {
            LoadData();
        }
        else
        {
            Debug.LogWarning("Invalid card data loaded on " + gameObject.name, this);
            currentData = null;
        }
    }
    protected abstract bool IsValidCardData();
    protected abstract void LoadData();
    protected abstract void OnTimerEnd();

    protected void OnValidate()
    {
        if (!IsValidCardData())
        {
            Debug.LogWarning("Invalid card data loaded on " + gameObject.name, this);
            currentData = null;
            return;
        }

        // Check if same scriptable object
        if (!object.ReferenceEquals(previousData, currentData))
        {
            previousData = currentData;
            LoadData();
        }
    }

    protected virtual void Start()
    {
        Timer.OnTimerEnd.AddListener(OnTimerEnd);
    }
}
