using System.ComponentModel;
using AutoEvent.Interfaces;

namespace AutoEvent.Games.NukeRun;

public class Config : EventConfig
{
    [Description("How long players have to escape in seconds. [Default: 70]")]
    public int EscapeDurationTime { get; set; } = 120;

    [Description("SCP-173 Escape")]
    public bool SpawnAsScp173 { get; set; } = false;
}