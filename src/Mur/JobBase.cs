using System;

namespace Mur
{
    public abstract class JobBase
    {
        public DateTime? LastRun { get; set; }

        public abstract void Run();
    }
}
