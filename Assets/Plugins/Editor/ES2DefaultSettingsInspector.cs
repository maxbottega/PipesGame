using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ES2GlobalSettings))]
public class ES2DefaultSettingsInspector : Editor
{
	[MenuItem ("Assets/Easy Save 2/Add Default Settings Object to Scene")]
    private static void AddAutoSave() 
    {
    	GameObject g = new GameObject();
    	g.name = "Easy Save Default Settings";
    	g.AddComponent<ES2GlobalSettings>();
    }
    
    public override void OnInspectorGUI()
	{
		//EditorGUIUtility.labelWidth = 165f;
		ES2GlobalSettings targetObj = target as ES2GlobalSettings;
		targetObj.pcDataPath = EditorGUILayout.TextField("Default PC Data Path:", targetObj.pcDataPath);
		targetObj.macDataPath = EditorGUILayout.TextField("Default Mac Data Path:", targetObj.macDataPath);
		//targetObj.saveLocation = (ES2.SaveLocation)EditorGUILayout.EnumPopup("Default Save Location:", (System.Enum)targetObj.saveLocation);
		targetObj.encrypt = EditorGUILayout.Toggle("Encrypt by Default:", targetObj.encrypt);
		targetObj.encryptionPassword = EditorGUILayout.TextField("Default Encryption Password:", targetObj.encryptionPassword);
		targetObj.webUsername = EditorGUILayout.TextField("Default Web Username:", targetObj.webUsername);
		targetObj.webPassword = EditorGUILayout.TextField("Default Web Password:", targetObj.webPassword);
	}
}