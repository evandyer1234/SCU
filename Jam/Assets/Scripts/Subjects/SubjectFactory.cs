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
            List<string> minigames)
        {
            Subject subject = new Subject();
            subject.name = name;
            subject.imageOutfit = FileLoader.GetSpriteByName(outfit);
            subject.imageUnder = FileLoader.GetSpriteByName(under);
            subject.imageOrgans = FileLoader.GetSpriteByName(organs);
            subject.imageSkeleton = FileLoader.GetSpriteByName(skeleton);
            subject.isAdult = isAdult;
            subject.subjectMinigames = minigames;
            return subject;
        }
        
        public static Dictionary<string, Subject> CreateSubjectMap()
        {
            Dictionary<string, Subject> subjectMap = new();
            List<string> minigames04 = new List<string>()
            {
                NamingConstants.TAG_MINIGAME_HEARTSTRING,
                NamingConstants.TAG_MINIGAME_LUNGPUMP,
                NamingConstants.TAG_MINIGAME_DRAIN,
            };
            Subject subject04 = CreateSubject(
                SubjectManager.SUBJECT_NAME_04, 
                SUBJECT_SPRITENAME_OUTFIT_04, 
                SUBJECT_SPRITENAME_UNDER_04, 
                SUBJECT_SPRITENAME_ORGANS_04, 
                SUBJECT_SPRITENAME_SKELETON_04,
                true,
                minigames04);
            List<string> minigames14 = new List<string>()
            {
                NamingConstants.TAG_MINIGAME_HEARTSTRING,
                NamingConstants.TAG_MINIGAME_LUNGPUMP,
                NamingConstants.TAG_MINIGAME_DRAIN,
            };
            Subject subject14 = CreateSubject(
                SubjectManager.SUBJECT_NAME_14, 
                SUBJECT_SPRITENAME_OUTFIT_14, 
                SUBJECT_SPRITENAME_UNDER_14,
                SUBJECT_SPRITENAME_ORGANS_14,
                SUBJECT_SPRITENAME_SKELETON_14,
                false,
                minigames14);
            List<string> minigames15 = new List<string>()
            {
                NamingConstants.TAG_MINIGAME_HEARTSTRING,
                NamingConstants.TAG_MINIGAME_LUNGPUMP,
                NamingConstants.TAG_MINIGAME_DRAIN,
            };
            Subject subject15 = CreateSubject(
                SubjectManager.SUBJECT_NAME_15, 
                SUBJECT_SPRITENAME_OUTFIT_15, 
                SUBJECT_SPRITENAME_UNDER_15, 
                SUBJECT_SPRITENAME_ORGANS_15, 
                SUBJECT_SPRITENAME_SKELETON_15,
                true,
                minigames15);
        
            subjectMap.Add(subject04.name, subject04);
            subjectMap.Add(subject14.name, subject14);
            subjectMap.Add(subject15.name, subject15);
            return subjectMap;
        }
    }
}