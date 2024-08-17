using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ScalesManager : MonoBehaviour
{
    public Action<float> OnLensScaleChanged;

    public static ScalesManager Instance;

    public float scale = 1;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void LensScaleChanged(float scrollDir)
    {
        scale += scrollDir;
        OnLensScaleChanged?.Invoke(scrollDir);
    }
}
