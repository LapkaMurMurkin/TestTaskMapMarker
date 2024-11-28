using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Map : MonoBehaviour, IPointerClickHandler
{
    private RectTransform _rectTransform;

    [SerializeField]
    private GameObject _pinPrefab;

    public void Initialize(List<PinInfo> pinInfos)
    {
        _rectTransform = GetComponent<RectTransform>();

        foreach (PinInfo pinInfo in pinInfos)
        {
            Pin pin = CreatePin(pinInfo.Coords);
            pin.UpdatePinInfo(pinInfo);
            ServiceLocator.Get<PinScroller>().UpdateItem(pin);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 coords;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, eventData.position, eventData.pressEventCamera, out coords);
        CreatePin(coords);
    }

    public Pin CreatePin(Vector2 coords)
    {
        GameObject pinObject = Instantiate(_pinPrefab, new Vector3(), new Quaternion(), transform);
        Pin pinComponent = pinObject.GetComponent<Pin>();
        pinComponent.Initialize(coords);

        ServiceLocator.Get<PinScroller>().AddItem(pinComponent);

        return pinComponent;
    }
}
