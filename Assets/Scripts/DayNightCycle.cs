using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
	//Gravitational constant
	const float G = 6.67430e-11f;
	//Multiplier for the calculations to make sense
	const int MULTIPLIER = 1000000000;

	[SerializeField]
	Light sun, secondSun;
	[Range(0, 24)]
	public float timeOfDay;
	[SerializeField]
	float sunRotationSpeed, distanceToChild;

	// Calculated using Kepler's Third Law of Planetary Motion
	float orbitalPeriod;
	float orbitalTime = 0;

	//Masses of the two suns
	readonly float m1 = 100f * MULTIPLIER, m2 = 60f * MULTIPLIER;

	void OnValidate()
	{
		orbitalPeriod = 2 * Mathf.PI * Mathf.Sqrt(Mathf.Pow(distanceToChild, 3) / (G * (m1 + m2)));

		UpdateSunsRotation();
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.RightArrow)) sunRotationSpeed += Time.deltaTime;
		if (Input.GetKey(KeyCode.LeftArrow)) sunRotationSpeed -= Time.deltaTime;

		orbitalTime += Time.deltaTime * sunRotationSpeed;
		timeOfDay += Time.deltaTime * sunRotationSpeed;

		if (timeOfDay >= 24) timeOfDay = 0;
		if (orbitalTime >= orbitalPeriod) orbitalTime = 0;

		UpdateSunsRotation();
	}

	void UpdateSunsRotation()
	{
		float rotation = Mathf.Lerp(-90, 270, timeOfDay / 24);
		transform.rotation = Quaternion.Euler(rotation, transform.rotation.y, transform.rotation.z);

		UpdateSecondSunRotation();
	}

	void UpdateSecondSunRotation()
	{
		float angularDisplacement = 2 * Mathf.PI * orbitalTime * distanceToChild / orbitalPeriod;

		// Calculate the orientations based on the angular displacement
		float xRotation = distanceToChild * -Mathf.Sin(angularDisplacement);
		float yRotation = distanceToChild * -Mathf.Cos(angularDisplacement);

		// Set the rotation of the smaller sun
		secondSun.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
	}
}
