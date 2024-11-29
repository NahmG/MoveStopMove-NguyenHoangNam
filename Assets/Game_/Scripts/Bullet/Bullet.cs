using UnityEngine;

public class Bullet : GameUnit
{
    [SerializeField] GameObject hit_vfx;
    [SerializeField] float speed;
    [SerializeField] float rotate;
    float moveSpeed;

    [field: SerializeField] protected Transform model;

    float range;

    Vector3 startPos;
    protected Vector3 direction;

    Character shooter;
    public Character Shooter => shooter;

    private void OnEnable()
    {
        shooter = null;
        range = 0;
        direction = Vector3.zero;
    }

    private void Update()
    {
        Shoot();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Character"))
        {
            Character target = col.gameObject.GetComponent<Character>();
            if (shooter != null && target != shooter)
            {
                shooter.LevelUp(target);

                Player player = shooter.GetComponent<Player>();
                if (player != null)
                {
                    player.CombatText(target);
                }

                target.Die();

                if (!shooter.IsBoosted)
                {
                    OnDespawn();
                }

                GameObject go = Instantiate(hit_vfx, transform.position, Quaternion.identity);
                Destroy(go, .5f);
            }
        }
    }

    public virtual void Shoot()
    {
        if (shooter.IsBoosted)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale * 2, 1f * Time.deltaTime);
            moveSpeed = Mathf.Lerp(speed, 15f, 5f * Time.deltaTime);
            if (Vector3.Distance(transform.position, startPos) > range * 1.5f)
            {
                OnDespawn();
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, startPos) > range)
            {
                OnDespawn();
            }
            moveSpeed = speed;
        }

        transform.Translate(moveSpeed * Time.deltaTime * direction);

        

        if (direction != Vector3.zero)
        {
            if (rotate == 0)
            {
                Quaternion rot = Quaternion.LookRotation(direction, Vector3.up);
                model.rotation = Quaternion.RotateTowards(model.rotation, rot, 120);

                Quaternion rotation = model.rotation;
                rotation = Quaternion.Euler(-90, rotation.eulerAngles.y, rotation.eulerAngles.z);

                model.rotation = rotation;
            }

            else
            {
                model.Rotate(0, 0, rotate);
            }
        }
        

    }

    public void Init(float range, Vector3 direction, Character shooter)
    {
        this.range = range;
        this.direction = direction;
        this.shooter = shooter;

        transform.localScale = shooter.transform.localScale;

        startPos = transform.position;
    }

    public void OnDespawn()
    {
        shooter.ResetBooster();
        MiniPool.Despawn(this);
    }
}
