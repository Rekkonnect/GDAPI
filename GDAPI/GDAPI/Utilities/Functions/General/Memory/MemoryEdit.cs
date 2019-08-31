using System;
using System.Runtime.InteropServices;
using static GDAPI.Utilities.Enumerations.Memory.MemoryPageProtection;
using static System.BitConverter;

namespace GDAPI.Utilities.Functions.General.Memory
{
    // TODO: Move these functions to a Windows-specific namespace regarding memory editing
    /// <summary>Contains the defintions of external and custom wrapper functions for memory editing in Windows.</summary>
    public unsafe static class MemoryEdit
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, ref uint lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress, byte[] buffer, int size, ref uint lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        [DllImport("psapi.dll")]
        public static extern uint GetModuleBaseName(IntPtr hProcess, IntPtr hModule, char* lpBaseName, uint nSize);

        [DllImport("psapi.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int EnumProcessModules(IntPtr hProcess, [Out] IntPtr lphModule, uint cb, out uint lpcbNeeded);

        /// <summary>Returns a byte buffer at the specified address with the specified size from a specified process.</summary>
        /// <param name="address">The starting address of the buffer.</param>
        /// <param name="processSize">The size of the buffer.</param>
        /// <param name="processHandle">The process containing the returned buffer.</param>
        public static byte[] ReadMemory(int address, int processSize, int processHandle)
        {
            byte[] buffer = new byte[processSize];
            uint shit = 0;
            ReadProcessMemory(new IntPtr(processHandle), new IntPtr(address), buffer, (uint)processSize, ref shit);
            return buffer;
        }
        /// <summary>Writes a byte buffer at the specified address to a specified process.</summary>
        /// <param name="address">The starting address of the buffer.</param>
        /// <param name="processBytes">The bytes to write.</param>
        /// <param name="processHandle">The process whose memory will be written.</param>
        public static void WriteMemory(int address, byte[] processBytes, int processHandle)
        {
            uint shit = 0;
            ChangeMemoryProtection(address, (uint)processBytes.Length, processHandle);
            WriteProcessMemory(processHandle, address, processBytes, processBytes.Length, ref shit);
        }
        /// <summary>Changes a memory page's protection to <seealso cref="ReadWrite"/> at the specified address to a specified process.</summary>
        /// <param name="address">The starting address of the page.</param>
        /// <param name="size">The size of the page whose protection to change.</param>
        /// <param name="processHandle">The process whose memory page protection will be changed.</param>
        public static void ChangeMemoryProtection(int address, uint size, int processHandle)
        {
            VirtualProtectEx(new IntPtr(processHandle), new IntPtr(address), new UIntPtr(size), (uint)ReadWrite, out _);
        }

        // TODO: Consider following the following ordering: `int processHandle, ..., int baseAddress, params int[] offsets`
        // TODO: Add documentation
        public static int GetAddressFromPointers(int baseAddress, int processHandle, params int[] offsets)
        {
            int value = ToInt32(ReadMemory(baseAddress, 4, processHandle), 0);
            for (int i = 0; i < offsets.Length - 1; i++)
                value = ToInt32(ReadMemory(value + offsets[i], 4, processHandle), 0);
            return value + offsets[offsets.Length - 1];
        }

        public static byte[] GetValueFromPointers(int baseAddress, int size, int processHandle, params int[] offsets)
        {
            return ReadMemory(GetAddressFromPointers(baseAddress, processHandle, offsets), size, processHandle);
        }
        public static int GetIntFromPointers(int baseAddress, int processHandle, params int[] offsets)
        {
            return ToInt32(GetValueFromPointers(baseAddress, sizeof(int), processHandle, offsets), 0);
        }
        public static float GetFloatFromPointers(int baseAddress, int processHandle, params int[] offsets)
        {
            return ToSingle(GetValueFromPointers(baseAddress, sizeof(float), processHandle, offsets), 0);
        }
        public static bool GetBoolFromPointers(int baseAddress, int processHandle, params int[] offsets)
        {
            return ToBoolean(GetValueFromPointers(baseAddress, sizeof(bool), processHandle, offsets), 0);
        }

        public static void SetValueFromPointers(int baseAddress, int processHandle, byte[] bytes, params int[] offsets)
        {
            WriteMemory(GetAddressFromPointers(baseAddress, processHandle, offsets), bytes, processHandle);
        }
        public static void SetIntFromPointers(int baseAddress, int processHandle, int value, params int[] offsets)
        {
            SetValueFromPointers(baseAddress, processHandle, GetBytes(value), offsets);
        }
        public static void SetFloatFromPointers(int baseAddress, int processHandle, float value, params int[] offsets)
        {
            SetValueFromPointers(baseAddress, processHandle, GetBytes(value), offsets);
        }

        public static void EditInt(int baseAddress, int processHandle, int value, params int[] offsets)
        {
            SetValueFromPointers(baseAddress, processHandle, GetBytes(GetIntFromPointers(baseAddress, processHandle, offsets) + value), offsets);
        }
        public static void EditFloat(int baseAddress, int processHandle, float value, params int[] offsets)
        {
            SetValueFromPointers(baseAddress, processHandle, GetBytes(GetIntFromPointers(baseAddress, processHandle, offsets) + value), offsets);
        }
    }
}
