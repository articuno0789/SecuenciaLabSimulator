using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class TransformModel : MonoBehaviour
{
    //Turn
    public bool turnWithMouse;
    public float horizontalSpeed = 25.0F;
    public float verticalSpeed = 25.0F;
    public float horizontalSpeedMouse = 3.0F;
    public float verticalSpeedMouse = 3.0F;
    public float turnSpeed = 20f;
    //Move
    public float moveSpeed = 10f;
    public float rightLimit = 10f;
    public float leftLimit = -10f;
    public float backwardLimit = 20f;
    public float forwardLimit = 2f;
    public float upLimit = 10f;
    public float downLimit = 0f;
    public Vector3 originalPosition;
    public Vector3 originalScale;
    public Vector3 originalRotation;

    void Update()
    {
        if (Input.GetKey(KeyCode.Keypad1) || Input.GetKey(KeyCode.Alpha1))
        {
            if (turnWithMouse)
            {
                float h = horizontalSpeedMouse * Input.GetAxis("Mouse X");
                float v = verticalSpeedMouse * Input.GetAxis("Mouse Y");
                transform.Rotate(v, -h, 0);
            }
            else
            {
                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                {
                    transform.Rotate(Vector3.right, verticalSpeed * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                {
                    transform.Rotate(Vector3.left, verticalSpeed * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {
                    transform.Rotate(Vector3.down, horizontalSpeed * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                {
                    transform.Rotate(Vector3.up, horizontalSpeed * Time.deltaTime);
                }
            }
        }
        else if (Input.GetKey(KeyCode.Keypad2) || Input.GetKey(KeyCode.Alpha2))
        {
            if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) 
                && transform.position.z >= forwardLimit && transform.position.y >= downLimit)
            {
                transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
            }
            if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) 
                && transform.position.z <= backwardLimit && transform.position.y <= upLimit)
            {
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            }
            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) 
                && transform.position.x >= leftLimit) {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            }
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) 
                && transform.position.x <= rightLimit) {
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            }
        }
        else if (Input.GetKey(KeyCode.Keypad3) || Input.GetKey(KeyCode.Alpha3))
        {
            transform.rotation = Quaternion.Euler(originalRotation);
            transform.position = originalPosition;
            transform.localScale = originalScale;
        }
    }
}