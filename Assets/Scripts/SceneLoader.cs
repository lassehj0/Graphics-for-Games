using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public class SceneLoader
{
	static SceneLoader()
	{
		// Delay the execution until the first Editor update
		EditorApplication.update += LoadScene;
	}

	static void LoadScene()
	{
		// Remove the callback
		EditorApplication.update -= LoadScene;

		string sceneToOpen = "Assets/Scenes/SampleScene.unity";

		// Open the scene if Unity is not in play mode and the specified scene is not already open
		if (!EditorApplication.isPlayingOrWillChangePlaymode)
		{
			if (EditorSceneManager.GetActiveScene().path != sceneToOpen)
			{
				Debug.Log("Opening scene: " + sceneToOpen);
				EditorSceneManager.OpenScene("Assets/Scenes/SampleScene.unity");
			}
		}
	}
}
