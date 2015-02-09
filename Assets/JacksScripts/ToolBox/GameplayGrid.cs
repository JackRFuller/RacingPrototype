using UnityEngine;
using System.Collections;

public class GameplayGrid : MonoBehaviour {

	public GameObject plane;

	public bool bl_ShowMain = true;
	public bool bl_ShowSub = false;

	public int in_gridSizeX;
	public int in_gridSizeY;
	public int in_gridSizeZ;

	public float fl_SmallStep;
	public float fl_LargeStep;

	public float fl_StartX;
	public float fl_StartY;
	public float fl_StartZ;

	private float fl_OffsetY = 0;
	private float fl_ScrollRate = 0.1F;
	private float fl_LastScroll = 0f;

	private Material lineMaterial;
	private Color mainColor = new Color(0f,1f,0f,1f);
	private Color subColor = new Color(0F,0.5F,0F,1F);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void CreateLineMaterial()
	{
		if(!lineMaterial)
		{
			lineMaterial = new Material( "Shader \"Lines/Colored Blended\" {" +
			                            "SubShader { Pass { " +
			                            "    Blend SrcAlpha OneMinusSrcAlpha " +
			                            "    ZWrite Off Cull Off Fog { Mode Off } " +
			                            "    BindChannels {" +
			                            "      Bind \"vertex\", vertex Bind \"color\", color }" +
			                            "} } }" );
			lineMaterial.hideFlags = HideFlags.HideAndDontSave;
			lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;		
		}
	}

	void OnPostRender()
	{
		CreateLineMaterial();

		lineMaterial.SetPass(0);

		GL.Begin(GL.LINES);

		if(bl_ShowSub)
		{
			GL.Color(subColor);

			//Layers
			for(float j = 0; j <= in_gridSizeY; j += fl_SmallStep)
			{
				//XAxisLines
				for (float i = 0; i <= in_gridSizeZ; i += fl_SmallStep)
				{
					GL.Vertex3(fl_StartX, j + fl_OffsetY, fl_StartZ + i);
					GL.Vertex3(in_gridSizeX, j + fl_OffsetY, fl_StartZ + i);
				}
				//ZAxisLines
				for (float i = 0; i <= in_gridSizeX; i += fl_SmallStep)
				{
					GL.Vertex3(fl_StartX + i, j + fl_OffsetY, fl_StartZ);
					GL.Vertex3(fl_StartX + i, j + fl_OffsetY, in_gridSizeZ);
				}

			}
			for(float i = 0; i <= in_gridSizeZ; i += fl_SmallStep)
			{
				for(float k = 0; k <= in_gridSizeX; k+= fl_SmallStep)
				{
					GL.Vertex3(fl_StartX + k, fl_StartY + fl_OffsetY,fl_StartZ + i);
					GL.Vertex3(fl_StartX + k, in_gridSizeY + fl_OffsetY, fl_OffsetY + i);
				}
			}
		}
		if(bl_ShowMain)
		{
			GL.Color(mainColor);
			
			//Layers
			for(float j = 0; j <= in_gridSizeY; j += fl_LargeStep)
			{
				//X axis lines
				for(float i = 0; i <= in_gridSizeZ; i += fl_LargeStep)
				{
					GL.Vertex3(fl_StartX, j + fl_OffsetY, fl_OffsetY + i);
					GL.Vertex3(in_gridSizeX, j + fl_OffsetY, fl_OffsetY + i);
				}
				
				//Z axis lines
				for(float i = 0; i <= in_gridSizeX; i += fl_LargeStep)
				{
					GL.Vertex3(fl_StartX + i, j + fl_OffsetY, fl_OffsetY);
					GL.Vertex3(fl_StartX + i, j + fl_OffsetY, in_gridSizeZ);
				}
			}
			
			//Y axis lines
			for(float i = 0; i <= in_gridSizeZ; i += fl_LargeStep)
			{
				for(float k = 0; k <= in_gridSizeX; k += fl_LargeStep)
				{
					GL.Vertex3(fl_StartX + k, fl_StartY + fl_OffsetY, fl_StartZ + i);
					GL.Vertex3(fl_StartX + k, in_gridSizeY + fl_OffsetY, fl_StartZ + i);
				}
			}
		}
		
		
		GL.End();
	}
}
