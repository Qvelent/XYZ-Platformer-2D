using UnityEngine;

namespace PlayerOption.Scripts.Utils
{
    public static class WindowUntils
    {
        public static void CreateWindow(string resourcePath)
        {
            var window = Resources.Load<GameObject>(resourcePath);
            var canvas = Object.FindObjectOfType<Canvas>();
            Object.Instantiate(window, canvas.transform);
        }
    }
}