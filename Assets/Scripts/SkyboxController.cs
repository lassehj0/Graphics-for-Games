using UnityEngine;

public class SkyboxController : MonoBehaviour
{
	[SerializeField]
	Material skyboxDay, skyboxNight;
	[SerializeField]
	Light sun, moon;
	[SerializeField]
	DayNightCycle dayNightCycle;

	[SerializeField]
	bool update;

	bool isDay;

	void OnValidate()
	{
		isDay = dayNightCycle.timeOfDay < 18 && dayNightCycle.timeOfDay >= 6;
		ChangeSkybox(isDay);
	}

	void Update()
	{
		if (isDay && dayNightCycle.timeOfDay >= 18 || dayNightCycle.timeOfDay < 6)
		{
			isDay = false;
			ChangeSkybox(isDay);
		}
		else if (!isDay && dayNightCycle.timeOfDay >= 6 && dayNightCycle.timeOfDay < 18)
		{
			isDay = true;
			ChangeSkybox(isDay);
		}
	}

	public void ChangeSkybox(bool isDaytime)
	{
		RenderSettings.skybox = isDaytime ? skyboxDay : skyboxNight;
		RenderSettings.sun = isDaytime ? sun : moon;
	}
}
