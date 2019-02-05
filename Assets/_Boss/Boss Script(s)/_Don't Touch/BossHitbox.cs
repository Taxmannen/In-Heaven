using UnityEngine;

/// <summary>
/// Made by: Filip Nilsson, planned by: Filip Nilsson + Jesper Uddefors
/// </summary>
public class BossHitbox : MonoBehaviour
{

    //Read Only

    [SerializeField] [ReadOnly] private bool weakpoint;
    [SerializeField] [ReadOnly] private float maxHP;
    [SerializeField] [ReadOnly] private float hP;
    [SerializeField] [ReadOnly] private int activePhase;



    //Private

    private Boss boss;



    //Main

    private void Start()
    {
        boss = GetComponentInParent<Boss>();

    }
    public bool Damagable()
    {

        if (!weakpoint)
        {
            return false;
        }

        if (activePhase != boss.GetActivePhase())
        {
            return false;
        }

        return true;

    }
    public void Receive(float damage)
    {


        //Fix
        hP -= damage;

        //Fix
        if (hP <= 0)
        {
            weakpoint = false;
        }


        boss.Receive(damage);
    }



    //Getters & Setters

    public bool GetWeakpoint()
    {
        return weakpoint;
    }

    public void SetWeakpoint(bool weakpoint)
    {
        this.weakpoint = weakpoint;

        if (this.weakpoint)
        {
            GetComponent<Renderer>().material.color = Color.cyan;
        }
    }

    public float GetMaxHP()
    {
        return maxHP;
    }

    public void SetMaxHP(float maxHP)
    {
        this.maxHP = maxHP;
    }

    public float GetHP()
    {
        return hP;
    }

    public void SetHP(float hP)
    {
        this.hP = hP;
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
