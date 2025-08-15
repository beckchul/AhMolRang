using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public CharacterStat Stat { get; private set; }



    private int maxHP = 0;
    private int currentHP = 0;
    private float moveSpeed = 0.0f;
    private float attackDamage = 0.0f;
    private float attackSpeed = 0.0f;

    public int MaxHP
    {
        get { return maxHP; }
        set { maxHP = value <= 0 ? 1 : value; }
    }
    public int CurrentHP
    {
        get { return currentHP; }
        set { maxHP = value <= 0 ? 1 : value; }
    }

    public float MoveSpeed;
    public float AttackDamage;
    public float AttackSpeed;


    public void InitPlayerStat()
    {
        Stat = new CharacterStat();

        MaxHP = Stat.hp;
        CurrentHP = MaxHP;
        MoveSpeed = Stat.moveSpeed;
        AttackDamage = 0.0f;
        AttackSpeed = 0.0f;
    }
}
