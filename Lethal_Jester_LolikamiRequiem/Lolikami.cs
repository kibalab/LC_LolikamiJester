
using System;
using System.Collections;
using System.IO;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using LCSoundTool;
using UnityEngine;

namespace Lethal_Jester_LolikamiRequiem
{
    [BepInPlugin(modGUID, modName, modVersion), BepInDependency("LCSoundTool", BepInDependency.DependencyFlags.HardDependency)]
    public class Lolikami : BaseUnityPlugin
    {
        private const string modGUID = "K13A.LCLolikami";
        private const string modName = "Lolikami Requiem";
        private const string modVersion = "1.0.6";

        private readonly Harmony _harmony = new Harmony(modGUID);

        public static BaseUnityPlugin Instance;

        internal static ManualLogSource mls;

        public static AudioClip audio;

        public static EnemyType[] Enemies;
        public ConfigEntry<int> Volume { get; private set; }
        
        private void Awake()
        {
            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            if (!Instance) Instance = this;

            mls.LogInfo("Initialized Lolikami Requiem plugin");
            
            mls.LogInfo($"Loading audio {Path.GetDirectoryName(((BaseUnityPlugin)this).Info.Location)}/lolikami.wav");
            if (!SoundTool.Instance) SoundTool.Instance = new SoundTool();
            audio = SoundTool.GetAudioClip(Path.GetDirectoryName(((BaseUnityPlugin)this).Info.Location), "lolikami.wav");
            mls.LogInfo($"Load Success {audio.name}");
            
            Volume = ((BaseUnityPlugin)this).Config.Bind<int>("Sound", "IntroVolume", 100, new ConfigDescription("Sets the volume of the cranking intro (in %)", (AcceptableValueBase)(object)new AcceptableValueRange<int>(0, 200), Array.Empty<object>()));
            
            _harmony.PatchAll(typeof(Lolikami));
            _harmony.PatchAll(typeof(JesterAIPatch));
        }
    }
}