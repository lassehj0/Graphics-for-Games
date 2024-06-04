#region Using statements

using UnityEngine;

#endregion

namespace Bitgem.VFX.StylisedWater
{
	public class WaterVolumeHelper : MonoBehaviour
	{
		static WaterVolumeHelper instance = null;
		public static WaterVolumeHelper Instance { get { return instance; } }

		public WaterVolumeBase WaterVolume = null;

		void Awake()
		{
			instance = this;
		}

		public float? GetHeight(Vector3 _position)
		{
			// ensure a water volume
			if (!WaterVolume) return 0f;

			// ensure a material
			var renderer = WaterVolume.gameObject.GetComponent<MeshRenderer>();
			if (!renderer || !renderer.sharedMaterial) return 0f;

			// replicate the shader logic, using parameters pulled from the specific material, to return the height at the specified position
			var waterHeight = WaterVolume.GetHeight(_position);
			if (!waterHeight.HasValue)
			{
				return null;
			}

			var _WaveFrequency = renderer.sharedMaterial.GetFloat("_WaveFrequency");
			var _WaveScale = renderer.sharedMaterial.GetFloat("_WaveScale");
			var _WaveSpeed = renderer.sharedMaterial.GetFloat("_WaveSpeed");

			var time = Time.time * _WaveSpeed;
			var shaderOffset = (Mathf.Sin(_position.x * _WaveFrequency + time) + Mathf.Cos(_position.z * _WaveFrequency + time)) * _WaveScale;
			return waterHeight.Value + shaderOffset;
		}
	}
}