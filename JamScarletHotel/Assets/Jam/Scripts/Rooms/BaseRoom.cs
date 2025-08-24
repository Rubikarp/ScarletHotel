using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseRoom : MonoBehaviour
{
    [Header("Info")]
    [field: SerializeField] public EInfluence RoomInfluence { get; protected set; } = 0;
    [field: SerializeField] public ERoomQuality RoomQuality { get; protected set; } = ERoomQuality.Shabby;
    [field: SerializeField] public ERoomState RoomState { get; protected set; } = ERoomState.Clean;
    public abstract ERoomType RoomType();

    [field: SerializeField] public GameTimer RoomTimer { get; protected set; }
    [field: SerializeField] public GameObject RoomWindow { get; protected set; }

    public void ToogleWindow()
    {
        if (RoomWindow != null) RoomWindow.SetActive(!RoomWindow.activeSelf);
    }
}

public enum ERoomType
{
    Bedroom = 0,
    Reception = 1,
    Outdoor = 2, // Throw away client
    Kitchen = 3,
    DanceHall = 4,
}
public enum ERoomQuality
{
    Shabby = 0,
    Decent = 1,
    Luxurious = 2,
}
public enum ERoomState
{
    Clean = 0,
    Dirty = 1,
    Messy = 2,
    Bloodied = 3,
}
