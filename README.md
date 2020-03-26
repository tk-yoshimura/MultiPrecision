# MultiPrecision
 float multi precision arithmetic implements

## Spec

Exponent : &plusmn;2147483647

Mantissa : 256-32768 bits

Round: half away from zero

MaxValue: &plusmn;8.808065x10^646456992

## Types

|type|mantissa bits|significant digits|
|----|----|----|
|MultiPrecision&lt;Pow2.N4&gt;|128|34|
|MultiPrecision&lt;Pow2.N8&gt;|256|73|
|MultiPrecision&lt;Pow2.N16&gt;|512|150|
|MultiPrecision&lt;Pow2.N32&gt;|1024|304|
|MultiPrecision&lt;Pow2.N64&gt;|2048|612|
|MultiPrecision&lt;Pow2.N128&gt;|4096|1229|
|MultiPrecision&lt;Pow2.N256&gt;|8192|2462|
|MultiPrecision&lt;Pow2.N512&gt;|16384|4928|
|MultiPrecision&lt;Pow2.N1024&gt;|32768|9860|

## Functions

|function|domain|mantissa error bits|note|usage|
|----|----|----|----|----|
|sqrt|&#91;0,+inf&#41;|1||MultiPrecision&lt;N&gt;.Sqrt(x)|
|cbrt|&#40;-inf,+inf&#41;|1||MultiPrecision&lt;N&gt;.Cbrt(x)|
|log2|&#40;0,+inf&#41;|0||MultiPrecision&lt;N&gt;.Log2(x)|
|log|&#40;0,+inf&#41;|1||MultiPrecision&lt;N&gt;.Log(x)|
|log10|&#40;0,+inf&#41;|1||MultiPrecision&lt;N&gt;.Log10(x)|
|pow2|&#40;-inf,+inf&#41;|0||MultiPrecision&lt;N&gt;.Pow2(x)|
|pow|&#40;-inf,+inf&#41;|2||MultiPrecision&lt;N&gt;.Pow(x, y)|
|pow10|&#40;-inf,+inf&#41;|2||MultiPrecision&lt;N&gt;.Pow10(x)|
|sin|&#40;-inf,+inf&#41;|0||MultiPrecision&lt;N&gt;.Sin(x)|
|cos|&#40;-inf,+inf&#41;|0||MultiPrecision&lt;N&gt;.Cos(x)|
|tan|&#40;-inf,+inf&#41;|4||MultiPrecision&lt;N&gt;.Tan(x)|
|sinh|&#40;-inf,+inf&#41;|2||MultiPrecision&lt;N&gt;.Sinh(x)|
|cosh|&#40;-inf,+inf&#41;|2||MultiPrecision&lt;N&gt;.Cosh(x)|
|tanh|&#40;-inf,+inf&#41;|2||MultiPrecision&lt;N&gt;.Tanh(x)|
|asin|&#91;-1,1&#93;|8||MultiPrecision&lt;N&gt;.Asin(x)|
|acos|&#91;-1,1&#93;|8||MultiPrecision&lt;N&gt;.Acos(x)|
|atan|&#40;-inf,+inf&#41;|8||MultiPrecision&lt;N&gt;.Atan(x)|
|atan2|&#40;-inf,+inf&#41;|8||MultiPrecision&lt;N&gt;.Atan2(y, x)|
|arsinh|&#40;-inf,+inf&#41;|2||MultiPrecision&lt;N&gt;.Arsinh(x)|
|arcosh|&#91;-1,+inf&#41;|2||MultiPrecision&lt;N&gt;.Arcosh(x)|
|artanh|&#40;-1,1&#41;|4||MultiPrecision&lt;N&gt;.Artanh(x)|
|loggamma|&#40;0,+inf&#41;|N/A|generation bits: 234<br>decimal degits: 72|MultiPrecision&lt;N&gt;.LogGamma(x)|
|gamma|&#40;-inf,+inf&#41;|N/A|generation bits: 234<br>decimal degits: 72|MultiPrecision&lt;N&gt;.Gamma(x)|
|ldexp|&#40;-inf,+inf&#41;|N/A||MultiPrecision&lt;N&gt;.Ldexp(x, y)|
|random|N/A|N/A|generation uniform random &#91;0, 1&#41;|MultiPrecision&lt;N&gt;.Random(random)|
|min|N/A|N/A||MultiPrecision&lt;N&gt;.Min(x, y)|
|max|N/A|N/A||MultiPrecision&lt;N&gt;.Max(x, y)|
|floor|N/A|N/A||MultiPrecision&lt;N&gt;.Floor(x)|
|ceiling|N/A|N/A||MultiPrecision&lt;N&gt;.Ceiling(x)|
|round|N/A|N/A||MultiPrecision&lt;N&gt;.Round(x)|
|truncate|N/A|N/A||MultiPrecision&lt;N&gt;.Truncate(x)|
|array sum|N/A|N/A|kahan summation|IEnumerable&lt;MultiPrecision&lt;N&gt;&gt;.Sum()|
|array average|N/A|N/A|kahan summation|IEnumerable&lt;MultiPrecision&lt;N&gt;&gt;.Average()|
|array min|N/A|N/A||IEnumerable&lt;MultiPrecision&lt;N&gt;&gt;.Min()|
|array max|N/A|N/A||IEnumerable&lt;MultiPrecision&lt;N&gt;&gt;.Max()|

## Casts

- long

  MultiPrecision&lt;N&gt; v0 = 123;

  long n0 = (long)v0;

- double

  MultiPrecision&lt;N&gt; v1 = 0.5;

  double n1 = (double)v1;

- string

  MultiPrecision&lt;N&gt; v2 = "3.14e0";

  string s0 = v2.ToString();

  string s1 = v2.ToString("E8");

  string s2 = $"{v2:E8}";

## I/O

BinaryWriter, BinaryReader, Serialize
