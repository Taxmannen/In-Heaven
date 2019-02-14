using UnityEngine;

/// <summary>
/// Made by: Filip Nilsson
/// </summary>
[System.Serializable]
public class BossHitboxElement
{

    //Serialized
    
    [SerializeField] private string name;
    [SerializeField] private BossHitbox bossHitbox;
    [SerializeField] private bool weakpoint;
    [SerializeField] private float maxHP;
    [SerializeField] private int activePhase;



    //Constructor

    public BossHitboxElement(string name, BossHitbox bossHitbox, bool weakpoint, float maxHP, int activePhase)
    {
        this.name = name;
        this.bossHitbox = bossHitbox;
        this.maxHP = maxHP;
        this.weakpoint = weakpoint;
        this.activePhase = activePhase;
    }



    //Getters & Setters

    public string GetName()
    {
        return name;
    }

    public void SetName(string name)
    {
        this.name = name;
    }

    public BossHitbox GetHitbox()
    {
        return bossHitbox;
    }

    public void SetHitbox(BossHitbox hitbox)
    {
        this.bossHitbox = hitbox;
    }

    public bool GetWeakpoint()
    {
        return weakpoint;
    }

    public void SetWeakpoint(bool weakpoint)
    {
        this.weakpoint = weakpoint;
    }

    public float GetMaxHP()
    {
        return maxHP;
    }

    public void SetMaxHP(float maxhp)
    {
        this.maxHP = maxhp;
    }

    public int GetActivePhase()
    {
        return activePhase;
    }

    public void SetActivePhase(int activePhase)
    {
        this.activePhase = activePhase;
    }

}
