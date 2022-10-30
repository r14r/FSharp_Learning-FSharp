namespace cheatsheet

module printResult =

    for value in result do
        printfn "%A" value

    // Print all elements in the array with Seq.iter.
    Seq.iter (fun x -> printfn "%A" x) result

module Strings =
    let animals = "bear,frog,eagle"

    // Split on the comma: Creating a single-element character array for us.
    let result = animals.Split ','
    // "bear"
    // "frog"
    // "eagle"

    // Seq.toList. This example adds some complexity. We introduce a splitLine function.
    // This receives a string and calls Split on it. Then it returns the result of Seq.toList.

    let splitLine = (fun (line : string) -> Seq.toList (line.Split ','))

    // Split this line into a list.
    let plants = "turnip,carrot,lettuce"
    let result = splitLine plants
    // "turnip"
    // "carrot"
    // "lettuce"
    
    // String separator. With Split() we can separate a string based on a substring delimiter. We create a string array containing all delimiters and pass it to Split.
    // RemoveEmptyEntries This is part of the System namespace. With it, empty array elements are removed before Split returns.
    // Result The items string has an empty string between two delimiters. But this is not part of the result.
    open System
    let items = "keyboard; mouse; ; monitor"

    let result = items.Split([|"; "|], StringSplitOptions.RemoveEmptyEntries)


    // Multiple delimiters
    let clothes = "pants.shoes:socks"
    let result = clothes.Split([|'.'; ':'|])
    