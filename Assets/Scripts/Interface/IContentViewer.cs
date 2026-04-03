using System.Collections.Generic;
using UnityEngine;

public interface IContentViewer
{
    public void ShowContents(IReadOnlyList<ContentSDBase> contents);
}
