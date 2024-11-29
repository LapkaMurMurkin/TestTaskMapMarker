using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PinInfoExpand : UIElement
{
    private Pin _pin;

    [SerializeField]
    private TextMeshProUGUI PlaceName;
    [SerializeField]
    private TextMeshProUGUI Description;
    [SerializeField]
    private RawImage Photo;
    [SerializeField]
    private Button CloseButton;

    public override void Initialize()
    {
        base.Initialize();
        
        CloseButton.onClick.AddListener(CloseExpandInfo);
    }

    public void OpenExpandInfo(Pin pin)
    {
        _pin = pin;

        PlaceName.text = pin.Info.PlaceName;
        Description.text = pin.Info.Description;
        Photo.texture = pin.Info.Photo;

        gameObject.SetActive(true);
        DoPopupAnimation();
    }

    public void CloseExpandInfo()
    {
        gameObject.SetActive(false);
    }
}
