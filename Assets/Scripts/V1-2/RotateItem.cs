using UnityEngine;

public class RotateItem : MonoBehaviour
{
    public float rotationSpeed = 100f; // degrees per second

    public bool rotateX = false;
    public bool rotateY = true;
    public bool rotateZ = false;

    void Update()
    {
        float x = rotateX ? rotationSpeed * Time.deltaTime : 0f;
        float y = rotateY ? rotationSpeed * Time.deltaTime : 0f;
        float z = rotateZ ? rotationSpeed * Time.deltaTime : 0f;

        transform.Rotate(x, y, z);
    }
}