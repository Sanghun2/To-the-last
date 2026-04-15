using System;
using UnityEngine;

public abstract class ProcessContextBuilder
{
    public abstract ProcessContext BuildContext();
}

public abstract class ProcessContextBuilder<TContext> : ProcessContextBuilder where TContext : ProcessContext
{
    public sealed override ProcessContext BuildContext() => BuildProcessContext();

    public abstract TContext BuildProcessContext();
}
