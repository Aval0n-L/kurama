using System.Runtime.InteropServices;

namespace Kurama.Services;

public class PowerManagement
{
    [DllImport("powrprof.dll", SetLastError = true)]
    private static extern bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);

    public static void Hibernate()
    {
        SetSuspendState(true, true, true);
    }

    public static void Sleep()
    {
        SetSuspendState(false, true, true);
    }
}