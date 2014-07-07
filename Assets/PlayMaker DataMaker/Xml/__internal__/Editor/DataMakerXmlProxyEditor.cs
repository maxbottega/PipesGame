using UnityEditor;
using UnityEngine;
using System.Collections;

using System;
using System.Xml;
using System.Xml.XPath;


[CustomEditor(typeof(DataMakerXmlProxy))]
public class DataMakerXmlProxyEditor : Editor {

	private Vector2 _scroll;
	
	private string content= "";
	private XmlNode node = null;
	
	private bool valid = false;
	private bool empty = true;
	
	public override void OnInspectorGUI()
	{
		//DrawDefaultInspector();
		DataMakerXmlProxy _target = target as DataMakerXmlProxy;
		
	  	_target.referenceName =  EditorGUILayout.TextField(new GUIContent("Reference"),_target.referenceName);
		
		
		if (!Application.isPlaying)
		{
			if (_target.useSource)
			{
				if(GUILayout.Button("Don't use Text Asset"))
				{
					content = null;
					node = null;
					empty = true;
					_target.XmlTextAsset= null;
					_target.useSource = false;
					
				}
				_target.XmlTextAsset = (TextAsset)EditorGUILayout.ObjectField(new GUIContent("Text Asset"),_target.XmlTextAsset,typeof(TextAsset),false);
				
				GUILayout.Label("This proxy also accepts runtime xml results.");
				GUILayout.Label("i.e Use actions like XMlSaveInProxy");
				
			}else{
				
				GUILayout.Label("This proxy accepts runtime xml results");
				GUILayout.Label("i.e Use actions like XMlSaveInProxy");
				GUILayout.Label("But you can also use this proxy as a source");
				if(GUILayout.Button("Use Text Asset"))
				{
					content = null;
					node = null;
					empty = true;
					_target.useSource = true;
				}
			}
		}else{
			if (_target.useSource)
			{
				_target.XmlTextAsset = (TextAsset)EditorGUILayout.ObjectField(new GUIContent("Text Asset"),_target.XmlTextAsset,typeof(TextAsset),false);
				
			}	
		}
		
		if (_target.xmlNode!=null)
		{
			/*
			if (_target.XmlTextAsset!=null)
			{
				DataMakerEditorGUILayoutUtils.feedbackLabel("XML DATA CHANGED.",DataMakerEditorGUILayoutUtils.labelFeedbacks.WARNING);
			}
			*/
			
			if (node==null || (!node.Equals(_target.xmlNode)) ){
				node = _target.xmlNode;
				content =DataMakerXmlUtils.XmlNodeToString(_target.xmlNode);
				empty = string.IsNullOrEmpty(content);
				Debug.Log("PARSING XML NODE");
				valid = DataMakerXmlUtils.StringToXmlNode(content) != null;
			}
		}else if (_target.XmlTextAsset!=null)
		{
			if (content==null || !content.Equals(_target.XmlTextAsset.text) ){
				content = _target.XmlTextAsset.text;
				empty = string.IsNullOrEmpty(content);
				//Debug.Log("PARSING TEXT ASSET");
				valid = DataMakerXmlUtils.StringToXmlNode(content) != null;
				
			}
		}

		if (empty)
		{
			DataMakerEditorGUILayoutUtils.feedbackLabel("No XML data",DataMakerEditorGUILayoutUtils.labelFeedbacks.WARNING);
			
		}else{
			if (!valid)
			{
			 	DataMakerEditorGUILayoutUtils.feedbackLabel("Xml Invalid",DataMakerEditorGUILayoutUtils.labelFeedbacks.ERROR);
			}else{
				DataMakerEditorGUILayoutUtils.feedbackLabel("Xml Valid",DataMakerEditorGUILayoutUtils.labelFeedbacks.OK);
			}
			_scroll = DataMakerEditorGUILayoutUtils.StringContentPreview(_scroll,content);
		}
		
		if(GUI.changed)
		{
             EditorUtility.SetDirty(target); 		
		}
	}
	
}
