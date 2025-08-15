using UnityEngine;

public class CharacterStat
{
    public int hp;
    public float moveSpeed;
    public float attackSpeed; // 1 = 100% 공속, 0.5 = 50% 공속, 2 = 200% 공속

    public CharacterStat(
        int hp = 100,
        float moveSpeed = 1f,
        float attackSpeed = 1f)
    {
        this.hp = hp;
        this.moveSpeed = moveSpeed;
        this.attackSpeed = attackSpeed;
    }
}
