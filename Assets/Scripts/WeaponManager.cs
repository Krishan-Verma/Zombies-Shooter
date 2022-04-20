using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public GameObject playCam;
    float range = 100f;
    public float damage=20f;
    public Animator playerAnimator;
    public ParticleSystem muzzleFlash;
    public GameObject nonTargetHitParticles;
    public GameObject HitParticles;
    public GameObject crossHair;
    public Text currAmmoText;
    public Text resAmmoText;
    public PlayerManager playerManager;

    public AudioClip gunshot;
    AudioSource audioSource;
   
    public float currentAmmo;
    public float maxAmmo;
    public float reloadTime;
    public float reserveAmmo;
    public float fireRate=10f;
    public float fireRateTimer;
    public float ammoCap;
    public bool isAutomatic;
    bool isReloading;


    private void OnEnable()
    {
        currAmmoText.text = currentAmmo.ToString();
        resAmmoText.text = reserveAmmo.ToString();

    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currAmmoText.text = currentAmmo.ToString();
        resAmmoText.text = reserveAmmo.ToString();
        ammoCap = reserveAmmo;
    }

    
    void Update()
    {
       

        if (reserveAmmo<=0 && currentAmmo<=0)
        {
            Debug.Log("No Ammo letf in this Weapon");
            return;
        }
       if(currentAmmo<=0 && !isReloading)
        {
            StartCoroutine(Reload(reloadTime));
            return;

        }

       if(isReloading)
        {
            return;
        }

       if(Input.GetKeyDown(KeyCode.R) && reserveAmmo>0 && isAutomatic)
        {
            StartCoroutine(Reload(reloadTime));
            return;
        }

       if(fireRateTimer>0)
        {
            fireRateTimer -= Time.deltaTime;
        }

    }

    public IEnumerator Reload(float rt)
    {
        isReloading = true;
        playerAnimator.SetTrigger("isReloading");
        

        yield return new WaitForSeconds(rt);

        float missingAmmo = maxAmmo - currentAmmo;

        if(reserveAmmo>=missingAmmo)
        {
            currentAmmo += missingAmmo;
            reserveAmmo -= missingAmmo;
            currAmmoText.text = currentAmmo.ToString();
            resAmmoText.text = reserveAmmo.ToString();
        }
        else
        {
            currentAmmo += reserveAmmo;
            reserveAmmo = 0;
            currAmmoText.text = currentAmmo.ToString();
            resAmmoText.text = reserveAmmo.ToString();
        }
        isReloading = false;
       
    }

   

    public void Shoot()
    {
        if (fireRateTimer <= 0)
        {
            Invoke("StopShooting",1f);
            fireRateTimer = 1 / fireRate;
            currentAmmo--;
            currAmmoText.text = currentAmmo.ToString();
            muzzleFlash.Play();
            audioSource.PlayOneShot(gunshot);

            RaycastHit hit;

            playerAnimator.SetBool("isShooting", true);

            if (Physics.Raycast(playCam.transform.position, playCam.transform.forward, out hit, range))
            {
                EnemyManager enemyManager = hit.transform.GetComponent<EnemyManager>();

                if (enemyManager != null)
                {
                    enemyManager.Hit(damage);

                    if (enemyManager.health <= 0)
                    {
                        playerManager.currentPoints += enemyManager.points;
                    }

                    GameObject InstParticles = Instantiate(HitParticles, hit.point, Quaternion.LookRotation(hit.normal));
                    InstParticles.transform.parent = hit.transform;
                    Destroy(InstParticles, 2f);
                }
                else
                {
                    GameObject InstParticles = Instantiate(nonTargetHitParticles, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(InstParticles, 20f);

                }
            }
        }
    }

    private void OnDisable()
    {

        playerAnimator.SetBool("isReloading", false);

        isReloading = false;

    }

    void StopShooting()
    {
      playerAnimator.SetBool("isShooting", false);
        
    }
}
