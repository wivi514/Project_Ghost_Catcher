using UnityEngine;

public class LogoHovering : MonoBehaviour
{
    public float amplitude = 0.1f;
    public float frequency = 1.0f;
    public float phaseOffset = 0.0f;

    private Vector3 initialPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * frequency + phaseOffset) * amplitude;
        transform.localPosition = initialPosition + new Vector3(0, yOffset, 0);
    }
}
