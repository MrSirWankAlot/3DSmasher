using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform player;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
    }
}
