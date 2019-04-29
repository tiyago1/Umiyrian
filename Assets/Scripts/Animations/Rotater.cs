using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    public void Update()
    {
        this.transform.Rotate(Vector3.forward* Time.deltaTime * 500);
    }
}
