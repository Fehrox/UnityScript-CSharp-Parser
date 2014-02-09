class Weapon extends System.Object{

	// fields
	var nameOfItem: String;	
	var shortNameOfItem: String;
	var weaponType: WeaponType;
	var priceOfItem: int;
	var itemPurchased: boolean;
	var descriptionOfItem: String;
	var packPath: String;
	var ejectionProperty: EjectionProperty;
	var defaultConfig: StorageManager.WeaponConfig;
	var weaponID: int;
	var listIndex: int;
	var productID: int;
	var camouflageIndex: int[];


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