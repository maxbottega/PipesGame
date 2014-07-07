using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	/* WEB FUNCTIONS */
	[ActionCategory("Easy Save 2")]
	[Tooltip("Saves an int to MySQL Server via ES2.php file. See moodkie.com/easysave/WebSetup.php for how to set up MySQL.")]
	public class UploadInt : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable we want to save.")]
		public FsmInt saveValue;
		[RequiredField]
		[Tooltip("The URL to our ES2.PHP file. See http://www.moodkie.com/easysave/WebSetup.php for more information on setting up ES2Web")]
		public FsmString urlToPHPFile = "http://www.mysite.com/ES2.php";
		[RequiredField]
		[Tooltip("The username that you have specified in your ES2.php file.")]
		public FsmString username = "ES2";
		[RequiredField]
		[Tooltip("The password that you have specified in your ES2.php file.")]
		public FsmString password = "65w84e4p994z3Oq";
		[RequiredField]
		[Tooltip("A unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file that we'll create to store our data. Leave as default if unsure.")]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("The Event to send if Upload succeeded.")]
		public FsmEvent isUploaded;
		[Tooltip("The event to send if Upload failed.")]
		public FsmEvent isError;
		[Tooltip("Where any errors thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorMessage = "";
		[Tooltip("Where any error codes thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorCode = "";
		
		private ES2Web web = null;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			saveValue = 0;	urlToPHPFile = "http://www.mysite.com/ES2.php";
			web = null;
			errorMessage = "";
			errorCode = "";
		}
		
		public override void OnEnter()
		{
			web = new ES2Web(urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile+"&webpassword="+password+"&webusername="+username);
			this.Fsm.Owner.StartCoroutine(web.Upload(saveValue.Value));
			Log("Uploading to "+urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile);
		}
		
		public override void OnUpdate()
		{
			if(web.isError)
			{
				errorMessage.Value = web.error;
				errorCode.Value = web.errorCode;
				Fsm.Event(isError);
			}
			else if(web.isDone)
				Fsm.Event(isUploaded);
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Downloads an int from MySQL Server via ES2.php file. See moodkie.com/easysave/WebSetup.php for how to set up MySQL.")]
	public class DownloadInt : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable we want to load our data into.")]
		public FsmInt loadValue = 0;
		[RequiredField]
		[Tooltip("The URL to our ES2.PHP file. See http://www.moodkie.com/easysave/WebSetup.php for more information on setting up ES2Web")]
		public FsmString urlToPHPFile = "http://www.mysite.com/ES2.php";
		[RequiredField]
		[Tooltip("The username that you have specified in your ES2.php file.")]
		public FsmString username = "ES2";
		[RequiredField]
		[Tooltip("The password that you have specified in your ES2.php file.")]
		public FsmString password = "65w84e4p994z3Oq";
		[RequiredField]
		[Tooltip("The unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("The name of the local file we want to create to store our data. Leave blank if you don't want to store data locally.")]
		public FsmString localFile = "";
		[Tooltip("The Event to send if Download succeeded.")]
		public FsmEvent isDownloaded;
		[Tooltip("The event to send if Download failed.")]
		public FsmEvent isError;
		[Tooltip("Where any errors thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorMessage = "";
		[Tooltip("Where any error codes thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorCode = "";
		
		private ES2Web web = null;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			localFile = "";
			loadValue = 0;
			urlToPHPFile = "http://www.mysite.com/ES2.php";
			web = null;
			errorMessage = "";
			errorCode = "";
		}
		
		public override void OnEnter()
		{
			web = new ES2Web(urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile+"&webpassword="+password+"&webusername="+username);
			this.Fsm.Owner.StartCoroutine(web.Download());
			Log("Downloading from "+urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile);
		}
		
		public override void OnUpdate()
		{
			if(web.isError)
			{
				errorMessage.Value = web.error;
				errorCode.Value = web.errorCode;
				Fsm.Event(isError);
			}
			else if(web.isDone)
			{
				Fsm.Event(isDownloaded);
				loadValue.Value = web.Load<int>(uniqueTag.Value);
				if(localFile.Value != "")
					web.SaveToFile(localFile.Value);
			}
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Saves an int to MySQL Server via ES2.php file. See moodkie.com/easysave/WebSetup.php for how to set up MySQL.")]
	public class UploadFloat : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable we want to save.")]
		public FsmFloat saveValue;
		[RequiredField]
		[Tooltip("The URL to our ES2.PHP file. See http://www.moodkie.com/easysave/WebSetup.php for more information on setting up ES2Web")]
		public FsmString urlToPHPFile = "http://www.mysite.com/ES2.php";
		[RequiredField]
		[Tooltip("The username that you have specified in your ES2.php file.")]
		public FsmString username = "ES2";
		[RequiredField]
		[Tooltip("The password that you have specified in your ES2.php file.")]
		public FsmString password = "65w84e4p994z3Oq";
		[RequiredField]
		[Tooltip("A unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file that we'll create to store our data. Leave as default if unsure.")]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("The Event to send if Upload succeeded.")]
		public FsmEvent isUploaded;
		[Tooltip("The event to send if Upload failed.")]
		public FsmEvent isError;
		[Tooltip("Where any errors thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorMessage = "";
		[Tooltip("Where any error codes thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorCode = "";
		
		private ES2Web web = null;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			saveValue = null;
			urlToPHPFile = "http://www.mysite.com/ES2.php";
			web = null;
			errorMessage = "";
			errorCode = "";
		}
		
		public override void OnEnter()
		{
			web = new ES2Web(urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile+"&webpassword="+password+"&webusername="+username);
			this.Fsm.Owner.StartCoroutine(web.Upload(saveValue.Value));
			Log("Uploading to "+urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile);
		}
		
		public override void OnUpdate()
		{
			if(web.isError)
			{
				errorMessage.Value = web.error;
				errorCode.Value = web.errorCode;
				Fsm.Event(isError);
			}
			else if(web.isDone)
				Fsm.Event(isUploaded);
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Downloads a float from MySQL Server via ES2.php file. See moodkie.com/easysave/WebSetup.php for how to set up MySQL.")]
	public class DownloadFloat: FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable we want to load our data into.")]
		public FsmFloat loadValue;
		[RequiredField]
		[Tooltip("The URL to our ES2.PHP file. See http://www.moodkie.com/easysave/WebSetup.php for more information on setting up ES2Web")]
		public FsmString urlToPHPFile = "http://www.mysite.com/ES2.php";
		[RequiredField]
		[Tooltip("The username that you have specified in your ES2.php file.")]
		public FsmString username = "ES2";
		[RequiredField]
		[Tooltip("The password that you have specified in your ES2.php file.")]
		public FsmString password = "65w84e4p994z3Oq";
		[RequiredField]
		[Tooltip("The unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("The name of the local file we want to create to store our data. Leave blank if you don't want to store data locally.")]
		public FsmString localFile = "";
		[Tooltip("The Event to send if Download succeeded.")]
		public FsmEvent isDownloaded;
		[Tooltip("The event to send if Download failed.")]
		public FsmEvent isError;
		[Tooltip("Where any errors thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorMessage = "";
		[Tooltip("Where any error codes thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorCode = "";
		
		private ES2Web web = null;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			loadValue = null;
			urlToPHPFile = "http://www.mysite.com/ES2.php";
			web = null;
			errorMessage = "";
			errorCode = "";
		}
		
		public override void OnEnter()
		{
			web = new ES2Web(urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile+"&webpassword="+password+"&webusername="+username);
			this.Fsm.Owner.StartCoroutine(web.Download());
			Log("Downloading from "+urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile);
		}
		
		public override void OnUpdate()
		{
			if(web.isError)
			{
				errorMessage.Value = web.error;
				errorCode.Value = web.errorCode;
				Fsm.Event(isError);
			}
			else if(web.isDone)
			{
				Fsm.Event(isDownloaded);
				loadValue.Value = web.Load<float>(uniqueTag.Value);
				if(localFile.Value != "")
					web.SaveToFile(localFile.Value);
			}
		}
	}
	
		[ActionCategory("Easy Save 2")]
	[Tooltip("Saves a bool to MySQL Server via ES2.php file. See moodkie.com/easysave/WebSetup.php for how to set up MySQL.")]
	public class UploadBool : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable we want to save.")]
		public FsmBool saveValue;
		[RequiredField]
		[Tooltip("The URL to our ES2.PHP file. See http://www.moodkie.com/easysave/WebSetup.php for more information on setting up ES2Web")]
		public FsmString urlToPHPFile = "http://www.mysite.com/ES2.php";
		[RequiredField]
		[Tooltip("The username that you have specified in your ES2.php file.")]
		public FsmString username = "ES2";
		[RequiredField]
		[Tooltip("The password that you have specified in your ES2.php file.")]
		public FsmString password = "65w84e4p994z3Oq";
		[RequiredField]
		[Tooltip("A unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file that we'll create to store our data. Leave as default if unsure.")]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("The Event to send if Upload succeeded.")]
		public FsmEvent isUploaded;
		[Tooltip("The event to send if Upload failed.")]
		public FsmEvent isError;
		[Tooltip("Where any errors thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorMessage = "";
		[Tooltip("Where any error codes thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorCode = "";
		
		private ES2Web web = null;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			saveValue = null;
			urlToPHPFile = "http://www.mysite.com/ES2.php";
			web = null;
			errorMessage = "";
			errorCode = "";
		}
		
		public override void OnEnter()
		{
			web = new ES2Web(urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile+"&webpassword="+password+"&webusername="+username);
			this.Fsm.Owner.StartCoroutine(web.Upload(saveValue.Value));
			Log("Uploading to "+urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile);
		}
		
		public override void OnUpdate()
		{
			if(web.isError)
			{
				errorMessage.Value = web.error;
				errorCode.Value = web.errorCode;
				Fsm.Event(isError);
			}
			else if(web.isDone)
				Fsm.Event(isUploaded);
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Downloads a bool from MySQL Server via ES2.php file. See moodkie.com/easysave/WebSetup.php for how to set up MySQL.")]
	public class DownloadBool : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable we want to load our data into.")]
		public FsmBool loadValue;
		[RequiredField]
		[Tooltip("The URL to our ES2.PHP file. See http://www.moodkie.com/easysave/WebSetup.php for more information on setting up ES2Web")]
		public FsmString urlToPHPFile = "http://www.mysite.com/ES2.php";
		[RequiredField]
		[Tooltip("The username that you have specified in your ES2.php file.")]
		public FsmString username = "ES2";
		[RequiredField]
		[Tooltip("The password that you have specified in your ES2.php file.")]
		public FsmString password = "65w84e4p994z3Oq";
		[RequiredField]
		[Tooltip("The unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("The name of the local file we want to create to store our data. Leave blank if you don't want to store data locally.")]
		public FsmString localFile = "";
		[Tooltip("The Event to send if Download succeeded.")]
		public FsmEvent isDownloaded;
		[Tooltip("The event to send if Download failed.")]
		public FsmEvent isError;
		[Tooltip("Where any errors thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorMessage = "";
		[Tooltip("Where any error codes thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorCode = "";
		
		private ES2Web web = null;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			loadValue = null;
			urlToPHPFile = "http://www.mysite.com/ES2.php";
			web = null;
			errorMessage = "";
			errorCode = "";
		}
		
		public override void OnEnter()
		{
			web = new ES2Web(urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile+"&webpassword="+password+"&webusername="+username);
			this.Fsm.Owner.StartCoroutine(web.Download());
			Log("Downloading from "+urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile);
		}
		
		public override void OnUpdate()
		{
			if(web.isError)
			{
				errorMessage.Value = web.error;
				errorCode.Value = web.errorCode;
				Fsm.Event(isError);
			}
			else if(web.isDone)
			{
				Fsm.Event(isDownloaded);
				loadValue.Value = web.Load<bool>(uniqueTag.Value);
				if(localFile.Value != "")
					web.SaveToFile(localFile.Value);
			}
		}
	}
	
		[ActionCategory("Easy Save 2")]
	[Tooltip("Saves a string to MySQL Server via ES2.php file. See moodkie.com/easysave/WebSetup.php for how to set up MySQL.")]
	public class UploadString : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable we want to save.")]
		public FsmString saveValue;
		[RequiredField]
		[Tooltip("The URL to our ES2.PHP file. See http://www.moodkie.com/easysave/WebSetup.php for more information on setting up ES2Web")]
		public FsmString urlToPHPFile = "http://www.mysite.com/ES2.php";
		[RequiredField]
		[Tooltip("The username that you have specified in your ES2.php file.")]
		public FsmString username = "ES2";
		[RequiredField]
		[Tooltip("The password that you have specified in your ES2.php file.")]
		public FsmString password = "65w84e4p994z3Oq";
		[RequiredField]
		[Tooltip("A unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file that we'll create to store our data. Leave as default if unsure.")]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("The Event to send if Upload succeeded.")]
		public FsmEvent isUploaded;
		[Tooltip("The event to send if Upload failed.")]
		public FsmEvent isError;
		[Tooltip("Where any errors thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorMessage = "";
		[Tooltip("Where any error codes thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorCode = "";
		
		private ES2Web web = null;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			saveValue = null;
			urlToPHPFile = "http://www.mysite.com/ES2.php";
			web = null;
			errorMessage = "";
			errorCode = "";
		}
		
		public override void OnEnter()
		{
			web = new ES2Web(urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile+"&webpassword="+password+"&webusername="+username);
			this.Fsm.Owner.StartCoroutine(web.Upload(saveValue.Value));
			Log("Uploading to "+urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile);
		}
		
		public override void OnUpdate()
		{
			if(web.isError)
			{
				errorMessage.Value = web.error;
				errorCode.Value = web.errorCode;
				Fsm.Event(isError);
			}
			else if(web.isDone)
				Fsm.Event(isUploaded);
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Downloads a string from MySQL Server via ES2.php file. See moodkie.com/easysave/WebSetup.php for how to set up MySQL.")]
	public class DownloadString: FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable we want to load our data into.")]
		public FsmString loadValue;
		[RequiredField]
		[Tooltip("The URL to our ES2.PHP file. See http://www.moodkie.com/easysave/WebSetup.php for more information on setting up ES2Web")]
		public FsmString urlToPHPFile = "http://www.mysite.com/ES2.php";
		[RequiredField]
		[Tooltip("The username that you have specified in your ES2.php file.")]
		public FsmString username = "ES2";
		[RequiredField]
		[Tooltip("The password that you have specified in your ES2.php file.")]
		public FsmString password = "65w84e4p994z3Oq";
		[RequiredField]
		[Tooltip("The unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("The name of the local file we want to create to store our data. Leave blank if you don't want to store data locally.")]
		public FsmString localFile = "";
		[Tooltip("The Event to send if Download succeeded.")]
		public FsmEvent isDownloaded;
		[Tooltip("The event to send if Download failed.")]
		public FsmEvent isError;
		[Tooltip("Where any errors thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorMessage = "";
		[Tooltip("Where any error codes thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorCode = "";
		
		private ES2Web web = null;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			loadValue = null;
			urlToPHPFile = "http://www.mysite.com/ES2.php";
			web = null;
			errorMessage = "";
			errorCode = "";
		}
		
		public override void OnEnter()
		{
			web = new ES2Web(urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile+"&webpassword="+password+"&webusername="+username);
			this.Fsm.Owner.StartCoroutine(web.Download());
			Log("Downloading from "+urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile);
		}
		
		public override void OnUpdate()
		{
			if(web.isError)
			{
				errorMessage.Value = web.error;
				errorCode.Value = web.errorCode;
				Fsm.Event(isError);
			}
			else if(web.isDone)
			{
				Fsm.Event(isDownloaded);
				loadValue.Value = web.Load<string>(uniqueTag.Value);
				if(localFile.Value != "")
					web.SaveToFile(localFile.Value);
			}
		}
	}
	
		[ActionCategory("Easy Save 2")]
	[Tooltip("Saves a Vector2 to MySQL Server via ES2.php file. See moodkie.com/easysave/WebSetup.php for how to set up MySQL.")]
	public class UploadVector2 : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable we want to save.")]
		public FsmVector2 saveValue;
		[RequiredField]
		[Tooltip("The URL to our ES2.PHP file. See http://www.moodkie.com/easysave/WebSetup.php for more information on setting up ES2Web")]
		public FsmString urlToPHPFile = "http://www.mysite.com/ES2.php";
		[RequiredField]
		[Tooltip("The username that you have specified in your ES2.php file.")]
		public FsmString username = "ES2";
		[RequiredField]
		[Tooltip("The password that you have specified in your ES2.php file.")]
		public FsmString password = "65w84e4p994z3Oq";
		[RequiredField]
		[Tooltip("A unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file that we'll create to store our data. Leave as default if unsure.")]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("The Event to send if Upload succeeded.")]
		public FsmEvent isUploaded;
		[Tooltip("The event to send if Upload failed.")]
		public FsmEvent isError;
		[Tooltip("Where any errors thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorMessage = "";
		[Tooltip("Where any error codes thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorCode = "";
		
		private ES2Web web = null;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			saveValue = null;
			urlToPHPFile = "http://www.mysite.com/ES2.php";
			web = null;
			errorMessage = "";
			errorCode = "";
		}
		
		public override void OnEnter()
		{
			web = new ES2Web(urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile+"&webpassword="+password+"&webusername="+username);
			this.Fsm.Owner.StartCoroutine(web.Upload(saveValue.Value));
			Log("Uploading to "+urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile);
		}
		
		public override void OnUpdate()
		{
			if(web.isError)
			{
				errorMessage.Value = web.error;
				errorCode.Value = web.errorCode;
				Fsm.Event(isError);
			}
			else if(web.isDone)
				Fsm.Event(isUploaded);
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Downloads a Vector2 from MySQL Server via ES2.php file. See moodkie.com/easysave/WebSetup.php for how to set up MySQL.")]
	public class DownloadVector2: FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable we want to load our data into.")]
		public FsmVector2 loadValue;
		[RequiredField]
		[Tooltip("The URL to our ES2.PHP file. See http://www.moodkie.com/easysave/WebSetup.php for more information on setting up ES2Web")]
		public FsmString urlToPHPFile = "http://www.mysite.com/ES2.php";
		[RequiredField]
		[Tooltip("The username that you have specified in your ES2.php file.")]
		public FsmString username = "ES2";
		[RequiredField]
		[Tooltip("The password that you have specified in your ES2.php file.")]
		public FsmString password = "65w84e4p994z3Oq";
		[RequiredField]
		[Tooltip("The unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("The name of the local file we want to create to store our data. Leave blank if you don't want to store data locally.")]
		public FsmString localFile = "";
		[Tooltip("The Event to send if Download succeeded.")]
		public FsmEvent isDownloaded;
		[Tooltip("The event to send if Download failed.")]
		public FsmEvent isError;
		[Tooltip("Where any errors thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorMessage = "";
		[Tooltip("Where any error codes thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorCode = "";
		
		private ES2Web web = null;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			loadValue = null;
			urlToPHPFile = "http://www.mysite.com/ES2.php";
			web = null;
			errorMessage = "";
			errorCode = "";
		}
		
		public override void OnEnter()
		{
			web = new ES2Web(urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile+"&webpassword="+password+"&webusername="+username);
			this.Fsm.Owner.StartCoroutine(web.Download());
			Log("Downloading from "+urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile);
		}
		
		public override void OnUpdate()
		{
			if(web.isError)
			{
				errorMessage.Value = web.error;
				errorCode.Value = web.errorCode;
				Fsm.Event(isError);
			}
			else if(web.isDone)
			{
				Fsm.Event(isDownloaded);
				loadValue.Value = web.Load<Vector2>(uniqueTag.Value);
				if(localFile.Value != "")
					web.SaveToFile(localFile.Value);
			}
		}
	}
	
				[ActionCategory("Easy Save 2")]
	[Tooltip("Saves a Vector3 to MySQL Server via ES2.php file. See moodkie.com/easysave/WebSetup.php for how to set up MySQL.")]
	public class UploadVector3 : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable we want to save.")]
		public FsmVector3 saveValue;
		[RequiredField]
		[Tooltip("The URL to our ES2.PHP file. See http://www.moodkie.com/easysave/WebSetup.php for more information on setting up ES2Web")]
		public FsmString urlToPHPFile = "http://www.mysite.com/ES2.php";
		[RequiredField]
		[Tooltip("The username that you have specified in your ES2.php file.")]
		public FsmString username = "ES2";
		[RequiredField]
		[Tooltip("The password that you have specified in your ES2.php file.")]
		public FsmString password = "65w84e4p994z3Oq";
		[RequiredField]
		[Tooltip("A unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file that we'll create to store our data. Leave as default if unsure.")]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("The Event to send if Upload succeeded.")]
		public FsmEvent isUploaded;
		[Tooltip("The event to send if Upload failed.")]
		public FsmEvent isError;
		[Tooltip("Where any errors thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorMessage = "";
		[Tooltip("Where any error codes thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorCode = "";
		
		private ES2Web web = null;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			saveValue = null;
			urlToPHPFile = "http://www.mysite.com/ES2.php";
			web = null;
			errorMessage = "";
			errorCode = "";
		}
		
		public override void OnEnter()
		{
			web = new ES2Web(urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile+"&webpassword="+password+"&webusername="+username);
			this.Fsm.Owner.StartCoroutine(web.Upload(saveValue.Value));
			Log("Uploading to "+urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile);
		}
		
		public override void OnUpdate()
		{
			if(web.isError)
			{
				errorMessage.Value = web.error;
				errorCode.Value = web.errorCode;
				Fsm.Event(isError);
			}
			else if(web.isDone)
				Fsm.Event(isUploaded);
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Downloads a Vector3 from MySQL Server via ES2.php file. See moodkie.com/easysave/WebSetup.php for how to set up MySQL.")]
	public class DownloadVector3: FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable we want to load our data into.")]
		public FsmVector3 loadValue;
		[RequiredField]
		[Tooltip("The URL to our ES2.PHP file. See http://www.moodkie.com/easysave/WebSetup.php for more information on setting up ES2Web")]
		public FsmString urlToPHPFile = "http://www.mysite.com/ES2.php";
		[RequiredField]
		[Tooltip("The username that you have specified in your ES2.php file.")]
		public FsmString username = "ES2";
		[RequiredField]
		[Tooltip("The password that you have specified in your ES2.php file.")]
		public FsmString password = "65w84e4p994z3Oq";
		[RequiredField]
		[Tooltip("The unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("The name of the local file we want to create to store our data. Leave blank if you don't want to store data locally.")]
		public FsmString localFile = "";
		[Tooltip("The Event to send if Download succeeded.")]
		public FsmEvent isDownloaded;
		[Tooltip("The event to send if Download failed.")]
		public FsmEvent isError;
		[Tooltip("Where any errors thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorMessage = "";
		[Tooltip("Where any error codes thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorCode = "";
		
		private ES2Web web = null;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			loadValue = null;
			urlToPHPFile = "http://www.mysite.com/ES2.php";
			web = null;
			errorMessage = "";
			errorCode = "";
		}
		
		public override void OnEnter()
		{
			web = new ES2Web(urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile+"&webpassword="+password+"&webusername="+username);
			this.Fsm.Owner.StartCoroutine(web.Download());
			Log("Downloading from "+urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile);
		}
		
		public override void OnUpdate()
		{
			if(web.isError)
			{
				errorMessage.Value = web.error;
				errorCode.Value = web.errorCode;
				Fsm.Event(isError);
			}
			else if(web.isDone)
			{
				Fsm.Event(isDownloaded);
				loadValue.Value = web.Load<Vector3>(uniqueTag.Value);
				if(localFile.Value != "")
					web.SaveToFile(localFile.Value);
			}
		}
	}
	
				[ActionCategory("Easy Save 2")]
	[Tooltip("Saves a Color to MySQL Server via ES2.php file. See moodkie.com/easysave/WebSetup.php for how to set up MySQL.")]
	public class UploadColor : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable we want to save.")]
		public FsmColor saveValue;
		[RequiredField]
		[Tooltip("The URL to our ES2.PHP file. See http://www.moodkie.com/easysave/WebSetup.php for more information on setting up ES2Web")]
		public FsmString urlToPHPFile = "http://www.mysite.com/ES2.php";
		[RequiredField]
		[Tooltip("The username that you have specified in your ES2.php file.")]
		public FsmString username = "ES2";
		[RequiredField]
		[Tooltip("The password that you have specified in your ES2.php file.")]
		public FsmString password = "65w84e4p994z3Oq";
		[RequiredField]
		[Tooltip("A unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file that we'll create to store our data. Leave as default if unsure.")]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("The Event to send if Upload succeeded.")]
		public FsmEvent isUploaded;
		[Tooltip("The event to send if Upload failed.")]
		public FsmEvent isError;
		[Tooltip("Where any errors thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorMessage = "";
		[Tooltip("Where any error codes thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorCode = "";
		
		private ES2Web web = null;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			saveValue = null;
			urlToPHPFile = "http://www.mysite.com/ES2.php";
			web = null;
			errorMessage = "";
			errorCode = "";
		}
		
		public override void OnEnter()
		{
			web = new ES2Web(urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile+"&webpassword="+password+"&webusername="+username);
			this.Fsm.Owner.StartCoroutine(web.Upload(saveValue.Value));
			Log("Uploading to "+urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile);
		}
		
		public override void OnUpdate()
		{
			if(web.isError)
			{
				errorMessage.Value = web.error;
				errorCode.Value = web.errorCode;
				Fsm.Event(isError);
			}
			else if(web.isDone)
				Fsm.Event(isUploaded);
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Downloads a Color from MySQL Server via ES2.php file. See moodkie.com/easysave/WebSetup.php for how to set up MySQL.")]
	public class DownloadColor: FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable we want to load our data into.")]
		public FsmColor loadValue;
		[RequiredField]
		[Tooltip("The URL to our ES2.PHP file. See http://www.moodkie.com/easysave/WebSetup.php for more information on setting up ES2Web")]
		public FsmString urlToPHPFile = "http://www.mysite.com/ES2.php";
		[RequiredField]
		[Tooltip("The username that you have specified in your ES2.php file.")]
		public FsmString username = "ES2";
		[RequiredField]
		[Tooltip("The password that you have specified in your ES2.php file.")]
		public FsmString password = "65w84e4p994z3Oq";
		[RequiredField]
		[Tooltip("The unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("The name of the local file we want to create to store our data. Leave blank if you don't want to store data locally.")]
		public FsmString localFile = "";
		[Tooltip("The Event to send if Download succeeded.")]
		public FsmEvent isDownloaded;
		[Tooltip("The event to send if Download failed.")]
		public FsmEvent isError;
		[Tooltip("Where any errors thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorMessage = "";
		[Tooltip("Where any error codes thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorCode = "";
		
		private ES2Web web = null;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			loadValue = null;
			urlToPHPFile = "http://www.mysite.com/ES2.php";
			web = null;
			errorMessage = "";
			errorCode = "";
		}
		
		public override void OnEnter()
		{
			web = new ES2Web(urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile+"&webpassword="+password+"&webusername="+username);
			this.Fsm.Owner.StartCoroutine(web.Download());
			Log("Downloading from "+urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile);
		}
		
		public override void OnUpdate()
		{
			if(web.isError)
			{
				errorMessage.Value = web.error;
				errorCode.Value = web.errorCode;
				Fsm.Event(isError);
			}
			else if(web.isDone)
			{
				Fsm.Event(isDownloaded);
				loadValue.Value = web.Load<Color>(uniqueTag.Value);
				if(localFile.Value != "")
					web.SaveToFile(localFile.Value);
			}
		}
	}
	
				[ActionCategory("Easy Save 2")]
	[Tooltip("Saves a Texture to MySQL Server via ES2.php file. See moodkie.com/easysave/WebSetup.php for how to set up MySQL.")]
	public class UploadTexture : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable we want to save.")]
		public FsmTexture saveValue;
		[RequiredField]
		[Tooltip("The URL to our ES2.PHP file. See http://www.moodkie.com/easysave/WebSetup.php for more information on setting up ES2Web")]
		public FsmString urlToPHPFile = "http://www.mysite.com/ES2.php";
		[RequiredField]
		[Tooltip("The username that you have specified in your ES2.php file.")]
		public FsmString username = "ES2";
		[RequiredField]
		[Tooltip("The password that you have specified in your ES2.php file.")]
		public FsmString password = "65w84e4p994z3Oq";
		[RequiredField]
		[Tooltip("A unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file that we'll create to store our data. Leave as default if unsure.")]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("The Event to send if Upload succeeded.")]
		public FsmEvent isUploaded;
		[Tooltip("The event to send if Upload failed.")]
		public FsmEvent isError;
		[Tooltip("Where any errors thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorMessage = "";
		[Tooltip("Where any error codes thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorCode = "";
		
		private ES2Web web = null;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			saveValue = null;
			urlToPHPFile = "http://www.mysite.com/ES2.php";
			web = null;
			errorMessage = "";
			errorCode = "";
		}
		
		public override void OnEnter()
		{
			web = new ES2Web(urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile+"&webpassword="+password+"&webusername="+username);
			this.Fsm.Owner.StartCoroutine(web.Upload(saveValue.Value));
			Log("Uploading to "+urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile);
		}
		
		public override void OnUpdate()
		{
			if(web.isError)
			{
				errorMessage.Value = web.error;
				errorCode.Value = web.errorCode;
				Fsm.Event(isError);
			}
			else if(web.isDone)
				Fsm.Event(isUploaded);
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Downloads a Texture from MySQL Server via ES2.php file. See moodkie.com/easysave/WebSetup.php for how to set up MySQL.")]
	public class DownloadTexture : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable we want to load our data into.")]
		public FsmTexture loadValue;
		[RequiredField]
		[Tooltip("The URL to our ES2.PHP file. See http://www.moodkie.com/easysave/WebSetup.php for more information on setting up ES2Web")]
		public FsmString urlToPHPFile = "http://www.mysite.com/ES2.php";
		[RequiredField]
		[Tooltip("The username that you have specified in your ES2.php file.")]
		public FsmString username = "ES2";
		[RequiredField]
		[Tooltip("The password that you have specified in your ES2.php file.")]
		public FsmString password = "65w84e4p994z3Oq";
		[RequiredField]
		[Tooltip("The unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("The name of the local file we want to create to store our data. Leave blank if you don't want to store data locally.")]
		public FsmString localFile = "";
		[Tooltip("The Event to send if Download succeeded.")]
		public FsmEvent isDownloaded;
		[Tooltip("The event to send if Download failed.")]
		public FsmEvent isError;
		[Tooltip("Where any errors thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorMessage = "";
		[Tooltip("Where any error codes thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorCode = "";
		
		private ES2Web web = null;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			loadValue = null;
			urlToPHPFile = "http://www.mysite.com/ES2.php";
			web = null;
			errorMessage = "";
			errorCode = "";
		}
		
		public override void OnEnter()
		{
			web = new ES2Web(urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile+"&webpassword="+password+"&webusername="+username);
			this.Fsm.Owner.StartCoroutine(web.Download());
			Log("Downloading from "+urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile);
		}
		
		public override void OnUpdate()
		{
			if(web.isError)
			{
				errorMessage.Value = web.error;
				errorCode.Value = web.errorCode;
				Fsm.Event(isError);
			}
			else if(web.isDone)
			{
				Fsm.Event(isDownloaded);
				loadValue.Value = web.Load<Texture2D>(uniqueTag.Value);
				if(localFile.Value != "")
					web.SaveToFile(localFile.Value);
			}
		}
	}
	
				[ActionCategory("Easy Save 2")]
	[Tooltip("Saves a Quaternion to MySQL Server via ES2.php file. See moodkie.com/easysave/WebSetup.php for how to set up MySQL.")]
	public class UploadQuaternion : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable we want to save.")]
		public FsmQuaternion saveValue;
		[RequiredField]
		[Tooltip("The URL to our ES2.PHP file. See http://www.moodkie.com/easysave/WebSetup.php for more information on setting up ES2Web")]
		public FsmString urlToPHPFile = "http://www.mysite.com/ES2.php";
		[RequiredField]
		[Tooltip("The username that you have specified in your ES2.php file.")]
		public FsmString username = "ES2";
		[RequiredField]
		[Tooltip("The password that you have specified in your ES2.php file.")]
		public FsmString password = "65w84e4p994z3Oq";
		[RequiredField]
		[Tooltip("A unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file that we'll create to store our data. Leave as default if unsure.")]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("The Event to send if Upload succeeded.")]
		public FsmEvent isUploaded;
		[Tooltip("The event to send if Upload failed.")]
		public FsmEvent isError;
		[Tooltip("Where any errors thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorMessage = "";
		[Tooltip("Where any error codes thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorCode = "";
		
		private ES2Web web = null;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			saveValue = null;
			urlToPHPFile = "http://www.mysite.com/ES2.php";
			web = null;
			errorMessage = "";
			errorCode = "";
		}
		
		public override void OnEnter()
		{
			web = new ES2Web(urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile+"&webpassword="+password+"&webusername="+username);
			this.Fsm.Owner.StartCoroutine(web.Upload(saveValue.Value));
			Log("Uploading to "+urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile);
		}
		
		public override void OnUpdate()
		{
			if(web.isError)
			{
				errorMessage.Value = web.error;
				errorCode.Value = web.errorCode;
				Fsm.Event(isError);
			}
			else if(web.isDone)
				Fsm.Event(isUploaded);
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Downloads a Quaternion from MySQL Server via ES2.php file. See moodkie.com/easysave/WebSetup.php for how to set up MySQL.")]
	public class DownloadQuaternion : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable we want to load our data into.")]
		public FsmQuaternion loadValue;
		[RequiredField]
		[Tooltip("The URL to our ES2.PHP file. See http://www.moodkie.com/easysave/WebSetup.php for more information on setting up ES2Web")]
		public FsmString urlToPHPFile = "http://www.mysite.com/ES2.php";
		[RequiredField]
		[Tooltip("The username that you have specified in your ES2.php file.")]
		public FsmString username = "ES2";
		[RequiredField]
		[Tooltip("The password that you have specified in your ES2.php file.")]
		public FsmString password = "65w84e4p994z3Oq";
		[RequiredField]
		[Tooltip("The unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("The name of the local file we want to create to store our data. Leave blank if you don't want to store data locally.")]
		public FsmString localFile = "";
		[Tooltip("The Event to send if Download succeeded.")]
		public FsmEvent isDownloaded;
		[Tooltip("The event to send if Download failed.")]
		public FsmEvent isError;
		[Tooltip("Where any errors thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorMessage = "";
		[Tooltip("Where any error codes thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorCode = "";
		
		private ES2Web web = null;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			loadValue = null;
			urlToPHPFile = "http://www.mysite.com/ES2.php";
			web = null;
			errorMessage = "";
			errorCode = "";
		}
		
		public override void OnEnter()
		{
			web = new ES2Web(urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile+"&webpassword="+password+"&webusername="+username);
			this.Fsm.Owner.StartCoroutine(web.Download());
			Log("Downloading from "+urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile);
		}
		
		public override void OnUpdate()
		{
			if(web.isError)
			{
				errorMessage.Value = web.error;
				errorCode.Value = web.errorCode;
				Fsm.Event(isError);
			}
			else if(web.isDone)
			{
				Fsm.Event(isDownloaded);
				loadValue.Value = web.Load<Quaternion>(uniqueTag.Value);
				if(localFile.Value != "")
					web.SaveToFile(localFile.Value);
			}
		}
	}
	
				[ActionCategory("Easy Save 2")]
	[Tooltip("Saves an AudioClip to MySQL Server via ES2.php file. See moodkie.com/easysave/WebSetup.php for how to set up MySQL.")]
	public class UploadAudioClip : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable we want to save.")]
		public FsmObject saveValue;
		[RequiredField]
		[Tooltip("The URL to our ES2.PHP file. See http://www.moodkie.com/easysave/WebSetup.php for more information on setting up ES2Web")]
		public FsmString urlToPHPFile = "http://www.mysite.com/ES2.php";
		[RequiredField]
		[Tooltip("The username that you have specified in your ES2.php file.")]
		public FsmString username = "ES2";
		[RequiredField]
		[Tooltip("The password that you have specified in your ES2.php file.")]
		public FsmString password = "65w84e4p994z3Oq";
		[RequiredField]
		[Tooltip("A unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file that we'll create to store our data. Leave as default if unsure.")]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("The Event to send if Upload succeeded.")]
		public FsmEvent isUploaded;
		[Tooltip("The event to send if Upload failed.")]
		public FsmEvent isError;
		[Tooltip("Where any errors thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorMessage = "";
		[Tooltip("Where any error codes thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorCode = "";
		
		private ES2Web web = null;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			saveValue = null;
			urlToPHPFile = "http://www.mysite.com/ES2.php";
			web = null;
			errorMessage = "";
			errorCode = "";
		}
		
		public override void OnEnter()
		{
			web = new ES2Web(urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile+"&webpassword="+password+"&webusername="+username);
			this.Fsm.Owner.StartCoroutine(web.Upload(saveValue.Value as AudioClip));
			Log("Uploading to "+urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile);
		}
		
		public override void OnUpdate()
		{
			if(web.isError)
			{
				errorMessage.Value = web.error;
				errorCode.Value = web.errorCode;
				Fsm.Event(isError);
			}
			else if(web.isDone)
				Fsm.Event(isUploaded);
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Downloads an AudioClip from MySQL Server via ES2.php file. See moodkie.com/easysave/WebSetup.php for how to set up MySQL.")]
	public class DownloadAudioClip: FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable we want to load our data into.")]
		public FsmObject loadValue;
		[RequiredField]
		[Tooltip("The URL to our ES2.PHP file. See http://www.moodkie.com/easysave/WebSetup.php for more information on setting up ES2Web")]
		public FsmString urlToPHPFile = "http://www.mysite.com/ES2.php";
		[RequiredField]
		[Tooltip("The username that you have specified in your ES2.php file.")]
		public FsmString username = "ES2";
		[RequiredField]
		[Tooltip("The password that you have specified in your ES2.php file.")]
		public FsmString password = "65w84e4p994z3Oq";
		[RequiredField]
		[Tooltip("The unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("The name of the local file we want to create to store our data. Leave blank if you don't want to store data locally.")]
		public FsmString localFile = "";
		[Tooltip("The Event to send if Download succeeded.")]
		public FsmEvent isDownloaded;
		[Tooltip("The event to send if Download failed.")]
		public FsmEvent isError;
		[Tooltip("Where any errors thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorMessage = "";
		[Tooltip("Where any error codes thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorCode = "";
		
		private ES2Web web = null;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			loadValue = null;
			urlToPHPFile = "http://www.mysite.com/ES2.php";
			web = null;
			errorMessage = "";
			errorCode = "";
		}
		
		public override void OnEnter()
		{
			web = new ES2Web(urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile+"&webpassword="+password+"&webusername="+username);
			this.Fsm.Owner.StartCoroutine(web.Download());
			Log("Downloading from "+urlToPHPFile+"?tag="+uniqueTag+"&webfilename="+saveFile);
		}
		
		public override void OnUpdate()
		{
			if(web.isError)
			{
				errorMessage.Value = web.error;
				errorCode.Value = web.errorCode;
				Fsm.Event(isError);
			}
			else if(web.isDone)
			{
				Fsm.Event(isDownloaded);
				loadValue.Value = web.Load<AudioClip>(uniqueTag.Value);
				if(localFile.Value != "")
					web.SaveToFile(localFile.Value);
			}
		}
	}
	
	/* UTILITY FUNCTIONS */
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Checks if data Delete at the specified path.")]
	public class Delete : FsmStateAction
	{
		[Tooltip("The tag that we want to delete (Optional).")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The file we want to delete, or delete the tag from.")]
		public FsmString saveFile = "defaultES2File.txt";
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
		}
		
		public override void OnEnter()
		{
			string path;
			if(uniqueTag.Value != "")
				path = saveFile.Value+"?tag="+uniqueTag.Value;
			else
				path = saveFile.Value;
			ES2.Delete(path);
			Log("Deleted "+path);
			Finish();
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Checks if data exists at the specified path.")]
	public class Exists : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Event to send if this it does exist.")]
		public FsmEvent ifExists;
		[RequiredField]
		[Tooltip("The event to sent if it doesn't exist.")]
		public FsmEvent ifDoesNotExist;
		[Tooltip("The tag that we want to check for (Optional).")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The file we want to check the existence of.")]
		public FsmString saveFile = "defaultES2File.txt";
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
		}
		
		public override void OnEnter()
		{
			string path;
			if(uniqueTag.Value != "")
				path = saveFile.Value+"?tag="+uniqueTag.Value;
			else
				path = saveFile.Value;
			
			Log("Checked existence of "+path);
			
			if(ES2.Exists(path))
				Fsm.Event(ifExists);
			else
				Fsm.Event(ifDoesNotExist);
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Deletes data from MySQL Server via ES2.php file. See moodkie.com/easysave/WebSetup.php for how to set up MySQL.")]
	public class DeleteFromWeb : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The URL to our ES2.PHP file. See http://www.moodkie.com/easysave/WebSetup.php for more information on setting up ES2Web")]
		public FsmString urlToPHPFile = "http://www.mysite.com/ES2.php";
		[RequiredField]
		[Tooltip("The username that you have specified in your ES2.php file.")]
		public FsmString username = "ES2";
		[RequiredField]
		[Tooltip("The password that you have specified in your ES2.php file.")]
		public FsmString password = "65w84e4p994z3Oq";
		[RequiredField]
		[Tooltip("The tag which we want to delete. Leave blank if deleting entire file.")]
		public FsmString tag = "";
		[RequiredField]
		[Tooltip("The name of the file that we want to either delete, or delete a tag from.")]
		public FsmString file = "defaultES2File.txt";
		[Tooltip("The Event to send if Delete succeeded.")]
		public FsmEvent isDeleted;
		[Tooltip("The event to send if Delete failed.")]
		public FsmEvent isError;
		[Tooltip("Where any errors thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorMessage = "";
		[Tooltip("Where any error codes thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorCode = "";
		
		private ES2Web web = null;
		
		public override void Reset()
		{
			tag = "";
			file = "defaultES2File.txt";
			urlToPHPFile = "http://www.mysite.com/ES2.php";
			web = null;
			errorMessage = "";
			errorCode = "";
		}
		
		public override void OnEnter()
		{
			web = new ES2Web(urlToPHPFile+"?tag="+tag+"&webfilename="+file+"&webpassword="+password+"&webusername="+username);
			this.Fsm.Owner.StartCoroutine(web.Delete());
			Log("Web Deleting from "+urlToPHPFile+"?tag="+tag+"&webfilename="+file);
		}
		
		public override void OnUpdate()
		{
			if(web.isError)
			{
				errorMessage.Value = web.error;
				errorCode.Value = web.errorCode;
				Fsm.Event(isError);
			}
			else if(web.isDone)
				Fsm.Event(isDeleted);
		}
	}
	
	/* NON-COMPONENT TYPES */
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Saves an int.")]
	public class SaveInt : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable we want to save.")]
		public FsmInt saveValue;
		[RequiredField]
		[Tooltip("A unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file that we'll create to store our data.")]
		public FsmString saveFile = "defaultES2File.txt";
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			saveValue = 0;
		}
		
		public override void OnEnter()
		{
			ES2.Save(saveValue.Value, saveFile+"?tag="+uniqueTag);
			Log("Saved to "+saveFile+"?tag="+uniqueTag);
			Finish();
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Loads a previously saved float.")]
	public class LoadInt : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable where we'll load our data into.")]
		public FsmInt loadValue;
		[RequiredField]
		[Tooltip("The unique tag for the data we want to load.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file our data is stored in.")]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("Whether the data we are loading is stored in the Resources folder.")]
		public bool loadFromResources = false;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			loadValue = 0;
			loadFromResources = false;
		}
		
		public override void OnEnter()
		{	
			ES2Settings loadSettings = new ES2Settings();
			if(loadFromResources)
				loadSettings.saveLocation = ES2Settings.SaveLocation.Resources;
			
			loadValue.Value = ES2.Load<int>(saveFile+"?tag="+uniqueTag, loadSettings);
			Log("Loaded from "+saveFile+"?tag="+uniqueTag);
			Finish();
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Saves a float.")]
	public class SaveFloat : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable we want to save.")]
		public FsmFloat saveValue;
		[RequiredField]
		[Tooltip("A unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file that we'll create to store our data.")]
		public FsmString saveFile = "defaultES2File.txt";
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			saveValue = 0;}
		
		public override void OnEnter()
		{
			ES2.Save(saveValue.Value, saveFile+"?tag="+uniqueTag);
			Log("Saved to "+saveFile+"?tag="+uniqueTag);
			Finish();
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Loads a previously saved float.")]
	public class LoadFloat : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable where we'll load our data into.")]
		public FsmFloat loadValue;
		[RequiredField]
		[Tooltip("The unique tag for the data we want to load.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file our data is stored in.")]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("Whether the data we are loading is stored in the Resources folder.")]
		public bool loadFromResources = false;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			loadValue = 0;
			loadFromResources = false;
		}
		
		public override void OnEnter()
		{
			ES2Settings loadSettings = new ES2Settings();
			if(loadFromResources)
				loadSettings.saveLocation = ES2Settings.SaveLocation.Resources;
			
			loadValue.Value = ES2.Load<float>(saveFile+"?tag="+uniqueTag, loadSettings);
			Log("Loaded from "+saveFile+"?tag="+uniqueTag);
			Finish();
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Saves a bool.")]
	public class SaveBool : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable we want to save.")]
		public FsmBool saveValue;
		[RequiredField]
		[Tooltip("A unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file that we'll create to store our data.")]
		public FsmString saveFile = "defaultES2File.txt";
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			saveValue = null;
		}
		
		public override void OnEnter()
		{
			ES2.Save(saveValue.Value, saveFile+"?tag="+uniqueTag);
			Log("Saved to "+saveFile+"?tag="+uniqueTag);
			Finish();
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Loads a previously saved bool.")]
	public class LoadBool : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable where we'll load our data into.")]
		public FsmBool loadValue;
		[RequiredField]
		[Tooltip("The unique tag for the data we want to load.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file our data is stored in.")]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("Whether the data we are loading is stored in the Resources folder.")]
		public bool loadFromResources = false;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			loadValue = null;
			loadFromResources = false;
		}
		
		public override void OnEnter()
		{
			ES2Settings loadSettings = new ES2Settings();
			if(loadFromResources)
				loadSettings.saveLocation = ES2Settings.SaveLocation.Resources;
			
			loadValue.Value = ES2.Load<bool>(saveFile+"?tag="+uniqueTag, loadSettings);
			Log("Loaded from "+saveFile+"?tag="+uniqueTag);
			Finish();
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Saves a string.")]
	public class SaveString : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable we want to save.")]
		public FsmString saveValue;
		[RequiredField]
		[Tooltip("A unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file that we'll create to store our data.")]
		public FsmString saveFile = "defaultES2File.txt";
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			saveValue = null;
		}
		
		public override void OnEnter()
		{
			ES2.Save(saveValue.Value, saveFile+"?tag="+uniqueTag);
			Log("Saved to "+saveFile+"?tag="+uniqueTag);
			Finish();
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Loads a previously saved string.")]
	public class LoadString : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable where we'll load our data into.")]
		public FsmString loadValue;
		[RequiredField]
		[Tooltip("The unique tag for the data we want to load.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file our data is stored in.")]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("Whether the data we are loading is stored in the Resources folder.")]
		public bool loadFromResources = false;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			loadValue = null;
			loadFromResources = false;
		}
		
		public override void OnEnter()
		{
			ES2Settings loadSettings = new ES2Settings();
			if(loadFromResources)
				loadSettings.saveLocation = ES2Settings.SaveLocation.Resources;
			
			loadValue.Value = ES2.Load<string>(saveFile+"?tag="+uniqueTag, loadSettings);
			Log("Loaded from "+saveFile+"?tag="+uniqueTag);
			Finish();
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Saves a Vector2.")]
	public class SaveVector2 : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable we want to save.")]
		public FsmVector2 saveValue;
		[RequiredField]
		[Tooltip("A unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file that we'll create to store our data.")]
		public FsmString saveFile = "defaultES2File.txt";
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			saveValue = null;
		}
		
		public override void OnEnter()
		{
			ES2.Save(saveValue.Value, saveFile+"?tag="+uniqueTag);
			Log("Saved to "+saveFile+"?tag="+uniqueTag);
			Finish();
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Loads a previously saved Vector2.")]
	public class LoadVector2 : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable where we'll load our data into.")]
		public FsmVector2 loadValue;
		[RequiredField]
		[Tooltip("The unique tag for the data we want to load.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file our data is stored in.")]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("Whether the data we are loading is stored in the Resources folder.")]
		public bool loadFromResources = false;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			loadValue = null;
			loadFromResources = false;
		}
		
		public override void OnEnter()
		{
			ES2Settings loadSettings = new ES2Settings();
			if(loadFromResources)
				loadSettings.saveLocation = ES2Settings.SaveLocation.Resources;
			
			loadValue.Value = ES2.Load<Vector2>(saveFile+"?tag="+uniqueTag, loadSettings);
			Log("Loaded from "+saveFile+"?tag="+uniqueTag);
			Finish();
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Saves a Vector3.")]
	public class SaveVector3 : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable we want to save.")]
		public FsmVector3 saveValue;
		[RequiredField]
		[Tooltip("A unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file that we'll create to store our data.")]
		public FsmString saveFile = "defaultES2File.txt";
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			saveValue = null;
		}
		
		public override void OnEnter()
		{
			ES2.Save(saveValue.Value, saveFile+"?tag="+uniqueTag);
			Log("Saved to "+saveFile+"?tag="+uniqueTag);
			Finish();
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Loads a previously saved Vector3.")]
	public class LoadVector3 : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable where we'll load our data into.")]
		public FsmVector3 loadValue;
		[RequiredField]
		[Tooltip("The unique tag for the data we want to load.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file our data is stored in.")]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("Whether the data we are loading is stored in the Resources folder.")]
		public bool loadFromResources = false;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			loadValue = null;
			loadFromResources = false;
		}
		
		public override void OnEnter()
		{
			ES2Settings loadSettings = new ES2Settings();
			if(loadFromResources)
				loadSettings.saveLocation = ES2Settings.SaveLocation.Resources;
			
			loadValue.Value = ES2.Load<Vector3>(saveFile+"?tag="+uniqueTag, loadSettings);
			Log("Loaded from "+saveFile+"?tag="+uniqueTag);
			Finish();
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Saves a Color.")]
	public class SaveColor : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable we want to save.")]
		public FsmColor saveValue;
		[RequiredField]
		[Tooltip("A unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file that we'll create to store our data.")]
		public FsmString saveFile = "defaultES2File.txt";
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			saveValue = null;
		}
		
		public override void OnEnter()
		{
			ES2.Save(saveValue.Value, saveFile+"?tag="+uniqueTag);
			Log("Saved to "+saveFile+"?tag="+uniqueTag);
			Finish();
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Loads a previously saved Color.")]
	public class LoadColor : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable where we'll load our data into.")]
		public FsmColor loadValue;
		[RequiredField]
		[Tooltip("The unique tag for the data we want to load.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file our data is stored in.")]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("Whether the data we are loading is stored in the Resources folder.")]
		public bool loadFromResources = false;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			loadValue = null;
			loadFromResources = false;
		}
		
		public override void OnEnter()
		{
			ES2Settings loadSettings = new ES2Settings();
			if(loadFromResources)
				loadSettings.saveLocation = ES2Settings.SaveLocation.Resources;
			
			loadValue.Value = ES2.Load<Color>(saveFile+"?tag="+uniqueTag, loadSettings);
			Log("Loaded from "+saveFile+"?tag="+uniqueTag);
			Finish();
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Saves a Texture.")]
	public class SaveTexture : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable we want to save.")]
		public FsmTexture saveValue;
		[RequiredField]
		[Tooltip("A unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file that we'll create to store our data.")]
		public FsmString saveFile = "defaultES2File.txt";
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			saveValue = null;
		}
		
		public override void OnEnter()
		{
			ES2.Save((saveValue.Value) as Texture2D, saveFile+"?tag="+uniqueTag);
			Log("Saved to "+saveFile+"?tag="+uniqueTag);
			Finish();
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Loads a previously saved Texture.")]
	public class LoadTexture : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable where we'll load our data into.")]
		public FsmTexture loadValue;
		[RequiredField]
		[Tooltip("The unique tag for the data we want to load.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file our data is stored in.")]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("Whether the data we are loading is stored in the Resources folder.")]
		public bool loadFromResources = false;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			loadValue = null;
			loadFromResources = false;
		}
		
		public override void OnEnter()
		{
			ES2Settings loadSettings = new ES2Settings();
			if(loadFromResources)
				loadSettings.saveLocation = ES2Settings.SaveLocation.Resources;
			
			loadValue.Value = ES2.Load<Texture2D>(saveFile+"?tag="+uniqueTag, loadSettings) as Texture;
			Log("Loaded from "+saveFile+"?tag="+uniqueTag);
			Finish();
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Saves a Quaternion.")]
	public class SaveQuaternion : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable we want to save.")]
		public FsmQuaternion saveValue;
		[RequiredField]
		[Tooltip("A unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file that we'll create to store our data.")]
		public FsmString saveFile = "defaultES2File.txt";
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			saveValue = null;
		}
		
		public override void OnEnter()
		{
			ES2.Save(saveValue.Value, saveFile+"?tag="+uniqueTag);
			Log("Saved to "+saveFile+"?tag="+uniqueTag);
			Finish();
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Loads a previously saved Quaternion.")]
	public class LoadQuaternion : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable where we'll load our data into.")]
		public FsmQuaternion loadValue;
		[RequiredField]
		[Tooltip("The unique tag for the data we want to load.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file our data is stored in.")]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("Whether the data we are loading is stored in the Resources folder.")]
		public bool loadFromResources = false;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			loadValue = null;
			loadFromResources = false;
		}
		
		public override void OnEnter()
		{
			ES2Settings loadSettings = new ES2Settings();
			if(loadFromResources)
				loadSettings.saveLocation = ES2Settings.SaveLocation.Resources;
			
			loadValue.Value = ES2.Load<Quaternion>(saveFile+"?tag="+uniqueTag, loadSettings);
			Log("Loaded from "+saveFile+"?tag="+uniqueTag);
			Finish();
		}
	}
	
	/* COMPONENT TYPES */
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Saves a Transform.")]
	public class SaveTransform : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject containing the Transform we want to save.")]
		public FsmOwnerDefault saveValue;
		[RequiredField]
		[Tooltip("A unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file that we'll create to store our data.")]
		public FsmString saveFile = "defaultES2File.txt";
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
		}
		
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(saveValue);
			Transform component;
			
			if(go == null)
			{
				LogError("Could not save Transform. No GameObject has been specified.");
				Finish ();
				return;
			}
			
			if((component = go.GetComponent<Transform>()) == null)
			{
				LogError("Could not save Transform. GameObject does not contain a Transform.");
				Finish ();
				return;
			}

			ES2.Save(component, saveFile+"?tag="+uniqueTag);
			Log("Saved to "+saveFile+"?tag="+uniqueTag);
			Finish();
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Loads a previously saved Transform.")]
	public class LoadTransform : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject containing the Transform we want to load into.")]
		public FsmOwnerDefault loadValue;
		[RequiredField]
		[Tooltip("The unique tag for the data we want to load.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file our data is stored in.")]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("Whether the data we are loading is stored in the Resources folder.")]
		public bool loadFromResources = false;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			loadFromResources = false;
		}
		
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(loadValue);
			Transform component;
			
			if(go == null)
			{
				LogError("Could not save Transform. No GameObject has been specified.");
				Finish ();
				return;
			}
			
			if((component = go.GetComponent<Transform>()) == null)
			{
				LogError("Could not save Transform. GameObject does not contain a Transform.");
				Finish ();
				return;
			}
			
			ES2Settings loadSettings = new ES2Settings();
			if(loadFromResources)
				loadSettings.saveLocation = ES2Settings.SaveLocation.Resources;
			
			ES2.Load<Transform>(saveFile+"?tag="+uniqueTag, component, loadSettings);
			Log("Loaded from "+saveFile+"?tag="+uniqueTag);
			Finish();
		}
	}

	[ActionCategory("Easy Save 2")]
	[Tooltip("Saves a MeshCollider.")]
	public class SaveMeshCollider : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject containing the MeshCollider we want to save.")]
		public FsmOwnerDefault saveValue;
		[RequiredField]
		[Tooltip("A unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file that we'll create to store our data.")]
		public FsmString saveFile = "defaultES2File.txt";
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
		}
		
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(saveValue);
			MeshCollider component;
			
			if(go == null)
			{
				LogError("Could not save MeshCollider. No GameObject has been specified.");
				Finish ();
				return;
			}
			
			if((component = go.GetComponent<MeshCollider>()) == null)
			{
				LogError("Could not save MeshCollider. GameObject does not contain a MeshCollider.");
				Finish ();
				return;
			}

			ES2.Save(component, saveFile+"?tag="+uniqueTag);
			Log("Saved to "+saveFile+"?tag="+uniqueTag);
			Finish();
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Loads a previously saved MeshCollider.")]
	public class LoadMeshCollider : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject containing the MeshCollider we want to load into.")]
		public FsmOwnerDefault loadValue;
		[RequiredField]
		[Tooltip("The unique tag for the data we want to load.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file our data is stored in.")]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("Whether the data we are loading is stored in the Resources folder.")]
		public bool loadFromResources = false;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			loadFromResources = false;
		}
		
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(loadValue);
			MeshCollider component;
			
			if(go == null)
			{
				LogError("Could not save MeshCollider. No GameObject has been specified.");
				Finish ();
				return;
			}
			
			if((component = go.GetComponent<MeshCollider>()) == null)
			{
				LogError("Could not save MeshCollider. GameObject does not contain a MeshCollider.");
				Finish ();
				return;
			}
			
			ES2Settings loadSettings = new ES2Settings();
			if(loadFromResources)
				loadSettings.saveLocation = ES2Settings.SaveLocation.Resources;
			
			ES2.Load<MeshCollider>(saveFile+"?tag="+uniqueTag, component, loadSettings);
			Log("Loaded from "+saveFile+"?tag="+uniqueTag);
			Finish();
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Saves a BoxCollider.")]
	public class SaveBoxCollider : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject containing the BoxCollider we want to save.")]
		public FsmOwnerDefault saveValue;
		[RequiredField]
		[Tooltip("A unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file that we'll create to store our data.")]
		public FsmString saveFile = "defaultES2File.txt";
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
		}
		
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(saveValue);
			BoxCollider component;
			
			if(go == null)
			{
				LogError("Could not save BoxCollider. No GameObject has been specified.");
				Finish ();
				return;
			}
			
			if((component = go.GetComponent<BoxCollider>()) == null)
			{
				LogError("Could not save BoxCollider. GameObject does not contain a BoxCollider.");
				Finish ();
				return;
			}

			ES2.Save(component, saveFile+"?tag="+uniqueTag);
			Log("Saved to "+saveFile+"?tag="+uniqueTag);
			Finish();
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Loads a previously saved BoxCollider.")]
	public class LoadBoxCollider : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject containing the BoxCollider we want to load into.")]
		public FsmOwnerDefault loadValue;
		[RequiredField]
		[Tooltip("The unique tag for the data we want to load.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file our data is stored in.")]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("Whether the data we are loading is stored in the Resources folder.")]
		public bool loadFromResources = false;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			loadFromResources = false;
		}
		
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(loadValue);
			BoxCollider component;
			
			if(go == null)
			{
				LogError("Could not save BoxCollider. No GameObject has been specified.");
				Finish ();
				return;
			}
			
			if((component = go.GetComponent<BoxCollider>()) == null)
			{
				LogError("Could not save BoxCollider. GameObject does not contain a BoxCollider.");
				Finish ();
				return;
			}
			
			ES2Settings loadSettings = new ES2Settings();
			if(loadFromResources)
				loadSettings.saveLocation = ES2Settings.SaveLocation.Resources;
			
			ES2.Load<BoxCollider>(saveFile+"?tag="+uniqueTag, component, loadSettings);
			Log("Loaded from "+saveFile+"?tag="+uniqueTag);
			Finish();
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Saves a SphereCollider.")]
	public class SaveSphereCollider : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject containing the SphereCollider we want to save.")]
		public FsmOwnerDefault saveValue;
		[RequiredField]
		[Tooltip("A unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file that we'll create to store our data.")]
		public FsmString saveFile = "defaultES2File.txt";
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
		}
		
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(saveValue);
			SphereCollider component;
			
			if(go == null)
			{
				LogError("Could not save SphereCollider. No GameObject has been specified.");
				Finish ();
				return;
			}
			
			if((component = go.GetComponent<SphereCollider>()) == null)
			{
				LogError("Could not save SphereCollider. GameObject does not contain a SphereCollider.");
				Finish ();
				return;
			}

			ES2.Save(component, saveFile+"?tag="+uniqueTag);
			Log("Saved to "+saveFile+"?tag="+uniqueTag);
			Finish();
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Loads a previously saved SphereCollider.")]
	public class LoadSphereCollider : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject containing the SphereCollider we want to load into.")]
		public FsmOwnerDefault loadValue;
		[RequiredField]
		[Tooltip("The unique tag for the data we want to load.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file our data is stored in.")]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("Whether the data we are loading is stored in the Resources folder.")]
		public bool loadFromResources = false;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			loadFromResources = false;
		}
		
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(loadValue);
			SphereCollider component;
			
			if(go == null)
			{
				LogError("Could not save SphereCollider. No GameObject has been specified.");
				Finish ();
				return;
			}
			
			if((component = go.GetComponent<SphereCollider>()) == null)
			{
				LogError("Could not save SphereCollider. GameObject does not contain a SphereCollider.");
				Finish ();
				return;
			}
			
			ES2Settings loadSettings = new ES2Settings();
			if(loadFromResources)
				loadSettings.saveLocation = ES2Settings.SaveLocation.Resources;
			
			ES2.Load<SphereCollider>(saveFile+"?tag="+uniqueTag, component, loadSettings);
			Log("Loaded from "+saveFile+"?tag="+uniqueTag);
			Finish();
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Saves a CapsuleCollider.")]
	public class SaveCapsuleCollider : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject containing the CapsuleCollider we want to save.")]
		public FsmOwnerDefault saveValue;
		[RequiredField]
		[Tooltip("A unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file that we'll create to store our data.")]
		public FsmString saveFile = "defaultES2File.txt";
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
		}
		
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(saveValue);
			CapsuleCollider component;
			
			if(go == null)
			{
				LogError("Could not save CapsuleCollider. No GameObject has been specified.");
				Finish ();
				return;
			}
			
			if((component = go.GetComponent<CapsuleCollider>()) == null)
			{
				LogError("Could not save CapsuleCollider. GameObject does not contain a CapsuleCollider.");
				Finish ();
				return;
			}

			ES2.Save(component, saveFile+"?tag="+uniqueTag);
			Log("Saved to "+saveFile+"?tag="+uniqueTag);
			Finish();
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Loads a previously saved CapsuleCollider.")]
	public class LoadCapsuleCollider : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject containing the CapsuleCollider we want to load into.")]
		public FsmOwnerDefault loadValue;
		[RequiredField]
		[Tooltip("The unique tag for the data we want to load.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file our data is stored in.")]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("Whether the data we are loading is stored in the Resources folder.")]
		public bool loadFromResources = false;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			loadFromResources = false;
		}
		
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(loadValue);
			CapsuleCollider component;
			
			if(go == null)
			{
				LogError("Could not save CapsuleCollider. No GameObject has been specified.");
				Finish ();
				return;
			}
			
			if((component = go.GetComponent<CapsuleCollider>()) == null)
			{
				LogError("Could not save CapsuleCollider. GameObject does not contain a CapsuleCollider.");
				Finish ();
				return;
			}
			
			ES2Settings loadSettings = new ES2Settings();
			if(loadFromResources)
				loadSettings.saveLocation = ES2Settings.SaveLocation.Resources;
			
			ES2.Load<CapsuleCollider>(saveFile+"?tag="+uniqueTag, component, loadSettings);
			Log("Loaded from "+saveFile+"?tag="+uniqueTag);
			Finish();
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Saves an AudioClip.")]
	public class SaveAudioClip : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable we want to save.")]
		public FsmObject saveValue;
		[RequiredField]
		[Tooltip("A unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file that we'll create to store our data.")]
		public FsmString saveFile = "defaultES2File.txt";
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			saveValue = null;
		}
		
		public override void OnEnter()
		{
			ES2.Save(saveValue.Value as AudioClip, saveFile+"?tag="+uniqueTag);
			Log("Saved to "+saveFile+"?tag="+uniqueTag);
			Finish();
		}
	}
	
	[ActionCategory("Easy Save 2")]
	[Tooltip("Loads a previously saved AudioClip.")]
	public class LoadAudioClip : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The variable where we'll load our data into.")]
		public FsmObject loadValue;
		[RequiredField]
		[Tooltip("The unique tag for the data we want to load.")]
		public FsmString uniqueTag = "";
		[RequiredField]
		[Tooltip("The name of the file our data is stored in.")]
		public FsmString saveFile = "defaultES2File.txt";
		[Tooltip("Whether the data we are loading is stored in the Resources folder.")]
		public bool loadFromResources = false;
		
		public override void Reset()
		{
			uniqueTag = "";
			saveFile = "defaultES2File.txt";
			loadValue = null;
			loadFromResources = false;
		}
		
		public override void OnEnter()
		{
			ES2Settings loadSettings = new ES2Settings();
			if(loadFromResources)
				loadSettings.saveLocation = ES2Settings.SaveLocation.Resources;
			
			loadValue.Value = ES2.Load<AudioClip>(saveFile+"?tag="+uniqueTag, loadSettings);
			Log("Loaded from "+saveFile+"?tag="+uniqueTag);
			Finish();
		}
	}
}