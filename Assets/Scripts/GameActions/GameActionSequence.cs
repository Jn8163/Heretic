using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActionSequence : MonoBehaviour
{
    #region Fields

    [SerializeField] private List<GameAction> actionList;

    #endregion



    #region Methods

    public void Play()
    {
        StartCoroutine(nameof(Sequence));
    }

    IEnumerator Sequence()
    {
        foreach (GameAction item in actionList)
        {
            yield return new WaitForSeconds(item.delay);
            item.Action();
        }
    }

    #endregion
}
