using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[RequireComponent(typeof(SpriteRenderer))]
public class BallView : MonoBehaviour, IBallView
{

    SpriteRenderer _spriteRenderer;

    IBallCollisionResponse _ballCollisionResponse;

    public void Init()
    {
        if (!TryGetComponent<SpriteRenderer>(out _spriteRenderer)) Debug.LogError("SpriteRenderer is missing");

        TryGetComponent<IBallCollisionResponse>(out _ballCollisionResponse);
    }

    public void SetBallViewTo(Color color, float size)
    {
        transform.localScale = new Vector3(size, size, size);
        _spriteRenderer.color = color;
    }

}
