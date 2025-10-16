using UnityEngine;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;

[RequireComponent(typeof(RectTransform))]
public class ScrollViewController : UIBehaviour
{
    [SerializeField] private bool isVertical = true;
    [SerializeField] private bool isUsingStep = false;
    [OnValueChanged(nameof(OnScrollPositionChanged))]
    [SerializeField, Min(0), ShowIf("isUsingStep")] private int step = 0;
    [SerializeField, Min(1), ShowIf("isUsingStep")] private int stepQuantity = 2;
    [Space]
    [SerializeField] private RectTransform content;
    [SerializeField] private RectTransform viewport;
    [Space]
    [OnValueChanged(nameof(OnScrollPositionChanged))]
    [SerializeField, DisableIf("isUsingStep")] private float scrollPosition = 0f;

    protected override void Awake()
    {
        base.Awake();

        // Ensure the component has the necessary hierarchy
        if (viewport == null) viewport = GetComponent<RectTransform>();
        if (content == null)
        {
            if (viewport.childCount > 0)
            {
                content = viewport.GetChild(0).GetComponent<RectTransform>();
            }
            else Debug.LogError("ScrollViewController requires a child RectTransform as the content.");
        }
    }

    private void Update()
    {
        if (content == null || viewport == null) return;

        // Handle scrolling logic
        if (isUsingStep)
        {
            step = Mathf.Clamp(step, 0, stepQuantity - 1);
            float normalizedStep = (float)step / (stepQuantity - 1);
            scrollPosition = Mathf.Lerp(0, 1, normalizedStep);
        }

        ApplyScrollPosition();
    }

    private void ApplyScrollPosition()
    {
        if (content == null || viewport == null) return;

        float contentWidth = content.rect.width;
        float viewportWidth = viewport.rect.width;
        float maxScrollX = Mathf.Max(0, contentWidth - viewportWidth);

        float contentHeight = content.rect.height;
        float viewportHeight = viewport.rect.height;
        float maxScrollY = Mathf.Max(0, contentHeight - viewportHeight);

        if (isVertical)
            content.anchoredPosition = Vector2.up * scrollPosition * maxScrollY;
        else
            content.anchoredPosition = Vector2.right * scrollPosition * maxScrollX;
    }

    private void OnScrollPositionChanged()
    {
        if (isUsingStep) 
            ScrollToStep(step);
        else
            ApplyScrollPosition();
    }

    public void ScrollToStep(int targetStep)
    {
        if (!isUsingStep) return;

        step = Mathf.Clamp(step, 0, stepQuantity - 1);
        scrollPosition = (float)step / (stepQuantity - 1);
        ApplyScrollPosition();
    }

    public void ScrollToPosition(float normalizedPosition)
    {
        scrollPosition = normalizedPosition;
        ApplyScrollPosition();
    }
}

