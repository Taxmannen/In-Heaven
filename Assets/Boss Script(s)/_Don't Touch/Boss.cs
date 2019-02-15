using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by: Filip Nilsson, planned by: Filip Nilsson + Jesper Uddefors
/// </summary>
public class Boss : Character
{

    //Serialized

    [Header("TRANSFORMS")]
    [SerializeField] private Transform spawnParent;
    [SerializeField] private Transform phaseParent;
    [SerializeField] private Transform movementParent;
    [SerializeField] private Transform attackParent;
    [SerializeField] private Transform deathParent;
    [SerializeField] internal Transform attackScriptTransfromList;
    [SerializeField] internal Transform movementScriptTransfromList;
    [SerializeField] public Transform bulletParent;


    [Header("HITBOXES")]
    [SerializeField] private List<BossHitboxElement> bossHitboxElements = new List<BossHitboxElement>();

    [Header("PREFABS")]
    [SerializeField] private BossSpawn spawnPrefab;
    [SerializeField] private List<BossPhase> phasePrefabs = new List<BossPhase>();
    [SerializeField] private BossDeath deathPrefab;

    [Header("ACTIVE PHASE")]
    [SerializeField] private int activePhase = 0;



    //Private

    private Coroutine bossRoutine = null;
    private List<BossPhase> phases = new List<BossPhase>();
    protected BossDeath death;
    protected BossSpawn spawn;



    //Main

    private void OnValidate()
    {

        if (bossHitboxElements.Count > 0)
        {

            foreach (BossHitboxElement bossHitboxElement in bossHitboxElements)
            {

                if (bossHitboxElement.GetWeakpoint())
                {

                    if (bossHitboxElement.GetMaxHP() < 0)
                    {
                        bossHitboxElement.SetMaxHP(0);
                    }

                    if (bossHitboxElement.GetActivePhase() < 1)
                    {
                        bossHitboxElement.SetActivePhase(1);
                    }

                    else if (bossHitboxElement.GetActivePhase() > phasePrefabs.Count)
                    {
                        bossHitboxElement.SetActivePhase(phasePrefabs.Count);
                    }

                }

                else
                {
                    bossHitboxElement.SetMaxHP(0);
                    bossHitboxElement.SetActivePhase(0);
                }

            }

        }

        if (activePhase > phasePrefabs.Count)
        {
            activePhase = phasePrefabs.Count;
        }

        if (activePhase < 1)
        {
            activePhase = 1;
        }

    }

    private new void Start()
    {

        SetupSpawn();

        SetupPhases();

        SetupDeath();

        SetupHitboxes();

        base.Start();

        StartBoss();

    }



    private void SetupSpawn()
    {

        if (!spawnParent)
        {
            GameObject spawnObject = Instantiate(new GameObject(), transform);
            spawnObject.name = "Spawn(s)";
            spawnParent = spawnObject.transform;
        }

        if (spawnParent)
        {

            if (spawnParent.childCount > 0)
            {

                foreach (GameObject child in spawnParent)
                {
                    Destroy(child);
                }

            }

            if (spawnPrefab)
            {
                spawn = Instantiate(spawnPrefab, spawnParent);
            }

            else
            {
                Debug.LogError("[Custom Error] : \"Spawn Prefab\" is null, make sure the boss has a \"Spawn Prefab\" attached.");
            }

        }

    }

    public void SetupPhases()
    {

        if (!phaseParent)
        {

            GameObject phaseGameObject = new GameObject();
            phaseGameObject.transform.parent = transform;
            phaseGameObject.transform.position = transform.position;
            phaseGameObject.name = "Phase(s)";
            phaseParent = phaseGameObject.transform;

        }

        if (phaseParent)
        {

            if (phasePrefabs.Count > 0)
            {
                
                phases.Clear();
                int i = 0;
                foreach (BossPhase phasePrefab in phasePrefabs)
                {
                    i++;
                    BossPhase inst = Instantiate(phasePrefab, phaseParent);
                    inst.transform.localPosition = new Vector3(0,0,0);
                    inst.name = "Phase: " + i;
                    phases.Add(inst);
                    
                }

            }

            else
            {
                Debug.LogError("[Custom Error] : List of \"Phase Prefabs\" for a boss is empty, make sure every boss on the scene has at least one phase.");
            }

        }

        else
        {
            Debug.LogError("[Custom Error] : Couldn't find the/instantiate a \"Phase Parent\" transform, check the \"Phase Parent\" parameter on a boss script in the scene in runtime.");
        }

    }

    private void SetupDeath()
    {

        if (!deathParent)
        {
            GameObject deathObject = Instantiate(new GameObject(), transform);
            deathObject.name = "Death(s)";
            deathParent = deathObject.transform;
        }

        if (deathParent)
        {

            if (deathParent.childCount > 0)
            {
                foreach (GameObject child in deathParent)
                {
                    Destroy(child);
                }
            }

            if (deathPrefab)
            {
                death = Instantiate(deathPrefab, deathParent);
            }

            else
            {
                Debug.LogError("[Custom Error] : \"Death Prefab\" is null, make sure the boss has a \"Death Prefab\" attached.");
            }

        }

    }

    private void SetupHitboxes()
    {

        maxHP = 0;

        if (bossHitboxElements.Count > GetComponentsInChildren<BossHitbox>().Length)
        {
            Debug.LogError("[Custom Error] : List of \"BossHitboxElements\" is out of date for a boss, press the button \"Update BossHitboxElement(s)\" on the boss whose list is out of date.");
            return;
        }

        foreach (BossHitboxElement bossHitboxElement in bossHitboxElements)
        {

            bossHitboxElement.SetName(bossHitboxElement.GetHitbox().name);

            if (bossHitboxElement.GetActivePhase() == activePhase)
            {
                maxHP += bossHitboxElement.GetMaxHP();
            }

            bossHitboxElement.GetHitbox().SetMaxHP(bossHitboxElement.GetMaxHP());
            bossHitboxElement.GetHitbox().SetHP(bossHitboxElement.GetHitbox().GetMaxHP());
            bossHitboxElement.GetHitbox().SetWeakpoint(bossHitboxElement.GetWeakpoint());

            if (bossHitboxElement.GetHitbox().GetWeakpoint())
            {
                bossHitboxElement.GetHitbox().SetActivePhase(bossHitboxElement.GetActivePhase());
            }

        }

    }
    


    private void StartBoss()
    {
        bossRoutine = StartCoroutine(BossRoutine());
    }

    private void FreezeBoss()
    {
        //Comment: Unused method, Implement a way to freeze the boss for future.
    }

    private void StopBoss()
    {
        //Comment: Unused method, Implement a way to stop the boss for future.
        StopCoroutine(bossRoutine);
        bossRoutine = null;
    }

    private IEnumerator BossRoutine()
    {

        BossSpawn();
        yield return new WaitUntil(() => spawn.GetExecuteRoutine() == null);

        for (int i = (activePhase - 1); i < phases.Count; i++)
        {

            InterfaceController.instance.UpdateBossHPBar(hP, maxHP);
            InterfaceController.instance.ShowBossHPBar();

            phases[i].StartPhase(this, movementParent, attackParent);

            yield return new WaitUntil(() => (activePhase - 1) != i);

            phases[i].StopPhase();

            if ((activePhase - 1) > i)
            {

                if (i + (activePhase - 1) - (i + 1) < (phases.Count - 1))
                {
                    i += (activePhase - 1) - (i + 1);
                }

                else
                {
                    i = phases.Count - 1;
                    //Comment: Implement Reset To Phase Health @ Last Phase ...
                }

            }

            else
            {
                i = (activePhase - 2);
                //Comment: Implement Reset To Phase Health @ Newly Set Phase ...
            }

        }

        BossDeath();

        yield return new WaitUntil(() => death.GetExecuteRoutine() == null);
        GameController.instance.SetGameState(Global.GameState.Success);
        bossRoutine = null;
        yield break;

    }

    private void BossSpawn()
    {

        if (spawn)
        {
            spawn.StartSpawn(this);
        }
        
    }

    private void BossDeath()
    {

        if (death)
        {
            death.StartDeath(this);
        }

    }

    

    internal override void Receive(float amt)
    {

        base.Receive(amt);
        InterfaceController.instance.UpdateBossHPBar(hP, maxHP);

    }

    internal override void Die()
    {

        if (activePhase < phases.Count)
        {

            activePhase++;

            maxHP = 0;

            foreach (BossHitboxElement bossHitboxElement in bossHitboxElements)
            {
                if (bossHitboxElement.GetActivePhase() == activePhase)
                {
                    maxHP += bossHitboxElement.GetMaxHP();
                }
            }

            hP = maxHP;

        }

        else
        {

            activePhase++;
            base.Die();
            InterfaceController.instance.HideBossHPBar();

        }

    }



    //Getters & Setters

    public void GetHitboxes()
    {

        for (int i = (bossHitboxElements.Count - 1); i >= GetComponentsInChildren<BossHitbox>().Length; i--)
        {
            bossHitboxElements.RemoveAt(i);
        }

        for (int i = 0; i < GetComponentsInChildren<BossHitbox>().Length; i++)
        {

            BossHitbox bhb = GetComponentsInChildren<BossHitbox>()[i];

            if (i > (bossHitboxElements.Count - 1))
            {
                bossHitboxElements.Add(new BossHitboxElement(bhb.name, bhb, bhb.GetWeakpoint(), bhb.GetMaxHP(), 0));
            }

            else
            {
                bossHitboxElements[i] = new BossHitboxElement(bhb.name, bhb, bossHitboxElements[i].GetWeakpoint(), bossHitboxElements[i].GetMaxHP(), 0);
            }

        }

        maxHP = 0;

        foreach (BossHitboxElement bossHitboxElement in bossHitboxElements)
        {

            bossHitboxElement.SetName(bossHitboxElement.GetHitbox().name);
            maxHP += bossHitboxElement.GetMaxHP();

            bossHitboxElement.GetHitbox().SetMaxHP(bossHitboxElement.GetMaxHP());
            bossHitboxElement.GetHitbox().SetHP(bossHitboxElement.GetHitbox().GetMaxHP());
            bossHitboxElement.GetHitbox().SetWeakpoint(bossHitboxElement.GetWeakpoint());

        }

    }

    public int GetActivePhase()
    {
        return activePhase;
    }

    public void SetActivePhase(int activePhase)
    {

        //Comment: Not used yet.

        if (this.activePhase == activePhase)
        {
            //Comment: Reset This Phase
        }

        if (activePhase < 1)
        {
            activePhase = 1;
        }

        else if (activePhase > phases.Count)
        {
            activePhase = phases.Count;
        }

        else
        {
            this.activePhase = activePhase;
        }

    }

}
