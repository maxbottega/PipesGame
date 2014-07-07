// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.
//
// To Learn about xPath syntax: http://msdn.microsoft.com/en-us/library/ms256471.aspx
//
using UnityEngine;

using System.Xml;
using System.Xml.XPath;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("DataMaker Xml")]
	[Tooltip("Gets a node attributes and cdata from a xml text asset and an xpath query. Properties are referenced from the node itself, so a '.' is prepended if you use xpath within the property string like ")]
	public class XmlSelectSingleNode : DataMakerXmlActions
	{
		
		[ActionSection("XML Source")]
		
		public FsmXmlSource xmlSource;
		
		[ActionSection("xPath Query")]
		
		[RequiredField]
		public FsmString xPathQuery;
		[Tooltip("Author defined variables to inject into the xPathQuery using '_0_', '_1_' '_i_' etc to reference each variable")]
		public FsmString[] xPathVariables;
		
		[ActionSection("Result")]
		
		[UIHint(UIHint.Variable)]
		public FsmString xmlResult;
		
		public FsmString storeReference;
		

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
			
			xPathQuery = null;
			xPathVariables = null;
			
			xmlResult = null;

			properties = null;
			propertyValues = null;
			
			foundEvent = null;
			notFoundEvent = null;
			errorEvent = null;
		}

		public override void OnEnter ()
		{

			SelectSingleNode();

			Finish ();
		}
		
		
		private void SelectSingleNode ()
		{
			
			string xPathString = xPathQuery.Value;
			
			int i = 0;
			foreach (FsmString xPathVar in xPathVariables) {
				xPathString = xPathString.Replace ("_" + i + "_", xPathVar.Value);
				i++;
			}
			
			//Debug.Log (xPathString);
			
			if (xmlSource.Value ==null)
			{
				Debug.LogWarning("XMl source is empty, or likely invalid");
				
				Fsm.Event (errorEvent);
				return;
			}
			
			XmlNode node = null;
			
			try{
				node = xmlSource.Value.SelectSingleNode(xPathString);
			}catch(XPathException e)
			{
				Debug.LogWarning(e.Message);
				Fsm.Event (errorEvent);
				return;
			}
			
			if (node != null) {

				if (!xmlResult.IsNone)
				{
					xmlResult.Value = DataMakerXmlUtils.XmlNodeToString(node);
				}

				int prop_i = 0;
				foreach (FsmString prop in properties) {
					propertyValues[prop_i].Value = GetNodeProperty(node,prop.Value);
					prop_i++;
				}
				
				Fsm.Event (foundEvent);
			} else {
				Fsm.Event (notFoundEvent);
			}
			
			if (!string.IsNullOrEmpty(storeReference.Value))
			{
				DataMakerXmlUtils.XmlStoreNode(node,storeReference.Value);
			}
			
			
			Finish ();
		}
		
	}
}