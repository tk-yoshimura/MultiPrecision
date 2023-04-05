# MultiPrecision
 MultiPrecision Arithmetic Implements

## Requirement
.NET 6.0

AVX2 suppoted CPU. (Intel:Haswell(2013)-, AMD:Excavator(2015)-)

## Install

[Download DLL](https://github.com/tk-yoshimura/MultiPrecision/releases)  
[Download Nuget package](https://www.nuget.org/packages/tyoshimura.multiprecision/)

- To install, just import the DLL.
- This library does not change the environment at all.

## More Functions ?
[DoubleDouble (30-31 digits)](https://github.com/tk-yoshimura/DoubleDouble)  

## Spec

Exponent : &plusmn;2147483647

Mantissa : 128-32768 bits

Round: half away from zero

MaxValue: &plusmn;8.808065x10^646456992

## Types

|type|mantissa bits|significant digits|note|
|----|----|----|----|
|MultiPrecision&lt;Pow2.N4&gt;|128|34|Fastest|
|MultiPrecision&lt;Pow2.N8&gt;|256|73|Fast|
|MultiPrecision&lt;Pow2.N16&gt;|512|150|Standard|
|MultiPrecision&lt;Pow2.N32&gt;|1024|304||
|MultiPrecision&lt;Pow2.N64&gt;|2048|612|Slow|
|MultiPrecision&lt;Pow2.N128&gt;|4096|1229||
|MultiPrecision&lt;Pow2.N256&gt;|8192|2462|Very slow|
|MultiPrecision&lt;Pow2.N512&gt;|16384|4928||
|MultiPrecision&lt;Pow2.N1024&gt;|32768|9860|Not recommended|
|MultiPrecision&lt;*N*&gt;|*Length* x 32|*Length* x 9.6 - 4| public struct *N* &#x3A; IConstant &#x7B; <br/>&emsp; public int Value &#x3D;&#x3E; *Length*&#x3B; <br/> &#x7D; |

## Functions

|function|domain|mantissa error bits|note|usage|
|----|----|----|----|----|
|sqrt|&#91;0,+inf&#41;|1||MultiPrecision&lt;*N*&gt;.Sqrt(x)|
|cbrt|&#40;-inf,+inf&#41;|1||MultiPrecision&lt;*N*&gt;.Cbrt(x)|
|log2|&#40;0,+inf&#41;|0||MultiPrecision&lt;*N*&gt;.Log2(x)|
|log|&#40;0,+inf&#41;|1||MultiPrecision&lt;*N*&gt;.Log(x)|
|log10|&#40;0,+inf&#41;|1||MultiPrecision&lt;*N*&gt;.Log10(x)|
|log1p|&#40;-1,+inf&#41;|1|log(1+x)|MultiPrecision&lt;*N*&gt;.Log1p(x)|
|pow2|&#40;-inf,+inf&#41;|0||MultiPrecision&lt;*N*&gt;.Pow2(x)|
|pow|&#40;-inf,+inf&#41;|1||MultiPrecision&lt;*N*&gt;.Pow(x, y)|
|pow10|&#40;-inf,+inf&#41;|1||MultiPrecision&lt;*N*&gt;.Pow10(x)|
|exp|&#40;-inf,+inf&#41;|1||MultiPrecision&lt;*N*&gt;.Exp(x)|
|expm1|&#40;-inf,+inf&#41;|1|exp(x)-1|MultiPrecision&lt;*N*&gt;.Expm1(x)|
|sin|&#40;-inf,+inf&#41;|1||MultiPrecision&lt;*N*&gt;.Sin(x)|
|cos|&#40;-inf,+inf&#41;|1||MultiPrecision&lt;*N*&gt;.Cos(x)|
|tan|&#40;-inf,+inf&#41;|2||MultiPrecision&lt;*N*&gt;.Tan(x)|
|sinpi|&#40;-inf,+inf&#41;|0| sin(&pi;x) |MultiPrecision&lt;*N*&gt;.SinPI(x)|
|cospi|&#40;-inf,+inf&#41;|0| cos(&pi;x) |MultiPrecision&lt;*N*&gt;.CosPI(x)|
|tanpi|&#40;-inf,+inf&#41;|1| tan(&pi;x) |MultiPrecision&lt;*N*&gt;.TanPI(x)|
|sinh|&#40;-inf,+inf&#41;|2||MultiPrecision&lt;*N*&gt;.Sinh(x)|
|cosh|&#40;-inf,+inf&#41;|2||MultiPrecision&lt;*N*&gt;.Cosh(x)|
|tanh|&#40;-inf,+inf&#41;|2||MultiPrecision&lt;*N*&gt;.Tanh(x)|
|asin|&#91;-1,1&#93;|2|Accuracy deteriorates near x=-1,1.|MultiPrecision&lt;*N*&gt;.Asin(x)|
|acos|&#91;-1,1&#93;|2|Accuracy deteriorates near x=-1,1.|MultiPrecision&lt;*N*&gt;.Acos(x)|
|atan|&#40;-inf,+inf&#41;|2||MultiPrecision&lt;*N*&gt;.Atan(x)|
|atan2|&#40;-inf,+inf&#41;|2||MultiPrecision&lt;*N*&gt;.Atan2(y, x)|
|arsinh|&#40;-inf,+inf&#41;|2||MultiPrecision&lt;*N*&gt;.Arsinh(x)|
|arcosh|&#91;1,+inf&#41;|2||MultiPrecision&lt;*N*&gt;.Arcosh(x)|
|artanh|&#40;-1,1&#41;|4|Accuracy deteriorates near x=-1,1.|MultiPrecision&lt;*N*&gt;.Artanh(x)|
|erf|&#40;-1,1&#41;|2|*Length* &leq; 256|MultiPrecision&lt;*N*&gt;.Erf(x)|
|erfc|&#40;0,2&#41;|2|*Length* &leq; 256|MultiPrecision&lt;*N*&gt;.Erfc(x)|
|inverse_erf|&#40;-1,1&#41;|2|*Length* &leq; 256|MultiPrecision&lt;*N*&gt;.InverseErf(x)|
|inverse_erfc|&#40;0,2&#41;|4|*Length* &leq; 256|MultiPrecision&lt;*N*&gt;.InverseErfc(x)|
|loggamma|&#40;0,+inf&#41;|2|Accuracy deteriorates near x=0.<br/>*Length* &leq; 256 |MultiPrecision&lt;*N*&gt;.LogGamma(x)|
|gamma|&#40;-inf,+inf&#41;|2|Accuracy deteriorates near non-positive intergers.<br/>*Length* &leq; 256 |MultiPrecision&lt;*N*&gt;.Gamma(x)|
|digamma|&#40;-inf,+inf&#41;|2|Accuracy deteriorates near non-positive intergers and zero points.<br/>*Length* &leq; 256 |MultiPrecision&lt;*N*&gt;.Digamma(x)|
|bessel_j|&#40;-inf,+inf&#41;|2|Accuracy deteriorates near zero points.<br/>(error &leq; 2^-(*mantissa bits* + 64))<br/>*Length* &leq; 65<br/>abs(nu) &leq; 64 |MultiPrecision&lt;*N*&gt;.BesselJ(nu, z)|
|bessel_y|&#40;-inf,+inf&#41;|2|Accuracy deteriorates near zero points.<br/>(error &leq; 2^-(*mantissa bits* + 64))<br/>*Length* &leq; 65<br/>abs(nu) &leq; 64 |MultiPrecision&lt;*N*&gt;.BesselY(nu, z)|
|bessel_i|&#91;0,+inf&#41;|2|*Length* &leq; 65<br/>abs(nu) &leq; 64 |MultiPrecision&lt;*N*&gt;.BesselI(nu, z)|
|bessel_k|&#91;0,+inf&#41;|2|*Length* &leq; 65<br/>abs(nu) &leq; 64 |MultiPrecision&lt;*N*&gt;.BesselK(nu, z)|
|elliptic_k|&#91;0,1&#93;|1|k: elliptic modulus, m=k^2|MultiPrecision&lt;*N*&gt;.EllipticK(m)|
|elliptic_e|&#91;0,1&#93;|1|k: elliptic modulus, m=k^2|MultiPrecision&lt;*N*&gt;.EllipticE(m)|
|elliptic_pi|&#91;0,1&#93;|1|k: elliptic modulus, m=k^2|MultiPrecision&lt;*N*&gt;.EllipticPi(n, m)|
|ldexp|&#40;-inf,+inf&#41;|N/A||MultiPrecision&lt;*N*&gt;.Ldexp(x, y)|
|random|N/A|N/A|generation uniform random &#91;0, 1&#41;|MultiPrecision&lt;*N*&gt;.Random(random)|
|min|N/A|N/A||MultiPrecision&lt;*N*&gt;.Min(x, y)|
|max|N/A|N/A||MultiPrecision&lt;*N*&gt;.Max(x, y)|
|floor|N/A|N/A||MultiPrecision&lt;*N*&gt;.Floor(x)|
|ceiling|N/A|N/A||MultiPrecision&lt;*N*&gt;.Ceiling(x)|
|round|N/A|N/A||MultiPrecision&lt;*N*&gt;.Round(x)|
|truncate|N/A|N/A||MultiPrecision&lt;*N*&gt;.Truncate(x)|
|array sum|N/A|N/A|kahan summation|IEnumerable&lt;MultiPrecision&lt;*N*&gt;&gt;.Sum()|
|array average|N/A|N/A|kahan summation|IEnumerable&lt;MultiPrecision&lt;*N*&gt;&gt;.Average()|
|array variance|N/A|N/A|**population** variance|IEnumerable&lt;MultiPrecision&lt;*N*&gt;&gt;.Variance()|
|array min|N/A|N/A||IEnumerable&lt;MultiPrecision&lt;*N*&gt;&gt;.Min()|
|array max|N/A|N/A||IEnumerable&lt;MultiPrecision&lt;*N*&gt;&gt;.Max()|

## Constants

|constant|value|note|usage|
|----|----|----|----|
|Pi|3.141592653589793238462...||MultiPrecision&lt;*N*&gt;.PI|
|Napier's E|2.718281828459045235360...||MultiPrecision&lt;*N*&gt;.E|
|Sqrt(2)|1.414213562373095048801...||MultiPrecision&lt;*N*&gt;.Sqrt2|
|lg(2)|0.301029995663981195213...|log10(2)<br/>lg:=log10 (ISO 80000-2-12.6)|MultiPrecision&lt;*N*&gt;.Lg2|
|lb(10)|3.321928094887362347870...|log2(10)<br/> lb:=log2 (ISO 80000-2-12.7)|MultiPrecision&lt;*N*&gt;.Lb10|
|log(2)|0.693147180559945309417...|log(2)<br/>ln:=log (ISO 80000-2-12.5)|MultiPrecision&lt;*N*&gt;.Ln2|
|lb(e)|1.442695040888963407359...|log2(e)|MultiPrecision&lt;*N*&gt;.LbE|
|Euler's Gamma|0.577215664901532860606...||MultiPrecision&lt;*N*&gt;.EulerGamma|
|&zeta;(3)|1.202056903159594285399...|Apery const.|MultiPrecision&lt;*N*&gt;.Zeta3|
|&zeta;(5)|1.036927755143369926331...||MultiPrecision&lt;*N*&gt;.Zeta5|
|&zeta;(7)|1.008349277381922826839...||MultiPrecision&lt;*N*&gt;.Zeta7|

## Sequence

|sequence|note|usage|
|----|----|----|
|Taylor|1/n!|MultiPrecision&lt;*N*&gt;.TaylorSequence|
|Bernoulli|B(2k)|MultiPrecision&lt;*N*&gt;.BernoulliSequence|
|Stirling|Gamma convergent series, Bayes(1763)|MultiPrecision&lt;*N*&gt;.StirlingSequence|
|HarmonicNumber|H_n|MultiPrecision&lt;*N*&gt;.HarmonicNumber|

## Coefficient

|coefficient|note|usage|
|----|----|----|
|Chebyshev|C(n, m)|MultiPrecision&lt;*N*&gt;.ChebyshevCoef|

## Util

- NewtonRaphsonRootFinding

- HalleyRootFinding

- RombergIntegrate

- FiniteDifference

## Casts

- long (accurately)

```csharp
MultiPrecision<N> v0 = 123;
long n0 = (long)v0;
```

- double (accurately)

```csharp
MultiPrecision<N> v1 = 0.5;
double n1 = (double)v1;
```

- decimal (approximately)

```csharp
MultiPrecision<N> v1 = 0.1m;
decimal n1 = (decimal)v1;
```

- string (approximately)

```csharp
MultiPrecision<N> v2 = "3.14e0";
string s0 = v2.ToString();
string s1 = v2.ToString("E8");
string s2 = $"{v2:E8}";
```
  
## I/O

BinaryWriter, BinaryReader

## Licence
[MIT](https://github.com/tk-yoshimura/MultiPrecision/blob/master/LICENSE)

## Author

[T.Yoshimura](https://github.com/tk-yoshimura)
