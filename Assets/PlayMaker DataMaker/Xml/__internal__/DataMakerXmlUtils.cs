using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Xml;

public class DataMakerXmlUtils {
	
	
	
		
	
	#region Memory Slots
	
	private static Dictionary<string,XmlNode> xmlNodeLUT;
	private static Dictionary<string,XmlNodeList> xmlNodeListLUT;
	
	public static void XmlStoreNode(XmlNode node,string reference)
	{
		
		if (string.IsNullOrEmpty(reference))
		{
			Debug.LogWarning("empty reference.");
		}
		
		if (xmlNodeLUT==null)
		{
			xmlNodeLUT = new Dictionary<string, XmlNode>();
		}
		
		xmlNodeLUT[reference] = node;
	}
	
	public static XmlNode XmlRetrieveNode(string reference)
	{
		
		if (string.IsNullOrEmpty(reference))
		{
			Debug.LogWarning("empty reference.");
		}
		if (xmlNodeLUT==null)
		{
			return null;
		}
		
		if (!xmlNodeLUT.ContainsKey(reference))
		{
			return null;
		}
		return xmlNodeLUT[reference];
	}

	public static void XmlStoreNodeList(XmlNodeList nodeList,string reference)
	{
		
		if (string.IsNullOrEmpty(reference))
		{
			Debug.LogWarning("empty reference.");
		}
		
		if (xmlNodeListLUT==null)
		{
			xmlNodeListLUT = new Dictionary<string, XmlNodeList>();
		}
		
		xmlNodeListLUT[reference] = nodeList;
	}
	
	public static XmlNodeList XmlRetrieveNodeList(string reference)
	{
		
		if (string.IsNullOrEmpty(reference))
		{
			Debug.LogWarning("empty reference.");
		}
		return xmlNodeListLUT[reference];
	}
	
	
	#endregion Memory Slots
	
	
	public static string lastError = "";
	
	public static XmlNode StringToXmlNode(string content)
	{
			XmlDocument xmlDoc = new XmlDocument();
			try{
				xmlDoc.LoadXml(content);
			}catch(XmlException e)
			{
				lastError = e.Message;
				return null;
			}
			return xmlDoc.DocumentElement as XmlNode;
	}
	
	public static string XmlNodeListToString(XmlNodeList nodeList)
	{
		return XmlNodeListToString(nodeList, 2);
	}
	
	public static string XmlNodeListToString(XmlNodeList nodeList, int indentation)
	{
		
		if (nodeList==null)
		{
			return "-- NULL --";
		}
		
	    using (var sw = new StringWriter())
	    {
	        using (var xw = new XmlTextWriter(sw))
	        {
	            xw.Formatting = Formatting.Indented;
	            xw.Indentation = indentation;
				xw.WriteRaw("<result>");
				
				foreach(XmlNode node in nodeList)
				{
	            	node.WriteTo(xw);
				}
				xw.WriteRaw("</result>");
	        }
	        return sw.ToString();
	    }
	}
	
	public static string XmlNodeToString(XmlNode node)
	{
		return XmlNodeToString(node, 2);
	}
	
	public static string XmlNodeToString(XmlNode node, int indentation)
	{
		if (node==null)
		{
			return "-- NULL --";
		}
	    using (var sw = new StringWriter())
	    {
	        using (var xw = new XmlTextWriter(sw))
	        {
	            xw.Formatting = Formatting.Indented;
	            xw.Indentation = indentation;
	            node.WriteTo(xw);
	        }
	        return sw.ToString();
	    }
	}
}
