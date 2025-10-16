using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RoomScreenManager : Singleton<RoomScreenManager>
{
    [Header("Prefab")]
    [field: SerializeField] public BaseRoomScreenUI RoomScreenPrefab {  get; private set; }
    [field: SerializeField] public Transform RoomContainer { get; private set; }   

    [Header("Data")]
    public List<RoomLink> roomToRoomScreens = new List<RoomLink>();

    public void OpenRoomScreen(BaseRoom roomRef)
    {
        if (roomToRoomScreens.Select(x => x.room == roomRef).Count() < 1)
        {
            CreateScreen(roomRef);
        }

        BaseRoomScreenUI screen = roomToRoomScreens.Find(x => x.room == roomRef).roomUI;
        screen?.gameObject.SetActive(true);
    }
    public void CloseRoomScreen(BaseRoom roomRef)
    {
        BaseRoomScreenUI screen = roomToRoomScreens.Find(x => x.room == roomRef).roomUI;
        screen.gameObject.SetActive(false);
    }

    private void CreateScreen(BaseRoom roomRef)
    {
        BaseRoomScreenUI screen = Instantiate(RoomScreenPrefab, RoomContainer);
        screen.gameObject.name = roomRef.name + "_UI-Screen";
        screen.LoadRoom(roomRef);

        roomToRoomScreens.Add(new RoomLink(roomRef, screen));
        screen.transform.position = roomRef.transform.position;
    }
}
public struct RoomLink
{
    public BaseRoom room;
    public BaseRoomScreenUI roomUI;

    public RoomLink(BaseRoom room, BaseRoomScreenUI roomUI)
    {
        this.room = room;
        this.roomUI = roomUI;
    }
}