using Sirenix.OdinInspector;
using UnityEngine;

public class CardHandler : Singleton<CardHandler>
{
    [field: Header("Component")]
    [field: SerializeField] public Transform CardHandlingContainer { get; private set; }
    [field: SerializeField] public SlotHolder Inventory { get; private set; }
    [field: SerializeField] public SlotHolder HotelEntry { get; private set; }

    [Header("Prefabs")]
    [SerializeField] private ObjectGameCard objectCardPrefab;
    [SerializeField] private ClientGameCard clientCardPrefab;
    [SerializeField] private EmployeeGameCard employeeCardPrefab;

    [Header("Debug")]
    [SerializeField] private BaseCardData debugEmployeeCard;
    [SerializeField] private BaseCardData debugClientCard;
    [SerializeField] private BaseCardData debugObjectCard;

    [Button] public void DEBUG_EmployeeCard() => SpawnCard(debugEmployeeCard as ICardData);
    [Button] public void DEBUG_ClientCard() => SpawnCard(debugClientCard as ICardData);
    [Button] public void DEBUG_ObjectCard() => SpawnCard(debugObjectCard as ICardData);

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
                Debug.LogError($"Can't spawn Any or People specific card", this);
                return;
        }

        card.TryLoadData(cardData);

        if (slot != null)
        {
            slot.ReceivedCard(card);
        }
        else
        {
            Inventory.ReceivedCard(card);
        }
    }

    public void SpawnCardInHand(ICardData cardData) => SpawnCard(cardData, Inventory);
}
