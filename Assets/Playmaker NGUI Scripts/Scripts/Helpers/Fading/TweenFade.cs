using UnityEngine;

/// <summary>
/// Handles fading in and out of NGUI controls
/// </summary>
public class TweenFade : MonoBehaviour
{
    #region AttachFadeIn

    /// <summary>
    /// Attaches the tween alpha script to the object for fading in
    /// </summary>
    /// <param name="duration">fade in time, in seconds</param>
    public void AttachFadeIn(float duration)
    {
        // remove any existing script
        Reset();

        // attach the tween script
        TweenAlpha Twa = gameObject.AddComponent<TweenAlpha>();

        // set properties
        Twa.duration = duration;
        Twa.from = 0f;
        Twa.to = 1f;

        // set callback
        //Twa.eventReceiver = gameObject;
        //Twa.callWhenFinished = finishFunctionName;
    }

    // AttachFadeIn
    #endregion

    #region AttachFadeout

    /// <summary>
    /// Attaches the tween alpha script to the object for fading out
    /// </summary>
    /// <param name="duration">fade in time, in seconds</param>
    /// <param name="deactivate">when true, the control will be deactivated after fade out</param>
    public void AttachFadeout(float duration, bool deactivate)
    {
        // remove any existing script
        Reset();

        // attach the tween script
        TweenAlpha Twa = gameObject.AddComponent<TweenAlpha>();

        // set properties
        Twa.duration = duration;
        Twa.from = 1f;
        Twa.to = 0f;

        // set callback
        if (deactivate)
        {
            Twa.eventReceiver = gameObject;
            Twa.callWhenFinished = "FadeOutFinished";
        }
    }

    // AttachFadeout
    #endregion

    #region FadeOutFinished

    /// <summary>
    /// Called when fading is finished
    /// </summary>
    void FadeOutFinished()
    {
        // disable the game object
        //Debug.Log(string.Format("Fade Out Finished for {0}", gameObject.name));
        NGUITools.SetActive(gameObject, false);
    }

    // FadeOutFinished
    #endregion

    #region Reset

    /// <summary>
    /// Resets the fading
    /// </summary>
    public void Reset()
    {
        // check if we already have a tween alpha, if so, remove it
        TweenAlpha Twa = gameObject.AddComponent<TweenAlpha>();
        if (Twa != null)
            Destroy(Twa);
    }

    // Reset
    #endregion
}
