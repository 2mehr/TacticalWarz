using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CHaracter : MonoBehaviour
{
   

    public delegate void Die(CHaracter dead);
    public PlayerSide Side;
   public  event Die OnDeath;
    Vector3 MovePoint = Vector3.zero;
    public LayerMask PlayerLayer;
    NavMeshAgent Agent;
    Vector3 StartPos;
    Vector3 DeltaPos;
    public RouteManager LM;
    private float hint;
    [SerializeField]
    private float range;
    public CharCasterType CasterType;
    public bool StopAttack;
    public List<Tile> Route = new List<Tile>();
    public Tile CurrentPos;
    public int MoveSpeed;
    public GameObject Bullet;
    private int bulletSpeed;
    [SerializeField]
    public int BulletSpeed
    {
        get
        {
            return bulletSpeed;
        }

        set
        {
            bulletSpeed = value;
        }
    }
    void Start()
    {
       
        WalkPoint = 5;
        DeltaPos = transform.position;
       // LM = GameObject.Find("LevelManager").GetComponent<RouteManager>();
       
    }


    void Update()
    {


        if (Route.Count > 0 && WalkPoint > 0)
        {
            Vector3 destination = Route[0].transform.position;
            destination.y = transform.position.y;

            transform.position = Vector3.MoveTowards(transform.position, destination, MoveSpeed * Time.deltaTime);
            if(ShootTarget!=null)
            transform.rotation.SetLookRotation(ShootTarget.transform.position);
            if (Vector3.Distance(transform.position, destination) < .1f)
            {
                Route.RemoveAt(0);

                WalkPoint--;

                if (WalkPoint == 0)
                {
                    Route.Clear();
                }
            }
        }
      

    }

    public  void Attack()
    {


        GameObject bullPrifabs = Instantiate<GameObject>(Bullet, transform.position, Quaternion.identity);
        bullPrifabs.GetComponent<Bullet>().Destination = ShootTarget;
        bullPrifabs.GetComponent<Bullet>().Shooter = this;
        bullPrifabs.GetComponent<Bullet>().BulletPower = 1;
        StopAttack = true;


    }
    private void LateUpdate()
    {
        DeltaPos = transform.position;
    }
 
    private void OnMouseDown()
    {
        print(Side);
        if (LM.CurrentPlayerSide == Side)
        {
            LM.SelectedChar = this.GetComponent<CHaracter>();

        }
        else if (LM.CurrentPlayerSide != this.Side)
        {
            ShootTarget = this.GetComponent<CHaracter>();
            print(ShootTarget.name);
        }
        else //if ((LM.CurrentPlayerSide == this.Side && LM.SelectedChar.CasterType == CharCasterType.Buffer)||LM.CurrentPlayerSide !=Side && LM.SelectedChar.CasterType == CharCasterType.Harmer)
        {
            ShootTarget = this.GetComponent<CHaracter>();

        }
       
    }
    public float Hint
    {
        get
        {
            return hint;
        }

        set
        {
            hint = value;
        }
    }
    [SerializeField]
    private int walkPoint;
    public int WalkPoint
    {
        get
        {
            return walkPoint;
        }

        set
        {
            walkPoint = value;
        }
    }
   
    [SerializeField]
    private CHaracter m_ShootTarget;
    public CHaracter ShootTarget
    {
        get
        {
            return m_ShootTarget;
        }

        set
        {
            m_ShootTarget = value;
        }
    }
 
  
    [SerializeField]
    private float power;
    public float Power
    {
        get
        {
            return power;
        }

        set
        {
            power = value;
        }
    }
    [SerializeField]
    private float hp;
    public float Hp
    {
        get
        {
            return hp;
        }

        set
        {
            hp = value;
        }
    }
    [SerializeField]
    private float attackRange;
    public float AttackRange
    {
        get
        {
            return attackRange;
        }

        set
        {
            attackRange = value;
        }
    }

    public float Range
    {
        get
        {
            return range;
        }

        set
        {
            range = value;
        }
    }

    public void GetShot(float power)
    {
        hp -= power;
        if (hp<=0)
        {
            Death();
            Debug.Log("Die");
        }
    }
    public  void Death()
    {
        if (OnDeath != null)
        {
            OnDeath(this);
        }
    }
    public void Move()
    {
       
    }
  
}
public enum PlayerSide
{
    Player1
        ,
    Player2
}
public enum CharCasterType
{
    Buffer
        ,
    Harmer
}

