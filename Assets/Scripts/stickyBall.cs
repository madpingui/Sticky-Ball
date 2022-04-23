using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stickyBall : MonoBehaviour {

    public float facingAngle = 0;
    float x = 0;
    float z = 0;
    Vector2 unitV2;

    public Joystick joyStick;

    public GameObject cameraReference;
    float distanceToCamera = 5;

    float size = 1;

    public GameObject[] category1;
    bool category1Unlocked = false;

    public GameObject[] category2;
    bool category2Unlocked = false;

    public GameObject[] category3;
    bool category3Unlocked = false;

    public Text sizeBall;

    private Rigidbody rb;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //User Controls
        x = joyStick.Horizontal * Time.deltaTime * -50;
        z = joyStick.Vertical * Time.deltaTime * 50;

        //Facing angle
        facingAngle += x;
        unitV2 = new Vector2(Mathf.Cos(facingAngle * Mathf.Deg2Rad), Mathf.Sin(facingAngle * Mathf.Deg2Rad));
    }

    private void FixedUpdate()
    {
        //Apply force behind the ball
        rb.AddForce(new Vector3(unitV2.x, 0, unitV2.y) * z * 3);

        //Set Camera Position Behind based on rotation
        cameraReference.transform.position = new Vector3(-unitV2.x * distanceToCamera, distanceToCamera, -unitV2.y * distanceToCamera) + this.transform.position;
        unlockPickupCategories();
    }

    void unlockPickupCategories()
    {
        if(category1Unlocked == false)
        {
            if(size >= 1)
            {
                category1Unlocked = true;
                for (int i = 0; i < category1.Length; i++)
                {
                    category1[i].GetComponent<Collider>().isTrigger = true;
                }
            }
        }
        else if (category2Unlocked == false)
        {
            if (size >= 1.5f)
            {
                category2Unlocked = true;
                for (int i = 0; i < category2.Length; i++)
                {
                    category2[i].GetComponent<Collider>().isTrigger = true;
                }
            }
        }
        else if (category3Unlocked == false)
        {
            if (size >= 2)
            {
                category3Unlocked = true;
                for (int i = 0; i < category3.Length; i++)
                {
                    category3[i].GetComponent<Collider>().isTrigger = true;
                }
            }
        }
    }

    //Pick up objects
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Sticky"))
        {
            //Grow the sticky ball
            transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            size += 0.01f;
            distanceToCamera += 0.08f;
            other.enabled = false;
            sizeBall.text = "Mass: " + size.ToString();

            //Becomes child of sticky ball
            other.transform.SetParent(this.transform);
        }
    }
}
