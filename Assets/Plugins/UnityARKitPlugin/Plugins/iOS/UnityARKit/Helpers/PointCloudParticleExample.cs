using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using UnityEngine.UI;

/// <summary>
/// Point cloud particle example.
/// </summary>
public class PointCloudParticleExample : MonoBehaviour {

	//脚本单例
	private static PointCloudParticleExample instance;
	public static PointCloudParticleExample Instance {
		get { 
			return instance;
		}
	}

    public ParticleSystem pointCloudParticlePrefab;
    public int maxPointsToShow;
    public float particleSize = 1.0f;

	//屏幕提示
	public Text tipText;

    private Vector3[] m_PointCloudData;
	private  bool  frameUpdated = false;
    private  ParticleSystem currentPS;
    private ParticleSystem.Particle [] particles;


	void Awake () {
		instance = this;
	}

	// Use this for initialization
	void Start () {
		
		StartScan ();
	}
	
    public  void ARFrameUpdated(UnityARCamera camera)
    {
        m_PointCloudData = camera.pointCloudData;
        frameUpdated = true;

		//屏幕提示
		tipText.text = "请点击屏幕...";
    }

	// Update is called once per frame
	void Update () {
        if (frameUpdated) {
            if (m_PointCloudData != null && m_PointCloudData.Length > 0) {
                int numParticles = Mathf.Min (m_PointCloudData.Length, maxPointsToShow);
                ParticleSystem.Particle[] particles = new ParticleSystem.Particle[numParticles];
                int index = 0;
                foreach (Vector3 currentPoint in m_PointCloudData) {     
                    particles [index].position = currentPoint;
                    particles [index].startColor = new Color (1.0f, 1.0f, 1.0f);
                    particles [index].startSize = particleSize;
                    index++;
                }
                currentPS.SetParticles (particles, numParticles);
            } else {
                ParticleSystem.Particle[] particles = new ParticleSystem.Particle[1];
                particles [0].startSize = 0.0f;
                currentPS.SetParticles (particles, 1);
            }
            frameUpdated = false;
        }
	}

	/// <summary>
	/// Starts the scan.
	/// </summary>
	public  void StartScan() {
		//屏幕提示：
		tipText.text = "请扫描一个平面...";

		UnityARSessionNativeInterface.ARFrameUpdatedEvent += ARFrameUpdated;
		currentPS = Instantiate (pointCloudParticlePrefab);
		frameUpdated = false;
	}

	/// <summary>
	/// Stops the scan.
	/// </summary>
	public  void StopScan() {

		UnityARSessionNativeInterface.ARFrameUpdatedEvent -= ARFrameUpdated;
		Destroy (currentPS);
		frameUpdated = false;
	}
}
