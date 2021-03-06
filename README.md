# dateformats
This library allows you to easily convert date time formats between .Net, moment.js, jQueryUI datepicker, Timepicker.

## CSharp

	// Convert from jQueryUI datepicker to moment.js ("'on 'yy-mm-dd" => [on ]YYYY-MM-DD)
	DateTimeFormatRules.Convert("'on 'yy-mm-dd", DateTimeFormatRules.JqueryDatePicker, DateTimeFormatRules.MomentJs)

## TypeScript/JavaScript

	// Convert from DotNet to Timepicker ("at HH:mm:ss.fff" => "'at 'HH:mm:ss.l")
	dateFormat.convert('at HH:mm:ss.fff', dateFormat.dotNet, dateFormat.timepicker)

The javascirpt version is also on [npm](https://www.npmjs.com/package/dateformats).

## Formats

- `dotNet` [DateTime(.net class)](https://msdn.microsoft.com/en-us/library/8kb3ddd4.aspx)
- `momentJs` [moment.js](http://momentjs.com/docs/#/displaying/format/)
- `datepicker` [jQueryUI datepicker](http://api.jqueryui.com/datepicker/#utility-formatDate)
- `timepicker` [Timepicker](http://trentrichardson.com/examples/timepicker/#tp-formatting)
- `standard` my own format

## Background

Some days ago I wrote this for my requirements and posted it on [stackoverflow](http://stackoverflow.com/questions/20101603). I was asked to put it on github and [npm](https://www.npmjs.com/package/dateformats), so here it is.

It comes in C# and TypeScript/JavaScript, you can use either or both. Several syntaxes are ignored during the conversion due to lack of support in most other rules, such as week of year in moment.js, however what got left is sufficient for many scenario in my opinion. 

I also created my own *standard* rule, I mean to stick to it in my future projects and convert to *local* format only at the last minute. Just remove it if you don't like it. You can also easily create other rules too, if necessary.

The C# version is tested more than the TypeScript version, though I can't say that there aren't any bugs. Use it at your own risk.