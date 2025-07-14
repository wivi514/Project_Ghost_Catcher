using UnityEngine;

public class LogoStretch : MonoBehaviour
{
    public float stretchAmplitude = 0.1f;  
    public float stretchFrequency = 1f;    

    private Vector3 initialScale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float stretch = Mathf.Sin(Time.time * stretchFrequency) * stretchAmplitude;
        float zScale = initialScale.z + stretch;

        transform.localScale = new Vector3(initialScale.x, initialScale.y, zScale);
    }
}
