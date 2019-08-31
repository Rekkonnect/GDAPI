using System;
using System.Collections.Generic;
using System.Text;

namespace GDAPI.Utilities.Enumerations.Memory
{
    // Documentation has been copy-pasted according to the WinAPI documentation found at
    // https://docs.microsoft.com/en-us/windows/win32/procthread/process-security-and-access-rights
    // with slight modifications to match this API's enumeration field naming convention
    // as well as intentional omissions of lack of support by Vista or earlier versions.

    /// <summary>Contains the values of the process access rights constants for Windows.</summary>
    public enum ProcessAccessRights : uint
    {
        /// <summary>Required to terminate a process using TerminateProcess.</summary>
        Terminate = 0x0001,
        /// <summary>Required to create a thread.</summary>
        CreateThread = 0x0002,
        /// <summary>Required to perform an operation on the address space of a process (see VirtualProtectEx and WriteProcessMemory).</summary>
        VMOperation = 0x0008,
        /// <summary>Required to read memory in a process using ReadProcessMemory.</summary>
        VMRead = 0x0010,
        /// <summary>Required to write to memory in a process using WriteProcessMemory.</summary>
        VMWrite = 0x0020,
        /// <summary>Required to duplicate a handle using DuplicateHandle.</summary>
        DuplicateHandle = 0x0040,
        /// <summary>Required to create a process.</summary>
        CreateProcesss = 0x0080,
        /// <summary>Required to set memory limits using SetProcessWorkingSetSize.</summary>
        SetQuota = 0x0100,
        /// <summary>Required to set certain information about a process, such as its priority class (see SetPriorityClass).</summary>
        SetInformation = 0x0200,
        /// <summary>Required to retrieve certain information about a process, such as its token, exit code, and priority class (see OpenProcessToken).</summary>
        QueryInformation = 0x0400,
        /// <summary>Required to suspend or resume a process.</summary>
        SuspendResume = 0x0800,
        /// <summary>Required to retrieve certain information about a process (see GetExitCodeProcess, GetPriorityClass, IsProcessInJob, QueryFullProcessImageName). A handle that has the <seealso cref="QueryInformation"/> access right is automatically granted <seealso cref="QueryLimitedInformation"/>.</summary>
        QueryLimitedInformation = 0x1000,
        /// <summary>Required to wait for the process to terminate using the wait functions.</summary>
        Synchronize = 0x00100000,

        /// <summary>All possible access rights for a process object.</summary>
        AllAccess = VMOperation | CreateThread    | Terminate     | SetQuota       | QueryInformation        |
                    VMRead      | CreateProcesss  | SuspendResume | SetInformation | QueryLimitedInformation |
                    VMWrite     | DuplicateHandle | Synchronize,
    }
}
