using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

using HutongGames.PlayMaker;
using UnityEditor;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class DataMakerActionEditorUtils {

	
	public static void EditHashSetField(Fsm fsm,FsmString key,FsmString content)
	{
		key = VariableEditor.FsmStringField(new GUIContent("Property"),fsm,key,null);
		content = VariableEditor.FsmStringField(new GUIContent("Value"),fsm,content,null);
	}
	
	public static bool EditFsmXmlSourceField(Fsm fsm,FsmXmlSource source)
	{
		
		source.sourceSelection = EditorGUILayout.Popup("Source selection",source.sourceSelection,source.sourceTypes);
			
		if (source.sourceString==null)
		{
			source.sourceString = new FsmString();
		}
		
		source.sourceString.UseVariable = source.sourceSelection==2;
		
		bool showPreview = false;
		string preview = "";
		
		if (source.sourceSelection==0)
		{
			
			
			source._sourceEdit = EditorGUILayout.Foldout(source._sourceEdit,new GUIContent("Edit"));
			if (source._sourceEdit)
			{
				source.sourceString.Value = EditorGUILayout.TextArea(source.sourceString.Value,GUILayout.Height(200));
			}
			
		}else if (source.sourceSelection==1)
		{
			source.sourcetextAsset = (TextAsset)EditorGUILayout.ObjectField("TextAsset Object",source.sourcetextAsset,typeof(TextAsset),false);
			if (source.sourcetextAsset!=null)
			{
				source._sourcePreview = EditorGUILayout.Foldout(source._sourcePreview,new GUIContent("Preview"));
				showPreview = source._sourcePreview;
				preview = source.sourcetextAsset.text;
			}
		}else if (source.sourceSelection==2)
		{
			
			source.sourceString = VariableEditor.FsmStringField(new GUIContent("Fsm String"),fsm,source.sourceString,null);
			
			if (!source.sourceString.UseVariable)
			{
				source.sourceSelection=0;
				return true;
			}
			
			if (!source.sourceString.IsNone)
			{
				source._sourcePreview = EditorGUILayout.Foldout(source._sourcePreview,new GUIContent("Preview"));
				showPreview = source._sourcePreview;
				preview = source.sourceString.Value;
			}
		}else if (source.sourceSelection==3)
		{
			if (source.sourceProxyGameObject ==null)
			{
				source.sourceProxyGameObject = new FsmGameObject();
				source.sourceProxyReference = new FsmString();
			}
			source.sourceProxyGameObject = VariableEditor.FsmGameObjectField(new GUIContent("GameObject"),fsm,source.sourceProxyGameObject);
			source.sourceProxyReference = VariableEditor.FsmStringField(new GUIContent("Reference"),fsm,source.sourceProxyReference,null);
			
			if (source.sourceProxyGameObject!=null)
			{
				DataMakerXmlProxy proxy = DataMakerCore.GetDataMakerProxyPointer(typeof(DataMakerXmlProxy), source.sourceProxyGameObject.Value, source.sourceProxyReference.Value, true) as DataMakerXmlProxy;
				if (proxy!=null)
				{
					if (proxy.XmlTextAsset!=null)
					{
						source._sourcePreview = EditorGUILayout.Foldout(source._sourcePreview,new GUIContent("Preview"));
						showPreview = source._sourcePreview;
						preview = proxy.XmlTextAsset.text;
					}else{
						//oupss...
					}
				}else{
					//oupss..
				}
			}
		}else if (source.sourceSelection ==4)
		{
			if (source.inMemoryReference==null)
			{
				source.inMemoryReference = new FsmString();
			}
			source.inMemoryReference = VariableEditor.FsmStringField(new GUIContent("Memory Reference"),fsm,source.inMemoryReference,null);
			
			if (!string.IsNullOrEmpty(source.inMemoryReference.Value) )
			{
				source._sourcePreview = EditorGUILayout.Foldout(source._sourcePreview,new GUIContent("Preview"));
				showPreview = source._sourcePreview;
				preview = DataMakerXmlUtils.XmlNodeToString(DataMakerXmlUtils.XmlRetrieveNode(source.inMemoryReference.Value));
			}
		}
		
		if (showPreview)
		{
			if (string.IsNullOrEmpty(preview))
			{
				GUILayout.Label("-- empty --");
			}else{
				source._scroll = GUILayout.BeginScrollView(source._scroll,"box", GUILayout.Height (200));
				GUI.skin.box.alignment = TextAnchor.UpperLeft;
				GUILayout.Box(preview,"",null);
				GUILayout.EndScrollView();
			}
		}
		
		
		return false;
	}
	
}
