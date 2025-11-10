using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(RectTransform))]
public class SlotElement : MonoBehaviour
{

}

[RequireComponent(typeof(RectTransform))]
public class SlotSpace : MonoBehaviour
{
    [field: SerializeField, ReadOnly]
    public SlotElement currentElement { get; private set; } = null;

}