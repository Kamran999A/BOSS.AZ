using System;

namespace Bossaz.Abstracts
{
    public abstract class Id
    {
        public Guid Guid { get; set; }

        protected Id()
        {
            Guid = Guid.NewGuid();
        }

        public override string ToString()
        {
            return $"Id: {Guid}";
        }
    }
}