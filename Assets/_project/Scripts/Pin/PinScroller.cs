using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinScroller : ScrollRect
{
    private GameObject _pinScrollerItemTemplate;
    private List<PinScrollerItem> _pinScrollerItems;

    public void Initialize(List<Pin> pins)
    {
        _pinScrollerItemTemplate = transform.GetChild(0).gameObject;
        _pinScrollerItems = new List<PinScrollerItem>();

        foreach (Pin pin in pins)
        {
            AddItem(pin);
        }
    }

    public PinScrollerItem AddItem(Pin pin)
    {
        GameObject newPinScrollerItemObject = Instantiate(_pinScrollerItemTemplate, content.transform);
        newPinScrollerItemObject.SetActive(true);
        PinScrollerItem newPinScrollerItemComponent = newPinScrollerItemObject.GetComponent<PinScrollerItem>();
        newPinScrollerItemComponent.Initialize(pin);
        _pinScrollerItems.Add(newPinScrollerItemComponent);
        return newPinScrollerItemComponent;
    }

    public void DeleteItem(Pin pin)
    {
        PinScrollerItem item = _pinScrollerItems.Find(item => item.Pin == pin);
        Destroy(item.gameObject);
    }

    public void UpdateItem(Pin pin)
    {
        PinScrollerItem item = _pinScrollerItems.Find(item => item.Pin == pin);
        item.UpdateItem();
    }
}
