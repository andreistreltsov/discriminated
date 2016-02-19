An implementation of a `Discriminated Union` data structure with pattern matching support.

## Example

    // defining a list of two-case discriminated unions where each can contain either an `int` or a `string`
    var items = new List<Union<int,string>>(){
        new Union<int, string>(1),
        new Union<int, string>(2),
        new Union<int, string>("one")
    };

    // printing each item to console, using pattern matching to determine the message
    items.ForEach(item => item.Match(
    		x => Console.WriteLine("the number is " + x),
			x => Console.WriteLine("the string is " + x)
	));

## Installation

The library is available on NuGet. 

To install, use Visual Studio mackage manager or run `PM> Install-Package discriminated`.

## Building

1. install *node*
2. run `$ npm install`
3. run `$ gulp build`