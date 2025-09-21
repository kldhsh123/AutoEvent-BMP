using System.Collections.Generic;
using AutoEvent.Games.AmongUs.Skeld;

namespace AutoEvent.Games.AmongUs.Features;

public class Task
{
    internal TaskName Name { get; set; }
    internal RoomName RoomName { get; set; }
    internal TaskType Type { get; set; }
    internal bool IsVisual { get; set; }
    internal bool IsDone { get; set; }
    internal string Description { get; set; }
    internal List<StageTask> StageTasks { get; set; } = [];
    internal int MaxStageTask { get; set; } = 1;
}

public class StageTask
{
    internal TaskName Name { get; set; }
    internal RoomName RoomName { get; set; }
    internal TaskType Type { get; set; }
    internal string Description { get; set; }
    internal bool IsDone { get; set; }
}