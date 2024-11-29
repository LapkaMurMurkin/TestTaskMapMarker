using System.IO;
using DG.Tweening;
using SFB;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PinInfoEditor : UIElement
{
    private Pin _pin;

    private Map _map;
    private PinScroller _pinScroller;

    [SerializeField]
    private TMP_InputField PlaceName;
    [SerializeField]
    private TMP_InputField Description;
    [SerializeField]
    private RawImage Photo;

    [SerializeField]
    private Button LoadPhotoButton;
    [SerializeField]
    private Button SavePinButton;
    [SerializeField]
    private Button CancelButton;
    [SerializeField]
    private Button DeletePinButton;

    public override void Initialize()
    {
        base.Initialize();

        LoadPhotoButton.onClick.AddListener(LoadPhoto);
        SavePinButton.onClick.AddListener(SavePin);
        CancelButton.onClick.AddListener(CancelEditing);
        DeletePinButton.onClick.AddListener(() => DeletePin(_pin));

        _map = ServiceLocator.Get<Map>();
        _pinScroller = ServiceLocator.Get<PinScroller>();

        _map.Clicked += CreatePin;
    }

    private void Destroy()
    {
        _map.Clicked -= CreatePin;
        Debug.Log("Destroy");
    }

    public void OpenEditor(Pin pin)
    {
        _pin = pin;

        PlaceName.text = pin.Info.PlaceName;
        Description.text = pin.Info.Description;
        Photo.texture = pin.Info.Photo;

        gameObject.SetActive(true);
        DoPopupAnimation();
    }

    public void CloseEditor()
    {
        gameObject.SetActive(false);
    }

    private void LoadPhoto()
    {
        ExtensionFilter[] extensions = new[] {
            new ExtensionFilter("Image Files", "png", "jpg", "jpeg" ),
        };
        string[] result = StandaloneFileBrowser.OpenFilePanel("Select Image", "", extensions, false);

        if (result.Length is not 0)
        {
            string path = result[0];

            if (path.Length is not 0)
            {
                byte[] fileContent = File.ReadAllBytes(path);
                Texture2D texture = new Texture2D(2, 2);
                ImageConversion.LoadImage(texture, fileContent);
                Photo.texture = texture;
            }
        }
    }

    private void SavePin()
    {
        PinInfo pinInfo = new PinInfo();
        pinInfo.PlaceName = PlaceName.text;
        pinInfo.Description = Description.text;
        pinInfo.Photo = Photo.texture as Texture2D;
        pinInfo.Coords = _pin.Info.Coords;

        _pin.UpdatePinInfo(pinInfo);
        _pinScroller.UpdateItem(_pin);
        
        CloseEditor();
    }

    private void CancelEditing()
    {
        CloseEditor();
    }

    public void CreatePin(Vector2 coords)
    {
        Pin pin = ServiceLocator.Get<Map>().CreatePin(coords);
        ServiceLocator.Get<PinScroller>().AddItem(pin);
    }

    public void DeletePin(Pin pin)
    {
        ServiceLocator.Get<Map>().DeletePin(pin);
        ServiceLocator.Get<PinScroller>().DeleteItem(pin);
        CancelEditing();
    }
}
