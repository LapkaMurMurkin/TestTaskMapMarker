using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Map : UIElement, IPointerClickHandler
{
    [SerializeField]
    private GameObject _pinPrefab;

    public Action<Vector2> Clicked;

    public List<Pin> Initialize(List<PinInfo> pinInfos)
    {
        base.Initialize();  

        List<Pin> pins = new List<Pin>();

        foreach (PinInfo pinInfo in pinInfos)
        {
            Pin pin = CreatePin(pinInfo.Coords);
            pin.UpdatePinInfo(pinInfo);
            pins.Add(pin);
            //ServiceLocator.Get<PinScroller>().UpdateItem(pin);
        }

        return pins;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 coords;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, eventData.position, eventData.pressEventCamera, out coords);
        Clicked?.Invoke(coords);
        //CreatePin(coords);
    }

    public Pin CreatePin(Vector2 coords)
    {
        GameObject pinObject = Instantiate(_pinPrefab, new Vector3(), new Quaternion(), transform);
        Pin pinComponent = pinObject.GetComponent<Pin>();
        pinComponent.Initialize(coords);

        return pinComponent;
    }

    public void DeletePin(Pin pin)
    {
        Destroy(pin.gameObject);
    }
}
