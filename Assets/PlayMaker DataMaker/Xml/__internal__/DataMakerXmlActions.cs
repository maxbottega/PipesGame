// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.
//
// © 2012 Jean Fabre http://www.fabrejean.net
//
//
using UnityEngine;
using System.Collections;

using System.Xml;

namespace HutongGames.PlayMaker.Actions
{
	public abstract class DataMakerXmlActions: FsmStateAction
	{
		
		public static string GetNodeProperty(XmlNode node,string property)
		{
			if (property.StartsWith("@"))
			{
				property = property.Remove(0,1);
				
				XmlAttribute att = node.Attributes[property];
				if (att != null) {
					return att.InnerText;
				} else {
					//Debug.LogWarning (property + " attribute not found");
				
				}
			}else if (property.StartsWith("/") || property.StartsWith("."))
			{
				if (property.StartsWith("/"))
				{
					property =  "."+property;
				}
				
				XmlNode subNode = node.SelectSingleNode(property);
				if (subNode != null) {
					return subNode.InnerText;
				} else {
					Debug.LogWarning (property + " not found");
				}
			
			}else
			{
				XmlNode innerNode = node[property];
				if (innerNode != null) {
					return innerNode.InnerText;
				} else {
					//Debug.LogWarning (property + " not found");
				}
			}
			
			return "";
		}
	}
}