using System;
using System.Collections.Generic;
using System.Linq;
using LabApi.Features.Wrappers;

namespace AutoEvent.Games.AmongUs.Features;

internal class TaskManager
{
    internal TaskManager(Player player, List<Task> task)
    {
        IsDone = false;
        Tasks = task;
        PlayerTasks[player] = this;
        LogManager.Debug($"Added to {player.Nickname}");
    }

    internal static Dictionary<Player, TaskManager> PlayerTasks { get; set; } = new();
    internal static bool IsDone { set; get; }
    internal List<Task> Tasks { set; get; }

    internal static void AddTask(TaskManager tm, Task task)
    {
        var maxStageTask = task.MaxStageTask;
        if (maxStageTask > 0 && task.StageTasks.Count > maxStageTask)
        {
            var stageTasks = task.StageTasks;
            var random = new Random();
            task.StageTasks = stageTasks.OrderBy(_ => random.Next()).Take(maxStageTask).ToList();
            LogManager.Debug(
                $"Task {task.Name} had more stage tasks than allowed. Reduced to {task.StageTasks.Count} stage tasks.");
        }

        tm.Tasks.Add(task);
    }

    internal static void Clear()
    {
        LogManager.Debug("Clearing TaskManager");
        PlayerTasks.Clear();
    }
    
    internal static void Remove(Player player)
    {
        if (!TryGet(player, out _)) return;
        PlayerTasks.Remove(player);
        LogManager.Debug($"Removed TaskManager from {player.Nickname}");
    }

    internal static bool TryGet(Player player, out TaskManager taskManager)
    {
        taskManager = null;
        return PlayerTasks.TryGetValue(player, out taskManager);
    }

    internal static List<StageTask> GetPlayerStageTasks(Player player)
    {
        if (!TryGet(player, out var tasks) || tasks is null) return [];
        return tasks.Tasks.Where(task => task.IsDone).SelectMany(t => t.StageTasks).Where(t => !t.IsDone).ToList();
    }

    internal static int CountByType(Player player, TaskType type)
    {
        if (!TryGet(player, out var tasks) || tasks is null || tasks.Tasks.Count == 0) return 0;
        return tasks.Tasks.Count(t => t.Type == type);
    }

    internal static bool IsTaskDone(Task task)
    {
        if (task is null) return false;
        if (task.StageTasks.Count == 0) return task.IsDone;
        return task.IsDone && task.StageTasks.All(st => st.IsDone);
    }

    internal static int GetLength(Task task)
    {
        return task.Type switch
        {
            TaskType.Short => 5,
            TaskType.Common => 10,
            TaskType.Long => 15,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    internal static int GetLength(StageTask task)
    {
        return task.Type switch
        {
            TaskType.Short => 5,
            TaskType.Common => 10,
            TaskType.Long => 15,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}