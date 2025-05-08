using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseRangeMst : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    private SpriteRenderer spriteRenderer;

    [Header("Close Range")]
    [SerializeField] private GameObject characterRanderer;
    // [SerializeField] private GameObject weaponsPivot;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection{get{return movementDirection;}}

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection{get{return lookDirection;}}

    // protected Vector2 knockback = Vector2.zero;
    // public Vector2 Knockback{get{return knockback;}}

    public void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = characterRanderer.GetComponent<SpriteRenderer>();
    }

    protected void FixedUpdate()
    {
        Movment(movementDirection);
        // if(knockback > 0.0f)
        // {
        //     // knockbackDuration -= Time.fixedDeltaTime;
        // }
    }

    private void Movment(Vector2 direction)
    {
        direction = direction * 5;

        // if(knockbackDuration > 0.0f)
        // {
        //     direction *= 0.2f;
        //     direction += knockback;
        // }
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        spriteRenderer.flipX = isLeft;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
