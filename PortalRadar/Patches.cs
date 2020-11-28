using Harmony;
using UnityEngine;

namespace PortalRadar
{

    [HarmonyPatch(typeof(PlayerCharacter))]
    [HarmonyPatch("Awake")]
    public class PlayerCharacterPatch
    {
        static void Postfix(Subtitle __instance)
        {
            new GameObject("Radar").AddComponent<PortalRadar>().transform.SetParent(__instance.transform);
        }
    }

    [HarmonyPatch(typeof(RadioTrigger))]
    [HarmonyPatch("Update")]
    public class RadioTriggerPatch
    {
        static void Postfix(RadioTrigger __instance)
        {
            PlayerCharacter player = PlayerCharacter.main;
            if (player == null)
            {
                return;
            }
            PortalRadar radar = player.GetComponentInChildren<PortalRadar>();
            if (radar == null)
            {
                return;
            }
            radar.RegisterPortal(__instance);

        }

    }
}