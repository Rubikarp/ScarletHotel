using Sirenix.OdinInspector;
using AYellowpaper;
using UnityEngine;
 

public class CardHandler : Singleton<CardHandler>
{
    [Header("Prefabs")]
    [SerializeField] private ObjectGameCard objectCardPrefab;
    [SerializeField] private ClientGameCard clientCardPrefab;
    [SerializeField] private EmployeeGameCard employeeCardPrefab;

    [field: Header("Component")]
    [field: SerializeField] public Transform CardHandlingContainer { get; private set; }
    [field: SerializeField] public SlotHolder Inventory { get; private set; }
    [field: SerializeField] public SlotHolder HotelEntry { get; private set; }

    [Header("Debug")]
    [SerializeField, RequireInterface(typeof(ICardData))] 
    private BaseCardData debugCard;
    public ICardData DebugCard => (ICardData)debugCard;

    [Button]
    public void DEBUG_InstatiateCard() => SpawnCard(DebugCard);

    public void SpawnCard(ICardData cardData, ICardSlot slot = null)
    {
        BaseGameCard card = null;
        switch (cardData.CardType)
        {
            case ECardType.Object:
                card = Instantiate(objectCardPrefab, CardHandlingContainer);
                break;
            case ECardType.Client:
                card = Instantiate(clientCardPrefab, CardHandlingContainer);
                break;
            case ECardType.Employee:
                card = Instantiate(employeeCardPrefab, CardHandlingContainer);
                break;
            default:
            case ECardType.Any:
            case ECardType.People:
                Debug.LogError($"Can't spawn Any or People specific card",this);
                return;
        }

        card.TryLoadData(cardData);

        if(slot != null)
        {
            slot.ReceivedCard(card);
        }
        else
        {
            Inventory.ReceivedCard(card);
        }
    }

    public void SpawnCardInHand(ICardData cardData) => SpawnCard(cardData, Inventory);
    public void SpawnCardAtHotelEntry(ICardData cardData) => SpawnCard(cardData, HotelEntry);
}
