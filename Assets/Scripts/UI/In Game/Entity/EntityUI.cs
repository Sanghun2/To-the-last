using System;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class EntityUI : UIBase
{
    public IReadOnlyEntity Entity => _entity;

    [SerializeField] Image entityImage;
    [SerializeField] protected AnimatorBase animtor;
    private IReadOnlyEntity _entity;

    public event Action<Entity> OnEntityInit;

    public virtual void InitEntity(Entity entity) {
        gameObject.name = $"{entity.EntityID}";
        _entity = entity;
        OnEntityInit?.Invoke(entity);
    }
    public void SetImage(Sprite image) {
        entityImage.sprite = image;
    }

    private void Reset() {
        if (entityImage == null) {
            entityImage = GetComponentInChildren<Image>();
        }
    }
}
