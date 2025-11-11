public class BedroomRoom : BaseRoom
{
    public override ERoomType RoomType() => ERoomType.Bedroom;
    public int price = 0;
    public float serviceDuration = 10f;

    public CardSlot PeopleSlot;

    private bool isProcessing = false;

    private void Start()
    {
        // Ensure the slot accepts people
        if (PeopleSlot != null && (PeopleSlot.Requirement.CardType != ECardType.People || PeopleSlot.Requirement.Influence != 0))
        {
            PeopleSlot.SetRequirement(ECardType.People, EInfluence.None);
        }
    }

    private void Update()
    {
        if (isProcessing || PeopleSlot == null) return;

        if (PeopleSlot.IsOccupied && (PeopleSlot.CurrentCard.CardData.CardType & ECardType.People) != 0)
        {
            BeginService();
        }
    }

    private void BeginService()
    {
        isProcessing = true;

        // Lock card interaction during service
        var cardManipulation = PeopleSlot.CurrentCard.GetComponent<CardManipulation>();
        if (cardManipulation != null) cardManipulation.enabled = false;

        // Start timer
        if (RoomTimer != null)
        {
            RoomTimer.OnTimerEnd.AddListener(OnServiceCompleted);
            RoomTimer.StartTimer(serviceDuration);
        }
        else
        {
            // Fallback: complete immediately if no timer is assigned
            OnServiceCompleted();
        }
    }

    private void OnServiceCompleted()
    {
        if (RoomTimer != null) RoomTimer.OnTimerEnd.RemoveListener(OnServiceCompleted);

        // Add money
        MoneyManager.Instance?.addMoneyAmmount(price);

        // Unlock card and free the slot
        if (PeopleSlot != null && PeopleSlot.CurrentCard != null)
        {
            var cardManipulation = PeopleSlot.CurrentCard.GetComponent<CardManipulation>();
            if (cardManipulation != null) cardManipulation.enabled = true;
            PeopleSlot.ReleaseCard();
        }

        isProcessing = false;
    }

    public void RentRoom()
    {

    }
    public void CleanRoom()
    {

    }
    public void UpgradeRoom()
    {

    }
}
