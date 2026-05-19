using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingVisual : MonoBehaviour
{
    public float floatHeight;
    public float floatSpeed;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * 0.2f;

        transform.localPosition = startPos + Vector3.up * (floatHeight + yOffset);
    }
}
