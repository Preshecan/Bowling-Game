using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

    public float standingThreshold = 10f;

    private Rigidbody rigidBody;
    public float distanceToRaise = 60f;


    void Start () {
        rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
          //print(name + IsStanding());
    }

    public void RaiseIfStanding() { 
        if (IsStanding()) {
            rigidBody.useGravity = false;
            //Debug.Log("gravity state: " + name + " " + rigidBody.useGravity );
            transform.Translate(new Vector3(0, distanceToRaise, 0), Space.World);
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
            transform.rotation = Quaternion.Euler(270f,0,0);
        }
    }

    public void Lower() {
        if (IsStanding()) {
            transform.Translate(new Vector3(0, -distanceToRaise, 0), Space.World);
            rigidBody.useGravity = true;
        }
    }

    public bool IsStanding ()
    {
        Vector3 rotationInEuler = transform.rotation.eulerAngles;

        float tiltInX = Mathf.Abs(rotationInEuler.x - 270);
        float tiltInZ = Mathf.Abs(rotationInEuler.z);

        if (tiltInX < standingThreshold && tiltInZ < standingThreshold)
        {
            return true;
        }
        else
        {
            return false; 
        }
    }
}
