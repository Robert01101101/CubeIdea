using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    private float origY, origX;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
        origY = transform.position.y;
        origX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.z > 0) transform.position = player.transform.position + offset;
        transform.position = new Vector3(origX, origY, transform.position.z);
    }
}
