using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

using HutongGames.PlayMaker;
using UnityEditor;
using UnityEngine;
using System;

[CustomActionEditor(typeof(XmlSelectNodes))]
public class XmlSelectNodesEditor : CustomActionEditor
{

    public override bool OnGUI()
    {
		bool edited = false;
		XmlSelectNodes _target = (XmlSelectNodes)target;
		
		if (_target.xmlSource==null)
		{
			_target.xmlSource = new FsmXmlSource();
		}
		

		edited = DataMakerActionEditorUtils.EditFsmXmlSourceField(_target.Fsm,_target.xmlSource);
		

		EditField("xPathQuery");
		EditField("xPathVariables");
		
		EditField("xmlResult");

		EditField("nodeCount");
					
		EditField("foundEvent");
		EditField("notFoundEvent");
		EditField("errorEvent");
		
		return GUI.changed || edited;
    }

}
