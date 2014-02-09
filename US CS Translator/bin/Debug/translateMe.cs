0class FadeOut : System.Object{
0	var on = true;
0	private GUITexture myTex;
0	private var alpha = 1.0;
0	var speed = 0.01;
0
0	// So it doesn't neccesarily start straight away
0	var startDelay = 0.0;
0	private var ready = false;
0
0	 GameObject enableOnDeath;
0
0	public void Start(){
0		myTex = guiTexture;	
0		yield WaitForSeconds(startDelay);
0		ready = true;
0	}
0
0	public void Update (){
0		if (ready)
0		{
0			myTex.color.a = alpha;
0			alpha -= speed;
0			
0			if (alpha < 0.01){
0				if (enableOnDeath != null) enableOnDeath.SetActiveRecursively(true);
0				 Destroy (this.gameObject);
0			}
0		}
0	}
0
0}
UnityScript to C# conversion complete.
