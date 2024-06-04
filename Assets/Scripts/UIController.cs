using TMPro;
using Unity.Profiling;
using UnityEngine;

public class UIController : MonoBehaviour
{
	[SerializeField]
	TextMeshProUGUI fps, memory;

	ProfilerRecorder totalReservedMemory;
	readonly float repeatRate = 0.3f;
	int updates = 0;

	void Start()
	{
		InvokeRepeating(nameof(SetStats), 0f, repeatRate);

		totalReservedMemory = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Total Used Memory");
	}

	void Update()
	{
		updates++;
	}

	void SetStats()
	{
		fps.text = $"FPS: {(int)(updates / repeatRate)}";
		updates = 0;

		memory.text = $"MEM: {totalReservedMemory.LastValue / 1000000}MB";
	}

	private void OnDisable()
	{
		totalReservedMemory.Dispose();
	}
}
