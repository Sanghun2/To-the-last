public interface IEntity
{

}

public readonly struct EffectContext
{
    public readonly IEntity executor;
    public readonly IEntity target;

    public EffectContext(IEntity executor, IEntity target) {
        this.executor = executor;
        this.target = target;
    }
}
