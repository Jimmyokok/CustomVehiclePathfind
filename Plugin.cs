using System.Linq;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using Game.Prefabs;
using Unity.Entities;
using BepInEx.Logging;
using BepInEx.Configuration;

#if BEPINEX_V6
    using BepInEx.Unity.Mono;
#endif

namespace CustomVehiclePathfind
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, "1.0.2")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
            var harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), MyPluginInfo.PLUGIN_GUID + "_Cities2Harmony");
            var patchedMethods = harmony.GetPatchedMethods().ToArray();

            Plugin.enabled_unsafe_punishment = base.Config.Bind<bool>("UnsafeBehaviorPunishmentEnabled", "UnsafeBehaviorPunishmentEnabled", true, "是否开启不安全行为寻路惩罚 | Enable unsafe behaviors punishment | 填true表示开启，false表示关闭 | true for enabled and false for disabled");
            Plugin.enabled_lane_punishment = base.Config.Bind<bool>("LaneChangingPunishmentEnabled", "LaneChangingPunishmentEnabled", false, "是否开启变道寻路惩罚 | Enable lane changing punishment | 填true表示开启，false表示关闭 | true for enabled and false for disabled");
            Plugin.enabled_driving_punishment = base.Config.Bind<bool>("DrivingPunishmentEnabled", "DrivingPunishmentEnabled", false, "是否开启车辆行驶寻路惩罚 | Enable vehicle usage punishment | 填true表示开启，false表示关闭 | true for enabled and false for disabled");
            Plugin.m_unsafe_punishment = base.Config.Bind<int>("UnsafeBehaviorPunishment", "UnsafeBehaviorPunishment", 10000, "不安全行为寻路惩罚值 | Punishment value of unsafe behaviors in vehicle pathfinding | 需填整数 |  INTEGER needed");
            Plugin.m_lane_punishment = base.Config.Bind<int>("LaneChangingPunishment", "LaneChangingPunishment", 5, "变道寻路惩罚值 | Punishment value of lane changing in vehicle pathfinding | 需填整数 | INTEGER needed");
            Plugin.m_driving_punishment = base.Config.Bind<float>("DrivingPunishment", "DrivingPunishment", (float)1.0, "车辆行驶惩罚值 | Punishment value of vehicle driving in vehicle pathfinding | 需填浮点数 | FLOAT needed");

            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} made patches! Patched methods: " + patchedMethods.Length);

            foreach (var patchedMethod in patchedMethods) {
                Logger.LogInfo($"Patched method: {patchedMethod.Module.Name}:{patchedMethod.Name}");
            }
        }
        public static ConfigEntry<int> m_unsafe_punishment;
        public static ConfigEntry<int> m_lane_punishment;
        public static ConfigEntry<float> m_driving_punishment;
        public static ConfigEntry<bool> enabled_unsafe_punishment;
        public static ConfigEntry<bool> enabled_lane_punishment;
        public static ConfigEntry<bool> enabled_driving_punishment;
    }

    [HarmonyPatch]
    internal class Patches
    {
        [HarmonyPatch(typeof(CarPathfind), "Initialize")]
        [HarmonyPrefix]
        private static bool Prefix(ref EntityManager entityManager, ref Entity entity, CarPathfind __instance)
        {
            float m_unsafe_punishment = Plugin.m_unsafe_punishment.Value;
            float m_lane_punishment = Plugin.m_lane_punishment.Value;
            float m_driving_punishment = Plugin.m_driving_punishment.Value;
            bool enabled_unsafe_punishment = Plugin.enabled_unsafe_punishment.Value;
            bool enabled_lane_punishment = Plugin.enabled_lane_punishment.Value;
            bool enabled_driving_punishment = Plugin.enabled_driving_punishment.Value;
            PathfindCostInfo modified_UnsafeUTurnCost;
            PathfindCostInfo modified_ForbiddenCost;
            PathfindCostInfo modified_LaneCrossCost;
            PathfindCostInfo modified_DrivingCost;

            if (enabled_unsafe_punishment)
            {
                modified_UnsafeUTurnCost = new PathfindCostInfo(m_unsafe_punishment, m_unsafe_punishment, m_unsafe_punishment, m_unsafe_punishment);
                modified_ForbiddenCost = new PathfindCostInfo(m_unsafe_punishment, m_unsafe_punishment, m_unsafe_punishment, m_unsafe_punishment);
            }
            else
            {
                modified_UnsafeUTurnCost = __instance.m_UnsafeUTurnCost;
                modified_ForbiddenCost = __instance.m_ForbiddenCost;
            }

            if (enabled_lane_punishment)
            {
                modified_LaneCrossCost = new PathfindCostInfo(m_lane_punishment, m_lane_punishment, 0 , m_lane_punishment);
            }
            else
            {
                modified_LaneCrossCost = __instance.m_LaneCrossCost;
            }

            if (enabled_driving_punishment)
            {
                modified_DrivingCost = new PathfindCostInfo(0, 0, m_driving_punishment, 0);
            }
            else
            {
                modified_DrivingCost = __instance.m_DrivingCost;
            }

            entityManager.SetComponentData<PathfindCarData>(entity, new PathfindCarData
            {
                m_DrivingCost = modified_DrivingCost.ToPathfindCosts(),
                m_TurningCost = __instance.m_TurningCost.ToPathfindCosts(),
                m_UTurnCost = __instance.m_UTurnCost.ToPathfindCosts(),
                m_UnsafeUTurnCost = modified_UnsafeUTurnCost.ToPathfindCosts(),
                m_LaneCrossCost = modified_LaneCrossCost.ToPathfindCosts(),
                m_ParkingCost = __instance.m_ParkingCost.ToPathfindCosts(),
                m_SpawnCost = __instance.m_SpawnCost.ToPathfindCosts(),
                m_ForbiddenCost = modified_ForbiddenCost.ToPathfindCosts()
            });
            return false;
        }
    }
}
