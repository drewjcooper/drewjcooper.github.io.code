# drewjcooper.github.io.code

Code samples discussed in my blog at [drewjcooper.github.io][blog-root].

## [MultiInstanceSeeding](https://github.com/drewjcooper/drewjcooper.github.io.code/tree/master/MultiInstanceSeeding)

A console app demonstrating a potential timing issue with creating multiple instances of
`System.Random` in a short period of time.

## [SystemRandomIsNotThreadSafe](https://github.com/drewjcooper/drewjcooper.github.io.code/tree/master/SystemRandomIsNotThreadSafe)

A console app demonstrating the corruption of `System.Random`'s internal state when used across
multiple threads.

[blog-root]: https://drewjcooper.github.io
