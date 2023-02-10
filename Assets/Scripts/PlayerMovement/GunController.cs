using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunController : MonoBehaviour
{
    //銃情報
    private int damage;
    public float fireRate, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletPerTap;
    public bool fullAuto;

    private int bulletsLeft, bulletShot;

    //フラグ
    [HideInInspector] public bool shooting;
    [HideInInspector] public bool readyToShoot, reloading, aiming;
    [HideInInspector] public bool hitEnemy;

    //参照
    public Camera playerCam;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    public PlayerMovement playerMove;
    public Vector3 direction;
    public AudioClip[] audioClips;
    [HideInInspector] public GameObject damageText;
    [HideInInspector] public DamagePopUp damagePU;

    //グラフィック
    public GameObject muzzleParticle, bulletHoleGraphic;
    public CameraShake cameraShake;
    public TextMeshProUGUI text;
    [HideInInspector] public Animator anim;
    public float camShakeMagnitude, camShakeDuration;

    private PlayerStatus playerSta;
    private AudioSource audioSource;

    private void Awake()
    {
        //PlayerStatusから攻撃力を取得
        playerSta = transform.root.gameObject.GetComponentInParent<PlayerStatus>();

        bulletsLeft = magazineSize;
        readyToShoot = true;

        muzzleParticle.SetActive(false);

        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        MyInput();

        //テキスト表示
        text.SetText(bulletsLeft + " / " + magazineSize);

        //マズルフラッシュ
        MuzzleFlash();

        //ADS
        Aim();
    }

    public void MyInput()
    {
        //フルオートであるか
        if (fullAuto)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        //ADS
        aiming = Input.GetKey(KeyCode.Mouse1);

        //リロード実行
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Reload();
        }

        //射撃開始
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            playerMove.moveSpeed = playerMove.walkSpeed;
            bulletShot = bulletPerTap;
            Shoot();
            audioSource.PlayOneShot(audioClips[0]);
            anim.SetBool("run", false);
            anim.SetBool("fire", true);
        }
        else if (!shooting || reloading || bulletsLeft <= 0)
        {
            //画面揺れ停止
            cameraShake.StopCoroutine("Shake");
            cameraShake.ResetCamPos();

            anim.SetBool("fire", false);
        }
    }

    public void Shoot()
    {
        readyToShoot = false;

        //散弾度
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //散弾度を加味した方向
        direction = playerCam.transform.forward + new Vector3(x, y, 0);

        //Raycast
        if (Physics.Raycast(playerCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            if (rayHit.collider.CompareTag("Enemy0") || rayHit.collider.CompareTag("Enemy1") || rayHit.collider.CompareTag("Enemy2") || rayHit.collider.CompareTag("Enemy3"))
            {
                damage = playerSta.damage;
                hitEnemy = true;
                //ダメージテキスト生成
                rayHit.collider.GetComponent<SlimeController>().SlimeDamage(damage);
            }
        }

        //ShakeCamera
        cameraShake.StartCoroutine("Shake");

        //Graphics
        Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));

        bulletsLeft--;
        bulletShot--;

        Invoke("ResetShot", fireRate);

        if (bulletShot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        audioSource.PlayOneShot(audioClips[1]);
        anim.SetBool("reload", true);
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        anim.SetBool("reload", false);
        reloading = false;
    }

    private void MuzzleFlash()
    {
        if (readyToShoot)
        {
            //particle.Stop();
            muzzleParticle.SetActive(false);
        }
        else
        {
            //particle.Play();
            muzzleParticle.SetActive(true);
        }
    }

    private void Aim()
    {
        if (aiming)
        {
            anim.SetBool("aim", true);
        }
        else
        {
            anim.SetBool("aim", false);
        }
    }
}
