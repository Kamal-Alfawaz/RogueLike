using UnityEngine;

public class BossSpawnInteractable : MonoBehaviour, IInteractable{
    static private int activateCount = 0;

    private bool isInRange = false;
    
    public GameObject bossPrefab;
    public GameObject interactionUI;
    public Transform bossSpawnPoint;
    private Outline outline;

    void Start(){
        outline = this.gameObject.AddComponent<Outline>();
        outline.enabled = false;        
    }
    
    public void Interact(){
        // spawn boss
        Debug.Log("detected");
        activateCount++;
        if (activateCount == 3){
            Instantiate(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);
        }
        this.gameObject.SetActive(false);
    }

    public void InRange(){
        isInRange = true;
        interactionUI.SetActive(true);
        outline.enabled = true;    
    }

    public void OutOfRange(){
        isInRange = false;
        interactionUI.SetActive(false);
        outline.enabled = false;     
    }
}