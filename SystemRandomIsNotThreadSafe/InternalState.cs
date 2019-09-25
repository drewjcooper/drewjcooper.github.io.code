using System;
using System.Linq;
using System.Reflection;

namespace SystemRandomIsNotThreadSafe
{
    class InternalState
    {
        private static readonly FieldInfo inextField;
        private static readonly FieldInfo inextpField;

        // The size of the internal SeedArray less one, because the internal
        // indicies are never 0.
        private const int modulus = 55;

        private readonly int sampleCount;
        private readonly int inext;
        private readonly int inextp;

        static InternalState()
        {
            // The fields have '_' in .Net Core, but not .Net Framework
            inextField = GetField("inext");
            inextpField = GetField("inextp");
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
            
            var fields = type.GetFields(privateInstance);

            return type.GetField(netCoreName, privateInstance) ??
                type.GetField(name, privateInstance) ??
                throw new FieldAccessException(
                    $"Unable to find field '{netCoreName}' or '{name}' in {type.FullName}. FieldsFound: {String.Join(",", fields.Select(f => f.Name))}");
        }

        public static InternalState Get(Random rnd, ref int sampleCount)
        {
            return new InternalState(
                sampleCount,
                (int)inextField.GetValue(rnd),
                (int)inextpField.GetValue(rnd));
        }

        public int IndexOffset => (modulus + inextp - inext) % modulus;

        public static string Header =>
            "Sample Count | Index Offset | inext | inextp" + Environment.NewLine +
            "-------------|--------------|-------|-------";

        public override string ToString()
        {
            return $"{sampleCount,12:###,###,###} | {IndexOffset,12:#0} | {inext,5} | {inextp,6}";
        }
    }
}
