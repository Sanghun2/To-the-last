using UnityEngine;

public interface IEffectHandler
{
    void Execute(IContext context);
}

public interface IEffectHandler<TContext> : IEffectHandler where TContext : IContext
{
    void Execute(TContext context);
}