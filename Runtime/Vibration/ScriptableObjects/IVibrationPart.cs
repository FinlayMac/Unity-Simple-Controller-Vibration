using UnityEngine;

public abstract class IVibrationPart : ScriptableObject
{

    public abstract void Activate();
    public abstract bool UpdateFrame();
    public abstract Vector2 GetPartDataStrength();
    public abstract float GetPartDataDuration();
}
