using UnityEngine;
namespace UniversalMobileController
{
    public class LimitFrameRate : MonoBehaviour
    {
        public int limitFrameRate = 90;
        void Start()
        {
            Application.targetFrameRate = limitFrameRate;
        }
    }
}
