using System.Collections;
using UnityEngine;

public enum CharacterStateType
{
    Idle = 0,
    Stun,
    Invincibility,
    Dead
}

public class CharacterState : MonoBehaviour
{
    public CharacterStateType StateType { get; private set; }
    private CharacterStat stat;
    private StatModifier md;


    public void InitState(CharacterStat stat)
    {
        this.stat = stat;
    }

    public void ChangeState(CharacterStateType state)
    {
        ExitState(state);
        StateType = state;
        EnterState(state);
    }

    private void EnterState(CharacterStateType state, float duration = 3.0f)
    {
        switch (state)
        {
            case CharacterStateType.Idle:
                {

                }
                break;
            case CharacterStateType.Stun:
                {
                    md = new StatModifier(0.0f, StatModifierType.Const);
                    stat.MoveSpeedStat.AddModifier(md);
                    StartCoroutine(WaitForIdle_Coroutine(duration));
                }
                break;
            case CharacterStateType.Invincibility:
                {
                    StartCoroutine(WaitForIdle_Coroutine(duration));
                }
                break;
            case CharacterStateType.Dead:
                {

                }
                break;
            default:
                break;
        }
    }

    private void ExitState(CharacterStateType state)
    {
        switch (state)
        {
            case CharacterStateType.Idle:
                {

                }
                break;
            case CharacterStateType.Stun:
                {
                    stat.MoveSpeedStat.RemoveModifier(md);
                }
                break;
            case CharacterStateType.Invincibility:
                {

                }
                break;
            case CharacterStateType.Dead:
                {

                }
                break;
            default:
                break;
        }
    }

    IEnumerator WaitForIdle_Coroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ChangeState(CharacterStateType.Idle);
    }
}
