using UnityEngine;
using System.Collections;

public static class Constants {

    // constant heights
    public static readonly float PLAY_HEIGHT = 1.0f;

    public static bool IsInLayerMask(GameObject test, LayerMask against)
    {
        int objLayerMask = (1 << test.layer);
        return (against.value & objLayerMask) > 0;
    }

}
