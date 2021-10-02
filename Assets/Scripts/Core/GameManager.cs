
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class GameManager : MonoBehaviour
    {
        readonly int frameRate = 300;
        public static GameManager Instance;
        [SerializeField] [Range(1, 4)] public int numberOfSamples = 1;
        [SerializeField] public int ballCount = 50;
        [SerializeField] BallFactory ballFactory;


        private void Awake()
        {
            Application.targetFrameRate = frameRate;
            Instance = this;
        }
    }
}
