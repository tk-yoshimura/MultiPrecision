# MultiPrecision
 float multi precision arithmetic implements

# Spec

Exponent : &plusmn;2147483647

Mantissa : 256-32768 bits

Round: half away from zero

MaxValue: &plusmn;8.808065x10^646456992

# Types

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

# Functions

|function|domain|mantissa error bits|note|
|----|----|----|----|
|sqrt|&#91;0,$+\infty$&#41;|1||
|cbrt|&#40;$-\infty$,$+\infty$&#41;|1||
|log2|&#40;0;,$+\infty$&#41;|0||
|log|&#40;0;,$+\infty$&#41;|1||
|log10|&#40;0;,$+\infty$&#41;|1||
|pow2|&#40;$-\infty$,$+\infty$&#41;|0||
|pow|&#40;$-\infty$,$+\infty$&#41;|2||
|pow10|&#40;$-\infty$,$+\infty$&#41;|2||
|sin|&#40;$-\infty$,$+\infty$&#41;|0||
|cos|&#40;$-\infty$,$+\infty$&#41;|0||
|tan|&#40;$-\infty$,$+\infty$&#41;|4||
|sinh|&#40;$-\infty$,$+\infty$&#41;|2||
|cosh|&#40;$-\infty$,$+\infty$&#41;|2||
|tanh|&#40;$-\infty$,$+\infty$&#41;|2||
|asin|&#91;-1;,1&#93;|8||
|acos|&#91;-1;,1&#93;|8||
|atan|&#40;$-\infty$,$+\infty$&#41;|8||
|atan2|&#40;$-\infty$,$+\infty$&#41;|8||
|arsinh|&#40;$-\infty$,$+\infty$&#41;|2||
|arcosh|&#91;-1;,$+\infty$&#41;|2||
|artanh|&#40;-1;,1&#41;|4||
|loggamma|&#40;0;,$+\infty$&#41;|N/A|generation bits: 234<br>decimal degits: 72|
|gamma|&#40;$-\infty$,$+\infty$&#41;|N/A|generation bits: 234<br>decimal degits: 72|
|ldexp|&#40;$-\infty$,+&inf&#41;|N/A||
|random|N/A|N/A|generation uniform random &#91;0, 1&#41;|
|min|N/A|N/A||
|max|N/A|N/A||
|floor|N/A|N/A||
|ceiling|N/A|N/A||
|round|N/A|N/A||
|truncate|N/A|N/A||