class Weapon : System.Object{

	// fields
	 String	 nameOfItem;
	 String shortNameOfItem;
	 WeaponType weaponType;
	 int priceOfItem;
	 bool itemPurchased;
	 String descriptionOfItem;
	 String packPath;
	 EjectionProperty ejectionProperty;
	 StorageManager.WeaponConfig defaultConfig;
	 int weaponID;
	 int listIndex;
	 int productID;
	 int[] camouflageIndex;


	//Constructor
	public function Weapon(	nameOfItem: String,
								shortNameOfItem: String,
								weaponType: WeaponType,
								priceOfItem: int,
								itemPurchased: boolean,
								descriptionOfItem: String,
								packPath: String,
								ejectionProperty: EjectionProperty,
								defaultConfig: StorageManager.WeaponConfig,
								weaponID: int,
								listIndex: int,
								productID: int,
								camouflageIndex: int[]
							 )
	{
		this.nameOfItem = nameOfItem;
		this.shortNameOfItem = shortNameOfItem;
		this.weaponType = weaponType;
		this.priceOfItem = priceOfItem;
		this.itemPurchased = itemPurchased;
		this.descriptionOfItem = descriptionOfItem;
		this.packPath = packPath;
		this.ejectionProperty = ejectionProperty;
		this.defaultConfig = defaultConfig;
		this.weaponID = weaponID;
		this.listIndex = listIndex;
		this.productID = productID;
		this.camouflageIndex = camouflageIndex;
	}
	
}