// See https://aka.ms/new-console-template for more information
using Assignment1.Enums;
using Assignment1.Items;
using ExpansionPack;
using ExpansionPack.Helper;
using ExpansionPack.ItemCreators;
Console.WriteLine("Hello ExpansionPack");
/*
var loot = new LootGenerator(new RandomArmorCreator(), new RandomWeaponCreator(), new EnumRandomizer());
Item item = loot.GetLoot(5);

Console.WriteLine(item.Name);
Console.WriteLine(item.RequiredLevel);
Console.WriteLine(item.Slot);

if (item.Slot == Assignment1.Enums.Slot.Weapon)
{
    Weapon weapon = (Weapon)item;
    Console.WriteLine(weapon.WeaponType);
    Console.WriteLine(weapon.WeaponDamage);
}
else 
{ 
    Armor armor= (Armor)item;
    Console.WriteLine(armor.ArmorType);
    var att = armor.ArmorAttribute.GetAttributes();
    if(att.TryGetValue(AttributeType.Strength, out var str)) Console.Write("str: " + str);
    if (att.TryGetValue(AttributeType.Dexterity, out var dex)) Console.Write(" dex: " + dex);
    if (att.TryGetValue(AttributeType.Intelligence, out var inte)) Console.WriteLine(" int: " + inte);
}

Console.WriteLine("");

*/