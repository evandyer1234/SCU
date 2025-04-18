using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Helpers
{
    public class FileLoader
    {
        /**
         * Gets a Sprite by name via the Unity Addressable system.
         * Make sure to add your Sprite to an Addressable Group before using this.
         * And give your sprite a readable name in the Addresable entry in the editor.
         * Then provide it here.
         *
         * Documentation:
         * https://docs.unity3d.com/2021.3/Documentation/Manual/com.unity.addressables.html
         */
        public static Sprite GetSpriteByName(string spriteName)
        {
            return Addressables
                .LoadAssetAsync<Sprite>(spriteName)
                .WaitForCompletion();
        }
    }
}