using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.Events;

public class SimpleButton : Selectable, IPointerClickHandler, ISubmitHandler
{
    [Header("Button Elements")]
    [SerializeField, Required] private MaskableGraphic[] coloredFont;
    [SerializeField, Required] private MaskableGraphic[] coloredBackground;
    [Space]
    [SerializeField] private Color fontColor = Color.black;
    [SerializeField] private Color backgroundColor = Color.white;

    public UnityEvent onClick;

    protected override void Awake()
    {
        base.Awake();
    }
    protected void OnValidate() 
    {
        targetGraphic = null;
        transition = Selectable.Transition.None;

        foreach (var txt in coloredFont) txt.color = fontColor;
        foreach (var img in coloredBackground) img.color = backgroundColor;
    }

    private void Press()
    {
        if (!IsActive() || !IsInteractable()) return;

        UISystemProfilerApi.AddMarker("Button.onClick", this);
        onClick?.Invoke();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        Press();
    }
    public void OnSubmit(BaseEventData eventData) => Press();

    public void UpdateAppearance(Color imgColor, Color textColor)
    {
        this.fontColor = textColor;
        this.backgroundColor = imgColor;

        foreach (var txt in coloredFont) txt.color = this.fontColor;
        foreach (var img in coloredBackground) img.color = backgroundColor;
    }
    public Tween DoUpdateAppearance(Color imgColor, Color textColor, float duration)
    {
        this.fontColor = textColor;
        this.backgroundColor = imgColor;

        var seq = DOTween.Sequence();
        foreach (var txt in coloredFont) seq.Join(txt.DOColor(this.fontColor, duration));
        foreach (var img in coloredBackground) seq.Join(img.DOColor(backgroundColor, duration));
        return seq;
    }
}