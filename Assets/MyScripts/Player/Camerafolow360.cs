using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Camerafolow360 : MonoBehaviour
{
	[Header("Camera Follow 360 ")]
	[SerializeField] protected Transform player;
	[SerializeField] protected Transform orientation;
	[SerializeField] protected Vector3 lookOffset = new Vector3(0, 1, 0);

	[SerializeField] protected float distance = 10;
	[SerializeField] protected float height = 5;
	[SerializeField] protected float cameraSpeed = 100;
	[SerializeField] protected float rotSpeed = 100;
	[SerializeField] protected float smoothFactor = 0.1f;
	private bool isRotate;

	//Test
	[SerializeField] protected LayerMask collisionLayers; // Các lớp để kiểm tra va chạm
	[SerializeField] protected float minDistance = 1.0f; // Khoảng cách tối thiểu của camera từ người chơi
	[SerializeField] protected float collisionSmooth = 0.1f; // Tốc độ chuyển động mượt mà khi va chạm

	void Update()
	{
		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = Input.GetAxis("Mouse Y");
            if (player)
            {
                Vector3 lookPosition = orientation.position + lookOffset;
                Vector3 relativePos = lookPosition - transform.position;
                Quaternion rot = Quaternion.LookRotation(relativePos);

                transform.rotation = Quaternion.Slerp(this.transform.rotation, rot, Time.deltaTime * rotSpeed * 0.1f);

                Vector3 desiredCameraPos = orientation.transform.position + orientation.transform.up * height - orientation.transform.forward * distance;

                // Kiểm tra va chạm vật thể
                RaycastHit hit;
                if (Physics.Linecast(orientation.position, desiredCameraPos, out hit, collisionLayers))
                {
                    float hitDistance = Vector3.Distance(orientation.position, hit.point) - 0.1f; 
                    desiredCameraPos = orientation.transform.position + orientation.transform.up * height - orientation.transform.forward * Mathf.Max(hitDistance, minDistance);
                }

                this.transform.position = Vector3.Lerp(this.transform.position, desiredCameraPos, Time.deltaTime * cameraSpeed * 0.1f);
            }

            if (Input.GetMouseButton(1))
            {
                player.transform.Rotate(Vector3.up * mouseX * rotSpeed * Time.deltaTime);
                orientation.transform.Rotate(Vector3.left * mouseY * rotSpeed * Time.deltaTime);

                // giới hạn xoay cam trục x ở -80 và 40
                Vector3 currentRotation = orientation.transform.localEulerAngles;
                float currentXRotation = currentRotation.x > 180 ? currentRotation.x - 360 : currentRotation.x;
                currentXRotation = Mathf.Clamp(currentXRotation, -80f, 40f);
                currentRotation.x = currentXRotation < 0 ? currentXRotation + 360 : currentXRotation;
                orientation.transform.localEulerAngles = currentRotation;
            }

            if (Input.GetMouseButtonUp(1) && PlayerMovement.isRunning == true)
            {
                isRotate = true;
            }

            if (isRotate)
            {
                orientation.transform.rotation = Quaternion.Slerp(orientation.transform.rotation, player.transform.rotation, smoothFactor * Time.deltaTime);
                if (Quaternion.Angle(orientation.transform.rotation, player.transform.rotation) < 0.1f)
                    isRotate = false;
            }
        }

    
}
