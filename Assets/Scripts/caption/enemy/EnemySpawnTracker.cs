using UnityEngine;
using System.Collections.Generic;

public class EnemySpawnTracker : MonoBehaviour
{
    // 静态变量，用于追踪所有敌人实例
    private static int totalSpawnedCount = 0;
    private static HashSet<EnemySpawnTracker> activeEnemies = new HashSet<EnemySpawnTracker>();

    [Header("设置")]
    [SerializeField] private int spawnThreshold = 5; // 生成次数阈值
    [SerializeField] private bool showDebugInfo = true; // 是否显示调试信息

    private MeshRenderer[] allRenderers;
    private bool isDisabled = false;

    void Awake()
    {
        // 获取所有MeshRenderer组件（包括子物体）
        allRenderers = GetComponentsInChildren<MeshRenderer>();

        if (allRenderers.Length == 0)
        {
            Debug.LogWarning($"在 {gameObject.name} 及其子物体上没有找到MeshRenderer组件！");
        }
    }

    void OnEnable()
    {
        // 增加生成计数
        totalSpawnedCount++;

        // 将此敌人添加到活跃敌人集合
        activeEnemies.Add(this);

        // 重新启用所有渲染器（以防之前被禁用）
        EnableAllRenderers();
        isDisabled = false;

        if (showDebugInfo)
        {
            Debug.Log($"敌人生成，累计生成次数: {totalSpawnedCount}, 当前场景敌人数: {activeEnemies.Count}");
        }

        // 检查是否需要禁用渲染
        CheckAndDisableRenderer();
    }

    void OnDisable()
    {
        // 从活跃敌人集合中移除
        activeEnemies.Remove(this);

        if (showDebugInfo)
        {
            Debug.Log($"敌人移除，当前场景敌人数: {activeEnemies.Count}");
        }

        // 检查所有活跃敌人是否需要禁用渲染
        CheckAllEnemiesRenderer();
    }

    void OnDestroy()
    {
        // 确保从集合中移除
        activeEnemies.Remove(this);

        // 检查所有活跃敌人是否需要禁用渲染
        CheckAllEnemiesRenderer();
    }

    private void CheckAndDisableRenderer()
    {
        if (totalSpawnedCount > spawnThreshold && activeEnemies.Count == 0)
        {
            DisableAllRenderers();
        }
    }

    private static void CheckAllEnemiesRenderer()
    {
        // 当场景中没有敌人时，检查是否达到阈值
        if (activeEnemies.Count == 0 && totalSpawnedCount > 5)
        {
            Debug.Log("场景中已无敌人，且累计生成次数超过5次");
        }
    }

    private void DisableAllRenderers()
    {
        if (!isDisabled && allRenderers != null)
        {
            foreach (MeshRenderer renderer in allRenderers)
            {
                if (renderer != null)
                {
                    renderer.enabled = false;
                }
            }

            isDisabled = true;

            if (showDebugInfo)
            {
                Debug.Log($"{gameObject.name} 的所有MeshRenderer（共{allRenderers.Length}个）已被禁用");
            }
        }
    }

    private void EnableAllRenderers()
    {
        if (allRenderers != null)
        {
            foreach (MeshRenderer renderer in allRenderers)
            {
                if (renderer != null)
                {
                    renderer.enabled = true;
                }
            }
        }
    }

    // 公共方法：手动重置计数器（用于重新开始游戏等场景）
    public static void ResetSpawnCount()
    {
        totalSpawnedCount = 0;
        activeEnemies.Clear();
        Debug.Log("敌人生成计数已重置");
    }

    // 获取当前统计信息
    public static void LogStats()
    {
        Debug.Log($"累计生成敌人数: {totalSpawnedCount}, 当前场景敌人数: {activeEnemies.Count}");
    }

    // 获取当前累计生成次数
    public static int GetTotalSpawnedCount()
    {
        return totalSpawnedCount;
    }

    // 获取当前场景敌人数量
    public static int GetActiveEnemyCount()
    {
        return activeEnemies.Count;
    }

    // 在屏幕上显示当前状态（可选）
    void OnGUI()
    {
        if (showDebugInfo && Debug.isDebugBuild)
        {
            GUILayout.Label($"累计生成: {totalSpawnedCount} | 当前场景: {activeEnemies.Count}");
        }
    }
}