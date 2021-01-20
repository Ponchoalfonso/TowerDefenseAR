using System.Collections.Generic;
using UnityEngine;

public class UnitController: MonoBehaviour
{
    protected bool attacking;

    private float currentHealth;
    private float currentDamagePoints;
    private float currentAttackSpeed;
    private float currentMovementSpeed;
    private float extraHealth;

    private List<Effect> effects = new List<Effect>();

    public Unit unit;

    public float hp { get { return currentHealth + extraHealth; } }
    public float absortion { get { return extraHealth; } }
    public float ad { get { return currentDamagePoints; } }
    public float asp { get { return currentAttackSpeed; } }
    public float ms { get { return currentMovementSpeed; } }
    public float maxHealth { get { return unit.healthPoints; } }

    protected void Start()
    {
        Reset();
    }
    
    protected void Update()
    {
        foreach (Effect effect in effects)
        {
            effect.timeLeft -= Time.deltaTime;
            if (effect.timeLeft <= 0)
                RemoveEffect(effect);
        }
    }

    public void Kill()
    {
         // Destroy(gameObject);
    }

    public delegate void EventHandler(float health);
    public event EventHandler OnDamage;
    public void Damage(float damage)
    {
        // Reduce extra health
        float residue = extraHealth - damage;
        extraHealth -= damage;
        if (extraHealth < 0)
        {
            extraHealth = 0;
        }

        // Reduce real health if there is any residue
        if (residue > 0) return;
        currentHealth += residue;

        if (OnDamage != null)
            OnDamage(hp);
    }

    public void Heal(float health)
    {
        currentHealth += health;
        if (hp > maxHealth)
            currentHealth = maxHealth;
    }

    public void Attack(UnitController unit)
    {
        unit.Damage(ad);
    }

    public void ApplyEffect(Effect effect)
    {
        switch (effect.type)
        {
            case Effects.Absortion:
                extraHealth = effect.multiplier;
                break;

            case Effects.Strength:
                currentDamagePoints = unit.damagePoints * effect.multiplier;
                break;

            case Effects.Swiftness:
                currentMovementSpeed = unit.movementSpeed * effect.multiplier;
                break;

            case Effects.Haste:
                currentAttackSpeed = unit.attackSpeed * effect.multiplier;
                break;

            case Effects.Fatigue:
                currentAttackSpeed = unit.attackSpeed / effect.multiplier;
                break;

            case Effects.Slowness:
                currentMovementSpeed = unit.movementSpeed / effect.multiplier;
                break;

            case Effects.Weakness:
                currentDamagePoints = unit.damagePoints / effect.multiplier;
                break;
        }
        effects.Add(effect);
    }

    public void RemoveEffect(Effect effect)
    {
        switch (effect.type)
        {
            case Effects.Absortion:
                extraHealth = 0;
                break;

            case Effects.Strength:
                currentDamagePoints = unit.damagePoints;
                break;

            case Effects.Swiftness:
                currentMovementSpeed = unit.movementSpeed;
                break;

            case Effects.Haste:
                currentAttackSpeed = unit.attackSpeed;
                break;

            case Effects.Fatigue:
                currentAttackSpeed = unit.attackSpeed;
                break;

            case Effects.Slowness:
                currentMovementSpeed = unit.movementSpeed;
                break;

            case Effects.Weakness:
                currentDamagePoints = unit.damagePoints;
                break;
        }
        effects.Remove(effect);
    }

    public void Reset()
    {
        currentHealth = unit.healthPoints;
        currentDamagePoints = unit.damagePoints;
        currentAttackSpeed = unit.attackSpeed;
        currentMovementSpeed = unit.movementSpeed;
        attacking = false;
        extraHealth = 0;
    }

}
