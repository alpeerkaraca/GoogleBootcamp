using System;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private Vector2 pfxMultiplier;
    private float _textureUnitSizeX;

    private Transform _cameraTransform;
    private Vector3 _lastCameraPos;

    private void Start()
    {
        if (Camera.main != null) _cameraTransform = Camera.main.transform;
        _lastCameraPos = _cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        _textureUnitSizeX = texture.width / sprite.pixelsPerUnit;

    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = _cameraTransform.position - _lastCameraPos;
        transform.position += new Vector3(deltaMovement.x * pfxMultiplier.x, deltaMovement.y * pfxMultiplier.y);
        _lastCameraPos = _cameraTransform.position;

        if (Math.Abs(_cameraTransform.position.x - transform.position.x) >= _textureUnitSizeX)
        {
            float offsetPosX = (_cameraTransform.position.x - transform.position.x) % _textureUnitSizeX;
            transform.position = new Vector3(_cameraTransform.position.x + offsetPosX , transform.position.y);
        }
    }
}
