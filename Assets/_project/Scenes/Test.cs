using UnityEngine;

public class Test : MonoBehaviour
{
    Vector3 direction = Vector3.up;
    Vector3 rotation = Vector3.zero;

    float maxTilt = 15f;
    float rotationTime = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        direction = Quaternion.Euler(0, 0, (360 * Time.deltaTime) / rotationTime) * direction;
        rotation = new Vector3(direction.y, direction.x, 0) * maxTilt;
        transform.eulerAngles = rotation;
        //Debug.Log(rotation);
    }
}
