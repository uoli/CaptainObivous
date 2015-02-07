using UnityEngine;
using System.Collections;


public class PerlinGenerator {
	private int pixWidth;
	private int pixHeight;
	public float xOrg;
	public float yOrg;
	public float scale = 5.0F;
	private Texture2D noiseTex;
	private Color[] pix;

	public PerlinGenerator(int width, int height) 
	{
		pixWidth = width;
		pixHeight = height;
		noiseTex = new Texture2D(width, height);
		pix = new Color[noiseTex.width * noiseTex.height];
	}

	public void CalcNoise() {
		float y = 0.0F;
		while (y < noiseTex.height) {
			float x = 0.0F;
			while (x < noiseTex.width) {
				float xCoord = xOrg + x / noiseTex.width * scale;
				float yCoord = yOrg + y / noiseTex.height * scale;
				float samplex = Mathf.PerlinNoise(xCoord, yCoord);
				float sampley = Mathf.PerlinNoise(xCoord + pixWidth, yCoord + pixHeight );
				float samplez = Mathf.PerlinNoise(xCoord + 2*pixWidth, yCoord + 2*pixHeight );
				
				
				int pixIndex = Mathf.CeilToInt(y * noiseTex.width + x);
				pix[pixIndex] = new Color(samplex, sampley, samplez);
				x++;
			}
			y++;
		}
		noiseTex.SetPixels(pix);
		noiseTex.Apply();
	}

	public Texture2D GetTexture()
	{
		return noiseTex;
	}
}

public class FullScreenImageEffect : MonoBehaviour {


	public Material mat;
	public float hueShiftSpeed = 1f;
	private float m_HueShift = 0;
	private PerlinGenerator perlinGenerator;

	// Use this for initialization
	void Start () 
	{
		perlinGenerator = new PerlinGenerator(400,400);
		perlinGenerator.CalcNoise();
		mat.SetTexture("_NoiseTex", perlinGenerator.GetTexture());
	}
	
	// Update is called once per frame
	void Update () 
	{
		//perlinGenerator.xOrg += Time.deltaTime;
		//perlinGenerator.yOrg += Time.deltaTime;
		//perlinGenerator.CalcNoise();
		
		m_HueShift = (m_HueShift + hueShiftSpeed * Time.deltaTime) % 360;
	}


	void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		mat.SetFloat("_HueShift", m_HueShift);
		
		Graphics.Blit(src, dest, mat);

	}
}
