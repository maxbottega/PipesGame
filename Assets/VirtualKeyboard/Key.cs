using UnityEngine;
using System.Collections;
namespace VirtualKeyboard{
[ExecuteInEditMode]
public class Key : MonoBehaviour {
	public UILabel uiLabel;
	public UIInput uiInput;
	public UIToggle capslock;
	[ContextMenu ("bind")]
	public void bind(){
		uiLabel=transform.Find("Label").GetComponent<UILabel>();
	}
	public void OnClick(){
		uiInput.text+=capslock.value?uiLabel.text.ToUpper():uiLabel.text.ToLower();

	}
}
	
}
