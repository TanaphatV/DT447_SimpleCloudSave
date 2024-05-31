using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;

    private Vector2 movement;

    public bool isInDungeon = false;

    public bool pause = true;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
       
    }

    void Update()
    {
        if (pause)
            return;

        float moveInputX = Input.GetAxisRaw("Horizontal");
        float moveInputY = Input.GetAxisRaw("Vertical");

        movement = new Vector2(moveInputX, moveInputY).normalized;

        if(Input.GetKeyDown(KeyCode.F))
        {
            Collider2D[] cols = Physics2D.OverlapBoxAll(transform.position, new Vector2(1, 1), 0);
            foreach(var col in cols)
            {
                if(col.TryGetComponent(out Stamp stamp))
                {
                    Destroy(col.gameObject);
                    return;
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            StampManager.instance.CreateStamp(Stamp.Shape.Square, transform.position);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            StampManager.instance.CreateStamp(Stamp.Shape.Triangle, transform.position);
        }
        if (Input.GetKeyDown(KeyCode.O))
            StageManager.instance.SaveStage();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }
}
