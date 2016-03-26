using UnityEngine;
using System;

namespace UnityEngine.UI
{
	[Serializable]
	public class UIItemInfo
	{
		public static int id = 0;
		public string name;
		public Sprite icon;
		//public string description;
		public UIEquipmentType equipType;
		//public int itemType;
		//public string type;
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

        public UIItemInfo(string _name, string _iconName, int _equipType, int _damage, int _armor, int _vitality, int _strength, int _intellect, int _spirit, int _crit)
        {
            id++;
            name = _name;
        }

    }
}