using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    public float thrustSpeed;
    public float turnSpeed;
    public float hoverPower;
    public float hoverHeight;

    private float thrustInput;
    private float turnInput;
    private Rigidbody shipRigidBody;

    // Use this for initialization
    void Start () {
        shipRigidBody = GetComponent<Rigidbody>();
    }
        
        // Update is called once per frame
        void Update () {
        thrustInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.F)){ // checking if the player pressed button F on the keyboard
            turnInput = -90; // rotate the spacecraft 90 degrees to the left
        }
        else if(Input.GetKey(KeyCode.H)){ // checking if the player pressed button H on the keyboard
            turnInput = 90; // rotate the spacecraft 90 degrees to the right
        }
        else if(Input.GetKey(KeyCode.T)){ // checking if the player pressed button T on the keyboard
            hoverHeight += 1; // increasing the flight altitude by 2 meters
        }
        else if(Input.GetKey(KeyCode.G)){ // checking if the player pressed button G on the keyboard
            hoverHeight -= 1; // decreasing the flight altitude by 2 meters
        }
    }

    void FixedUpdate() {
        // Turning the ship
        shipRigidBody.AddRelativeTorque(0f, turnInput * turnSpeed, 0f);

        // Moving the ship
        shipRigidBody.AddRelativeForce(0f, 0f, thrustInput * thrustSpeed);

        // Hovering
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, hoverHeight))
        {
            float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
            Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverPower;
            shipRigidBody.AddForce(appliedHoverForce, ForceMode.Acceleration);
        }
    }
}