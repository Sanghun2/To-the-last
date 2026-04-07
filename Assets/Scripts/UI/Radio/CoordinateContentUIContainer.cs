using System;
using UnityEngine;

public class CoordinateContentUIContainer : ListContainerBase<CoordinateContentUI>
{
    public CoordinateContentUI FindContent(Predicate<CoordinateContentUI> finder) {
        return contentList.Find(finder);
    }
}
