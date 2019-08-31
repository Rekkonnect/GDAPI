using System;
using System.Diagnostics;
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
        /// <param name="processHandle">The process containing the returned buffer.</param>
        /// <param name="bufferSize">The size of the buffer.</param>
        /// <param name="address">The starting address of the buffer.</param>
        public static byte[] ReadMemory(int processHandle, int bufferSize, int address)
        {
            byte[] buffer = new byte[bufferSize];
            uint shit = 0;
            ReadProcessMemory(new IntPtr(processHandle), new IntPtr(address), buffer, (uint)bufferSize, ref shit);
            return buffer;
        }
        /// <summary>Writes a byte buffer at the specified address to a specified process.</summary>
        /// <param name="processHandle">The process whose memory will be written.</param>
        /// <param name="processBytes">The bytes to write.</param>
        /// <param name="address">The starting address of the buffer.</param>
        public static void WriteMemory(int processHandle, byte[] processBytes, int address)
        {
            uint shit = 0;
            ChangeMemoryProtection(processHandle, address, (uint)processBytes.Length);
            WriteProcessMemory(processHandle, address, processBytes, processBytes.Length, ref shit);
        }
        /// <summary>Changes a memory page's protection to <seealso cref="ReadWrite"/> at the specified address to a specified process.</summary>
        /// <param name="address">The starting address of the page.</param>
        /// <param name="size">The size of the page whose protection to change.</param>
        /// <param name="processHandle">The process whose memory page protection will be changed.</param>
        public static void ChangeMemoryProtection(int processHandle, int address, uint size)
        {
            VirtualProtectEx(new IntPtr(processHandle), new IntPtr(address), new UIntPtr(size), (uint)ReadWrite, out _);
        }

        // TODO: Add documentation
        public static int GetAddressFromPointers(int processHandle, int baseAddress, params int[] offsets)
        {
            int value = ToInt32(ReadMemory(processHandle, 4, baseAddress), 0);
            for (int i = 0; i < offsets.Length - 1; i++)
                value = ToInt32(ReadMemory(processHandle, 4, value + offsets[i]), 0);
            return value + offsets[offsets.Length - 1];
        }

        public static byte[] GetValueFromPointers(int processHandle, int size, int baseAddress, params int[] offsets)
        {
            return ReadMemory(processHandle, size, GetAddressFromPointers(processHandle, baseAddress, offsets));
        }
        public static int GetIntFromPointers(int processHandle, int baseAddress, params int[] offsets)
        {
            return ToInt32(GetValueFromPointers(processHandle, sizeof(int), baseAddress, offsets), 0);
        }
        public static float GetFloatFromPointers(int processHandle, int baseAddress, params int[] offsets)
        {
            return ToSingle(GetValueFromPointers(processHandle, sizeof(float), baseAddress, offsets), 0);
        }
        public static bool GetBoolFromPointers(int processHandle, int baseAddress, params int[] offsets)
        {
            return ToBoolean(GetValueFromPointers(processHandle, sizeof(bool), baseAddress, offsets), 0);
        }

        public static void SetValueFromPointers(int processHandle, byte[] bytes, int baseAddress, params int[] offsets)
        {
            WriteMemory(processHandle, bytes, GetAddressFromPointers(processHandle, baseAddress, offsets));
        }
        public static void SetIntFromPointers(int processHandle, int value, int baseAddress, params int[] offsets)
        {
            SetValueFromPointers(processHandle, GetBytes(value), baseAddress, offsets);
        }
        public static void SetFloatFromPointers(int processHandle, float value, int baseAddress, params int[] offsets)
        {
            SetValueFromPointers(processHandle, GetBytes(value), baseAddress, offsets);
        }

        public static void EditInt(int processHandle, int value, int baseAddress, params int[] offsets)
        {
            SetValueFromPointers(processHandle, GetBytes(GetIntFromPointers(baseAddress, processHandle, offsets) + value), baseAddress, offsets);
        }
        public static void EditFloat(int processHandle, float value, int baseAddress, params int[] offsets)
        {
            SetValueFromPointers(processHandle, GetBytes(GetIntFromPointers(baseAddress, processHandle, offsets) + value), baseAddress, offsets);
        }
    }
}
