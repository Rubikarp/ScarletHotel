using UnityEngine;

public static class Extension_Transform
{
    public static void DeleteChildren(this Transform transform)
    {
        int childCount = transform.childCount;
        if (Application.isPlaying)
            for (int i = childCount - 1; i >= 0; i--)
            {
                Object.Destroy(transform.GetChild(i).gameObject);
            }
        else
            for (int i = childCount - 1; i >= 0; i--)
            {
                Object.DestroyImmediate(transform.GetChild(i).gameObject);
            }
    }

    public static void SetTop(this RectTransform rt, float top) => rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
    public static void SetLeft(this RectTransform rt, float left) => rt.offsetMin = new Vector2(left, rt.offsetMin.y);
    public static void SetRight(this RectTransform rt, float right) => rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
    public static void SetBottom(this RectTransform rt, float bottom) => rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);

    public static void AddOffsetHori(this RectTransform rt, float value)
    {
        rt.offsetMin = rt.offsetMax + Vector2.right * value;
        rt.offsetMax = rt.offsetMax + Vector2.right * value;
    }
    public static void AddOffsetVert(this RectTransform rt, float value)
    {
        rt.offsetMin = rt.offsetMax + Vector2.up * value;
        rt.offsetMax = rt.offsetMax + Vector2.up * value;
    }
    public static RectTransform AsRect(this Transform transform)
    {
        if (transform == null) return null;
        if (transform is RectTransform == false)
        {
            Debug.LogError($"Transform {transform.name} is not a RectTransform");
            return null;
        }
        return transform as RectTransform;
    }
}

public static class Extension_RectTransform
{
    public static Vector3 GetWorldPosOfCanvasElement(this RectTransform element)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(element, element.position, Camera.main, out var result);
        return result;
    }
}