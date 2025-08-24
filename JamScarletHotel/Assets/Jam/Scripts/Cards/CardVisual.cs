using UnityEngine;
using UnityEngine.UI;
using AYellowpaper;
using TMPro;

//———————————No Graph ?———————————
//⠀⣞⢽⢪⢣⢣⢣⢫⡺⡵⣝⡮⣗⢷⢽⢽⢽⣮⡷⡽⣜⣜⢮⢺⣜⢷⢽⢝⡽⣝
//⠸⡸⠜⠕⠕⠁⢁⢇⢏⢽⢺⣪⡳⡝⣎⣏⢯⢞⡿⣟⣷⣳⢯⡷⣽⢽⢯⣳⣫⠇
//⠀⠀⢀⢀⢄⢬⢪⡪⡎⣆⡈⠚⠜⠕⠇⠗⠝⢕⢯⢫⣞⣯⣿⣻⡽⣏⢗⣗⠏⠀
//⠀⠪⡪⡪⣪⢪⢺⢸⢢⢓⢆⢤⢀⠀⠀⠀⠀⠈⢊⢞⡾⣿⡯⣏⢮⠷⠁⠀⠀
//⠀⠀⠀⠈⠊⠆⡃⠕⢕⢇⢇⢇⢇⢇⢏⢎⢎⢆⢄⠀⢑⣽⣿⢝⠲⠉⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⡿⠂⠠⠀⡇⢇⠕⢈⣀⠀⠁⠡⠣⡣⡫⣂⣿⠯⢪⠰⠂⠀⠀⠀⠀
//⠀⠀⠀⠀⡦⡙⡂⢀⢤⢣⠣⡈⣾⡃⠠⠄⠀⡄⢱⣌⣶⢏⢊⠂⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⢝⡲⣜⡮⡏⢎⢌⢂⠙⠢⠐⢀⢘⢵⣽⣿⡿⠁⠁⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠨⣺⡺⡕⡕⡱⡑⡆⡕⡅⡕⡜⡼⢽⡻⠏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⣼⣳⣫⣾⣵⣗⡵⡱⡡⢣⢑⢕⢜⢕⡝⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⣴⣿⣾⣿⣿⣿⡿⡽⡑⢌⠪⡢⡣⣣⡟⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⡟⡾⣿⢿⢿⢵⣽⣾⣼⣘⢸⢸⣞⡟⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠁⠇⠡⠩⡫⢿⣝⡻⡮⣒⢽⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//—————————————————————————————
public class CardVisual : MonoBehaviour
{
    private RectTransform rect;

    [Header("CardManipulation Info")]
    [SerializeField]
    private BaseGameCard card;
    private RectTransform cardRect;

    [SerializeField, RequireInterface(typeof(ICardData))]
    private BaseCardData cardData;
    public ICardData CardData
    {
        get => (ICardData)cardData;
        set
        {
            cardData = (BaseCardData)value;
            UpdateVisuals();
        }
    }

    [Header("Motion Settings")]
    [Tooltip("How quickly it accelerates toward the target.")] 
    public float stiffness = 200f;
    [Tooltip("How much resistance to motion (prevents infinite bouncing).")] 
    public float damping = 20f;
    [Tooltip("The offset from the card's position to the visual's position.")] 
    public Vector3 offset = Vector3.zero;
    [Range(-0.1f, 0.1f)]
    [Tooltip("How sensitive the rotation is to the card's movement.")]
    public float rotationSensibility = .1f;
    private Vector3 velocity;

    [Header("Visuals")]
    [SerializeField] private Image artwork;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    private void Start()
    {
        LinkToCard(GetComponentInParent<BaseGameCard>());
    }

    private void Update()
    {
        if (card == null) return;

        if(transform.parent != card.transform.parent)
        {
            transform.SetParent(cardRect.parent, true);
        }

        FollowCard();
        rect.sizeDelta = cardRect.sizeDelta;
        rect.localScale = Vector3.Lerp(rect.localScale, Vector3.one, Time.deltaTime);
    }

    private void FollowCard()
    {
        Vector3 targetPos = cardRect.position + offset;
        Vector3 displacement = rect.position - targetPos;

        Vector3 springForce = -displacement * stiffness;
        Vector3 dampingForce = -velocity * damping;

        Vector3 acceleration = springForce + dampingForce;

        velocity += acceleration * Time.deltaTime;
        rect.position += velocity * Time.deltaTime;

        //Rot form velocity
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward - (velocity * rotationSensibility), Vector3.up);
        rect.rotation = targetRotation;
    }

    private void UpdateVisuals()
    {
        artwork.sprite = CardData?.Artwork;

        titleText.text = CardData?.Title ?? "Lorem Ipsum";
        descriptionText.text = CardData?.Description ?? "No Description";
    }

    public void OnValidate()
    {
        if (card != null) LinkToCard(card);
    }

    public void LinkToCard(BaseGameCard nexCard)
    {
        card = nexCard;
        if (card != null)
        {
            cardRect = card.GetComponent<RectTransform>();
            CardData = card.CardData;
            card.Visual = this;
        }
    }
}
