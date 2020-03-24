using System;
using System.Linq;
using System.Runtime.Serialization;

namespace MultiPrecision {

    [Serializable]
    public sealed partial class MultiPrecision<N> : ISerializable {
        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue(nameof(Sign), Sign, typeof(Sign));
            info.AddValue(nameof(Exponent), exponent, typeof(UInt32));
            info.AddValue(nameof(Mantissa), Mantissa.ToArray(), typeof(UInt32[]));
        }

        public MultiPrecision(SerializationInfo info, StreamingContext context){
            Sign sign = (Sign)info.GetValue(nameof(Sign), typeof(Sign));
            if (!Enum.IsDefined(typeof(Sign), sign)){
                throw new FormatException();
            }

            UInt32 exponent = (UInt32)info.GetValue(nameof(Exponent), typeof(UInt32));
            UInt32[] mantissa = (UInt32[])info.GetValue(nameof(Mantissa), typeof(UInt32[]));

            if (mantissa.Length != Length) {
                throw new FormatException();
            }

            if (UIntUtil.IsZero(mantissa)) {
                if (exponent != ExponentMin && exponent != ExponentMax) {
                    throw new FormatException();
                }
            }
            else { 
                if (UIntUtil.LeadingZeroCount(mantissa) != 0u) {
                    throw new FormatException();
                }
            }

            this.Sign = sign;
            this.exponent = exponent;
            this.mantissa = new Mantissa<N>(mantissa);
        }
    }
}
