# SystemRandomIsNotThreadSafe

This program demonstrates the consequences of using `System.Random` incorrectly
in a multi-threaded environment. I wrote it to demonstrate the issue for my blog
series on [Misusing System.Random][blog-link]

## Build

The project is configured for multi-targeting, and will build 4 executables
targeting .Net 4.5.2, .Net Core 1.0, .Net Core 2.2, and .Net Core 3.0. You'll
need all 4 SDKs on your system to build properly. Either that, or edit the
`<TargetFrameworks>` element in the `.csproj` file.

## Usage

```powershell
dotnet run
```

Because the app's results reply on a race condition between multiple thread the
run time will vary. It's even possible, though rare, for the app not to
complete. If it's running for more than about 5 seconds, then hit `Ctrl-C` and
try again.

## Results

You should see results similar to this, demonstrating the corruption of the
internal state of the PRNG. See the [blog post][blog-link] for more details.

```text
Sample Count | Index Offset | inext | inextp
-------------|--------------|-------|-------
          11 |           21 |    11 |     32
     164,651 |           20 |    27 |     47
     196,772 |           19 |    26 |     45
     391,831 |           20 |    36 |      1
     414,271 |           19 |     1 |     20
     444,791 |           18 |    45 |      8
     687,552 |           17 |    35 |     52
     805,083 |           16 |    35 |     51
     998,891 |           15 |     6 |     21
   1,000,394 |           14 |    21 |     35
   1,032,161 |           13 |    50 |      8
   1,095,091 |           12 |    28 |     40
   1,215,291 |           10 |    53 |      8
   1,217,751 |            9 |    34 |     43
   1,321,631 |            8 |    38 |     46
   1,569,942 |            7 |    23 |     30
   1,663,211 |            6 |    53 |      4
   1,773,751 |            5 |     6 |     11
   2,154,352 |            4 |    22 |     26
   2,310,921 |            5 |     9 |     14
   2,434,362 |            4 |    19 |     23
   2,579,441 |            3 |    15 |     18
   2,638,602 |            2 |    55 |      2
   2,767,892 |            1 |    30 |     31
   2,774,282 |            0 |    26 |     26
```

[blog-link]: https://drewjcooper.github.io/coding/2019/09/28/misusing-system-random-not-thread-safe.html
