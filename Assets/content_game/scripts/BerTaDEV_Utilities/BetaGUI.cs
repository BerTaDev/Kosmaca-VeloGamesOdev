using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BerTaDEV
{
    public class BetaGUI : MonoBehaviour
    {
        public int build_index;
        public float FrameCount;
        public static BetaGUI instance;
        private void Awake()
        {
            instance = this;
        }
        private IEnumerator Start()
        {
            GUI.depth = 2;
            while (true)
            {
                FrameCount = 1f / Time.unscaledDeltaTime;
                yield return new WaitForSeconds(0.1f);
            }
        }
        void OnGUI()
        {
            GUI.color = Color.black;
            GUI.Label(new Rect(10, 10, 500, 20), Application.productName.ToString() + " By:" + Application.companyName.ToString());
            GUI.color = Color.black;
            GUI.Label(new Rect(10, 30, 500, 20), "Version: " + Application.version.ToString());
            GUI.color = Color.red;
            GUI.Label(new Rect(10, 50, 500, 20), "var    FPS : " + Mathf.Round(FrameCount));
            GUI.Label(new Rect(10, 70, 500, 20), "build : " + build_index.ToString());
        }
    }
}
