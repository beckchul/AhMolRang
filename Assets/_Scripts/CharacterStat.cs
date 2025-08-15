using UnityEngine;

public class CharacterStat
{
    public int hp;
    public float moveSpeed;

    public CharacterStat(
        int hp = 100,
        float moveSpeed = 1f)
    {
        this.hp = hp;
        this.moveSpeed = moveSpeed;
    }
}
