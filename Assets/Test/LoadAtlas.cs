using UnityEngine;

public class LoadAtlas : MonoBehaviour
{
//	public UIAtlas referenceAtlas;
	public string sdAtlas = "SD";
	public string hdAtlas = "HD";
	public bool loadHD = true;

	void Awake ()
	{
//		GameObject go = Resources.Load(loadHD ? hdAtlas : sdAtlas, typeof(GameObject)) as GameObject;
//		referenceAtlas.replacement = go.GetComponent<UIAtlas>();
	}
}
