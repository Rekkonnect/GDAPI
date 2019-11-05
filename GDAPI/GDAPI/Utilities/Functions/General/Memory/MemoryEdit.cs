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

        #region ReadMemory
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
        /// <summary>Returns a byte buffer at the specified address with the specified size from a specified process.</summary>
        /// <param name="processHandle">The process containing the returned buffer.</param>
        /// <param name="address">The starting address of the buffer.</param>
        public static byte[] ReadMemory<T>(int processHandle, int address) where T : unmanaged => ReadMemory(processHandle, sizeof(T), address);
        /// <summary>Returns a <seealso cref="byte"/> at the specified address with the specified size from a specified process.</summary>
        /// <param name="processHandle">The process containing the returned buffer.</param>
        /// <param name="address">The starting address of the buffer.</param>
        public static byte ReadByte(int processHandle, int address) => ReadMemory<byte>(processHandle, address)[0];
        /// <summary>Returns a <seealso cref="short"/> at the specified address with the specified size from a specified process.</summary>
        /// <param name="processHandle">The process containing the returned buffer.</param>
        /// <param name="address">The starting address of the buffer.</param>
        public static short ReadShort(int processHandle, int address) => ToInt16(ReadMemory<short>(processHandle, address), 0);
        /// <summary>Returns an <seealso cref="int"/> at the specified address with the specified size from a specified process.</summary>
        /// <param name="processHandle">The process containing the returned buffer.</param>
        /// <param name="address">The starting address of the buffer.</param>
        public static int ReadInt(int processHandle, int address) => ToInt32(ReadMemory<int>(processHandle, address), 0);
        /// <summary>Returns a <seealso cref="long"/> at the specified address with the specified size from a specified process.</summary>
        /// <param name="processHandle">The process containing the returned buffer.</param>
        /// <param name="address">The starting address of the buffer.</param>
        public static long ReadLong(int processHandle, int address) => ToInt64(ReadMemory<long>(processHandle, address), 0);
        /// <summary>Returns a <seealso cref="sbyte"/> at the specified address with the specified size from a specified process.</summary>
        /// <param name="processHandle">The process containing the returned buffer.</param>
        /// <param name="address">The starting address of the buffer.</param>
        public static sbyte ReadSByte(int processHandle, int address) => (sbyte)ReadMemory<sbyte>(processHandle, address)[0];
        /// <summary>Returns a <seealso cref="ushort"/> at the specified address with the specified size from a specified process.</summary>
        /// <param name="processHandle">The process containing the returned buffer.</param>
        /// <param name="address">The starting address of the buffer.</param>
        public static ushort ReadUShort(int processHandle, int address) => ToUInt16(ReadMemory<ushort>(processHandle, address), 0);
        /// <summary>Returns a <seealso cref="uint"/> at the specified address with the specified size from a specified process.</summary>
        /// <param name="processHandle">The process containing the returned buffer.</param>
        /// <param name="address">The starting address of the buffer.</param>
        public static uint ReadUInt(int processHandle, int address) => ToUInt32(ReadMemory<uint>(processHandle, address), 0);
        /// <summary>Returns a <seealso cref="ulong"/> at the specified address with the specified size from a specified process.</summary>
        /// <param name="processHandle">The process containing the returned buffer.</param>
        /// <param name="address">The starting address of the buffer.</param>
        public static ulong ReadULong(int processHandle, int address) => ToUInt64(ReadMemory<ulong>(processHandle, address), 0);
        /// <summary>Returns a <seealso cref="float"/> at the specified address with the specified size from a specified process.</summary>
        /// <param name="processHandle">The process containing the returned buffer.</param>
        /// <param name="address">The starting address of the buffer.</param>
        public static float ReadFloat(int processHandle, int address) => ToSingle(ReadMemory<float>(processHandle, address), 0);
        /// <summary>Returns a <seealso cref="double"/> at the specified address with the specified size from a specified process.</summary>
        /// <param name="processHandle">The process containing the returned buffer.</param>
        /// <param name="address">The starting address of the buffer.</param>
        public static double ReadDouble(int processHandle, int address) => ToDouble(ReadMemory<double>(processHandle, address), 0);
        /// <summary>Returns a <seealso cref="bool"/> at the specified address with the specified size from a specified process.</summary>
        /// <param name="processHandle">The process containing the returned buffer.</param>
        /// <param name="address">The starting address of the buffer.</param>
        public static bool ReadBool(int processHandle, int address) => ToBoolean(ReadMemory<bool>(processHandle, address), 0);
        /// <summary>Returns a <seealso cref="char"/> at the specified address with the specified size from a specified process.</summary>
        /// <param name="processHandle">The process containing the returned buffer.</param>
        /// <param name="address">The starting address of the buffer.</param>
        public static char ReadChar(int processHandle, int address) => ToChar(ReadMemory<char>(processHandle, address), 0);
        #endregion

        #region WriteMemory
        /// <summary>Writes a byte buffer at the specified address to a specified process.</summary>
        /// <param name="processHandle">The process whose memory will be written.</param>
        /// <param name="bytes">The bytes to write.</param>
        /// <param name="address">The starting address of the buffer.</param>
        public static void WriteMemory(int processHandle, byte[] bytes, int address)
        {
            uint shit = 0;
            ChangeMemoryProtection(processHandle, address, (uint)bytes.Length);
            WriteProcessMemory(processHandle, address, bytes, bytes.Length, ref shit);
        }
        /// <summary>Writes an <seealso cref="byte"/> at the specified address to a specified process.</summary>
        /// <param name="processHandle">The process whose memory will be written.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="address">The starting address of the buffer.</param>
        public static void WriteMemory(int processHandle, byte value, int address) => WriteMemory(processHandle, new byte[] { value }, address);
        /// <summary>Writes an <seealso cref="short"/> at the specified address to a specified process.</summary>
        /// <param name="processHandle">The process whose memory will be written.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="address">The starting address of the buffer.</param>
        public static void WriteMemory(int processHandle, short value, int address) => WriteMemory(processHandle, GetBytes(value), address);
        /// <summary>Writes an <seealso cref="int"/> at the specified address to a specified process.</summary>
        /// <param name="processHandle">The process whose memory will be written.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="address">The starting address of the buffer.</param>
        public static void WriteMemory(int processHandle, int value, int address) => WriteMemory(processHandle, GetBytes(value), address);
        /// <summary>Writes a <seealso cref="long"/> at the specified address to a specified process.</summary>
        /// <param name="processHandle">The process whose memory will be written.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="address">The starting address of the buffer.</param>
        public static void WriteMemory(int processHandle, long value, int address) => WriteMemory(processHandle, GetBytes(value), address);
        /// <summary>Writes an <seealso cref="sbyte"/> at the specified address to a specified process.</summary>
        /// <param name="processHandle">The process whose memory will be written.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="address">The starting address of the buffer.</param>
        public static void WriteMemory(int processHandle, sbyte value, int address) => WriteMemory(processHandle, new byte[] { *(byte*)&value }, address);
        /// <summary>Writes a <seealso cref="ushort"/> at the specified address to a specified process.</summary>
        /// <param name="processHandle">The process whose memory will be written.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="address">The starting address of the buffer.</param>
        public static void WriteMemory(int processHandle, ushort value, int address) => WriteMemory(processHandle, GetBytes(value), address);
        /// <summary>Writes a <seealso cref="uint"/> at the specified address to a specified process.</summary>
        /// <param name="processHandle">The process whose memory will be written.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="address">The starting address of the buffer.</param>
        public static void WriteMemory(int processHandle, uint value, int address) => WriteMemory(processHandle, GetBytes(value), address);
        /// <summary>Writes a <seealso cref="ulong"/> at the specified address to a specified process.</summary>
        /// <param name="processHandle">The process whose memory will be written.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="address">The starting address of the buffer.</param>
        public static void WriteMemory(int processHandle, ulong value, int address) => WriteMemory(processHandle, GetBytes(value), address);
        /// <summary>Writes a <seealso cref="float"/> at the specified address to a specified process.</summary>
        /// <param name="processHandle">The process whose memory will be written.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="address">The starting address of the buffer.</param>
        public static void WriteMemory(int processHandle, float value, int address) => WriteMemory(processHandle, GetBytes(value), address);
        /// <summary>Writes a <seealso cref="double"/> at the specified address to a specified process.</summary>
        /// <param name="processHandle">The process whose memory will be written.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="address">The starting address of the buffer.</param>
        public static void WriteMemory(int processHandle, double value, int address) => WriteMemory(processHandle, GetBytes(value), address);
        /// <summary>Writes a <seealso cref="bool"/> at the specified address to a specified process.</summary>
        /// <param name="processHandle">The process whose memory will be written.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="address">The starting address of the buffer.</param>
        public static void WriteMemory(int processHandle, bool value, int address) => WriteMemory(processHandle, GetBytes(value), address);
        /// <summary>Writes a <seealso cref="char"/> at the specified address to a specified process.</summary>
        /// <param name="processHandle">The process whose memory will be written.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="address">The starting address of the buffer.</param>
        public static void WriteMemory(int processHandle, char value, int address) => WriteMemory(processHandle, GetBytes(value), address);
        #endregion

        /// <summary>Changes a memory page's protection to <seealso cref="ReadWrite"/> at the specified address to a specified process.</summary>
        /// <param name="address">The starting address of the page.</param>
        /// <param name="size">The size of the page whose protection to change.</param>
        /// <param name="processHandle">The process whose memory page protection will be changed.</param>
        public static void ChangeMemoryProtection(int processHandle, int address, uint size)
        {
            VirtualProtectEx(new IntPtr(processHandle), new IntPtr(address), new UIntPtr(size), (uint)ReadWrite, out _);
        }

        /// <summary>Gets an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static int GetAddressFromPointers(int processHandle, int baseAddress, params int[] offsets)
        {
            int value = ToInt32(ReadMemory(processHandle, 4, baseAddress), 0);
            for (int i = 0; i < offsets.Length - 1; i++)
                value = ToInt32(ReadMemory(processHandle, 4, value + offsets[i]), 0);
            return value + offsets[offsets.Length - 1];
        }

        #region GetFromPointers
        /// <summary>Gets a memory buffer from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="size">The size of the memory buffer to get.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static byte[] GetValueFromPointers(int processHandle, int size, int baseAddress, params int[] offsets)
        {
            return ReadMemory(processHandle, size, GetAddressFromPointers(processHandle, baseAddress, offsets));
        }
        /// <summary>Gets a memory buffer from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static byte[] GetValueFromPointers<T>(int processHandle, int baseAddress, params int[] offsets) where T : unmanaged => GetValueFromPointers(processHandle, sizeof(T), baseAddress, offsets);
        /// <summary>Gets an <seealso cref="byte"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static byte GetByteFromPointers(int processHandle, int baseAddress, params int[] offsets)
        {
            return GetValueFromPointers<byte>(processHandle, baseAddress, offsets)[0];
        }
        /// <summary>Gets an <seealso cref="short"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static short GetShortFromPointers(int processHandle, int baseAddress, params int[] offsets)
        {
            return ToInt16(GetValueFromPointers<short>(processHandle, baseAddress, offsets), 0);
        }
        /// <summary>Gets an <seealso cref="int"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static int GetIntFromPointers(int processHandle, int baseAddress, params int[] offsets)
        {
            return ToInt32(GetValueFromPointers<int>(processHandle, baseAddress, offsets), 0);
        }
        /// <summary>Gets an <seealso cref="long"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static long GetLongFromPointers(int processHandle, int baseAddress, params int[] offsets)
        {
            return ToInt64(GetValueFromPointers<long>(processHandle, baseAddress, offsets), 0);
        }
        /// <summary>Gets an <seealso cref="sbyte"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static sbyte GetSByteFromPointers(int processHandle, int baseAddress, params int[] offsets)
        {
            var value = GetValueFromPointers<sbyte>(processHandle, baseAddress, offsets)[0];
            return *(sbyte*)&value;
        }
        /// <summary>Gets an <seealso cref="ushort"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static ushort GetUShortFromPointers(int processHandle, int baseAddress, params int[] offsets)
        {
            return ToUInt16(GetValueFromPointers<ushort>(processHandle, baseAddress, offsets), 0);
        }
        /// <summary>Gets an <seealso cref="uint"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static uint GetUIntFromPointers(int processHandle, int baseAddress, params int[] offsets)
        {
            return ToUInt32(GetValueFromPointers<uint>(processHandle, baseAddress, offsets), 0);
        }
        /// <summary>Gets an <seealso cref="ulong"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static ulong GetULongFromPointers(int processHandle, int baseAddress, params int[] offsets)
        {
            return ToUInt64(GetValueFromPointers<ulong>(processHandle, baseAddress, offsets), 0);
        }
        /// <summary>Gets an <seealso cref="float"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static float GetFloatFromPointers(int processHandle, int baseAddress, params int[] offsets)
        {
            return ToSingle(GetValueFromPointers<float>(processHandle, baseAddress, offsets), 0);
        }
        /// <summary>Gets an <seealso cref="double"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static double GetDoubleFromPointers(int processHandle, int baseAddress, params int[] offsets)
        {
            return ToDouble(GetValueFromPointers<double>(processHandle, baseAddress, offsets), 0);
        }
        /// <summary>Gets a <seealso cref="bool"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static bool GetBoolFromPointers(int processHandle, int baseAddress, params int[] offsets)
        {
            return ToBoolean(GetValueFromPointers<bool>(processHandle, baseAddress, offsets), 0);
        }
        /// <summary>Gets a <seealso cref="char"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static char GetCharFromPointers(int processHandle, int baseAddress, params int[] offsets)
        {
            return ToChar(GetValueFromPointers<char>(processHandle, baseAddress, offsets), 0);
        }
        #endregion

        #region SetFromPointers
        /// <summary>Sets a memory buffer from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="bytes">The bytes that will be set to the memory buffer.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static void SetValueFromPointers(int processHandle, byte[] bytes, int baseAddress, params int[] offsets)
        {
            WriteMemory(processHandle, bytes, GetAddressFromPointers(processHandle, baseAddress, offsets));
        }
        /// <summary>Sets an <seealso cref="byte"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="value">The value that will be set to the memory buffer.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static void SetByteFromPointers(int processHandle, byte value, int baseAddress, params int[] offsets)
        {
            SetValueFromPointers(processHandle, new byte[] { value }, baseAddress, offsets);
        }
        /// <summary>Sets an <seealso cref="short"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="value">The value that will be set to the memory buffer.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static void SetShortFromPointers(int processHandle, short value, int baseAddress, params int[] offsets)
        {
            SetValueFromPointers(processHandle, GetBytes(value), baseAddress, offsets);
        }
        /// <summary>Sets an <seealso cref="int"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="value">The value that will be set to the memory buffer.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static void SetIntFromPointers(int processHandle, int value, int baseAddress, params int[] offsets)
        {
            SetValueFromPointers(processHandle, GetBytes(value), baseAddress, offsets);
        }
        /// <summary>Sets an <seealso cref="long"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="value">The value that will be set to the memory buffer.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static void SetLongFromPointers(int processHandle, long value, int baseAddress, params int[] offsets)
        {
            SetValueFromPointers(processHandle, GetBytes(value), baseAddress, offsets);
        }
        /// <summary>Sets an <seealso cref="sbyte"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="value">The value that will be set to the memory buffer.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static void SetSByteFromPointers(int processHandle, sbyte value, int baseAddress, params int[] offsets)
        {
            SetValueFromPointers(processHandle, new byte[] { *(byte*)&value }, baseAddress, offsets);
        }
        /// <summary>Sets an <seealso cref="ushort"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="value">The value that will be set to the memory buffer.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static void SetUShortFromPointers(int processHandle, ushort value, int baseAddress, params int[] offsets)
        {
            SetValueFromPointers(processHandle, GetBytes(value), baseAddress, offsets);
        }
        /// <summary>Sets an <seealso cref="uint"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="value">The value that will be set to the memory buffer.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static void SetUIntFromPointers(int processHandle, uint value, int baseAddress, params int[] offsets)
        {
            SetValueFromPointers(processHandle, GetBytes(value), baseAddress, offsets);
        }
        /// <summary>Sets an <seealso cref="ulong"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="value">The value that will be set to the memory buffer.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static void SetULongFromPointers(int processHandle, ulong value, int baseAddress, params int[] offsets)
        {
            SetValueFromPointers(processHandle, GetBytes(value), baseAddress, offsets);
        }
        /// <summary>Sets a <seealso cref="float"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="value">The value that will be set to the memory buffer.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static void SetFloatFromPointers(int processHandle, float value, int baseAddress, params int[] offsets)
        {
            SetValueFromPointers(processHandle, GetBytes(value), baseAddress, offsets);
        }
        /// <summary>Sets a <seealso cref="double"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="value">The value that will be set to the memory buffer.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static void SetDoubleFromPointers(int processHandle, double value, int baseAddress, params int[] offsets)
        {
            SetValueFromPointers(processHandle, GetBytes(value), baseAddress, offsets);
        }
        /// <summary>Sets a <seealso cref="bool"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="value">The value that will be set to the memory buffer.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static void SetBoolFromPointers(int processHandle, bool value, int baseAddress, params int[] offsets)
        {
            SetValueFromPointers(processHandle, GetBytes(value), baseAddress, offsets);
        }
        /// <summary>Sets a <seealso cref="char"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="value">The value that will be set to the memory buffer.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static void SetCharFromPointers(int processHandle, char value, int baseAddress, params int[] offsets)
        {
            SetValueFromPointers(processHandle, GetBytes(value), baseAddress, offsets);
        }
        #endregion

        #region Adjust
        /// <summary>Adjusts a <seealso cref="byte"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="value">The value by which the specified <seealso cref="byte"/> will be adjusted.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static void AdjustByte(int processHandle, byte value, int baseAddress, params int[] offsets)
        {
            SetByteFromPointers(processHandle, (byte)(GetByteFromPointers(baseAddress, processHandle, offsets) + value), baseAddress, offsets);
        }
        /// <summary>Adjusts a <seealso cref="short"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="value">The value by which the specified <seealso cref="short"/> will be adjusted.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static void AdjustShort(int processHandle, short value, int baseAddress, params int[] offsets)
        {
            SetShortFromPointers(processHandle, (short)(GetShortFromPointers(baseAddress, processHandle, offsets) + value), baseAddress, offsets);
        }
        /// <summary>Adjusts a <seealso cref="int"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="value">The value by which the specified <seealso cref="int"/> will be adjusted.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static void AdjustInt(int processHandle, int value, int baseAddress, params int[] offsets)
        {
            SetIntFromPointers(processHandle, GetIntFromPointers(baseAddress, processHandle, offsets) + value, baseAddress, offsets);
        }
        /// <summary>Adjusts a <seealso cref="long"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="value">The value by which the specified <seealso cref="long"/> will be adjusted.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static void AdjustLong(int processHandle, long value, int baseAddress, params int[] offsets)
        {
            SetLongFromPointers(processHandle, GetLongFromPointers(baseAddress, processHandle, offsets) + value, baseAddress, offsets);
        }
        /// <summary>Adjusts a <seealso cref="sbyte"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="value">The value by which the specified <seealso cref="sbyte"/> will be adjusted.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static void AdjustSByte(int processHandle, sbyte value, int baseAddress, params int[] offsets)
        {
            SetSByteFromPointers(processHandle, (sbyte)(GetSByteFromPointers(baseAddress, processHandle, offsets) + value), baseAddress, offsets);
        }
        /// <summary>Adjusts a <seealso cref="ushort"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="value">The value by which the specified <seealso cref="ushort"/> will be adjusted.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static void AdjustUShort(int processHandle, ushort value, int baseAddress, params int[] offsets)
        {
            SetUShortFromPointers(processHandle, (ushort)(GetUShortFromPointers(baseAddress, processHandle, offsets) + value), baseAddress, offsets);
        }
        /// <summary>Adjusts a <seealso cref="uint"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="value">The value by which the specified <seealso cref="uint"/> will be adjusted.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static void AdjustUInt(int processHandle, uint value, int baseAddress, params int[] offsets)
        {
            SetUIntFromPointers(processHandle, GetUIntFromPointers(baseAddress, processHandle, offsets) + value, baseAddress, offsets);
        }
        /// <summary>Adjusts a <seealso cref="ulong"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="value">The value by which the specified <seealso cref="ulong"/> will be adjusted.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static void AdjustULong(int processHandle, ulong value, int baseAddress, params int[] offsets)
        {
            SetULongFromPointers(processHandle, GetULongFromPointers(baseAddress, processHandle, offsets) + value, baseAddress, offsets);
        }
        /// <summary>Adjusts a <seealso cref="float"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="value">The value by which the specified <seealso cref="float"/> will be adjusted.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static void AdjustFloat(int processHandle, float value, int baseAddress, params int[] offsets)
        {
            SetFloatFromPointers(processHandle, GetFloatFromPointers(baseAddress, processHandle, offsets) + value, baseAddress, offsets);
        }
        /// <summary>Adjusts a <seealso cref="double"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="value">The value by which the specified <seealso cref="double"/> will be adjusted.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static void AdjustDouble(int processHandle, double value, int baseAddress, params int[] offsets)
        {
            SetDoubleFromPointers(processHandle, GetDoubleFromPointers(baseAddress, processHandle, offsets) + value, baseAddress, offsets);
        }
        /// <summary>Adjusts a <seealso cref="char"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="value">The value by which the specified <seealso cref="char"/> will be adjusted.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static void AdjustChar(int processHandle, char value, int baseAddress, params int[] offsets)
        {
            SetCharFromPointers(processHandle, (char)(GetCharFromPointers(baseAddress, processHandle, offsets) + value), baseAddress, offsets);
        }
        /// <summary>Inverts a <seealso cref="bool"/> from an address from the given pointers, including the process handle, the base process address and an array of pointer offsets.</summary>
        /// <param name="processHandle">The process handle of the process whose memory to refer to.</param>
        /// <param name="baseAddress">The base address that contains the starting address that will be offset.</param>
        /// <param name="offsets">The offsets of the pointers.</param>
        public static void InvertBool(int processHandle, int baseAddress, params int[] offsets)
        {
            SetBoolFromPointers(processHandle, !GetBoolFromPointers(baseAddress, processHandle, offsets), baseAddress, offsets);
        }
        #endregion
    }
}
