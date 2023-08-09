using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuRu : MonoBehaviour
{
    public Sprite[] sprites;
    SpriteRenderer render;
    Rigidbody2D rigid;

    public GameObject kinggu;

    public Rigidbody2D Rigid { get { return rigid; } }

    public float dir = 1;

    [SerializeField] Collider2D capsule;
    [SerializeField] Collider2D box;

    [SerializeField] Collider2D itemCollider;

    bool isJumped = false;
    public bool isStopped = false;

    [SerializeField] Collider2D[] colliders;

    AudioSource source;

    [SerializeField] Transform head;
    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

        source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (!TryGetComponent(out rigid))
        {
            rigid = gameObject.AddComponent<Rigidbody2D>();
        }
        foreach (var x in colliders)
        {
            x.enabled = false;
        }
        render.sprite = sprites[0];
        box.enabled = true;
        isJumped = false;
        isStopped = false;
        itemCollider.enabled = false;
        rigid.gravityScale = 0;
    }
    private void Update()
    {
        if (GameManager.Instance.IsGameOver) return;
        if (isStopped) return;

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (!isJumped)
            {
                Jump();
                GameManager.Instance.OnJump.Invoke();
            }
        }
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.IsGameOver) return;
        if (isStopped) return;

        if (!isJumped)
        {
            transform.Translate(GameManager.Instance.Speed * dir * Time.deltaTime * Vector3.right);
        }

        if (isJumped && rigid.velocity.y < 0)
        {
            foreach (var x in colliders)
            {
                x.enabled = true;
            }
        }

    }
    public void Jump()
    {
        box.enabled = false;
        rigid.AddForce(Vector2.up * 800);
        rigid.gravityScale = 2;
        isJumped = true;
        render.sprite = sprites[1];

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isStopped) return;

        if (collision.gameObject.CompareTag("Head"))
        {
            render.sprite = sprites[2];
            render.sortingLayerName = "Default";

            rigid.bodyType = RigidbodyType2D.Static;

            collision.collider.enabled = false;

            capsule.enabled = false;
            itemCollider.enabled = true;
            if (collision.transform.parent != null)
            {
                Vector2 dir = transform.position - collision.transform.position;
                dir.Normalize();
                foreach (var x in collision.transform.parent.GetComponentsInChildren<Collider2D>())
                {
                    x.enabled = false;
                }

                float angle = Vector2.Angle(transform.up, dir);

                if (dir.x > 0) angle *= -1;

                transform.Rotate(new Vector3(0, 0, angle / 2));

                Vector3 angles = transform.eulerAngles;
                angles.x = WrapAngle(angles.x);
                angles.y = WrapAngle(angles.y);
                angles.z = WrapAngle(angles.z);
                angles.z = Mathf.Clamp(angles.z, -25, 25);

                transform.eulerAngles = angles;
                transform.position = collision.transform.position + new Vector3(dir.x, dir.y, 1) * 0.75f;
            }
            isStopped = true;

            Destroy(rigid);
            GameManager.Instance.Spawner.Enqueue(this);
            GameManager.Instance.last = head;
            transform.SetParent(GameManager.Instance.Spawner.ruru.transform);

            GameManager.Instance.OnLand.Invoke();

        }
    }

    float WrapAngle(float angle)
    {
        angle %= 360;

        if (angle > 180)
        {
            return angle - 360;
        }

        return angle;
    }

    bool isKinggu;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isStopped) return;

        if (collision.gameObject.CompareTag("Wall"))
        {
            dir *= -1;
        }

        if (isJumped && collision.gameObject.CompareTag("Floor"))
        {
            if (GameManager.Instance.kingguObtained > 0)
            {
                StartCoroutine(UseKinggu());
            }
            else
            {
                GameManager.Instance.Fall();
                source.Play();
                Destroy(this);
                Destroy(gameObject, 2f);
            }
        }
    }

    IEnumerator UseKinggu()
    {
        if (GameManager.Instance.last != null)
        {
            if (!isKinggu)
            {

                transform.position = GameManager.Instance.floor.position - Vector3.up * 3;
                transform.position = new Vector3(GameManager.Instance.last.position.x, transform.position.y, 5);
                GameObject obj = Instantiate(kinggu);
                obj.transform.position = transform.position;
                obj.transform.SetParent(transform);

                isKinggu = true;
                rigid.gravityScale = 0;
                rigid.velocity = Vector3.zero;

                yield return new WaitForSeconds(0.5f);

                foreach (var x in colliders)
                {
                    x.enabled = false;
                }

                Vector3 startPos = transform.position;
                Vector3 endPos = GameManager.Instance.last.position + Vector3.up * 1.5f;
                endPos = new Vector3(endPos.x, endPos.y, 5);

                float lerpTime = 1;
                float curTime = 0;

                while (curTime < lerpTime)
                {
                    transform.position = Vector3.Lerp(startPos, endPos, curTime / lerpTime);
                    curTime += Time.fixedDeltaTime;
                    yield return new WaitForFixedUpdate();
                }

                rigid.gravityScale = 1;
                isKinggu = false;
                GameManager.Instance.kingguObtained--;
                Destroy(obj);
            }
        }
    }
}
