using UnityEngine;
using System.Collections;

public class SpawnSurface : MonoBehaviour {

    public Color colorFrom, colorTo;
    public float totalTime = 1.0f;
    private Hashtable flashUp, flashDown;
    [HideInInspector]
    public bool IsFlashing { get; set; }
    public GameObject spawnPoint, originPoint;
    public GameObject risingBox;
    public GameObject objectToSpawn { get; set; }
    public GameObject target;
    public GameObject owner;
    public LayerMask collisionMask;

    public bool debug = false;

	// Use this for initialization
	void Start () {
        IsFlashing = false;
        InitHash();
	}

    private void InitHash()
    {
        flashDown = new Hashtable();
        flashUp = new Hashtable();

        flashUp.Add("color", colorTo);
        flashDown.Add("color", colorFrom);

        flashUp.Add("oncomplete", "DoneFlashUp");
        flashDown.Add("oncomplete", "DoneFlashing");
        flashUp.Add("includechildren", false);
        flashDown.Add("includechildren", false);

        flashUp.Add("time", (totalTime / 2));
        flashDown.Add("time", (totalTime / 2));
    }

    

    void CreateRiser()
    {
        GameObject spawned = Instantiate(risingBox, originPoint.transform.position, transform.rotation) as GameObject;
        RisingCode riser = spawned.GetComponent<RisingCode>();
        riser.Rise(spawnPoint.transform.position, owner, objectToSpawn, target);
    }

    void Update()
    {
        if (debug)
        {
            Debug.Log("SeatsTaken: " + SeatsTaken());
            if (!IsFlashing)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    FlashUp();
                }
            }
        }

        
    }

    public void FlashUp()
    {
        iTween.ColorTo(gameObject, flashUp);
        IsFlashing = true;
    }

    public void FlashDown()
    {
        iTween.ColorTo(gameObject, flashDown);
    }

    private void DoneFlashUp()
    {
        CreateRiser();
        StartCoroutine(DelayedFlashDown(0.5f));


    }

    public bool SeatsTaken()
    {
        return Physics.CheckSphere(spawnPoint.transform.position, 1.0f, collisionMask);
    }

    IEnumerator DelayedFlashDown(float time)
    {
        yield return new WaitForSeconds(time);
        FlashDown();
    }

    private void DoneFlashing()
    {
        IsFlashing = false;
    }
}
