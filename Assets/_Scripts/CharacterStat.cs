using UnityEngine;

public class CharacterStat
{
    public int hp;
    public float moveSpeed;
    public float attackSpeed; // 1 = 100% 공속, 0.5 = 50% 공속, 2 = 200% 공속

    private readonly int originalHp;
    private readonly float originalMoveSpeed;
    private readonly float originalAttackSpeed;


    public CharacterStat(
        int hp = 100,
        float moveSpeed = 1f,
        float attackSpeed = 1f)
    {
        originalHp = hp;
        originalMoveSpeed = moveSpeed;
        originalAttackSpeed = attackSpeed;

        Reset();
    }

    public void Reset()
    {
        hp = originalHp;
        moveSpeed = originalMoveSpeed;
        attackSpeed = originalAttackSpeed;
    }
}
