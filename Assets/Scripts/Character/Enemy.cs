using UnityEngine;

public class Enemy : Characters
{
    [SerializeField]
    private int expDrop;
    public int ExpDrop { get { return expDrop; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case CharState.Walk:
                WalkUpdate();
                break;
            case CharState.WalkToEnemy:
                WalkToEnemyUpdate();
                break;
            case CharState.Attack:
                AttackUpdate();
                break;

        }
    }

    protected override void Die() 
    {
        base.Die();
        partyManager.DistributeTotalExp(expDrop);
    }
}
