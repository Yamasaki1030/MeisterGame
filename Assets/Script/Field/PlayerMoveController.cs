using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class PlayerMoveController : MonoBehaviour {

    Rigidbody rb;
    [SerializeField, HideInInspector] Animator animator;
    Vector3 move;
    float radian;
    float speed = 6.0f;
    // 近くの採掘ポイント
    public GameObject activeMiningpoint;

    // マウス・画面フリックで使用
    Vector2 startScreenPos;
    Vector2 nowScreenPos;

    // NavMesh移動
    [SerializeField, HideInInspector] NavMeshAgent playerNav;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        // 初期リス地点
        //transform.position = LoadData.getPlayerPos();
        // NavMesh移動
        playerNav = GetComponent<NavMeshAgent>();

        LoadData.DataLoad();
    }

    // 個別の採集ポイントを選択
    public void onClickMiningPoint()
    {
        activeMiningpoint.GetComponent<MiningGenerator>().OnClick();
    }

    private void Update()
    {
        // キーボード移動
        //MoveKeyBord();
        // マウス移動
        MoveClick();
        // 実機でフリック移動
        //MoveFrick();
        // NavMesh移動
        //MoveNav();


        if (Input.GetKeyDown(KeyCode.Space))
        {
            speed = 12.0f;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            speed = 6.0f;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            //GameObject.Find("DOAnimation").GetComponent<DOAnimation>().InOut_DOFade(true);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        MoveFrick();
    }


    // キーボード移動
    void MoveKeyBord()
    {
        float h = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float v = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        move = new Vector3(h, 0, v);
        rb.MovePosition(transform.position + move);

        // 移動方向の算出
        if (move.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(move.x, 0
                , move.z));
            animator.SetBool("is_running", true);
        }
        else
        {
            animator.SetBool("is_running", false);
        }
    }

    // マウス移動
    void MoveClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startScreenPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            nowScreenPos = Input.mousePosition;
            Vector2 dt = nowScreenPos - startScreenPos;

            // speed量の変化

            // 角度の計算
            float degree = Mathf.Atan2(dt.y, dt.x) * Mathf.Rad2Deg;
            radian = degree * Mathf.PI / 180;
        }
        
        // 移動方向の算出
        if (move.magnitude > 0.01f)
        {
            
            animator.SetBool("is_running", true);
        }
        else
        {
            animator.SetBool("is_running", false);
        }
    }

    // 実機でフリック移動
    void MoveFrick()
    {
        rb.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, radian, 0), 10);
    }

    // NavMesh移動
    void MoveNav()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(IsGUIHit(Input.mousePosition)) { return; }
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                if(hit.collider.gameObject.tag == "Ground")
                {
                    playerNav.SetDestination(hit.point);
                    animator.speed = 1;
                }

            }
        }

        animator.SetFloat("Speed", playerNav.velocity.sqrMagnitude);
    }

    // GUIとフィールドの判別
    bool IsGUIHit(Vector3 _scrPos)
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = _scrPos;
        List<RaycastResult> result = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, result);
        return (result.Count > 1);
    }


    public GameObject GetPlayerData()
    {
        return this.gameObject;
    }
    public void SetPlayerData(GameObject playerData)
    {
        transform.position = playerData.transform.position;
        transform.rotation = playerData.transform.rotation;
    }
}
