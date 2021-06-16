using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float jumpForce;
    [SerializeField] float speedZ;
    [SerializeField] float speedX;
    Vector3 latestPos;
    [SerializeField] float maxSpeedZ;
    [SerializeField] GameObject fireworks;
    [SerializeField] GameObject mainCamera;
    [SerializeField] AudioSource audioBGM;
    [SerializeField] GameObject restartBtn;
    [SerializeField] GameObject player;
    public bool isGoal { get; set; } = false;
    public int life { get; set; } = 3;
    //FacePartsSpereに衝突時の効果音
    AudioSource audioSource;
    bool ismovedToFoward;
    bool isChangedDirectionToRight;
    bool isChangeDirectionToLeft;
    [SerializeField] Animator charactorAnimator;
    GameObject obj;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject destroyPlayer;
    bool isGround; // true→地面についている
    List<GameObject> faceParts = new List<GameObject>();
    [SerializeField] GameObject faceFoundation;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Animator charactorAnimator = player.GetComponent<Animator>();
    }

    void Start()
    {
        latestPos = transform.position;

        audioSource = GetComponent<AudioSource>();

        charactorAnimator.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        //顔のパーツののposition
        Vector3 facePartsPos = new Vector3(0, 0, 0);
        //顔のパーツののrotation
        Vector3 facePartsRotVector3 = new Vector3(0, 0, 0);
        Quaternion facePartsRot = Quaternion.Euler(0.0f, 180.0f, 180.0f);
        //顔のパーツのscale
        Vector3 facePartsScale = new Vector3(0, 0, 0);
        //顔のパーツに触れた
        bool isAttachedFaceParts = false;
        //顔のパーツのスクリプト
        FaceParts facePartsScript;

        //衝突した顔のパーツによってpositinを変更
        switch (other.tag)
        {
            case "LeftEye":
                //顔パーツの回転を停止
                facePartsScript = other.gameObject.GetComponent<FaceParts>();
                facePartsScript.enabled = false;

                //顔のパーツをplayerにセット
                other.GetComponent<MeshCollider>().enabled = false;
                facePartsPos = new Vector3(0.082f, 6.185f, 0.22f);
                facePartsScale = new Vector3(0.08f, 0.08f, 0);
                facePartsRotVector3 = other.gameObject.transform.eulerAngles;
                facePartsRot = Quaternion.Euler(0.0f, 180.0f, facePartsRotVector3.z);


                //各フラグを変更
                isAttachedFaceParts = true;
                FaceParts.hasAttachedLeftEye = true;
                break;
            case "RightEye":
                //顔パーツの回転を停止
                facePartsScript = other.gameObject.GetComponent<FaceParts>();
                facePartsScript.enabled = false;

                other.GetComponent<MeshCollider>().enabled = false;
                facePartsPos = new Vector3(-0.066f, 6.184f, 0.22f);
                facePartsScale = new Vector3(0.08f, 0.08f, 0);
                facePartsRotVector3 = other.gameObject.transform.eulerAngles;
                facePartsRot = Quaternion.Euler(0.0f, 180.0f, facePartsRotVector3.z);

                isAttachedFaceParts = true;
                FaceParts.hasAttachedRightEye = true;
                break;
            case "Nose":
                //顔パーツの回転を停止
                facePartsScript = other.gameObject.GetComponent<FaceParts>();
                facePartsScript.enabled = false;

                other.GetComponent<MeshCollider>().enabled = false;
                facePartsPos = new Vector3(0.016f, 6.1f, 0.22f);
                facePartsScale = new Vector3(0.07f, 0.07f, 0);
                facePartsRotVector3 = other.gameObject.transform.eulerAngles;
                facePartsRot = Quaternion.Euler(0.0f, 180.0f, facePartsRotVector3.z);

                isAttachedFaceParts = true;
                FaceParts.hasAttachedNose = true;
                break;
            case "Mouth":
                //顔パーツの回転を停止
                facePartsScript = other.gameObject.GetComponent<FaceParts>();
                facePartsScript.enabled = false;

                other.GetComponent<MeshCollider>().enabled = false;
                facePartsPos = new Vector3(0.016f, 5.94f, 0.22f);
                facePartsScale = new Vector3(0.15f, 0.1f, 0);
                facePartsRotVector3 = other.gameObject.transform.eulerAngles;
                facePartsRot = Quaternion.Euler(0.0f, 180.0f, facePartsRotVector3.z);

                isAttachedFaceParts = true;
                FaceParts.hasAttachedMouth = true;
                break;
            case "ReturnMainSphere":
                audioSource.Play();
                break;
            case "Goal":
                //効果音
                audioBGM.Stop();
                other.GetComponent<AudioSource>().Play();

                //ゴールポイントを非表示
                other.gameObject.GetComponent<MeshRenderer>().enabled = false;

                //花火をplay
                fireworks.SetActive(true);

                //プレイヤーを固定
                Rigidbody p_rigidbody = GetComponent<Rigidbody>();
                p_rigidbody.constraints = RigidbodyConstraints.FreezeAll;

                Transform transformAtGoal = gameObject.transform;
                Destroy(destroyPlayer);
                obj = Instantiate(playerPrefab);
                obj.transform.SetParent(transform);
                Vector3 pos = new Vector3(0, 5, 0);
                obj.transform.localPosition = pos;
                obj.transform.localRotation = Quaternion.Euler(0, 0, 0);

                //カメラでキャラの正面を写すフラグ
                isGoal = true;

                //リスタートボタン表示
                restartBtn.SetActive(true);

                //managerSceneのロードフラグをfalse
                ManagerSceneLoader.isloaded = false;

                // 顔のパーツを表示
                faceFoundation.SetActive(true);
                for (int i = 0; i < faceParts.Count; i++)
                {
                    faceParts[i].GetComponent<MeshRenderer>().enabled = true;
                }
                    break;
            default:
                break;
        }

        //顔のパーツをセット
        if (isAttachedFaceParts)
        {
            //顔のパーツをセット
            other.gameObject.transform.parent = transform;
            other.gameObject.transform.localPosition = facePartsPos;
            other.gameObject.transform.localRotation = facePartsRot;
            other.gameObject.transform.localScale = facePartsScale;

            // 顔のパーツを非表示
            faceParts.Add(other.gameObject);
            faceParts.Last().GetComponent<MeshRenderer>().enabled = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground") isGround = true;
    }

    public void FixedUpdate()
    {
        Vector3 pos = transform.position;

        //ゴール後にカメラをplayerの正面に移動
        if (isGoal && mainCamera.transform.localRotation.eulerAngles.y <= 180)
        {
            mainCamera.transform.RotateAround(pos, transform.up, 0.72f);
        }

        //前進
        if (ismovedToFoward)
        {
            if (rb.velocity.y > 0) ismovedToFoward = false;
            else rb.AddForce(transform.forward * speedZ * Time.fixedDeltaTime);
        }

        //右に方向転換
        if (isChangedDirectionToRight)
        {
            transform.Rotate(0.0f, speedX, 0.0f);
        }

        //左に方向転換
        if (isChangeDirectionToLeft)
        {
            transform.Rotate(0.0f, -speedX, 0.0f);
        }
    }

    void Update()
    {
        // Animatorにキャラの環境やパラメーターを設定する
        ApplyAnimatorParameter();

        //デバッグ用
        if (Input.GetKeyDown(KeyCode.UpArrow)) PointerDownForMoveToForward();
        if (Input.GetKeyUp(KeyCode.UpArrow)) PointerUpForMoveToForward();
        if (Input.GetKeyDown(KeyCode.RightArrow)) PointerDownForChangeDirectionToRight();
        if (Input.GetKeyUp(KeyCode.RightArrow)) PointerUpForChangeDirectionToRight();
        if (Input.GetKeyDown(KeyCode.LeftArrow)) PointerDownForChangeDirectionToLeft();
        if (Input.GetKeyUp(KeyCode.LeftArrow)) PointerUpForChangeDirectionToLeft();
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
    }

    void ApplyAnimatorParameter()
    {
        if (!isGoal)
        {
            // Animatorにキャラの環境やパラメーターを設定する
            Vector3 xyVector = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            float speed = Mathf.Abs(xyVector.magnitude);
            charactorAnimator.SetFloat("WalkSpeed", speed);

            charactorAnimator.SetBool("IsGround", isGround);
            charactorAnimator.SetFloat("FallSpeed", rb.velocity.y);
        }
    }

    public void PointerDownForMoveToForward()
    {
        if (rb.velocity.magnitude < maxSpeedZ && !isGoal)
        {
            ismovedToFoward = true;
        }
    }

    public void PointerUpForMoveToForward()
    {
        if (!isGoal) ismovedToFoward = false;
    }

    public void PointerDownForChangeDirectionToRight()
    {
        if (!isGoal) isChangedDirectionToRight = true;
    }

    public void PointerUpForChangeDirectionToRight()
    {
        if (!isGoal) isChangedDirectionToRight = false;
    }

    public void PointerDownForChangeDirectionToLeft()
    {
        if (!isGoal) isChangeDirectionToLeft = true;
    }

    public void PointerUpForChangeDirectionToLeft()
    {
        if (!isGoal) isChangeDirectionToLeft = false;
    }

    public void Jump()
    {
        if (isGround)
        {
            this.rb.AddForce(transform.up * jumpForce);
            isGround = false;
        }
    }
}
