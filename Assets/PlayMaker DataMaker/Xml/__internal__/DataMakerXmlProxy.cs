// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.
//
// © 2012 Jean Fabre http://www.fabrejean.net
//
//
using UnityEngine;
using System.Collections;

using System.Xml;


public class DataMakerXmlProxy : DataMakerProxyBase {
	
	public bool useSource;
	public TextAsset XmlTextAsset;
	
	[HideInInspector]
	public XmlNode xmlNode;

	void Awake () {
		
		
		if (useSource && XmlTextAsset!=null)
		{
			InjectXmlString(XmlTextAsset.text);
		}
	}
	
	public void InjectXmlNode(XmlNode node)
	{
		xmlNode = node;
	}
	
	public void InjectXmlNodeList(XmlNodeList nodeList)
	{
		 XmlDocument doc = new XmlDocument();
    	xmlNode =  doc.CreateElement("root");
		foreach(XmlNode _node in nodeList)
		{
			xmlNode.AppendChild(_node);
		}
	
		Debug.Log(DataMakerXmlUtils.XmlNodeToString(xmlNode));
	}
	
	public void InjectXmlString(string source)
	{
		xmlNode = DataMakerXmlUtils.StringToXmlNode(source);
	}
	
}
