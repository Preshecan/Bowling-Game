using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public Vector3 launchVelocity;
    public Rigidbody rigidBody;
    public bool inPlay = false;

    private AudioSource audioSource;
    private int n = 0;
    private Vector3 ballStartPosition;

	public void Start ()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
        rigidBody.useGravity = false;
        ballStartPosition = rigidBody.transform.position;
    }

    public void Launch(Vector3 velocity)
    {
        inPlay = true;

        rigidBody.velocity = velocity;
        rigidBody.useGravity = true;
        audioSource = gameObject.GetComponent<AudioSource>();

        
        while (n < 1) {
            audioSource.Play();
            n++;
        }
        
    }

    public void Reset() {
        inPlay = false;
        rigidBody.transform.position = ballStartPosition;
        rigidBody.transform.rotation = Quaternion.identity;
        rigidBody.velocity = new Vector3(0,0,0);
        rigidBody.useGravity = false;
        rigidBody.angularVelocity = Vector3.zero;                   //Vector3.zero simplification
        rigidBody.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
