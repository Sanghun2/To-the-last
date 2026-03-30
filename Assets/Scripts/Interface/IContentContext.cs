using System.Collections.Generic;
using UnityEngine;

public interface IContentContext
{

}

public interface IContentContext<TContent>
    where TContent : class
{
    public IReadOnlyList<TContent> ContentList { get; }
}
