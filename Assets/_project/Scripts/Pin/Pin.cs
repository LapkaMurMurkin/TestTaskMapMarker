using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pin : UIElement, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public PinInfo Info { get; private set; }

    private bool _isPointerDown;
    private bool _isDrag;

    private float _longPressTime;
    private float _timer;

    public void Initialize(Vector2 coords)
    {
        base.Initialize();

        _rectTransform.anchoredPosition = coords;

        Info = new PinInfo();
        Info.Coords = coords;

        _isPointerDown = false;
        _isDrag = false;

        _longPressTime = 0.4f;
        _timer = 0;

        DoBounceAnimationLoop();
        DoFaidInAnimation();
    }

    private void Update()
    {
        if (_isPointerDown)
        {
            _timer += Time.deltaTime;
            if (_timer > _longPressTime)
            {
                AllowDrag();
            }
        }
        else
        {
            ResetDrag();
        }
    }

    public void UpdatePinInfo(PinInfo info)
    {
        Info.PlaceName = info.PlaceName;
        Info.Description = info.Description;
        Info.Photo = info.Photo;
        Info.Coords = info.Coords;
    }

    private void AllowDrag()
    {
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = 0.5f;
        _isDrag = true;
    }

    private void ResetDrag()
    {
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.alpha = 1f;
        _isDrag = false;
        _timer = 0;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPointerDown = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.dragging)
            return;

        ServiceLocator.Get<PinTooltip>().Show(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_isDrag == false)
        {
            eventData.pointerDrag = null;
            return;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _parentCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Info.Coords = _rectTransform.anchoredPosition;
        ResetDrag();
    }
}
