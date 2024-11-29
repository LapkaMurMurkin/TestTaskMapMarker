using DG.Tweening;
using UnityEngine;

public class UIElement : MonoBehaviour
{
    protected Canvas _parentCanvas;
    protected CanvasGroup _canvasGroup;
    protected RectTransform _rectTransform;

    //Animation
    Vector3 direction = Vector3.up;
    Vector3 rotation = Vector3.zero;

    float maxTilt = 15f;
    float rotationTime = 3f;
    int clockwiseRotation;

    public virtual void Initialize()
    {
        _parentCanvas = GetComponentInParent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _rectTransform = GetComponent<RectTransform>();

        direction = Random.insideUnitCircle.normalized;
        clockwiseRotation = Random.Range(0, 2) * 2 - 1;
    }

    public void DoTiltAroundAnimation()
    {
        direction = Quaternion.Euler(0, 0, (360 * clockwiseRotation * Time.deltaTime) / rotationTime) * direction;
        rotation = new Vector3(direction.y, direction.x, 0) * maxTilt;
        transform.eulerAngles = rotation;
    }

    protected virtual void DoFaidInAnimation()
    {
        float fadeTime = 0.2f;
        Vector3 origPosition = _rectTransform.anchoredPosition;
        _canvasGroup.alpha = 0f;
        _rectTransform.localPosition = origPosition + new Vector3(0, 100f, 0f);
        _rectTransform.DOAnchorPos(origPosition, fadeTime, false).SetEase(Ease.InFlash);
        _canvasGroup.DOFade(1, fadeTime);
    }

    protected virtual void DoPopupAnimation()
    {
        float fadeTime = 0.2f;
        Vector3 origScale = _rectTransform.localScale;
        _canvasGroup.alpha = 0f;
        _rectTransform.localScale = new Vector3(0, 0, 0f);
        _rectTransform.DOScale(origScale, fadeTime);
        _canvasGroup.DOFade(1, fadeTime);
    }

    protected virtual void DoBounceAnimationLoop()
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
}
