using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookBall : MonoBehaviour {

    public GameObject ball;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = new Vector3(0, 1.5f, 0);
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(ball.transform.position + offset);
	}
}
