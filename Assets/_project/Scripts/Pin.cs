using System.IO;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Pin : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Canvas _parentCanvas;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    public PinInfo Info { get; private set; }

    private bool _isPointerDown;
    private bool _isLongPress;

    private float _pressTime;
    private float _timer;

    public void Initialize(Vector2 coords)
    {
        _parentCanvas = GetComponentInParent<Canvas>();
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();

        _rectTransform.anchoredPosition = coords;

        Info = new PinInfo();
        Info.Coords = coords;

        _isPointerDown = false;
        _isLongPress = false;

        _pressTime = 0.4f;
        _timer = 0;

        DoBounceAnimationLoop();
        DoFaidInAnimation();
    }

    private void Update()
    {
        if (_isPointerDown)
        {
            _timer += Time.deltaTime;
            if (_timer > _pressTime)
            {
                _canvasGroup.alpha = 0.5f;
                _isLongPress = true;
            }
        }
        else
        {
            _timer = 0;
            _isLongPress = false;
        }
    }

    private void DoBounceAnimationLoop()
    {
        Vector3 origScale = _rectTransform.localScale;
        origScale.x = origScale.x * 1.15f;
        origScale.y = origScale.y * 0.85f;

        Vector3 upScale = origScale;
        origScale.x = origScale.x * 0.85f;
        origScale.y = origScale.y * 1.15f;

        transform.DOScale(upScale, 0.3f)
            .SetEase(Ease.OutFlash)
            .SetDelay(Random.Range(0, 0.6f))
            .SetLoops(-1, LoopType.Yoyo)
            .SetLink(gameObject);
    }

    private void DoFaidInAnimation()
    {
        float fadeTime = 0.2f;
        Vector3 origPosition = _rectTransform.anchoredPosition;
        _canvasGroup.alpha = 0f;
        _rectTransform.localPosition = origPosition + new Vector3(0, 100f, 0f);
        _rectTransform.DOAnchorPos(origPosition, fadeTime, false).SetEase(Ease.InFlash);
        _canvasGroup.DOFade(1, fadeTime);
    }

    public void UpdatePinInfo(PinInfo info)
    {
        Info.PlaceName = info.PlaceName;
        Info.Description = info.Description;
        Info.Photo = info.Photo;
        Info.Coords = info.Coords;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ServiceLocator.Get<PinTooltip>().Show(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isLongPress)
            _rectTransform.anchoredPosition += eventData.delta / _parentCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.alpha = 1f;
        Info.Coords = _rectTransform.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPointerDown = false;
    }
}
