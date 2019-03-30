using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2D : MonoBehaviour
{
    public MoveKeysPlan moveKeysPlan = MoveKeysPlan.WASD;
    public Direction direction = Direction.DOWN;
    private Direction firstKeyDirection = Direction.DOWN;
    public float speed = 2;

    private GameObject gameObj;
    private KeyCode up;
    private KeyCode down;
    private KeyCode left;
    private KeyCode right;
    private bool isDirectionChanged = false;

    // Start is called before the first frame update
    void Start()
    {
        SetMoveKeyCode(moveKeysPlan);
        gameObj = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        ResetDirection();
        Move();
    }

    private void SetMoveKeyCode(MoveKeysPlan plan)
    {
        switch (plan)
        {
            case MoveKeysPlan.ARROW:
                up = KeyCode.UpArrow;
                down = KeyCode.DownArrow;
                left = KeyCode.LeftArrow;
                right = KeyCode.RightArrow;
                break;
            case MoveKeysPlan.WASD:
            default:
                up = KeyCode.W;
                down = KeyCode.S;
                left = KeyCode.A;
                right = KeyCode.D;
                break;
        }
    }

    private void ResetDirection()
    {
        if (Input.GetKey(up) && !Input.GetKey(down) && !Input.GetKey(left) && !Input.GetKey(right))
        {
            isDirectionChanged = direction != Direction.UP;
            firstKeyDirection = Direction.UP;
            direction = Direction.UP;
        }
        else if (!Input.GetKey(up) && Input.GetKey(down) && !Input.GetKey(left) && !Input.GetKey(right))
        {
            isDirectionChanged = direction != Direction.DOWN;
            firstKeyDirection = Direction.DOWN;
            direction = Direction.DOWN;
        }
        else if (!Input.GetKey(up) && !Input.GetKey(down) && Input.GetKey(left) && !Input.GetKey(right))
        {
            isDirectionChanged = direction != Direction.LEFT;
            firstKeyDirection = Direction.LEFT;
            direction = Direction.LEFT;
        }
        else if (!Input.GetKey(up) && !Input.GetKey(down) && !Input.GetKey(left) && Input.GetKey(right))
        {
            isDirectionChanged = direction != Direction.RIGHT;
            firstKeyDirection = Direction.RIGHT;
            direction = Direction.RIGHT;
        }
        else if (Input.GetKey(up) && Input.GetKey(down) && !Input.GetKey(left) && !Input.GetKey(right))
        {
            switch (firstKeyDirection)
            {
                case Direction.UP:
                    isDirectionChanged = direction != Direction.DOWN;
                    direction = Direction.DOWN;
                    break;
                case Direction.DOWN:
                    isDirectionChanged = direction != Direction.UP;
                    direction = Direction.UP;
                    break;
            }
        }
        else if (Input.GetKey(up) && !Input.GetKey(down) && Input.GetKey(left) && !Input.GetKey(right))
        {
            switch (firstKeyDirection)
            {
                case Direction.UP:
                    isDirectionChanged = direction != Direction.LEFT;
                    direction = Direction.LEFT;
                    break;
                case Direction.LEFT:
                    isDirectionChanged = direction != Direction.UP;
                    direction = Direction.UP;
                    break;
            }
        }
        else if (Input.GetKey(up) && !Input.GetKey(down) && !Input.GetKey(left) && Input.GetKey(right))
        {
            switch (firstKeyDirection)
            {
                case Direction.UP:
                    isDirectionChanged = direction != Direction.RIGHT;
                    direction = Direction.RIGHT;
                    break;
                case Direction.RIGHT:
                    isDirectionChanged = direction != Direction.UP;
                    direction = Direction.UP;
                    break;
            }
        }
        else if (!Input.GetKey(up) && Input.GetKey(down) && Input.GetKey(left) && !Input.GetKey(right))
        {
            switch (firstKeyDirection)
            {
                case Direction.DOWN:
                    isDirectionChanged = direction != Direction.LEFT;
                    direction = Direction.LEFT;
                    break;
                case Direction.LEFT:
                    isDirectionChanged = direction != Direction.DOWN;
                    direction = Direction.DOWN;
                    break;
            }
        }
        else if (!Input.GetKey(up) && Input.GetKey(down) && !Input.GetKey(left) && Input.GetKey(right))
        {
            switch (firstKeyDirection)
            {
                case Direction.DOWN:
                    isDirectionChanged = direction != Direction.RIGHT;
                    direction = Direction.RIGHT;
                    break;
                case Direction.RIGHT:
                    isDirectionChanged = direction != Direction.DOWN;
                    direction = Direction.DOWN;
                    break;
            }
        }
        else if (!Input.GetKey(up) && !Input.GetKey(down) && Input.GetKey(left) && Input.GetKey(right))
        {
            switch (firstKeyDirection)
            {
                case Direction.LEFT:
                    isDirectionChanged = direction != Direction.RIGHT;
                    direction = Direction.RIGHT;
                    break;
                case Direction.RIGHT:
                    isDirectionChanged = direction != Direction.LEFT;
                    direction = Direction.LEFT;
                    break;
            }
        }

    }

    private void Move()
    {
        if (!IsMoving())
        {
            return;
        }
        
        switch (direction)
        {
            case Direction.UP:
                gameObj.transform.position += new Vector3(0, speed * Time.deltaTime, 0);
                break;
            case Direction.DOWN:
                gameObj.transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
                break;
            case Direction.LEFT:
                gameObj.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
                break;
            case Direction.RIGHT:
                gameObj.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
                break;
        }
    }

    public bool IsMoving()
    {
        bool ret = false;
        ret = Input.GetKey(up) || Input.GetKey(down) || Input.GetKey(left) || Input.GetKey(right);
        return ret;
    }

    public bool IsDirectiionChanged()
    {
        return isDirectionChanged;
    }
}
// 移动按钮方案
public enum MoveKeysPlan
{
    WASD = 1,
    ARROW = 2
}
// 朝向
public enum Direction
{
    UP = 1,
    DOWN = 2,
    LEFT = 3,
    RIGHT = 4
}