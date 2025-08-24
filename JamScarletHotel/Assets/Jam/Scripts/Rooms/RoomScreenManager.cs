using UnityEngine;
using VInspector;

public class RoomScreenManager : Singleton<RoomScreenManager>
{
    [Header("Prefab")]
    [field: SerializeField] public BaseRoomScreenUI RoomScreenPrefab {  get; private set; }
    [field: SerializeField] public Transform RoomContainer { get; private set; }   

    [Header("Data")]
    public SerializedDictionary<BaseRoom, BaseRoomScreenUI> roomToRoomScrens;

    public void OpenRoomScreen(BaseRoom roomRef)
    {
        if (!roomToRoomScrens.ContainsKey(roomRef))
        {
            CreateScreen(roomRef);
        }

        BaseRoomScreenUI screen = roomToRoomScrens[roomRef];
        screen.gameObject.SetActive(true);
    }
    public void CloseRoomScreen(BaseRoom roomRef)
    {
        BaseRoomScreenUI screen = roomToRoomScrens[roomRef];
        screen.gameObject.SetActive(false);
    }

    private void CreateScreen(BaseRoom roomRef)
    {
        BaseRoomScreenUI screen = Instantiate(RoomScreenPrefab, RoomContainer);
        screen.gameObject.name = roomRef.name + "_UiScreen";
        screen.LoadRoom(roomRef);
        roomToRoomScrens[roomRef] = screen;
    }
}
