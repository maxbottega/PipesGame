using UnityEngine;
using System.Collections;

public class DataMakerEditorGUILayoutUtils {
	
	
	public enum labelFeedbacks { OK , WARNING , ERROR};

	public static void feedbackLabel(string message,labelFeedbacks type)
	{
		switch ( type)
		{
		case labelFeedbacks.OK:
			GUI.color = Color.green;
			break;
		case labelFeedbacks.WARNING:
			GUI.color = Color.yellow;
			break;
		case labelFeedbacks.ERROR:
			GUI.color = Color.red;
			break;
		}
		
		GUILayout.BeginHorizontal("box",GUILayout.ExpandWidth(true));
		GUI.color = Color.white;
		GUILayout.Label(message);	
		GUILayout.EndHorizontal();
	}
	
	
	public static Vector2 StringContentPreview(Vector2 scroll, string content)
	{
		scroll = GUILayout.BeginScrollView(scroll,"box", GUILayout.Height (200));
		GUI.skin.box.alignment = TextAnchor.UpperLeft;
		GUILayout.Box(content,"",null);
		GUILayout.EndScrollView();
		
		return scroll;
	}
	
}
