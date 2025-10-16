using UnityEngine.SceneManagement;
using UnityEngine;
using Sirenix.OdinInspector;

public class BasicSceneManagement : MonoBehaviour
{
	[SerializeField, SceneObjectsOnly] private int scene;

	[Button]
	public void LoadScene()
	{
		SceneManager.LoadScene(scene, LoadSceneMode.Single);
	}
	public void LoadScene(int index)
	{
		scene = index;
		LoadScene();
	}

	[Button]
	public void Quit()
	{
		if (Application.isEditor)
		{
			Debug.LogError("Application Quit");
		}
		else
		{
			Application.Quit();
		}
	}
}
