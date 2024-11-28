using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PinTooltip : MonoBehaviour, IPointerExitHandler
{
    private Pin _pin;

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    [SerializeField]
    private TextMeshProUGUI _placeName;
    [SerializeField]
    private TextMeshProUGUI _description;
    [SerializeField]
    private Button ExpandButton;
    [SerializeField]
    private Button EditButton;

    public void Initialize()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();

        EditButton.onClick.AddListener(Edit);
        ExpandButton.onClick.AddListener(Expand);
    }

    public void Show(Pin pin)
    {
        _pin = pin;

        //Vector2 position = Camera.main.ScreenToWorldPoint(pin.transform.position);
        transform.position = pin.transform.position;

        _placeName.text = pin.Info.PlaceName;

        if (pin.Info.Description.Length > 40)
            _description.text = pin.Info.Description.Substring(0, 40) + "...";
        else
            _description.text = pin.Info.Description;

        gameObject.SetActive(true);
        DoFaidInAnimation();
    }

    public void Hide()
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
