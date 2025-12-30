using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.Scp173Events;
using LabApi.Events.Arguments.ServerEvents;
using PlayerRoles;

namespace AutoEvent.Games.NukeRun;

public class EventHandler(Plugin plugin)
{
    public static void OnAnnoucingScpTermination(CassieAnnouncingEventArgs ev)
    {
        if (ev.Words.Contains("SCP") && ev.Words.Contains("CONTAINED"))
            ev.IsAllowed = false;
    }

    public void OnJoined(PlayerJoinedEventArgs ev)
    {
        ev.Player.SetRole(plugin.Config.SpawnAsScp173 ? RoleTypeId.Scp173 : RoleTypeId.ClassD);
    }

    public static void OnPlacingTantrum(Scp173CreatingTantrumEventArgs ev)
    {
        ev.IsAllowed = false;
    }

    public static void OnUsingBreakneckSpeeds(Scp173BreakneckSpeedChangingEventArgs ev)
    {
        ev.IsAllowed = false;
    }
}