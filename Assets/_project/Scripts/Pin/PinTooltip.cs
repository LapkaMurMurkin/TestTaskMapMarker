using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PinTooltip : UIElement, IPointerExitHandler
{
    private Pin _pin;

    [SerializeField]
    private TextMeshProUGUI _placeName;
    [SerializeField]
    private TextMeshProUGUI _description;
    [SerializeField]
    private Button ExpandButton;
    [SerializeField]
    private Button EditButton;

    public override void Initialize()
    {
        base.Initialize();

        EditButton.onClick.AddListener(Edit);
        ExpandButton.onClick.AddListener(Expand);
    }

    public void Show(Pin pin)
    {
        _pin = pin;

        transform.position = pin.transform.position;

        _placeName.text = pin.Info.PlaceName;

        if (pin.Info.Description.Length > 40)
            _description.text = pin.Info.Description.Substring(0, 40) + "...";
        else
            _description.text = pin.Info.Description;

        gameObject.SetActive(true);
        DoPopupAnimation();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Hide();
    }

    private void Edit()
    {
        Hide();
        ServiceLocator.Get<PinInfoEditor>().OpenEditor(_pin);
    }

    private void Expand()
    {
        Hide();
        ServiceLocator.Get<PinInfoExpand>().OpenExpandInfo(_pin);
    }
}
