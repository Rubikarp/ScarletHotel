using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using NaughtyAttributes;

namespace SevenLies.core
{
    public class HyperLink : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private string link = "https://youtu.be/dQw4w9WgXcQ";

        [Button]
        public void OpenLink()
        {
            Application.OpenURL(link);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OpenLink();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OpenLink();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
        }
    }
}
