# WSU Salary Inversion project
* This project was created in CS 3750 at Weber State University and is used to indentify cases of salary inversion within the university.

# Team Uno coding standards

## Visual Studio/Intellisense is your best friend. 
* Most of these standards are from the official Microsoft C# standards so Intellisense will be a huge help to you.

## Naming
* Use PascalCase for methods and camelCase for properties, i.e. 

``` C#
private string phoneNumber; // camelCase

private void UpdatePhone(string phone) { // PascalCase
    // Do some validating of input, trimming, etc...
    phoneNumber = phone;
}
```

* Exception: when creating an auto-implemented property, PascalCase should be used. Refactoring the example from above: 
``` C#
private string PhoneNumber { get; set; }
```

* Do **Not** use Hungarian notation such as `string strHello = 'Hello World'`. Visual Studio makes it very easy to tell what type a variable is making this notation redundant.


* Create properties and methods that have descriptive, easy to understand names. Good code is self documenting. Where possible name methods in the style of VerbNoun such as ` private void CreateContact() `.

## Commenting
* Good code is self documenting, but when needed it is best to have comments above the block of code they're describing instead of at the end of a line.
``` C#
private void populateDatabase() {
    // The following lines convert the raw data...
    
}
```
* Despite the previous point, methods and some class level properties should have XML comments that explain what they do and more importantly how to interact with them. This is useful as Intellisense will display XML comments for methods and properties when inspecting calls to them. You can create XML comments by typing 3 slashes /// which will automatically add fields that can then be filled in.
``` C#
using System.Collections.Generic;

private string CustomerName { get; set; }

/// <summary>
/// A list of Invoice objects representing a customers order history.
/// </summary>
private List<Invoice> customerOrders = new List<Invoice>();

/// <summary>
/// Adds a new order to the customers order history.
/// </summary>
/// <param name="newOrder">An invoice to be added to the customers history</param>
private void AddOrder(Invoice newOrder) {
    customerOrders.Add(newOrder);
}
```

## Logic
* Add blocks to control structures such as if, while, and for. Single line if statements can be replaced by ternary operators.

* Add default cases to switch statements and else clauses in if-else if chains.
  * In cases where default or else should not be reached throw an exception.

* Use bool values directly instead of checking if they are equal to true or false.
``` C#
bool isEnabled = true;

if (isEnabled) {

} 
else {

}
```

