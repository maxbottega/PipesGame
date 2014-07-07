// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.
//
// © 2012 Jean Fabre http://www.fabrejean.net
//
// To Learn about xPath syntax: http://msdn.microsoft.com/en-us/library/ms256471.aspx
//
using UnityEngine;

using System.Xml;
using System.Xml.XPath;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("DataMaker Xml")]
	[Tooltip("Gets a node attributes and Properties, Properties are referenced from the node itself, so a '.' is prepended if you use xpath within the property string like ")]
	public class XmlGetNodeProperties : DataMakerXmlActions
	{

		[ActionSection("XML Source")]
		
		public FsmXmlSource xmlSource;
		
		[ActionSection("Result")]
		
		[CompoundArray("Node Properties", "Property", "Value")]
		public FsmString[] properties;
		
		[UIHint(UIHint.Variable)]
		public FsmString[] propertyValues;
		
		[ActionSection("Feedback")]
		public FsmEvent foundEvent;
		public FsmEvent notFoundEvent;
		public FsmEvent errorEvent;
		
		public override void Reset ()
		{
			xmlSource = null;

			properties = null;
			propertyValues = null;
			
			foundEvent = null;
			notFoundEvent = null;
			errorEvent = null;
		}

		public override void OnEnter ()
		{
			GetNodeProps();

			Finish ();
		}
		
		
		private void GetNodeProps()
		{

			XmlNode node = null;
			
			try{
				node = xmlSource.Value;
			}catch(XPathException e)
			{
				Debug.LogWarning(e.Message);
				Fsm.Event (errorEvent);
				return;
			}
			
			if (node != null) {
				int prop_i = 0;
				foreach (FsmString prop in properties) {
					propertyValues [prop_i].Value = GetNodeProperty(node,prop.Value);
					prop_i++;
				}
				
				Fsm.Event (foundEvent);
			} else {
				Fsm.Event (notFoundEvent);
			}
			
			Finish ();
		}

	}
}