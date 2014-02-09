class FadeOut extends System.Object{
	var on = true;
	private var myTex : GUITexture;
	private var alpha = 1.0;
	var speed = 0.01;

	// So it doesn't neccesarily start straight away
	var startDelay = 0.0;
	private var ready = false;

	var enableOnDeath : GameObject;

	function Start(){
		myTex = guiTexture;	
		yield WaitForSeconds(startDelay);
		ready = true;
	}

	function Update () {
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