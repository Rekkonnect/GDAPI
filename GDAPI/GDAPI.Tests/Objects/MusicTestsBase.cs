using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using GDAPI.Objects.Music;

namespace GDAPI.Tests.Objects
{
    /// <summary>Contains some fundamental instances that may be used in music-related tests.</summary>
    public abstract class MusicTestsBase
    {
        protected readonly TimeSignature CommonTimeSignature = new(4, 4);
        protected readonly TimeSignature WaltzTimeSignature = new(3, 4);
        protected readonly TimeSignature UncommonTimeSignature = new(5, 4);
    }
}
