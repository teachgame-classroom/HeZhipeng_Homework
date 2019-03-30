using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2DAnimation : MonoBehaviour
{

    [Header("四个方向移动的Sprite数组")]
    public Sprite[] walkUp = new Sprite[4];
    public Sprite[] walkDown = new Sprite[4];
    public Sprite[] walkLeft = new Sprite[4];
    public Sprite[] walkRight = new Sprite[4];

    [Header("Sprite切换间隔(s)")]
    public float interval = 0.5f;

    // 是否从第二帧开始
    [Header("是否从第二帧开始")]
    public bool isStartOnSecondSprite = true;

    private Move2D move2D;
    private SpriteRenderer spriteRenderer;
    private int walkArrayIdx = 0;
    private float deltaTimeAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        move2D = GetComponent<Move2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        deltaTimeAmount = isStartOnSecondSprite ? interval : 0;
    }

    // Update is called once per frame
    void Update()
    {
        SwitchSprite();
    }

    private void SwitchSprite()
    {
        if (!move2D.IsMoving() && walkArrayIdx == 0)
        {
            return;
        }
        if (!move2D.IsMoving() || move2D.IsDirectiionChanged())
        {
            walkArrayIdx = 0;
            deltaTimeAmount = isStartOnSecondSprite ? interval : 0;
        }
        else if (deltaTimeAmount >= interval)
        {
            walkArrayIdx++;
            deltaTimeAmount = 0;
        }
        deltaTimeAmount += Time.deltaTime;

        switch (move2D.direction)
        {
            case Direction.UP:
                walkArrayIdx = walkArrayIdx % walkUp.Length;
                spriteRenderer.sprite = walkUp[walkArrayIdx];
                break;
            case Direction.DOWN:
                walkArrayIdx = walkArrayIdx % walkDown.Length;
                spriteRenderer.sprite = walkDown[walkArrayIdx];
                break;
            case Direction.LEFT:
                walkArrayIdx = walkArrayIdx % walkLeft.Length;
                spriteRenderer.sprite = walkLeft[walkArrayIdx];
                break;
            case Direction.RIGHT:
                walkArrayIdx = walkArrayIdx % walkRight.Length;
                spriteRenderer.sprite = walkRight[walkArrayIdx];
                break;
        }
    }
}
