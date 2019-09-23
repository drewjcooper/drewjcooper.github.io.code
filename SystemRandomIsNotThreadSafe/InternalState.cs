using System;
using System.Reflection;

namespace SystemRandomIsNotThreadSafe
{
    class InternalState
    {
        private static readonly FieldInfo inextField = GetField("inext");
        private static readonly FieldInfo inextpField = GetField("inextp");

        // The size of the internal SeedArray less one, because the internal
        // indicies are never 0.
        private const int modulus = 55;

        private readonly int sampleCount;
        private readonly int inext;
        private readonly int inextp;

        static InternalState()
        {
            // The fields have '_' in .Net Core, but not .Net Framework
            inextField = GetField("_inext");
            inextpField = GetField("_inextp");
        }

        private InternalState(int sampleCount, int inext, int inextp)
        {
            this.sampleCount = sampleCount;
            this.inext = inext;
            this.inextp = inextp;
        }

        private static FieldInfo GetField(string name)
        {
            var type = typeof(Random);
            var privateInstance = BindingFlags.Instance | BindingFlags.NonPublic;
            
            // The fields have '_' in .Net Core, but not .Net Framework
            var netCoreName = $"_{name}";
            
            return type.GetField(netCoreName, privateInstance) ??
                type.GetField(name, privateInstance) ??
                throw new FieldAccessException(
                    $"Unable to find field '{netCoreName}' or '{name}' in {type.FullName}");
        }

        public static InternalState Get(Random rnd, ref int sampleCount)
        {
            return new InternalState(
                sampleCount,
                (int)inextField.GetValue(rnd),
                (int)inextpField.GetValue(rnd));
        }

        public int IndexOffset => (modulus + inextp - inext) % modulus;

        public override string ToString()
        {
            return $"After {sampleCount,11:###,###,###} samples, index offset is {IndexOffset,2:#0} ({inext,2}, {inextp,2})";
        }
    }
}
