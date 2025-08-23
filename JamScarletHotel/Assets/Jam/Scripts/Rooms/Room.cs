using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Info")]
    [field: SerializeField] public ERoomType RoomType { get; private set; } = ERoomType.Bedroom;
    [field: SerializeField] public EInfluence RoomInfluence { get; private set; } = 0;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

public enum ERoomType
{
    Bedroom,
}
public enum ERoomLevel
{
    Basic,
    Advanced,
    Master,
}