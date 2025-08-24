public class ReceptionRoom : BaseRoom
{
    public override ERoomType RoomType() => ERoomType.Reception;

    //public CardSlotHolder WaitingClients;
    public CardSlot PeopleSlot;

    public void ReceiveClient()
    {

    }
}
