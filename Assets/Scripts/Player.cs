using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Player : MonoBehaviour
{
    // playerのrigidbody
    Rigidbody rb;
    //FacePartsSpereに衝突時の効果音
    AudioSource audioSource;
    // 前に進む→true
    bool ismovedToFoward;
    // 右方向に回転する→true
    bool isChangedDirectionToRight;
    // 左方向に回転する→true
    bool isChangeDirectionToLeft;
    // ゴール時にインスタンス化するプレファブ
    GameObject prefabAtGoal;
    // true→地面についている
    bool isGround;
    // 取得したfacePartsオブジェクトのリスト
    List<GameObject> faceParts = new List<GameObject>();

    [SerializeField] float jumpForce;
    [SerializeField] float speedZ;
    [SerializeField] float speedX;
    [SerializeField] float maxSpeedZ;
    [SerializeField] GameObject fireworks;
    [SerializeField] GameObject mainCamera;
    [SerializeField] AudioSource audioBGM;
    [SerializeField] GameObject restartBtn;
    [SerializeField] Animator charactorAnimator;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject destroyPlayer;
    [SerializeField] GameObject faceFoundation;

    public static bool isGoal { get; set; } = false; // ゴールした→true
    public static int life { get; set; } = 3;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        charactorAnimator.enabled = true;
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

    void OnTriggerEnter(Collider other)
    {
        //顔のパーツののposition
        Vector3 facePartsPos = new Vector3(0, 0, 0);
        //顔のパーツののrotation
        Vector3 facePartsRotVector3 = new Vector3(0, 0, 0);
        Quaternion facePartsRot = Quaternion.Euler(0.0f, 180.0f, 180.0f);
        //顔のパーツのscale
        Vector3 facePartsScale = new Vector3(0, 0, 0);
        //顔のパーツに触れた→true
        bool isAttachedFaceParts = false;
        //顔のパーツのスクリプト
        FaceParts facePartsScript;

        //衝突した顔のパーツによってpositinを変更
        switch (other.tag)
        {
            case "LeftEye":
                //顔のパーツをplayerにセット
                facePartsPos = new Vector3(0.082f, 6.185f, 0.22f);
                facePartsScale = new Vector3(0.08f, 0.08f, 0);
                //各フラグを変更
                isAttachedFaceParts = true;
                FaceParts.hasAttachedLeftEye = true;
                break;
            case "RightEye":
                //顔のパーツをplayerにセット
                facePartsPos = new Vector3(-0.066f, 6.184f, 0.22f);
                facePartsScale = new Vector3(0.08f, 0.08f, 0);
                //各フラグを変更
                isAttachedFaceParts = true;
                FaceParts.hasAttachedRightEye = true;
                break;
            case "Nose":
                //顔のパーツをplayerにセット
                facePartsPos = new Vector3(0.016f, 6.1f, 0.22f);
                facePartsScale = new Vector3(0.07f, 0.07f, 0);
                //各フラグを変更
                isAttachedFaceParts = true;
                FaceParts.hasAttachedNose = true;
                break;
            case "Mouth":
                //顔のパーツをplayerにセット
                facePartsPos = new Vector3(0.016f, 5.94f, 0.22f);
                facePartsScale = new Vector3(0.15f, 0.1f, 0);
                //各フラグを変更
                isAttachedFaceParts = true;
                FaceParts.hasAttachedMouth = true;
                break;
            // メインステージへ戻る球体
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
                rb.constraints = RigidbodyConstraints.FreezeAll;
                // 顔パーツとplayerのアニメーションが連動出来ない為、
                // キャラの初期状態を表示
                Transform transformAtGoal = gameObject.transform;
                Destroy(destroyPlayer);
                prefabAtGoal = Instantiate(playerPrefab);
                prefabAtGoal.transform.SetParent(transform);
                prefabAtGoal.transform.localPosition = new Vector3(0, 5, 0);;
                prefabAtGoal.transform.localRotation = Quaternion.Euler(0, 0, 0);
                //ゴールした→true
                isGoal = true;
                //リスタートボタン表示
                restartBtn.SetActive(true);
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
            //顔パーツの回転を停止
            facePartsScript = other.gameObject.GetComponent<FaceParts>();
            facePartsScript.enabled = false;
            //顔のパーツをセット
            other.gameObject.transform.parent = transform;
            other.gameObject.transform.localPosition = facePartsPos;
            facePartsRotVector3 = other.gameObject.transform.eulerAngles;
            facePartsRot = Quaternion.Euler(0.0f, 180.0f, facePartsRotVector3.z);
            other.gameObject.transform.localRotation = facePartsRot;
            other.gameObject.transform.localScale = facePartsScale;
            // 顔のパーツを非表示
            faceParts.Add(other.gameObject);
            faceParts.Last().GetComponent<MeshRenderer>().enabled = false;
            faceParts.Last().GetComponent<MeshCollider>().enabled = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground") isGround = true;
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
