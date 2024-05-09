using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform target;

    //public Transform transform;

    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
    }
}
