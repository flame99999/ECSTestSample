using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjController : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        var pos = transform.position;
        pos += transform.forward * (GameManager.Instance.moveSpeed * Time.deltaTime);

        if (pos.z > GameManager.Instance.UpperBounds)
        {
            pos.z = GameManager.Instance.BottomBounds;
        }
        transform.position = pos;
    }
}