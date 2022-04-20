using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float health=100f;
    public float healthCap;
    public Text healthText;
    public GameManager gameManager;
    public GameObject playerCamera;
    private Quaternion playerCameraOriginalRotation;


    public CanvasGroup hurtPanel;
    private float shakeTime;
    private float shakeDuration;

    public GameObject WeaponHolder;
    public int activeWeaponIndex;
    GameObject activeWeapon;

    public float currentPoints;
    public Text pointsNumber;

    private void Start()
    {
        playerCameraOriginalRotation = playerCamera.transform.localRotation;
        hurtPanel.gameObject.SetActive(true);
        AudioListener.volume = 1;
        SwitchWeapon();
        healthCap = health;
    }
    private void Update()
    {
        if(hurtPanel.alpha>0)
        {
            hurtPanel.alpha -= Time.deltaTime;
        }

        if (shakeTime < shakeDuration)
        {
            shakeTime += Time.deltaTime;
            ShakeCamera();
        }

        if (Input.GetAxis("Mouse ScrollWheel")!=0)
        {
            SwitchWeapon();
        }

        pointsNumber.text = currentPoints.ToString();
    }
    public void Hit(float damage)
    {
        health -= damage;
        healthText.text = "Health: " + health.ToString();
        if(health<=0)
        {
            hurtPanel.gameObject.SetActive(false);
            gameManager.EndGame();
            shakeDuration = 0f;

        }
        else
        {
            shakeTime = 0f;
            shakeDuration = 0.5f;
            hurtPanel.alpha = 1;
        }
    }

    public void ShakeCamera()
    {
        playerCamera.transform.localRotation = Quaternion.Euler(Random.Range(-2, 2), 0, 0);
        playerCamera.transform.localRotation = playerCameraOriginalRotation;
    }

    public void SwitchWeapon()
    {
        int weaponIndex = activeWeaponIndex + 1;
        int index = 0;
        int amountOfWeapon = WeaponHolder.transform.childCount;

        if(weaponIndex>amountOfWeapon-1)
        {
            weaponIndex = 0;
        }

        foreach(Transform child in WeaponHolder.transform)
        {
            if (index == weaponIndex)
            {
                child.gameObject.SetActive(true);
                activeWeapon = child.gameObject;

            }

            else if (child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(false);

            }

            
            index++;
        }

        activeWeaponIndex = weaponIndex;
    }


    public void ActiveWeaponShoot()
    {
        activeWeapon.GetComponent<WeaponManager>().Shoot();
    }
      
    public void ActiveWeaponReload()
    {
       StartCoroutine(activeWeapon.GetComponent<WeaponManager>().Reload(1f));
    }
}
