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

    //#region QUEUES

    //public static Vector3 LineBehindPosition(int position)
    //{
        
    //}

    //#endregion

    public static float GetLeftHorizontal()
    {
        return Input.GetAxis("Horizontal");
    }

    public static float GetLeftVertical()
    {
        return Input.GetAxis("Vertical");
    }

    public static float GetRightHorizontal()
    {
        return Input.GetAxis("FireHorizontal");
    }

    public static float GetRightVertical()
    {
        return Input.GetAxis("FireVertical");
    }

    public static bool IsLeftStickAlive(float threshold)
    {
        return Mathf.Abs(GetLeftHorizontal()) >= threshold || Mathf.Abs(GetLeftVertical()) >= threshold;
    }

    public static bool IsRightStickAlive(float threshold)
    {
        return Mathf.Abs(GetRightHorizontal()) >= threshold || Mathf.Abs(GetRightVertical()) >= threshold;
    }

    public static Vector3 GetLeftStickXZ()
    {
        return new Vector3(GetLeftHorizontal(), 0, GetLeftVertical());
    }

    public static Vector3 GetRightStickXZ()
    {
        return new Vector3(GetRightHorizontal(), 0, GetRightVertical());
    }

    public static Vector3 GetRightTwo()
    {
        return new Vector2(GetRightHorizontal(), GetRightVertical());

    }

}
