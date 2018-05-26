using UnityEngine;
using System.Collections;

public class Utils {

	public static float randomI(float a ,float b)
	{
		return Random.value*(b-a) + a;
	}

	public static bool isEqualF(float a ,float b)
	{
		return (a-b) >= -float.Epsilon && (a-b) <= float.Epsilon;
	}

	public static void cleanChlidren(GameObject obj)
	{
		for(int i = 0;i< obj.transform.childCount;i++)
		{
			GameObject go = obj.transform.GetChild(i).gameObject;
			MonoBehaviour.Destroy(go);
		}
	}

    //return [0 ~ 360)
	public static float getAngleByVec2(Vector2 vec)
	{
		if(vec.x == 0)
        {
            return vec.y >= 0 ? 90 : 270;
        }

        float rad = Mathf.Atan(vec.y / vec.x);
        float ang = rad* Mathf.Rad2Deg;
        if (vec.x < 0) //2,3象限
            ang += 180;
        else if (vec.y < 0)//4象限
            ang += 360;

        return ang;
	}

    public static Vector2 getVecByAngle(float ang)
    {
        float rad = ang * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }
}
