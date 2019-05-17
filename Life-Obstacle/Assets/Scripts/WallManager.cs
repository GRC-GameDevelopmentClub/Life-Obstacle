using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour {
    public float backgroundSize;

    private Transform cameraTransform;
    private Transform[] layers;
    private float viewZone = 10;
    private int upIndex;
    private int lowerIndex;
    // Use this for initialization
    void Start() {
        layers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            layers[i] = transform.GetChild(i);
        }
        lowerIndex = 0;
        upIndex = layers.Length - 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            GoDown();
        }
    }

    private void GoDown()
    {
        int lastUpIndex = upIndex;
        layers[upIndex].position = Vector3.down * (layers[lowerIndex].position.y - backgroundSize);
        lowerIndex = upIndex;
        upIndex--;
        if (upIndex < 0)
        {
            upIndex = layers.Length - 1;
        }
    }
}
