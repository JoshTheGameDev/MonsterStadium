using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//******************************
//
//  Created by: Joshua D'Agostino
//
//  Last edited by: Joshua D'Agostino
//  Last edited on: 25/09/19
//
//******************************

public class RotateObj : MonoBehaviour
{

    public GameObject objToRotate;

    [SerializeField]
    private float speed = 10.0f;

    // Update is called once per frame
    void FixedUpdate()
    {

        objToRotate.transform.Rotate(0, Time.deltaTime * speed, 0);

    }
}
