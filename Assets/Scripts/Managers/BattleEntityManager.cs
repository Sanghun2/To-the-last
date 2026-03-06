using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class BattleEntityManager
{
    private BattleEntity player;
    private List<BattleEntity> enemyList = new List<BattleEntity>();

    /// <summary>
    /// int -> remain count
    /// </summary>
    public event Action<int> OnEnemyRemoved;

    public void RegisterPlayer(BattleEntity player) {
        this.player = player;
    }
    public void RegisterEnemy(BattleEntity enemy) {
        enemyList.Add(enemy);
    }

    public void RemoveEnemy(BattleEntity enemy) {
        for (int i = 0; i < enemyList.Count; i++) {
            var enemyEntity = enemyList[i];
            if (enemyEntity.Equals(enemy)) {
                enemyList.RemoveAt(i);
                OnEnemyRemoved?.Invoke(enemyList.Count);
                return;
            }
        }
    }
}
