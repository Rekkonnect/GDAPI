using System;
using System.Collections.Generic;
using System.Text;

namespace GDAPI.Utilities.Enumerations.Memory
{
    // Documentation has been copy-pasted according to the WinAPI documentation found at
    // https://docs.microsoft.com/en-us/windows/win32/api/memoryapi/nf-memoryapi-virtualallocex
    // and https://docs.microsoft.com/en-us/windows/win32/api/memoryapi/nf-memoryapi-virtualfreeex
    // with slight modifications to match this API's enumeration field naming convention
    // as well as intentional omissions of lack of support by Vista or earlier versions.

    /// <summary>Contains the values of the memory allocation type constants for Windows.</summary>
    public enum MemoryAllocationType
    {
        /// <summary>
        /// <para>Allocates memory charges (from the overall size of memory and the paging files on disk) for the specified reserved memory pages. The function also guarantees that when the caller later initially accesses the memory, the contents will be zero. Actual physical pages are not allocated unless/until the virtual addresses are actually accessed. To reserve and commit pages in one step, call VirtualAllocEx with <seealso cref="Commit"/> | <seealso cref="Reserve"/>.</para>
        /// <para>Attempting to commit a specific address range by specifying <seealso cref="Commit"/> without <seealso cref="Reserve"/> and a non-<see langword="null"/> lpAddress fails unless the entire range has already been reserved. The resulting error code is ERROR_INVALID_ADDRESS.</para>
        /// <para>An attempt to commit a page that is already committed does not cause the function to fail. This means that you can commit pages without first determining the current commitment state of each page.</para>
        /// <para>If lpAddress specifies an address within an enclave, flAllocationType must be <seealso cref="Commit"/>.</para>
        /// </summary>
        Commit = 0x00001000,
        /// <summary>
        /// <para>Reserves a range of the process's virtual address space without allocating any actual physical storage in memory or in the paging file on disk.</para>
        /// <para>You commit reserved pages by calling VirtualAllocEx again with <seealso cref="Commit"/>. To reserve and commit pages in one step, call VirtualAllocEx with <seealso cref="Commit"/> | <seealso cref="Reserve"/>.</para>
        /// <para>Other memory allocation functions, such as malloc and LocalAlloc, cannot use reserved memory until it has been released.</para>
        /// </summary>
        Reserve = 0x00002000,
        /// <summary>
        /// <para>Decommits the specified region of committed pages. After the operation, the pages are in the reserved state.</para>
        /// <para>The function does not fail if you attempt to decommit an uncommitted page. This means that you can decommit a range of pages without first determining their current commitment state.</para>
        /// <para>Do not use this value with <seealso cref="Release"/>.</para>
        /// <para>The <seealso cref="Decommit"/> value is not supported when the lpAddress parameter provides the base address for an enclave.</para>
        /// </summary>
        Decommit = 0x00004000,
        /// <summary>
        /// <para>Releases the specified region of pages, or placeholder (for a placeholder, the address space is released and available for other allocations). After the operation, the pages are in the free state.</para>
        /// <para>If you specify this value, dwSize must be 0 (zero), and lpAddress must point to the base address returned by the VirtualAllocEx function when the region is reserved. The function fails if either of these conditions is not met.</para>
        /// <para>If any pages in the region are committed currently, the function first decommits, and then releases them.</para>
        /// <para>The function does not fail if you attempt to release pages that are in different states, some reserved and some committed. This means that you can release a range of pages without first determining the current commitment state.</para>
        /// <para>Do not use this value with <seealso cref="Decommit"/>.</para>
        /// </summary>
        Release = 0x00008000,
        /// <summary>
        /// <para>Indicates that data in the memory range specified by lpAddress and dwSize is no longer of interest. The pages should not be read from or written to the paging file. However, the memory block will be used again later, so it should not be decommitted. This value cannot be used with any other value.</para>
        /// <para>Using this value does not guarantee that the range operated on with <seealso cref="Reset"/> will contain zeros. If you want the range to contain zeros, decommit the memory and then recommit it.</para>
        /// <para>When you use <seealso cref="Reset"/>, the VirtualAllocEx function ignores the value of fProtect. However, you must still set fProtect to a valid protection value, such as <seealso cref="MemoryPageProtection.NoAccess"/>.</para>
        /// <para>VirtualAllocEx returns an error if you use <seealso cref="Reset"/> and the range of memory is mapped to a file. A shared view is only acceptable if it is mapped to a paging file.</para>
        /// </summary>
        Reset = 0x00080000,
        /// <summary>Allocates memory at the highest possible address. This can be slower than regular allocations, especially when there are many allocations.</summary>
        TopDown = 0x00100000,
        /// <summary>
        /// <para>Reserves an address range that can be used to map Address Windowing Extensions (AWE) pages.</para>
        /// <para>This value must be used with <seealso cref="Reserve"/> and no other values.</para>
        /// </summary>
        Physical = 0x00400000,
        /// <summary>
        /// <para><seealso cref="ResetUndo"/> should only be called on an address range to which <seealso cref="Reset"/> was successfully applied earlier. It indicates that the data in the specified memory range specified by lpAddress and dwSize is of interest to the caller and attempts to reverse the effects of <seealso cref="Reset"/>. If the function succeeds, that means all data in the specified address range is intact. If the function fails, at least some of the data in the address range has been replaced with zeroes.</para>
        /// <para>This value cannot be used with any other value. If <seealso cref="ResetUndo"/> is called on an address range which was not <seealso cref="Reset"/> earlier, the behavior is undefined. When you specify <seealso cref="Reset"/>, the VirtualAllocEx function ignores the value of flProtect. However, you must still set flProtect to a valid protection value, such as <seealso cref="MemoryPageProtection.NoAccess"/>.</para>
        /// <para>Windows Server 2008 R2, Windows 7, Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP:  The <seealso cref="ResetUndo"/> flag is not supported until Windows 8 and Windows Server 2012.</para>
        /// </summary>
        ResetUndo = 0x01000000,
        /// <summary>
        /// <para>Allocates memory using large page support.</para>
        /// <para>The size and alignment must be a multiple of the large-page minimum. To obtain this value, use the GetLargePageMinimum function.</para>
        /// <para>If you specify this value, you must also specify <seealso cref="Reserve"/> and <seealso cref="Commit"/>.</para>
        /// </summary>
        LargePages = 0x20000000,
    }
}
