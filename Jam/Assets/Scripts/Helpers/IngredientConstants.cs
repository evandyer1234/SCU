using System.Collections.Generic;

namespace Helpers
{
    public class IngredientConstants
    {
        //  PLANTS
        public const string INGREDIENT_ID_NETTLE = "Nettle";
        public const string INGREDIENT_ID_SUN_BATHED_IVY = "Sun Bathed Ivy";
        public const string INGREDIENT_ID_DEATHS_FLOWER = "Deaths Flower";
        public const string INGREDIENT_ID_SUSPENDED_ROSE = "Suspended Rose";
        public const string INGREDIENT_ID_MANDRAKE = "Mandrake";
        public const string INGREDIENT_ID_BURNT_TREE_BARK = "Burnt Tree Bark";
        public const string INGREDIENT_ID_AMARANTH = "Amaranth";
        public const string INGREDIENT_ID_FAIRY_DUST = "Fairy Dust";
        public const string INGREDIENT_ID_MYRRH = "Myrrh";
        public const string INGREDIENT_ID_CHIMERA_CLAW = "Chimera Claw";
        public const string INGREDIENT_ID_MOON_MUSHROOM = "Moon Mushroom";
        
        // LIQUIDS
        public const string INGREDIENT_ID_MOONWATER = "Moonwater";
        public const string INGREDIENT_ID_DARKENED_WATER = "Darkened Water";
        public const string INGREDIENT_ID_DRAGONS_BLOOD = "Dragons Blood";
        public const string INGREDIENT_ID_SWEET_VITRIOL = "Sweet Vitriol";
        public const string INGREDIENT_ID_102_PURE_TEA = "102% Pure Tea";
        public const string INGREDIENT_ID_SPIRIT_OF_THE_SAGES = "Spirit of the Sages";
        public const string INGREDIENT_ID_QUICKSILVER = "Quicksilver";
        public const string INGREDIENT_ID_BASILISK_VENOM = "Basilisk Venom";
        
        // MINERALS
        public const string INGREDIENT_ID_PEARL_ASH = "Pearl Ash";
        public const string INGREDIENT_ID_SUNSTONE = "Sunstone";
        public const string INGREDIENT_ID_LIME = "Lime";
        public const string INGREDIENT_ID_BRIMSTONE = "Brimstone";
        public const string INGREDIENT_ID_BLUESTONE = "Bluestone";
        public const string INGREDIENT_ID_SILVER = "Silver";
        public const string INGREDIENT_ID_MOONSTONE = "Moonstone";
        
        // OPERATION TYPES
        public const string OPERATION_BOIL = "Boiled";
        public const string OPERATION_GRIND = "Grinded";

        public static List<string> PLANT_INGREDIENT_TO_SPRITE_LIST = new List<string>
        {
            INGREDIENT_ID_NETTLE,
            INGREDIENT_ID_SUN_BATHED_IVY,
            INGREDIENT_ID_DEATHS_FLOWER,
            INGREDIENT_ID_SUSPENDED_ROSE,
            INGREDIENT_ID_MANDRAKE,
            INGREDIENT_ID_BURNT_TREE_BARK,
            INGREDIENT_ID_AMARANTH,
            INGREDIENT_ID_FAIRY_DUST,
            INGREDIENT_ID_MYRRH,
            INGREDIENT_ID_CHIMERA_CLAW,
            INGREDIENT_ID_MOON_MUSHROOM
        };

        public static List<string> LIQUID_INGREDIENT_TO_SPRITE_LIST = new List<string>
        {
            INGREDIENT_ID_MOONWATER,
            INGREDIENT_ID_DARKENED_WATER,
            INGREDIENT_ID_DRAGONS_BLOOD,
            INGREDIENT_ID_SWEET_VITRIOL,
            INGREDIENT_ID_102_PURE_TEA,
            INGREDIENT_ID_SPIRIT_OF_THE_SAGES,
            INGREDIENT_ID_QUICKSILVER,
            INGREDIENT_ID_BASILISK_VENOM
        };

        public static List<string> MINERAL_INGREDIENT_TO_SPRITE_LIST = new List<string>
        {
            INGREDIENT_ID_PEARL_ASH,
            INGREDIENT_ID_SUNSTONE,
            INGREDIENT_ID_LIME,
            INGREDIENT_ID_BRIMSTONE,
            INGREDIENT_ID_BLUESTONE,
            INGREDIENT_ID_SILVER,
            INGREDIENT_ID_MOONSTONE
        };
    }
}