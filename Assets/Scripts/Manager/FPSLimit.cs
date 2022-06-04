using UnityEngine;

namespace Manager
{
    public class FPSLimit : MonoBehaviour
    {
        public int targetFPS = 60;
        private void Start()
        {
            Application.targetFrameRate = targetFPS;
        }
    }
}
