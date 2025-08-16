using System.Collections.Generic;
using UnityEngine;

public class ExpManager : MonoSingleton<ExpManager>
{
    [SerializeField]
    private GameObject expPrefab;

    [SerializeField]
    private List<GameObject> expList = new();

    public void DropExp(Vector3 dropPos)
    {
        var exp = Instantiate(expPrefab, dropPos, Quaternion.identity);

        expList.Add(exp);
    }

    public void DisposeExp(GameObject exp)
    {
        expList.Remove(exp);
        Destroy(exp);
    }
}
