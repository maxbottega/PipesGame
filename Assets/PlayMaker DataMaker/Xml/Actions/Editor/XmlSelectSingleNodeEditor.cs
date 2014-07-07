using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

using HutongGames.PlayMaker;
using UnityEditor;
using UnityEngine;
using System;

[CustomActionEditor(typeof(XmlSelectSingleNode))]
public class XmlSelectSingleNodeEditor : CustomActionEditor
{

    public override bool OnGUI()
    {
		bool edited = false;
		XmlSelectSingleNode _target = (XmlSelectSingleNode)target;
		
		if (_target.xmlSource==null)
		{
			_target.xmlSource = new FsmXmlSource();
		}
	
		edited = DataMakerActionEditorUtils.EditFsmXmlSourceField(_target.Fsm,_target.xmlSource);

		EditField("xPathQuery");
		EditField("xPathVariables");
			
		EditField("xmlResult");
		EditField("storeReference");
		
		edited = edited || EditResultProperties(_target);

		EditField("foundEvent");
		EditField("notFoundEvent");
		EditField("errorEvent");
		
		return GUI.changed || edited;
    }
	
	
	private bool EditResultProperties(XmlSelectSingleNode _target)
	{

		bool edited = false;
		
		int count = 0;
		
		if (_target.properties !=null &&  _target.propertyValues !=null)
		{
			count = _target.properties.Length;
			
			
			for(int i=0;i<count;i++)
			{
				GUILayout.BeginHorizontal();
				
					GUILayout.Label("Property item "+i);
					GUILayout.FlexibleSpace();
		
				
					if (FsmEditorGUILayout.MiniButton(new GUIContent("x"),GUILayout.Width(16)))
					{
						ArrayUtility.RemoveAt(ref _target.properties,i);
						ArrayUtility.RemoveAt(ref _target.propertyValues,i);
						return true; // we must not continue, an entry is going to be deleted so the loop is broken here. next OnGui, all will be well.
					}
					GUILayout.Space(5);
				
				GUILayout.EndHorizontal();
				
				_target.properties[i] = VariableEditor.FsmStringField(new GUIContent("Property"),_target.Fsm,_target.properties[i],null);

				_target.propertyValues[i] = VariableEditor.FsmStringField(new GUIContent("Value"),_target.Fsm,_target.propertyValues[i],null);
				
				
				
				FsmEditorGUILayout.LightDivider();
			}	
		}
		
		string _addButtonLabel = "Get a Property";
		
		if (count>0)
		{
			_addButtonLabel = "Get another Property";
		}
		
		if ( GUILayout.Button(_addButtonLabel) )
		{		
			
			if (_target.properties==null)
			{
				_target.properties = new FsmString[0];
				_target.propertyValues = new FsmString[0];
			}
			
			
			ArrayUtility.Add<FsmString>(ref _target.properties, new FsmString());
			ArrayUtility.Add<FsmString>(ref _target.propertyValues,new FsmString());
			edited = true;	
		}
			
		return edited || GUI.changed;
	}
		
		
}
