using System.IO;
using DG.Tweening;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PinInfoEditor : MonoBehaviour
{
    private Pin _pin;

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

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    public void Initialize()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();

        LoadPhotoButton.onClick.AddListener(LoadPhoto);
        SavePinButton.onClick.AddListener(SavePin);
        CancelButton.onClick.AddListener(CancelEditing);
        DeletePinButton.onClick.AddListener(() => DeletePin(_pin));
    }

    public void OpenEditor(Pin pin)
    {
        _pin = pin;

        PlaceName.text = pin.Info.PlaceName;
        Description.text = pin.Info.Description;
        Photo.texture = pin.Info.Photo;

        gameObject.SetActive(true);
        DoFaidInAnimation();
    }

    public void CloseEditor()
    {
        gameObject.SetActive(false);
    }

    private void DoFaidInAnimation()
    {
        float fadeTime = 0.2f;
        Vector3 origScale = _rectTransform.localScale;
        _canvasGroup.alpha = 0f;
        _rectTransform.localScale = new Vector3(0, 0, 0f);
        _rectTransform.DOScale(origScale, fadeTime);
        _canvasGroup.DOFade(1, fadeTime);
    }

    private void LoadPhoto()
    {
        string path = EditorUtility.OpenFilePanel("Select Image", "", "png");

        if (path.Length is not 0)
        {
            byte[] fileContent = File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(2, 2);
            ImageConversion.LoadImage(texture, fileContent);
            Photo.texture = texture;
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

        ServiceLocator.Get<PinScroller>().UpdateItem(_pin);

        CloseEditor();
    }

    private void CancelEditing()
    {
        CloseEditor();
    }

    public void DeletePin(Pin pin)
    {
        ServiceLocator.Get<PinScroller>().DeleteItem(pin);
        Destroy(pin.gameObject);
        CancelEditing();
    }
}
