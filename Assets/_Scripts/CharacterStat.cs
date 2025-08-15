using UnityEngine;

public class CharacterStat
{
    public int hp;
    public float moveSpeed;
    public float attackSpeed; // 1 = 100% ����, 0.5 = 50% ����, 2 = 200% ����

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
