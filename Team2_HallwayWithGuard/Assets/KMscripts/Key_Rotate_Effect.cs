using UnityEngine;

public class KeyFloat : MonoBehaviour
{
    public float rotationSpeed = 60f;
    public float bobSpeed = 2f;
    public float bobHeight = 0.2f;

    private Vector3 startPos;
    private float currentYRotation = 0f;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        currentYRotation += rotationSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0f, currentYRotation, 0f);

        float newY = startPos.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.localPosition = new Vector3(startPos.x, newY, startPos.z);
    }
}