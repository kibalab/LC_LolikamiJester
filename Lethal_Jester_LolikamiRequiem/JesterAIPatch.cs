using HarmonyLib;
using UnityEngine;

namespace Lethal_Jester_LolikamiRequiem
{
    [HarmonyPatch(typeof(JesterAI))]
    internal class JesterAIPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        private static void Postfix(ref AudioClip ___popGoesTheWeaselTheme, ref AudioSource ___farAudio)
        {
            Lolikami.mls.LogInfo($" Replace Audio {___popGoesTheWeaselTheme.name} to {Lolikami.audio.name}");
            ___popGoesTheWeaselTheme = Lolikami.audio;
            ___farAudio.volume = (float)((Lolikami)Lolikami.Instance).Volume.Value / 100f;
        }
    }
}