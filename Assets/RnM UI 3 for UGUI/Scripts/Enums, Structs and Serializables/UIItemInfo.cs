using UnityEngine;
using System;

namespace UnityEngine.UI
{
	[Serializable]
	public class UIItemInfo
	{
        //public static int counter = 1;
		//public int id;
		public string name;
		public Sprite icon;
		//public string description;
		public UIEquipmentType equipType;
		public string itemType;
        public string itemClass;
		//public string subtype;
		public int damage;
		//public float attackSpeed;
		//public int block;
		public int armor;
		public int vitality;
		public int strength;
        public int intellect;
        public int spirit;
        public int crit;

        public UIItemInfo(string _name, string _iconName, int _equipType, string _itemType, int _damage, int _armor, int _vitality, int _strength, int _intellect, int _spirit, int _crit)
        {
            //id = counter;
            name = _name;
            icon = Resources.Load<Sprite>(_iconName);
            equipType = (UIEquipmentType)_equipType;
            itemType = _itemType;
            itemClass = "Item";
            damage = _damage;
            armor = _armor;
            vitality = _vitality;
            strength = _strength;
            intellect = _intellect;
            spirit = _spirit;
            crit = _crit;

            //counter++;
        }

    }
}