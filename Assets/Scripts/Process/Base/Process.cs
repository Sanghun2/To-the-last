using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class Process
{
    private UniTaskCompletionSource completionSource;
    protected ProcessContextBuilder contextBuilder;

    public Process(ProcessContextBuilder contextBuilder) {
        this.contextBuilder = contextBuilder;
    }

    public async UniTask ExecuteProcessAsync(ProcessContext context, CancellationToken cancellationToken) {
        completionSource = new UniTaskCompletionSource();
        await ExecuteProcessInternalAsync(context, cancellationToken);
    }
    protected abstract UniTask ExecuteProcessInternalAsync(ProcessContext context, CancellationToken cancellationToken);


    public void CompleteProcess() {
        completionSource?.TrySetResult();
    }
    public ProcessContext BuildContext() {
        return contextBuilder?.BuildContext();
    }


    protected UniTask WaitForComplete(CancellationToken cancellationToken) {
        return completionSource.Task.AttachExternalCancellation(cancellationToken);
    }
}

public abstract class Process<TContext> : Process where TContext : ProcessContext
{
    protected Process(ProcessContextBuilder<TContext> contextBuilder) : base(contextBuilder) {

    }

    public abstract UniTask ExecuteProcessAsync(TContext context, CancellationToken cancellationToken);

    protected override async UniTask ExecuteProcessInternalAsync(ProcessContext context, CancellationToken cancellationToken) {
        if (context is TContext tContext) {
            await ExecuteProcessAsync(tContext, cancellationToken);
        }
        else {
            Debug.LogError($"<color=red>context type mismatch</color>");
        }
    }
}