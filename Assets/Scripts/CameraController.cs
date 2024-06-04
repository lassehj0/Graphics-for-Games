using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float turnSpeed = 300;

	void FixedUpdate()
	{
		float turnInput = Input.GetAxis("Mouse Y");

		if (turnInput != 0f)
		{
			float angle = Mathf.Clamp(turnInput, -1f, 1f) * turnSpeed;
			transform.Rotate(Vector3.left, Time.fixedDeltaTime * angle);
		}
	}
}
