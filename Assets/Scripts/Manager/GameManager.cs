using Cinemachine;
using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Transform respawnPoint;
        [SerializeField] private GameObject player;
        [SerializeField] private float respawnTime;

        private CinemachineVirtualCamera _cvCam;

        private void Start()
        {
            _cvCam = GameObject.Find("Cinemachine Camera").GetComponent<CinemachineVirtualCamera>();
        }

        private float _respawnTimeStart;
        private bool _respawn;

        private void Update()
        {
            CheckRespawn();
        }

        public void Respawn()
        {
            _respawnTimeStart = Time.time;
            _respawn = true;
        }

        private void CheckRespawn()
        {
            if (Time.time >= _respawnTimeStart + respawnTime && _respawn)
            {
                var playerTemp =  Instantiate(player, respawnPoint);
                _cvCam.m_Follow = playerTemp.transform;
                _respawn = false;
            }
        }
    }
}
