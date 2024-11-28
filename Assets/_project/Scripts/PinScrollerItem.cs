using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PinScrollerItem : MonoBehaviour, IPointerClickHandler
{
    public Pin Pin { get; private set; }
    [SerializeField]
    private RawImage _image;
    [SerializeField]
    private TextMeshProUGUI _name;
    [SerializeField]
    private Button _deleteButton;

    Vector3 direction = Vector3.up;
    Vector3 rotation = Vector3.zero;

    float maxTilt = 15f;
    float rotationTime = 3f;
    int clockwiseRotation;

    public void Initialize()
    {
        direction = Random.insideUnitCircle.normalized;
        clockwiseRotation = Random.Range(0, 2) * 2 - 1;
        _deleteButton.onClick.AddListener(Delete);
    }

    private void Update()
    {
        DoAnimation();
    }

    public void UpdateItem(Pin pin)
    {
        Pin = pin;
        _image.texture = Pin.Info.Photo;
        _name.text = Pin.Info.PlaceName;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ServiceLocator.Get<PinInfoEditor>().OpenEditor(Pin);
    }

    public void DoAnimation()
    {
        direction = Quaternion.Euler(0, 0, (360 * clockwiseRotation * Time.deltaTime) / rotationTime) * direction;
        rotation = new Vector3(direction.y, direction.x, 0) * maxTilt;
        transform.eulerAngles = rotation;
    }

    public void Delete()
    {
        ServiceLocator.Get<PinInfoEditor>().DeletePin(Pin);
    }
}
