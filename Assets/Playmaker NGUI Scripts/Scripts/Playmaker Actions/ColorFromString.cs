using HutongGames.PlayMaker;
using UnityEngine;
//// using Tooltip = HutongGames.PlayMaker;

/*
 * *************************************************************************************
 * Created by: Rocket Games Mobile  (http://www.rocketgamesmobile.com), 2013-2014
 * For use in Unity 3.5, Unity 4.0+
 * *************************************************************************************
*/

[ActionCategory(ActionCategory.Color)]
[HutongGames.PlayMaker.Tooltip("Sets a color variable based on a string value")]
public class ColorFromString : FsmStateAction
{
    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("String to read the value from")]
    public FsmString ColorString;

    [RequiredField]
    [UIHint(UIHint.Variable)]
    [HutongGames.PlayMaker.Tooltip("The color variable to updated")]
    public FsmColor color;

    [HutongGames.PlayMaker.Tooltip("Default color to assign if no match")]
    public FsmColor DefaultColor;

    [HutongGames.PlayMaker.Tooltip("When true, runs on every frame")]
    public bool everyFrame;

    public override void Reset()
    {
        ColorString = null;
        color = null;
        DefaultColor = null;
        everyFrame = false;
    }

    public override void OnEnter()
    {
        DoSetColor();

        if (! everyFrame)
            Finish();
    }

    public override void OnUpdate()
    {
        DoSetColor();
    }

    private void DoSetColor()
    {
        // exit if objects are null
        if ((ColorString == null) || (color == null))
            return;

        switch (ColorString.Value.ToLower())
        {
            default:
                if (DefaultColor != null)
                    color.Value = DefaultColor.Value;
                break;

            case "black": color.Value = Color.black; break;
            case "white": color.Value = Color.white; break;
            case "red": color.Value = Color.red; break;
            case "green": color.Value = Color.green; break;
            case "blue": color.Value = Color.blue; break;
            case "yellow": color.Value = Color.yellow; break;
            case "cyan": color.Value = Color.cyan; break;
            case "magenta": color.Value = Color.magenta; break;
            case "clear": color.Value = Color.clear; break;
            case "gray": color.Value = Color.gray; break;
            case "grey": color.Value = Color.grey; break;
        }
    }
}