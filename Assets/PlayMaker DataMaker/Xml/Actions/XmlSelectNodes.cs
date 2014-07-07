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
	[Tooltip("Gets nodes a xml text asset and an xpath query. Properties are referenced from the node itself, so a '.' is prepended if you use xpath within the property string like ")]
	public class XmlSelectNodes : DataMakerXmlActions
	{
		
		[ActionSection("XML Source")]
		
		public FsmXmlSource xmlSource;
		
		[ActionSection("xPath Query")]
		
		[RequiredField]
		public FsmString xPathQuery;
		[Tooltip("Author defined variables to inject into the xPathQuery using '_0_', '_1_' '_i_' etc to reference each variable")]
		public FsmString[] xPathVariables;
		
		[ActionSection("Result")]
		
		[Tooltip("The result of the  xPathQuery, wrapped into a 'result' node, so that it's resuable and a valid xml")]
		[UIHint(UIHint.Variable)]
		public FsmString xmlResult;
		
		[Tooltip("The number of entries found for the xPathQuery")]
		[UIHint(UIHint.Variable)]
		public FsmInt nodeCount;

		public FsmEvent foundEvent;
		public FsmEvent notFoundEvent;
		public FsmEvent errorEvent;
		
		public override void Reset ()
		{
			xmlSource = null;
			xPathQuery = null;
			xPathVariables = null;
			nodeCount = null;
			
			xmlResult = null;
			
			foundEvent = null;
			notFoundEvent = null;
			errorEvent = null;
			
		}

		public override void OnEnter ()
		{
			SelectNodeList();

			Finish ();
		}
		
		
		private void SelectNodeList ()
		{
			
			string xPathString = xPathQuery.Value;
			
			int i = 0;
			foreach (FsmString xPathVar in xPathVariables) {
				xPathString = xPathString.Replace ("_" + i + "_", xPathVar.Value);
				i++;
			}
			
			Debug.Log (xPathString);
			XmlNodeList nodeList =null;
			
			if (xmlSource.Value ==null)
			{
				Debug.LogWarning("XMl source is empty, or likely invalid");
				
				Fsm.Event (errorEvent);
				return;
			}
			
			try{
				nodeList = xmlSource.Value.SelectNodes(xPathString);
			}catch(XPathException e)
			{
				Debug.LogWarning(e.Message);
				Fsm.Event (errorEvent);
				return;
			}
			
			if (nodeList != null) {
				
				nodeCount.Value = nodeList.Count;
				
				if (nodeList.Count==0)
				{
					Fsm.Event (notFoundEvent);
					return;
				}
				
				if (!xmlResult.IsNone)
				{
					xmlResult.Value = DataMakerXmlUtils.XmlNodeListToString(nodeList);
				//	Debug.Log(xmlResult.Value);
				}
				
				Fsm.Event (foundEvent);
			} else {
				Fsm.Event (notFoundEvent);
			}
			
		}
		
	}
}