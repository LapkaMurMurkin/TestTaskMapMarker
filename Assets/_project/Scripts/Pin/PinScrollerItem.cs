using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PinScrollerItem : UIElement, IPointerClickHandler
{
    public Pin Pin { get; private set; }
    [SerializeField]
    private RawImage _image;
    [SerializeField]
    private TextMeshProUGUI _name;
    [SerializeField]
    private Button _deleteButton;

    public void Initialize(Pin pin)
    {
        base.Initialize();

        Pin = pin;
        UpdateItem();

        _deleteButton.onClick.AddListener(Delete);
    }

    private void Update()
    {
        DoTiltAroundAnimation();
    }

    public void UpdateItem()
    {
        _image.texture = Pin.Info.Photo;
        _name.text = Pin.Info.PlaceName;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ServiceLocator.Get<PinInfoEditor>().OpenEditor(Pin);
    }

    public void Delete()
    {
        ServiceLocator.Get<PinInfoEditor>().DeletePin(Pin);
    }
}
