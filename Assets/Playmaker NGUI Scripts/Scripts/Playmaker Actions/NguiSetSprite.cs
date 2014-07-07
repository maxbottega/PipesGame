using HutongGames.PlayMaker;
// using Tooltip = HutongGames.PlayMaker;

/*
 * *************************************************************************************
 * Created by: Rocket Games Mobile  (http://www.rocketgamesmobile.com), 2013-2014
 * For use in Unity 3.5, Unity 4.0+
 * *************************************************************************************
*/

/// <summary>
/// Sets the sprite value of a UISprite
/// </summary>
[ActionCategory("NGUI")]
[HutongGames.PlayMaker.Tooltip("Sets the sprite value of a UISprite")]
public class NguiSetSprite : FsmStateAction
{
    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("NGUI Sprite to set")]
    public FsmOwnerDefault NguiSprite;

    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("The name of the new sprite")]
    public FsmString NewSpriteName;

    public override void Reset()
    {
        NguiSprite = null;
        NewSpriteName = null;
    }

    public override void OnUpdate()
    {
        // set the sprite
        DoSetSprite();
        Finish();
    }

    private void DoSetSprite()
    {
        // exit if objects are null
        if ((NguiSprite == null) || (NewSpriteName == null) || (string.IsNullOrEmpty(NewSpriteName.Value)))
            return;

        // get the sprite
        UISprite sprite = Fsm.GetOwnerDefaultTarget(NguiSprite).GetComponent<UISprite>();

        // exit if no sprite found
        if (sprite == null)
            return;

        // set new sprite name
        sprite.spriteName = NewSpriteName.Value;
    }
}