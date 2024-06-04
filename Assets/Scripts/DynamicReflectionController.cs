using UnityEngine;

public class DynamicReflectionController : MonoBehaviour
{
	public Transform player;
	public Vector3 offset;

	Vector3 lastPlayerPos;
	Vector3[] waterPositions;
	ReflectionProbe probe;

	void Awake()
	{
		probe = GetComponent<ReflectionProbe>();
		lastPlayerPos = transform.position;
		waterPositions = new Vector3[3];
		waterPositions[0] = new Vector3(3, 2.5f, -4);
		waterPositions[1] = new Vector3(-90, 2.5f, 70);
		waterPositions[2] = new Vector3(-160, 2.5f, 160);

		InvokeRepeating(nameof(UpdatePosition), 0f, 0.05f);
	}

	void UpdatePosition()
	{
		// The distance between the last player position and the current ignoring the y axis
		float distance = Mathf.Abs(player.position.x - lastPlayerPos.x) + Mathf.Abs(player.position.z - lastPlayerPos.z);

		if (distance > 0.5f)
		{
			if (!WaterInSight()) return;

			lastPlayerPos = player.position;

			// Update the reflection probe position based on player position
			transform.position = new Vector3(player.position.x + offset.x, offset.y, player.position.z + offset.z);

			// Optionally, you could also force the probe to update its reflection here
			probe.RenderProbe();
		}
	}

	bool WaterInSight()
	{
		foreach (Vector3 waterPosition in waterPositions)
		{
			Vector3 directionToWater = (new Vector3(waterPosition.x, player.position.y, waterPosition.z) - player.position).normalized;

			// Player's forward direction, normalized
			Vector3 playerForward = new Vector3(player.forward.x, 0, player.forward.z).normalized;

			// Calculate dot product
			float dot = Vector3.Dot(playerForward, directionToWater);

			if (dot > 0.5f) return true;
		}

		return false;
	}
}