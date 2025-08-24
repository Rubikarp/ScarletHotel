using UnityEngine;
using UnityEngine.EventSystems;

namespace WG.Common
{
    public class DragParentUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Canvas canvas;
        private RectTransform parentRect;

        private void Start()
        {
            canvas = GetComponentInParent<Canvas>();
            parentRect = transform.parent.GetComponent<RectTransform>();
        }

        public void OnBeginDrag(PointerEventData eventData) { }
        public void OnEndDrag(PointerEventData eventData) { }

        public void OnDrag(PointerEventData eventData)
        {
            parentRect.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }
}
