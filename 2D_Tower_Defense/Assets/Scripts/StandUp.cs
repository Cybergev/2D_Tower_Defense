using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandUp : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_Rigit;
    [SerializeField] private SpriteRenderer m_Sprite;

    private void Start()
    {
        if (!m_Rigit)
        {
            m_Rigit = transform.root.GetComponent<Rigidbody2D>();
        }
        if (!m_Sprite)
        {
            m_Sprite = transform.root.GetComponent<SpriteRenderer>();
        }
    }
    private void LateUpdate()
    {
        transform.up = Vector2.up;
        var xMotion = m_Rigit.velocity.x;
        if (xMotion > 0.01f)
            m_Sprite.flipX = false;
        else if (xMotion < 0.01f)
            m_Sprite.flipX = true;

    }
}
