using UnityEngine;

public class GhostHover : MonoBehaviour
{
    [SerializeField] private float amplitude = 0.05f;
    [SerializeField] private float frequency = 8f;

    private Vector3 startLocalPos;

    void Start()
    {
        startLocalPos = transform.localPosition; // Use local position so it hovers relative to parent
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.localPosition = startLocalPos + new Vector3(0, yOffset, 0);
    }
}
