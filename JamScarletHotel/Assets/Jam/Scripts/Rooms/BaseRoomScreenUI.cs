using NaughtyAttributes;
using System;
using TMPro;
using UnityEngine;

public class BaseRoomScreenUI : MonoBehaviour
{
    [field: SerializeField, ReadOnly] public BaseRoom BaseRoom { get; private set; }

    [field: SerializeField, ReadOnly] public TextMeshProUGUI Title { get; private set; }
    [field: SerializeField, ReadOnly] public TextMeshProUGUI Description { get; private set; }

    public void LoadRoom(BaseRoom baseRoom)
    {

    }
}
