using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossTriggerArea : MonoBehaviour
{

    public LayerMask triggerLayers;
    public WeakPointBoss owner;
    private Mesh myMesh;
    private Vector3 maxBounds, minBounds;
    public GameObject shotMarker;
    public List<Vector3> activeShots;
    private Transform _transform;
    public float MinDistance = 3.0f;
    private float sqrMinDistance;
    void Start()
    {
        sqrMinDistance = MinDistance*MinDistance;
        myMesh = ((MeshFilter) transform.GetComponent<MeshFilter>()).mesh;
        _transform = transform;
        activeShots = new List<Vector3>();
        //maxBounds = myMesh.bounds.max;
        //minBounds = myMesh.bounds.min;

        CalculateMinMax();

    }

    private void CalculateMinMax()
    {
        // get center
        Vector3 center = _transform.position;
        float halfX = _transform.localScale.x/2;
        float halfZ = _transform.localScale.z/2;
        maxBounds = new Vector3(center.x + halfX, 0, center.z + halfZ);
        minBounds = new Vector3(center.x - halfX, 0, center.z - halfZ);
    }

    public Vector3 GetRandomWithinBounds(float yValue)
    {
        return new Vector3(Random.Range(minBounds.x, maxBounds.x), yValue, Random.Range(minBounds.z, maxBounds.z));
    }

    public bool IsShotLegal(Vector3 proposed)
    {
        foreach (Vector3 shot in activeShots)
        {
            if (shot.sqrMagnitude - proposed.sqrMagnitude < sqrMinDistance)
            {
                return false;
            }
        }

        return true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (Constants.IsInLayerMask(other.gameObject, triggerLayers))
        {
            owner.PlayerInRange();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            CreateShotMarker();
        }
    }

    private void CreateShotMarker()
    {
        Vector3 markerLocation = GetRandomWithinBounds(-0.299f);

        while (!IsShotLegal(markerLocation))
        {
            markerLocation = GetRandomWithinBounds(-0.299f);
        }

        GameObject marker = Instantiate(shotMarker, markerLocation, Quaternion.identity) as GameObject;
    }
}
