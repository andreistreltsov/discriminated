An implementation of a *Discriminated Union* data structure with pattern matching support.


## Example

```c#
// define a list of two-case discriminated unions where each instance contain either an `int` or a `string`
var items = new List<Union<int,string>>(){
    new Union<int, string>(1),
    new Union<int, string>(2),
    new Union<int, string>("one")
};

// print each item to console, using pattern matching to determine the message
items.ForEach(item => item.Match(
		x => Console.WriteLine("the number is " + x),
		x => Console.WriteLine("the string is " + x)
));
```

## Installation

The library is available on NuGet. 

To install use Visual Studio mackage manager or run `PM> Install-Package discriminated`.

## Learn More 

Here's a post that explains [how to implement discriminated unions using this library](http://astreltsov.com/software-development/discriminated-unions-in-c-sharp-dot-net.html).

## Building

1. install *node*
2. run `$ npm install`
3. run `$ gulp build`

