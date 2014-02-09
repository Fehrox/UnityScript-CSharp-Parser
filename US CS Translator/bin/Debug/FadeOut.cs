class FadeOut : System.Object{
	var on = true;
	private GUITexture myTex;
	private var alpha = 1.0;
	var speed = 0.01;

	// So it doesn't neccesarily start straight away
	var startDelay = 0.0;
	private var ready = false;

	 GameObject enableOnDeath;

	public void Start(){
		myTex = guiTexture;	
		yield WaitForSeconds(startDelay);
		ready = true;
	}

	public void Update (){
		if (ready)
		{
			myTex.color.a = alpha;
			alpha -= speed;
			
			if (alpha < 0.01){
				if (enableOnDeath != null) enableOnDeath.SetActiveRecursively(true);
				 Destroy (this.gameObject);
			}
		}
	}

}