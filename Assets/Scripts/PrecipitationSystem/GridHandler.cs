/*
    script to handle specifying where on a grid the player is
*/
using System;
using UnityEngine;

[ExecuteInEditMode]
public class GridHandler : MonoBehaviour
{
	public float gridSize = 10f;
	public Transform playerTransform;

	// a callback to subscribe to when the player grid changes
	public event Action<Vector3Int> onPlayerGridChange;

	Vector3 halfGrid;
	Vector3Int lastPlayerGrid, playerGrid;

	private void OnValidate()
	{
		lastPlayerGrid = new Vector3Int(-99999, -99999, -99999);
		Vector3 playerPos = playerTransform.position;
		playerGrid = new Vector3Int(
			Mathf.FloorToInt(playerPos.x / gridSize),
			Mathf.FloorToInt(playerPos.y / gridSize),
			Mathf.FloorToInt(playerPos.z / gridSize)
		);

		halfGrid = new Vector3(gridSize * .5f, gridSize * .5f, gridSize * .5f);
	}

	void Update()
	{
		// Calculate the grid coordinate where the player currently is
		Vector3 playerPos = playerTransform.position;
		playerGrid.x = Mathf.FloorToInt(playerPos.x / gridSize);
		playerGrid.y = Mathf.FloorToInt(playerPos.y / gridSize);
		playerGrid.z = Mathf.FloorToInt(playerPos.z / gridSize);

		// Check if the player changed grid coordinates since the last check
		if (playerGrid != lastPlayerGrid)
		{
			// Broadcast the new grid coordinates to whoever subscribed to the callback
			onPlayerGridChange?.Invoke(playerGrid);

			lastPlayerGrid = playerGrid;
		}
	}

	// Calculate the center position of a grid coordinate
	public Vector3 GetGridCenter(Vector3Int grid) =>
		new Vector3(grid.x, grid.y, grid.z) * gridSize + halfGrid;

	// Draw gizmo cubes around the grids where the player is
	// so we can see it in the scene view
	void OnDrawGizmos()
	{
		// loop in a 3 x 3 x 3 grid
		for (int x = -1; x <= 1; x++)
			for (int y = -1; y <= 1; y++)
				for (int z = -1; z <= 1; z++)
				{
					bool isCenter = x == 0 && y == 0 && z == 0;
					Vector3 gridCenter = GetGridCenter(lastPlayerGrid + new Vector3Int(x, y, z));

					// make the center one green and slightly smaller so it stands out visually
					Gizmos.color = isCenter ? Color.green : Color.red;
					Gizmos.DrawWireCube(gridCenter, Vector3.one * (gridSize * (isCenter ? .95f : 1.0f)));
				}
	}
}