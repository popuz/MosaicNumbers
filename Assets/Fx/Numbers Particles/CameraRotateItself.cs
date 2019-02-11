using UnityEngine;
using System.Collections;

public class CameraRotateItself : MonoBehaviour 
{
	private Transform _transform;

	float rotationX = -90;
	float rotationY;
	float rotationZ;
	float dampRotationX;
	float dampRotationY;
	float rotationVelX;
	float rotationVelY;

	public int mouseSensetivityX = 200;
	public int mouseSensetivityY = 200;
	public int clampAngle = 90;

	public float zRotationSpeed = 0.5f;
	// Use this for initialization
	void Awake () => _transform = transform;

	// Update is called once per frame
	void Update () 
	{
		//Screen.lockCursor = true;
		rotationX += -Input.GetAxis ("Mouse Y")*mouseSensetivityX*Time.deltaTime;
		rotationX = Mathf.Clamp (rotationX, -clampAngle, clampAngle);
		rotationY += Input.GetAxis ("Mouse X") * mouseSensetivityY * Time.deltaTime;

		dampRotationX = Mathf.SmoothDamp (dampRotationX,rotationX,ref rotationVelX, 0.5f);
		dampRotationY = Mathf.SmoothDamp (dampRotationY,rotationY,ref rotationVelY, 0.5f);
		rotationZ += zRotationSpeed;
		_transform.rotation = Quaternion.Euler (new Vector3(dampRotationX,dampRotationY,rotationZ));
	}
}
