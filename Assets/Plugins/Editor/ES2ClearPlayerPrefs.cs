using UnityEngine;
using UnityEditor;
using System.Collections;

public class ES2ClearPlayerPrefsInspector : Editor
{
	[MenuItem ("Assets/Easy Save 2/Clear PlayerPrefs")]
    private static void ClearPlayerPrefs() 
    {
    	PlayerPrefs.DeleteAll();
    }
}