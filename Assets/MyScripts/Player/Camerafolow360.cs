using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafolow360 : MonoBehaviour
{
	public Transform player;
	public Transform orientation;
	public float distance = 10;
	public float height = 5;
	public Vector3 lookOffset = new Vector3(0, 1, 0);
	float cameraSpeed = 100;
	float rotSpeed = 100;

	void FixedUpdate()
	{
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
			//player.transform.Rotate(Vector3.up * mouseX * rotSpeed * Time.deltaTime);
			//orientation.transform.rotation = player.transform.rotation;

		}
  //      if (Input.GetMouseButton(1))
  //      {
			

		//	// Rotate the camera based on mouse movement
		//	orientation.transform.Rotate(Vector3.up * mouseX * rotSpeed * Time.deltaTime);
		//	orientation.transform.Rotate(Vector3.left * mouseY * rotSpeed * Time.deltaTime);

		//}
	}
}
