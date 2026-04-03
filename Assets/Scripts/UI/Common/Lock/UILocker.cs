using BilliotGames;
using UnityEngine;

public class UILocker : UIBase
{
    [SerializeField] GameObject[] lockObjs;

    public virtual void SetLock(bool @lock) {
        for (int i = 0; i < lockObjs.Length; i++) {
            var lockObj = lockObjs[i];
            lockObj.SetActive(@lock);
        }
    }
}
