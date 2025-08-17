using UnityEngine;

namespace WG.Common
{
    public class TimeManager : Singleton<TimeManager>
    {
        [Header("Time")]
        public bool isPaused = false;
        [SerializeField] public float gameTimer = 0;
        public float gameTimeSpeed = 1f;
        [Range(10, 600)] public float seasonDuration = 300f;
        public int seasonNumber = 0;

        public float DeltaSimulTime
        {
            get
            {
                return isPaused ? 0 : Time.deltaTime * gameTimeSpeed;
            }
        }
        public bool isFreeze
        {
            get
            {
                return gameTimeSpeed <= 0;
            }
        }


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (!isPaused)
            {
                gameTimer += DeltaSimulTime;

                if (gameTimer > (seasonDuration * seasonNumber))
                {
                    seasonNumber++;
                    Debug.Log("pouet");
                }
            }
        }



       
        public void Pause()
        {
            isPaused = !isPaused;
        }
        public void SetPause(bool state)
        {
            isPaused = state;
        }
        public void SetSpeed(float speed)
        {
            gameTimeSpeed = speed;
        }
    }
}
