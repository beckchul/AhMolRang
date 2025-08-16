using UnityEngine;

public class ExpItem : MonoBehaviour
{
    public enum ExpItemState
    {
        None,
        Consume,
        Get
    }
       
    public Transform findTarget;

    private ExpItemState state = ExpItemState.None;

    private const int   expValue = 1;
    private const float consumeSpeed = 3.0f;
    private const float getDistance = 1.0f;

    private void Start()
    {
        findTarget = PlayerManager.Instance.PlayerObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == ExpItemState.None)
        {
            FindPlayer();
        }
        else if(state == ExpItemState.Consume)
        {
            Consume();
        }
    }

    private void FindPlayer()
    {
        float findDistance = PlayerManager.Instance.PlayerScript.Stat.EXPArea;
        float diff = (transform.position - findTarget.position).magnitude;

        if (diff <= findDistance)
        {
            state = ExpItemState.Consume;
        }
    }

    private void Consume()
    {
        float diff = (transform.position - findTarget.position).magnitude;

        if (diff <= getDistance)
        {
            state = ExpItemState.Get;
            PlayerManager.Instance.PlayerScript.Stat.AddEXP(expValue);
            ExpManager.Instance.DisposeExp(gameObject);
            return;
        }
        else
        {
            var dir = (findTarget.position - transform.position).normalized;

            transform.position += dir * consumeSpeed * Time.deltaTime;
        }
    }
}
