using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour
{

    private static int _referenceCount = 0;
    private static SceneManager _instance;

    [HideInInspector]
    public float ShotScale = 0;
    
    public int ShotScaleCoefficient = 2;
    [HideInInspector]
    public Vector3 ShotScaleVector;
    public UILabel ShotScaleLabel;

    [HideInInspector]
    public float EnemyScale = 0;
    [HideInInspector]
    public Vector3 EnemyScaleVector;

    public float EnemyScaleCoefficient = 1;
    public UILabel EnemyScaleLabel;

    public float EnemySpeedCoefficient = 3;
    public UILabel EnemySpeedLabel;
    public float EnemySpeed;

    [HideInInspector] 
    public float WallScale = 0;
    public float WallScaleCoefficient = 5f;
    public UILabel WallScaleLabel;
    public GameObject LeftWall, RightWall, TopWall, BottomWall;

    private Vector3 leftWallPos, rightWallPos, topWallPos, bottomWallPos;
    private Transform leftWallTrans, rightWallTrans, topWallTrans, bottomWallTrans;

    public static SceneManager Instance
    {
        get { return _instance; }
    }

    void Start()
    {
        UpdateShot();
        InitWalls();
    }

    private void InitWalls()
    {
        if (!WallsNull())
        {
            leftWallTrans = LeftWall.transform;
            rightWallTrans = RightWall.transform;
            topWallTrans = TopWall.transform;
            bottomWallTrans = BottomWall.transform;

            leftWallPos = leftWallTrans.position;
            rightWallPos = rightWallTrans.position;
            topWallPos = topWallTrans.position;
            bottomWallPos = bottomWallTrans.position;
        }
    }

	void Awake()
	{
	    _referenceCount++;
        if (_referenceCount > 1)
        {
            DestroyImmediate(this.gameObject);
            return;
        }

	    _instance = this;
	}

    void OnDestroy()
    {
        _referenceCount--;
        if (_referenceCount == 0)
        {
            _instance = null;
        }
    }

    void EnemySliderChange(float val)
    {
        if (val > 0)
        {
            UpdateEnemyScale(val);
        }
    }

    private void UpdateEnemyScale(float val)
    {
        EnemyScale = val*2f;

        EnemyScaleVector = new Vector3(EnemyScale, 1f, EnemyScale);

        if (EnemyScaleLabel != null)
        {
            EnemyScaleLabel.text = EnemyScale.ToString("0.00");

        }
    }

    void StageScaleSliderChange(float val)
    {
        if (!WallsNull())
        {
            MoveTheWalls(val);
        }
    }

    private void MoveTheWalls(float val)
    {
        float _modVal = (val - 0.5f * 2)*WallScaleCoefficient;
        // move left wall
        leftWallTrans.position = new Vector3(leftWallPos.x - _modVal, leftWallPos.y, leftWallPos.z);
        
        // move right wall
        rightWallTrans.position = new Vector3(rightWallPos.x + _modVal, rightWallPos.y, rightWallPos.z);
        
        // move top wall
        topWallTrans.position = new Vector3(topWallPos.x, topWallPos.y, topWallPos.z + _modVal);
        
        // move bottom wall
        bottomWallTrans.position = new Vector3(bottomWallPos.x, bottomWallPos.y, bottomWallPos.z - _modVal);

        if (WallScaleLabel != null)
        {
            WallScaleLabel.text = _modVal.ToString("000");
        }
    }

    private bool WallsNull()
    {
        return (LeftWall == null || RightWall == null || TopWall == null || BottomWall == null);
    }

    void EnemySpeedSliderChange(float val)
    {
        EnemySpeed = val*EnemySpeedCoefficient;
        if (EnemySpeedLabel != null)
        {
            EnemySpeedLabel.text = EnemySpeed.ToString("00");
        }
    }

    void ShotSliderChange(float val)
    {
        if (val > 0)
        {
            CalculateRealScaleFloat(val);
            UpdateShot();
        }
        
    }

    private void CalculateRealScaleFloat(float val)
    {
        
        if (val > 0)
        {
            ShotScale = val * ShotScaleCoefficient;
        }
        else
        {
            ShotScale = val;
        }
        
    }

    private void UpdateShot()
    {
        
        Vector3 tempVector = Vector3.one*ShotScale;
        
        ShotScaleVector = tempVector;

        if (ShotScaleLabel != null)
        {
            ShotScaleLabel.text = ShotScale.ToString("0.00");

        }
    }
}
