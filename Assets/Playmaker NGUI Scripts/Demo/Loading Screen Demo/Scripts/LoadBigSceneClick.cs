using UnityEngine;
using System.Collections;

public class LoadBigSceneClick : MonoBehaviour 
{
    void OnClick()
    {
        // load the big scene level
        Application.LoadLevel("Demo - Big Scene - Loaded by Script");
    }
}
