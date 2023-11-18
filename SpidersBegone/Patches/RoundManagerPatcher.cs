using HarmonyLib;

namespace SpidersBegone.Patches
{
    [HarmonyPatch]
    internal class RoundManagerPatcher
    {
        /// <summary>
        /// Prevent any spiders from spawning by pretending that they're too powerful for the current conditions.
        /// This happens during a server-only planning stage so this works even if clients do not have the mod.
        /// </summary>
        [HarmonyPostfix]
        [HarmonyPatch(typeof(RoundManager), nameof(RoundManager.EnemyCannotBeSpawned))]
        private static void PreventSpiderSpawns(RoundManager __instance, ref bool __result, int enemyIndex)
        {
            var enemy = __instance.currentLevel.Enemies[enemyIndex];
            if (enemy.enemyType.enemyName.ToLower().Contains("spider"))
            {
                SpidersBegone.Log.LogDebug("Prevented spider spawn.");
                __result = true;
            }
        }
    }
}