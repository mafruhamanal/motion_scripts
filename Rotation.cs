using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public GameObject PlantObject;
    public Vector3 RotationVector;

    private void Update()
    {
        PlantObject.transform.Rotate(RotationVector * Time.deltaTime);
    }
}
