using System;
using UnityEngine;

namespace Player
{
    public class PlayerAfterImageSprite : MonoBehaviour
    {
        [SerializeField]
        private float activeTime = 0.1f;
        private float timeActivated;
        private float alpha;
        private float alphaSet = 0.8f;
        [SerializeField]
        private float alphaMultiplier = 0.85f;

        private Transform _player;

        private SpriteRenderer _sr;

        private SpriteRenderer _playerSr;

        private Color color;

        private void OnEnable()
        {
            _sr = GetComponent<SpriteRenderer>();
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _playerSr = _player.GetComponent<SpriteRenderer>();

            alpha = alphaSet;
            _sr.sprite = _playerSr.sprite;
            transform.position = _player.position;
            transform.rotation = _player.rotation;
            timeActivated = Time.time;
        }

        private void Update()
        {
            alpha *= alphaMultiplier;
            color = new Color(1f, 1f, 1f, alpha);
            _sr.color = color;
            if (Time.time >= (timeActivated + activeTime))
            {
                PlayerAfterImagePool.Instance.AddToPool(gameObject);
            }
        }
    }
    
    
}
