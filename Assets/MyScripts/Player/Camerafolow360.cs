using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafolow360 : MonoBehaviour
{
	[SerializeField] protected Transform player;
	[SerializeField] protected Transform orientation;
	[SerializeField] protected float distance = 10;
	[SerializeField] protected float height = 5;
	[SerializeField] protected Vector3 lookOffset = new Vector3(0, 1, 0);
	[SerializeField] protected float cameraSpeed = 100;
	[SerializeField] protected float rotSpeed = 100;
	[SerializeField] protected float smoothFactor = 0.1f;
	private bool isRotate;

	void Update()
	{
		//Debug.Log("IsRunning :" + PlayerMovement.isRunning);
		//Debug.Log("IsRotate :" + isRotate);
		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = Input.GetAxis("Mouse Y");
		if (player)
		{
			Vector3 lookPosition = orientation.position + lookOffset;
			Vector3 relativePos = lookPosition - transform.position;
			Quaternion rot = Quaternion.LookRotation(relativePos);

			transform.rotation = Quaternion.Slerp(this.transform.rotation, rot, Time.deltaTime * rotSpeed * 0.1f);

			Vector3 targetPos = orientation.transform.position + orientation.transform.up * height - orientation.transform.forward * distance;

			this.transform.position = Vector3.Lerp(this.transform.position, targetPos, Time.deltaTime * cameraSpeed * 0.1f);

		}
        if (Input.GetMouseButton(1))
        {


            // Rotate the camera based on mouse movement
            player.transform.Rotate(Vector3.up * mouseX * rotSpeed * Time.deltaTime);
            orientation.transform.Rotate(Vector3.left * mouseY * rotSpeed * Time.deltaTime);

        }else
        if (Input.GetMouseButtonUp(1) && PlayerMovement.isRunning == true)
        {
			isRotate = true;
			//orientation.transform.rotation = player.transform.rotation;
			
		}
		
		if(isRotate)
        {
			orientation.transform.rotation = Quaternion.Slerp(orientation.transform.rotation, player.transform.rotation, smoothFactor * Time.deltaTime);
			if (orientation.transform.rotation == player.transform.rotation)
				isRotate = false;
		}
    }
}
