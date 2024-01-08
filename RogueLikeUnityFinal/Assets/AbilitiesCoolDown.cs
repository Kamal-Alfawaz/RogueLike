using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesCoolDown : MonoBehaviour
{
    public ThirdPersonShooterController Controller;

    [Header("Dash")]
    public Image dashIcon;
    public float dashCoolDown = 2;
    bool isDashing = false;

    public TextMeshProUGUI DashTimerText;

    [Header("Grenade")]
    public Image grenadeIcon;
    public float grenadeCoolDown = 3;
    bool isGrenading = false;

    public TextMeshProUGUI GrenadeTimerText;

    [Header("Double Fire")]
    public Image doubleFireIcon;
    public float doubleFireCoolDown = 10;
    bool isDoubleFiring = false;

    public TextMeshProUGUI DoubleFireTimerText;

    [Header("AP Bullets")]
    public Image apBulletsIcon;
    public float apBulletsCoolDown = 0.5f;
    bool isAPBullets = false;

    public TextMeshProUGUI APBulletsTimerText;

    void Start()
    {
        dashIcon.fillAmount = 0;
        UpdateDashTimerText();

        grenadeIcon.fillAmount = 0;
        UpdateGrenadeTimerText();

        doubleFireIcon.fillAmount = 0;
        UpdateDoubleFireTimerText();

        apBulletsIcon.fillAmount = 0;
        UpdateAPBulletsTimerText();
    }

    void Update()
    {
        DashIconCooldown();
        UpdateDashTimerText();

        GrenadeIconCooldown();
        UpdateGrenadeTimerText();

        DoubleFireIconCooldown();
        UpdateDoubleFireTimerText();

        APBulletsIconCooldown();
        UpdateAPBulletsTimerText();
    }

    public void DashIconCooldown()
    {
        Controller = GetComponent<ThirdPersonShooterController>();
        if (isDashing == false && Controller.starterAssetsInputs.dash == true)
        {
            isDashing = true;
            dashIcon.fillAmount = 1;
        }
        if (isDashing)
        {
            dashIcon.fillAmount -= 1 / dashCoolDown * Time.deltaTime;
            if (dashIcon.fillAmount <= 0)
            {
                dashIcon.fillAmount = 0;
                isDashing = false;
            }
        }
    }

    void UpdateDashTimerText()
    {
        if (Controller.starterAssetsInputs.dash)
        {
            DashTimerText.text = "";
        }
        else
        {
            float remainingTime = Mathf.Ceil(dashIcon.fillAmount * dashCoolDown);
            DashTimerText.text = remainingTime > 0 ? remainingTime.ToString() : "";
        }
    }

    public void GrenadeIconCooldown()
    {
        Controller = GetComponent<ThirdPersonShooterController>();
        if (isGrenading == false && Controller.starterAssetsInputs.grenade == true)
        {
            isGrenading = true;
            grenadeIcon.fillAmount = 1;
        }
        if (isGrenading)
        {
            grenadeIcon.fillAmount -= 1 / grenadeCoolDown * Time.deltaTime;
            if (grenadeIcon.fillAmount <= 0)
            {
                grenadeIcon.fillAmount = 0;
                isGrenading = false;
            }
        }
    }

    void UpdateGrenadeTimerText()
    {
        if (Controller.starterAssetsInputs.grenade)
        {
            GrenadeTimerText.text = "";
        }
        else
        {
            float remainingTime = Mathf.Ceil(grenadeIcon.fillAmount * grenadeCoolDown);
            GrenadeTimerText.text = remainingTime > 0 ? remainingTime.ToString() : "";
        }
    }

    public void DoubleFireIconCooldown()
    {
        Controller = GetComponent<ThirdPersonShooterController>();
        if (isDoubleFiring == false && Controller.starterAssetsInputs.doubleFire == true)
        {
            isDoubleFiring = true;
            doubleFireIcon.fillAmount = 1;
        }
        if (isDoubleFiring)
        {
            doubleFireIcon.fillAmount -= 1 / doubleFireCoolDown * Time.deltaTime;
            if (doubleFireIcon.fillAmount <= 0)
            {
                doubleFireIcon.fillAmount = 0;
                isDoubleFiring = false;
            }
        }
    }

    void UpdateDoubleFireTimerText()
    {
        if (Controller.starterAssetsInputs.doubleFire)
        {
            DoubleFireTimerText.text = "";
        }
        else
        {
            float remainingTime = Mathf.Ceil(doubleFireIcon.fillAmount * doubleFireCoolDown);
            DoubleFireTimerText.text = remainingTime > 0 ? remainingTime.ToString() : "";
        }
    }

    public void APBulletsIconCooldown()
    {
        Controller = GetComponent<ThirdPersonShooterController>();

        if (Controller.starterAssetsInputs.shoot)
        {
            if (isAPBullets == false)
            {
                isAPBullets = true;
                apBulletsIcon.fillAmount = 1;
            }

            apBulletsIcon.fillAmount -= 1 / apBulletsCoolDown * Time.deltaTime;
            if (apBulletsIcon.fillAmount <= 0)
            {
                apBulletsIcon.fillAmount = 0;
                isAPBullets = false;
            }
        }
        else
        {
            apBulletsIcon.fillAmount = 0;
            isAPBullets = false;
        }
    }

    void UpdateAPBulletsTimerText()
    {
        if (Controller.starterAssetsInputs.shoot)
        {
            APBulletsTimerText.text = "";
        }
        else
        {
            float remainingTime = Mathf.Ceil(apBulletsIcon.fillAmount * apBulletsCoolDown);
            APBulletsTimerText.text = remainingTime > 0 ? remainingTime.ToString() : "";
        }
    }
}
