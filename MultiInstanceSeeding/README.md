# MultiInstanceSeeding

This program demonstrates the potential timing issues of creating multiple instances of
`System.Random` in a short period of time. I wrote it to demonstrate the issue for
my blog series on [Misusing System.Random][blog-link].

See also my follow-up blog post in the [improvements in .Net Core 2.0][blog-link-2] that eliminate
this issue.

## Build

The project is configured for multi-targeting, and will build 4 executables targeting .Net 4.5.2,
.Net 4.8, .Net Core 1.0, .Net Core 1.1, .Net Core 2.0, .Net Core 2.2, and .Net Core 3.0. You'll need
all these SDKs on your system to build properly. Either that, or edit the `<TargetFrameworks>`
element in the `.csproj` file.

## Usage

```powershell
dotnet run -f netcoreapp1.1
```

and

```powershell
dotnet run -f netcoreapp2.0
```

## Results

Running against the .Net Core 1.1 framework or earlier (including any version of .Net Framework),
will yield results similar to:

```text
PRNG Id | Sequence
--------+-------------------------------------------------------------------------------
    3.0 | 44, 63, 50, 67,  9, 59, 95, 10, 60, 22, 61, 10, 63, 93, 93, 13, 16, 89, 26, 44
    3.1 | 44, 63, 50, 67,  9, 59, 95, 10, 60, 22, 61, 10, 63, 93, 93, 13, 16, 89, 26, 44
    3.2 | 44, 63, 50, 67,  9, 59, 95, 10, 60, 22, 61, 10, 63, 93, 93, 13, 16, 89, 26, 44
    4.0 | 44, 63, 50, 67,  9, 59, 95, 10, 60, 22, 61, 10, 63, 93, 93, 13, 16, 89, 26, 44
    4.1 | 44, 63, 50, 67,  9, 59, 95, 10, 60, 22, 61, 10, 63, 93, 93, 13, 16, 89, 26, 44
    4.2 | 44, 63, 50, 67,  9, 59, 95, 10, 60, 22, 61, 10, 63, 93, 93, 13, 16, 89, 26, 44
    5.0 | 44, 63, 50, 67,  9, 59, 95, 10, 60, 22, 61, 10, 63, 93, 93, 13, 16, 89, 26, 44
    5.1 | 44, 63, 50, 67,  9, 59, 95, 10, 60, 22, 61, 10, 63, 93, 93, 13, 16, 89, 26, 44
    5.2 | 44, 63, 50, 67,  9, 59, 95, 10, 60, 22, 61, 10, 63, 93, 93, 13, 16, 89, 26, 44
```

You'll get a different sequence each run, of course, but you'll almost always see all the instances
producing the same sequence. My [blog post][blog-link] explains why this happens.

Running against .Net Core 2.0 or later will show that this problem no longer exists, as described in
my [follow-up blog post][blog-link-2].

```text
PRNG Id | Sequence
--------+-------------------------------------------------------------------------------
    3.2 |  0, 66, 24, 98, 70, 10,  1,  6, 96, 48, 35, 27, 56, 91, 98,  3, 36, 29, 80, 20
    4.1 |  3, 81, 80, 87, 58, 99, 93, 54, 95,  0, 62,  8, 26, 90, 74, 11, 33,  0, 40, 21
    4.0 |  7, 24, 69, 78, 54, 25, 81, 48, 72, 51, 16, 55, 24, 46, 18,  4, 90, 65, 11, 88
    4.2 | 21, 31, 44, 80,  7, 41, 71, 80, 64, 20, 32, 93, 51,  9, 25, 39, 11, 34, 79,  6
    3.1 | 32, 31, 15, 17, 21, 40, 83, 51, 24, 63, 82, 21, 92, 56, 59, 69, 70,  7, 34, 56
    5.2 | 42, 47, 76, 96, 96, 16, 12, 27, 73, 32, 12, 25, 93, 99, 26, 81, 28, 14, 93,  0
    3.0 | 69, 52, 48, 35, 56, 58, 89, 49, 18, 22, 84, 27,  6, 66, 68, 17, 84, 42, 68, 81
    5.1 | 70, 10, 96, 74, 35, 57, 83, 45, 76, 36, 92, 88, 87, 84, 83, 61, 77, 69, 67, 53
    5.0 | 82, 91, 90, 78,  6, 27, 78, 75, 94, 20, 73,  7, 88, 24, 36, 79, 30, 16, 59, 38
```

[blog-link]: https://drewjcooper.github.io/coding/2019/09/10/misusing-system-random-seeding.html
[blog-link-2]: https://drewjcooper.github.io/coding/2019/09/13/misusing-system-random-dotnet-core.html
