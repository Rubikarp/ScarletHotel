using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine;

public abstract class BaseRoom : MonoBehaviour
{
    private RoomScreenManager roomScreenManager;

    [Header("Info")]
    [field: SerializeField] public EInfluence RoomInfluence { get; protected set; } = EInfluence.None;
    [field: SerializeField] public ERoomQuality RoomQuality { get; protected set; } = ERoomQuality.Shabby;
    [field: SerializeField] public ERoomState RoomState { get; protected set; } = ERoomState.Clean;
    public abstract ERoomType RoomType();

    [field: SerializeField] public GameTimer RoomTimer { get; protected set; }
    [field: SerializeField] public GameObject RoomWindow { get; protected set; }

    protected virtual void Awake()
    {
        roomScreenManager = RoomScreenManager.Instance;
    }

    public void ToogleWindow() => roomScreenManager.OpenRoomScreen(this);
}

public enum ERoomType
{
    Bedroom = 0,
    Reception = 1,
    Trash = 2, // Throw away client
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
