using UnityEngine;
namespace TRNTH{
public class DebugShowFps : MonoBehaviour {
	bool isHidden=false;
	public Rect rect;
	void OnGUI(){
		GUILayout.Label("fps:"+Mathf.Floor(1.0f/Time.smoothDeltaTime));
		GUILayout.Label("q:"+QualitySettings.GetQualityLevel());
	}
}
}
