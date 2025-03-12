using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Rotate : MonoBehaviour
{
    public UnityEvent OnHour;
    public float t;

    void Update()
    {
        Vector3 rotate = transform.eulerAngles;
        rotate.z -= t * Time.deltaTime;
        transform.eulerAngles = rotate;
     
        if (rotate.z < 0)
        {
            OnHour.Invoke();
            rotate.z = 0;
        }
    }
}
