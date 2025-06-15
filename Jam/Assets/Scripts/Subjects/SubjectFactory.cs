using System.Collections.Generic;
using Helpers;

namespace Subjects
{
    public class SubjectFactory
    {
        /* subject sprite addressables */
        // subject 04
        private const string SUBJECT_SPRITENAME_OUTFIT_04 = "char_04_outfit";
        private const string SUBJECT_SPRITENAME_UNDER_04 = "char_04_under";
        private const string SUBJECT_SPRITENAME_ORGANS_04 = "char_04_organs";
        private const string SUBJECT_SPRITENAME_SKELETON_04 = "char_04_skeleton";
        
        // subject 14
        private const string SUBJECT_SPRITENAME_OUTFIT_14 = "char_14_outfit";
        private const string SUBJECT_SPRITENAME_UNDER_14 = "char_14_under";
        private const string SUBJECT_SPRITENAME_ORGANS_14 = "char_14_organs";
        private const string SUBJECT_SPRITENAME_SKELETON_14 = "char_14_skeleton";
        
        // subject 15
        private const string SUBJECT_SPRITENAME_OUTFIT_15 = "char_15_outfit";
        private const string SUBJECT_SPRITENAME_UNDER_15 = "char_15_under";
        private const string SUBJECT_SPRITENAME_ORGANS_15 = "char_15_organs";
        private const string SUBJECT_SPRITENAME_SKELETON_15 = "char_15_skeleton";
        
        private static Subject CreateSubject(
            string name, 
            string outfit, 
            string under, 
            string organs, 
            string skeleton,
            bool isAdult,
            List<string> minigames,
            List<Ingredient> neededIngredients,
            List<string> ingredientHints,
            List<string> allergies)
        {
            Subject subject = new Subject();
            subject.name = name;
            subject.imageOutfit = FileLoader.GetSpriteByName(outfit);
            subject.imageUnder = FileLoader.GetSpriteByName(under);
            subject.imageOrgans = FileLoader.GetSpriteByName(organs);
            subject.imageSkeleton = FileLoader.GetSpriteByName(skeleton);
            subject.isAdult = isAdult;
            subject.subjectMinigames = minigames;
            subject.neededIngredientsFromPotion = neededIngredients;
            subject.ingredientHints = ingredientHints;
            subject.allergies = allergies;
            return subject;
        }
        
        public static Dictionary<string, Subject> CreateSubjectMap()
        {
            Dictionary<string, Subject> subjectMap = new();
            List<string> minigames04 = new List<string>()
            {
                NamingConstants.TAG_MINIGAME_DRAIN,
                NamingConstants.TAG_MINIGAME_ABDOMEN,
            };
            Subject subject04 = CreateSubject(
                SubjectManager.SUBJECT_NAME_04, 
                SUBJECT_SPRITENAME_OUTFIT_04, 
                SUBJECT_SPRITENAME_UNDER_04, 
                SUBJECT_SPRITENAME_ORGANS_04, 
                SUBJECT_SPRITENAME_SKELETON_04,
                true,
                minigames04,
                new List<Ingredient>
                {
                    new (IngredientConstants.INGREDIENT_ID_AMARANTH, new List<string>{IngredientConstants.OPERATION_GRIND, IngredientConstants.OPERATION_BOIL}),
                    new (IngredientConstants.INGREDIENT_ID_CHIMERA_CLAW, new List<string>{IngredientConstants.OPERATION_GRIND, IngredientConstants.OPERATION_BOIL}),
                    new (IngredientConstants.INGREDIENT_ID_SPIRIT_OF_THE_SAGES, new List<string>{IngredientConstants.OPERATION_BOIL}),
                },
                new List<string>
                {
                    IngredientConstants.INGREDIENT_ID_AMARANTH,
                    IngredientConstants.INGREDIENT_ID_CHIMERA_CLAW,
                },
                new List<string>
                {
                    IngredientConstants.INGREDIENT_ID_MOON_MUSHROOM,
                    IngredientConstants.INGREDIENT_ID_FAIRY_DUST,
                });
            List<string> minigames14 = new List<string>()
            {
                NamingConstants.TAG_MINIGAME_LUNGPUMP,
                NamingConstants.TAG_MINIGAME_ABDOMEN,
            };
            Subject subject14 = CreateSubject(
                SubjectManager.SUBJECT_NAME_14, 
                SUBJECT_SPRITENAME_OUTFIT_14, 
                SUBJECT_SPRITENAME_UNDER_14,
                SUBJECT_SPRITENAME_ORGANS_14,
                SUBJECT_SPRITENAME_SKELETON_14,
                false,
                minigames14,
                new List<Ingredient>
                {
                    new (IngredientConstants.INGREDIENT_ID_FAIRY_DUST, new List<string>{IngredientConstants.OPERATION_GRIND, IngredientConstants.OPERATION_BOIL}),
                    new (IngredientConstants.INGREDIENT_ID_AMARANTH, new List<string>{IngredientConstants.OPERATION_BOIL}),
                    new (IngredientConstants.INGREDIENT_ID_102_PURE_TEA, new List<string>{IngredientConstants.OPERATION_BOIL}),
                },
                new List<string>
                {
                    IngredientConstants.INGREDIENT_ID_FAIRY_DUST,
                    IngredientConstants.INGREDIENT_ID_MYRRH,
                },
                new List<string>
                {
                    IngredientConstants.INGREDIENT_ID_MYRRH,
                });
            List<string> minigames15 = new List<string>()
            {
                NamingConstants.TAG_MINIGAME_HEARTSTRING,
                NamingConstants.TAG_MINIGAME_DRAIN,
            };
            Subject subject15 = CreateSubject(
                SubjectManager.SUBJECT_NAME_15,
                SUBJECT_SPRITENAME_OUTFIT_15, 
                SUBJECT_SPRITENAME_UNDER_15, 
                SUBJECT_SPRITENAME_ORGANS_15, 
                SUBJECT_SPRITENAME_SKELETON_15,
                true,
                minigames15,
                new List<Ingredient>
                {
                    new (IngredientConstants.INGREDIENT_ID_CHIMERA_CLAW, new List<string>{IngredientConstants.OPERATION_GRIND}),
                    new (IngredientConstants.INGREDIENT_ID_MOONSTONE, new List<string>{IngredientConstants.OPERATION_GRIND}),
                    new (IngredientConstants.INGREDIENT_ID_SPIRIT_OF_THE_SAGES, new List<string>{}),
                },
                new List<string>
                {
                    IngredientConstants.INGREDIENT_ID_DEATHS_FLOWER,
                    IngredientConstants.INGREDIENT_ID_MOONSTONE,
                },
                new List<string>
                {
                    IngredientConstants.INGREDIENT_ID_DEATHS_FLOWER,
                    IngredientConstants.INGREDIENT_ID_FAIRY_DUST,
                });
        
            subjectMap.Add(subject04.name, subject04);
            subjectMap.Add(subject14.name, subject14);
            subjectMap.Add(subject15.name, subject15);
            return subjectMap;
        }
    }
}