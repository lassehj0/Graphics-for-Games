using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	float moveSpeed = 5f, turnSpeed = 300, flySpeed = 5f;

	bool upInput, downInput, sprint;
	float forwardInput, sideInput, turnInput;

	new Rigidbody rigidbody;

	void Awake()
	{
		rigidbody = GetComponent<Rigidbody>();
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void FixedUpdate()
	{
		GetInputValues();
		ProcessActions();
	}

	void ProcessActions()
	{
		if (turnInput != 0f)
		{
			float angle = Mathf.Clamp(turnInput, -1f, 1f) * turnSpeed;
			transform.Rotate(Vector3.up, Time.fixedDeltaTime * angle);
		}

		var velo = Vector3.zero;

		if (upInput) velo += Vector3.up * flySpeed;
		if (downInput) velo += Vector3.down * flySpeed;

		velo += Mathf.Clamp(forwardInput, -1f, 1f) * moveSpeed * transform.forward;
		velo += Mathf.Clamp(sideInput, -1f, 1f) * moveSpeed * transform.right;

		if (sprint) velo *= 5;

		rigidbody.velocity = velo;
	}

	void GetInputValues()
	{
		forwardInput = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
		sideInput = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
		downInput = Input.GetKey(KeyCode.LeftShift);
		upInput = Input.GetKey(KeyCode.Space);
		sprint = Input.GetKey(KeyCode.LeftControl);
		turnInput = Input.GetAxis("Mouse X");
	}
}
