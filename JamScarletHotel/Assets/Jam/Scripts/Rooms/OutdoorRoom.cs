using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class OutdoorRoom : BaseRoom
{
    public override ERoomType RoomType() => ERoomType.Outdoor;

    [Required]
    public CardSlot PeopleSlot;

    public Button EjectButton;
    public Button FiredButton;

    private void Start()
    {
        if (PeopleSlot == null)
        {
            Debug.LogError("PeopleSlot is not assigned in Outdoor room.", this);
        }
        if (PeopleSlot.AcceptedType != ECardType.People || PeopleSlot.RequiredInfluences != 0)
        {
            PeopleSlot.SetRequirement(ECardType.People, EInfluence.None);
        }

        EjectButton?.gameObject.SetActive(false);
        FiredButton?.gameObject.SetActive(false);
    }
    private void Update()
    {
        EjectButton?.gameObject.SetActive(CanEjectClient());
        FiredButton?.gameObject.SetActive(CanFiredEmployee());
    }

    public bool CanEjectClient()
    {
        if (!PeopleSlot.IsOccupied) return false;

        // IsClient
        return (PeopleSlot.CurrentCard.CardData.CardType & ECardType.Client) != 0;
    }
    public bool CanFiredEmployee()
    {
        if (!PeopleSlot.IsOccupied) return false;

        // IsClient
        return (PeopleSlot.CurrentCard.CardData.CardType & ECardType.Employee) != 0;
    }

    public void EjectClient()
    {
        var client = PeopleSlot.CurrentCard as ClientGameCard;
        PeopleSlot.ReleaseCard();

        Destroy(client.gameObject);
    }
    public void FiredEmployee()
    {
        var employee = PeopleSlot.CurrentCard as EmployeeGameCard;
        PeopleSlot.ReleaseCard();

        employee.DestroyCard();

    }
}