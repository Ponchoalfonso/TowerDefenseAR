public enum Effects
{
    Fatigue,
    Slowness,
    Weakness,
    Haste,
    Swiftness,
    Strength,
    Absortion,
}

public class Effect
{
    public readonly Effects type;
    public readonly float duration;
    public readonly int multiplier;
    public float timeLeft;
    public Effect(Effects type, float duration, int multiplier)
    {
        this.type = type;
        this.duration = duration;
        this.multiplier = multiplier;
        timeLeft = duration;
    }
}
