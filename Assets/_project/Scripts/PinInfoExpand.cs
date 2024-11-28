using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PinInfoExpand : MonoBehaviour
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

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    public void Initialize()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();

        CloseButton.onClick.AddListener(CloseExpandInfo);
    }

    public void OpenExpandInfo(Pin pin)
    {
        _pin = pin;

        PlaceName.text = pin.Info.PlaceName;
        Description.text = pin.Info.Description;
        Photo.texture = pin.Info.Photo;

        gameObject.SetActive(true);
        DoFaidInAnimation();
    }

    public void CloseExpandInfo()
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
}
