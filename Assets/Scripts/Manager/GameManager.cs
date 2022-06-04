using System;
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
        
        private float _respawnTimeStart;
        public bool _respawn;

        private void Start()
        {
            _cvCam = GameObject.Find("Cinemachine Camera").GetComponent<CinemachineVirtualCamera>();
        }
        private void Update()
        {
            CheckRespawn();
            if (GameObject.FindWithTag("Player") != null)
            {
                _cvCam.m_Follow = GameObject.FindWithTag("Player").transform;
            }
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
                _cvCam.PreviousStateIsValid = false;
                _respawn = false;
            }
        }
    }
}
