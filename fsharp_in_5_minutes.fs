namespace fsharp_in_5_minutes

module CommentsSyntax =
    // single line comments use a double slash
    (* multi line comments use (* . . . *) pair

    -end of multi line comment- *)

    // XML doc comments come after /// allowing us to use XML tags to generate documentation.


    module Cheatsheet =

        // Collections

        // Lists: A list is an immutable collection of elements of the same type.
        // Lists use square brackets and `;` delimiter
        let list1 = [ "a"; "b" ]
        // :: is prepending
        let list2 = "c" :: list1
        // @ is concat
        let list3 = list1 @ list2

        // Recursion on list using (::) operator
        let rec sum list =
            match list with
            | [] -> 0
            | x :: xs -> x + sum xs


        // Arrays: Arrays are fixed-size, zero-based, mutable collections of consecutive data elements.
        // Arrays use square brackets with bar
        let array1 = [| "a"; "b" |]
        // Indexed access using dot
        let first = array1.[0]


        // Sequences: A sequence is a logical series of elements of the same type. Individual sequence elements are computed only as required, so a sequence can provide better performance than a list in situations in which not all the elements are used.
        // Sequences can use yield and contain subsequences
        let seq1 =
            seq {
                // "yield" adds one element
                yield 1
                yield 2

                // "yield!" adds a whole subsequence
                yield! [ 5..10 ]
            }

        // Higher-order functions on collections
        // The same list [ 1; 3; 5; 7; 9 ] or array [| 1; 3; 5; 7; 9 |] can be generated in various ways.


        // Using range operator ..
        let xs = [ 1..2..9 ]


        // Using list or array comprehensions
        let ys = [| for i in 0..4 -> 2 * i + 1 |]

        // Using init function
        let zs = List.init 5 (fun i -> 2 * i + 1)

        // Lists and arrays have comprehensive sets of higher-order functions for manipulation.
        // fold starts from the left of the list (or array) and foldBack goes in the opposite direction

        let xs' = Array.fold (fun str n -> sprintf "%s,%i" str n) "" [| 0..9 |]

        // reduce doesn't require an initial accumulator
        let last xs = List.reduce (fun acc x -> x) xs

        // map transforms every element of the list (or array)
        let ys' = Array.map (fun x -> x * x) [| 0..9 |]

        // iterate through a list and produce side effects
        let _ = List.iter (printfn "%i") [ 0..9 ]

        // All these operations are also available for sequences. The added benefits of sequences are laziness and uniform treatment of all collections implementing IEnumerable<'T>.
        let zs' =
            seq {
                for i in 0..9 do
                    printfn "Adding %d" i
                    yield i
            }


        (* 
        Tuples and Records: A tuple is a grouping of unnamed but ordered values, possibly of different types: 
    *)

        // Tuple construction
        let x = (1, "Hello")

        // Triple
        let y = ("one", "two", "three")

        // Tuple deconstruction / pattern
        let (a', b') = x

        // The first and second elements of a tuple can be obtained using fst, snd, or pattern matching:
        let c' = fst (1, 2)
        let d' = snd (1, 2)

        let print' tuple =
            match tuple with
            | (a, b) -> printfn "Pair %A %A" a b

        // Records represent simple aggregates of named values, optionally with members:

        // Declare a record type
        type Person = { Name: string; Age: int }

        // Create a value via record expression
        let paul = { Name = "Paul"; Age = 28 }

        // 'Copy and update' record expression
        let paulsTwin = { paul with Name = "Jim" }

        // Records can be augmented with properties and methods:
        type Person with

            member x.Info = (x.Name, x.Age)

        // Records are essentially sealed classes with extra topping: default immutability, structural equality, and pattern matching support.

        let isPaul person =
            match person with
            | { Name = "Paul" } -> true
            | _ -> false

        // Discriminated Unions: Discriminated unions (DU) provide support for values that can be one of a number of named cases, each possibly with different values and types.

        type Tree<'T> =
            | Node of Tree<'T> * 'T * Tree<'T>
            | Leaf


        let rec depth =
            function
            | Node (l, _, r) -> 1 + max (depth l) (depth r)
            | Leaf -> 0

        // F# Core has a few built-in discriminated unions for error handling, e.g., Option and Choice.

        let optionPatternMatch input =
            match input with
            | Some i -> printfn "input is an int=%d" i
            | None -> printfn "input is missing"

        // Single-case discriminated unions are often used to create type-safe abstractions with pattern matching support:

        type OrderId = Order of string

        // Create a DU value
        let orderId = Order "12"

        // Use pattern matching to deconstruct single-case DU
        let (Order id) = orderId

        // Exceptions: The failwith function throws an exception of type Exception.

        let divideFailwith x y =
            if y = 0 then failwith "Divisor cannot be zero." else x / y

        // Exception handling is done via try/with expressions.

        let divide x y =
            try
                Some(x / y)
            with :? System.DivideByZeroException ->
                printfn "Division by zero!"
                None

        // The try/finally expression enables you to execute clean-up code even if a block of code throws an exception. Here's an example which also defines custom exceptions.
        exception InnerError of string
        exception OuterError of string

        let handleErrors x y =
            try
                try
                    if x = y then
                        raise (InnerError("inner"))
                    else
                        raise (OuterError("outer"))
                with InnerError (str) ->
                    printfn "Error1 %s" str
            finally
                printfn "Always print this."


        // Classes and Inheritance
        // This example is a basic class with (1) local let bindings, (2) properties, (3) methods, and (4) static members.

        type Vector(x: float, y: float) =
            let mag = sqrt (x * x + y * y) // (1)
            member this.X = x // (2)
            member this.Y = y
            member this.Mag = mag

            member this.Scale(s) = // (3)
                Vector(x * s, y * s)

            static member (+)(a: Vector, b: Vector) = // (4)
                Vector(a.X + b.X, a.Y + b.Y)

        // Call a base class from a derived one.
        type Animal() =
            member __.Rest() = ()

        type Dog() =
            inherit Animal()
            member __.Run() = base.Rest()

        // Upcasting is denoted by :> operator.

        let dog = Dog()
        let animal = dog :> Animal

        // Dynamic downcasting (:?>) might throw an InvalidCastException if the cast doesn't succeed at runtime.
        let shouldBeADog = animal :?> Dog


        // Interfaces and Object Expressions
        // Declare IVector interface and implement it in Vector'.

        type IVector =
            abstract Scale: float -> IVector

        type Vector'(x, y) =
            interface IVector with
                member __.Scale(s) = Vector'(x * s, y * s) :> IVector

            member __.X = x
            member __.Y = y

        // Another way of implementing interfaces is to use object expressions.

        type ICustomer =
            abstract Name: string
            abstract Age: int

        let createCustomer name age =
            { new ICustomer with
                member __.Name = name
                member __.Age = age }

        // Active Patterns
        // Complete active patterns:

        let (|Even|Odd|) i = if i % 2 = 0 then Even else Odd

        let testNumber i =
            match i with
            | Even -> printfn "%d is even" i
            | Odd -> printfn "%d is odd" i

        // Parameterized active patterns:
        let (|DivisibleBy|_|) by n =
            if n % by = 0 then Some DivisibleBy else None

        let fizzBuzz =
            function
            | DivisibleBy 3 & DivisibleBy 5 -> "FizzBuzz"
            | DivisibleBy 3 -> "Fizz"
            | DivisibleBy 5 -> "Buzz"
            | i -> string i


    module BasicSyntax =
        printfn "# fsharp_in_5_minutes: BasicsSyntax -------------------------------------------------------"

        (* = "Variables" (but not really) ================================================ *)
        // The "let" keyword defines an (immutable) value
        let myInt = 5
        let myFloat = 3.14

        (* = Strings: F# string type is an alias for System.String type.  ================================================ *)
        let myString = "hello"

        /// Create a string using string concatenation
        let hello = "Hello" + " World"

        // Use verbatim strings preceded by @ symbol to avoid escaping control characters (except escaping " by "").

        let verbatimXml = @"<book title=""Paradise Lost"">"

        // We don't even have to escape " with triple-quoted strings.

        let tripleXml = """<book title="Paradise Lost">"""

        // Backslash strings indent string contents by stripping leading spaces.

        let poem =
            "The lesser world was daubed\n\
        By a colorist of modest skill\n\
        A master limned you in the finest inks\n\
        And with a fresh-cut quill."

        (* = Basic Types and Literals  ================================================ *)
        // Most numeric types have associated suffixes, e.g., uy for unsigned 8-bit integers and L for signed 64-bit integer.

        let b, i, l = 86uy, 86, 86L

        // val b : byte = 86uy
        // val i : int = 86
        // val l : int64 = 86L

        // Other common examples are F or f for 32-bit floating-point numbers, M or m for decimals, and I for big integers.
        let s, f, d, bi = 4.14F, 4.14, 0.7833M, 9999I

        // val s : float32 = 4.14f
        // val f : float = 4.14
        // val d : decimal = 0.7833M
        // val bi : System.Numerics.BigInteger = 9999

        (* = Lists ================================================ *)
        // Square brackets create a list with semicolon delimiters.
        let twoToFive = [ 2; 3; 4; 5 ]

        // :: creates list with new 1st element
        let oneToFive = 1 :: twoToFive
        // The result is [1; 2; 3; 4; 5]

        // @ concats two lists
        let zeroToFive = [ 0; 1 ] @ twoToFive

// IMPORTANT: commas are never used as delimiters, only semicolons!

module Function =
    // The "let" keyword also defines a named function. Note that no parens are used.
    // In F# there is no "return" keyword. A function always returns the value of the last expression used.


    let negate x = x * -1
    let square x = x * x
    let print x = printfn "The number is: %d" x

    printfn "%i" (square 3)

    let squareNegateThenPrint x = print (negate (square x))

    (* Pipe and composition operators *)
    // Pipe operator |> is used to chain functions and arguments together. Double-backtick identifiers are handy to improve readability especially in unit testing:
    let ``square, negate, then print`` x = x |> square |> negate |> print

    // This operator is essential in assisting the F# type checker by providing type information before use:


    (* Composition operator >> is used to compose functions *)
    let squareNegateThenPrint' = square >> negate >> print

    //
    let add x y = x + y
    let addOne = add 1
    let addTwo = add 2

    let addThree = addOne >> addTwo

    (* *)
    // don't use add (x,y)! It means something completely different.
    let add x y = x + y
    printfn "%i" (add 2 3)

    // to define a multiline function, just use indents. No semicolons needed.
    let evens list =
        // Define "isEven" as an inner ("nested") function
        let isEven x = x % 2 = 0

        // List.filter is a library function with two parameters: a boolean function and a list to work on
        List.filter isEven list

    evens oneToFive |> ignore

    // You can use parens to clarify precedence. In this example, do "map" first, with two args, then do "sum" on the result.
    // Without the parens, "List.map" would be passed as an arg to List.sum
    let sumOfSquaresTo100 = List.sum (List.map square [ 1..100 ])

    // You can pipe the output of one operation to the next using "|>"
    // Piping data around is very common in F#, similar to UNIX pipes.
    let sumOfSquaresTo100piped = [ 1..100 ] |> List.map square |> List.sum

    // you can define lambdas (anonymous functions) using the "fun" keyword
    let sumOfSquaresTo100withFun = [ 1..100 ] |> List.map (fun x -> x * x) |> List.sum


    let sumOfLengths (xs: string[]) =
        xs |> Array.map (fun s -> s.Length) |> Array.sum


    (* Recursive functions: The rec keyword is used together with the let keyword to define a recursive function *)

    let rec fact x = if x < 1 then 1 else x * fact (x - 1)

    // Mutually recursive functions (those functions which call each other) are indicated by and keyword:

    let rec even x = if x = 0 then true else odd (x - 1)

    and odd x = if x = 1 then true else even (x - 1)

module PatternMatching =

    (* = Pattern Matching ================================================ *)
    // Match..with.. is a supercharged case/switch statement.
    let simplePatternMatch =
        let x = "a"

        match x with
        | "a" -> printfn "x is a"
        | "b" -> printfn "x is b"
        | _ -> printfn "x is something else" // underscore matches anything

    // F# doesn't allow nulls by default -- you must use an Option type and then pattern match.
    // Some(..) and None are roughly analogous to Nullable wrappers
    let validValue = Some(99)
    let invalidValue = None

    // In this example, match..with matches the "Some" and the "None", and also unpacks the value in the "Some" at the same time.
    let optionPatternMatch input =
        match input with
        | Some i -> printfn "input is an int=%d" i
        | None -> printfn "input is missing"

    optionPatternMatch validValue
    optionPatternMatch invalidValue

    (* = Printing ================================================ *)
    printfn "Printing an int %i, a float %f, a bool %b" 1 2.0 true
    printfn "A string %s, and something generic %A" "hello" [ 1; 2; 3; 4 ]


    // Pattern matching is often facilitated through match keyword.
    let rec fib n =
        match n with
        | 0 -> 0
        | 1 -> 1
        | _ -> fib (n - 1) + fib (n - 2)


    // In order to match sophisticated inputs, one can use when to create filters or guards on patterns:
    let sign x =
        match x with
        | 0 -> 0
        | x when x < 0 -> -1
        | x -> 1

    // Pattern matching can be done directly on arguments:
    let fst' (x, _) = x

    // or implicitly via function keyword:
    // Similar to `fib`; using `function` for pattern matching
    let rec fib' =
        function
        | 0 -> 0
        | 1 -> 1
        | n -> fib' (n - 1) + fib' (n - 2)


module FunctionExamples =

    // define a simple adding function
    let add x y = x + y

    // basic usage of a function
    let a = add 1 2
    printfn "1 + 2 = %i" a

    // partial application to "bake in" parameters
    let add42 = add 42
    let b = add42 1
    printfn "42 + 1 = %i" b

    // composition to combine functions
    let add1 = add 1
    let add2 = add 2
    let add3 = add1 >> add2
    let c = add3 7
    printfn "3 + 7 = %i" c

    // higher order functions
    [ 1..10 ] |> List.map add3 |> printfn "new list is %A"

    // lists of functions, and more
    let add6 = [ add1; add2; add3 ] |> List.reduce (>>)
    let d = add6 7
    printfn "1 + 2 + 3 + 7 = %i" d

module ListExamples =
    // There are three types of ordered collection:
    // * Lists are most basic immutable collection.
    // * Arrays are mutable and more efficient when needed.
    // * Sequences are lazy and infinite (e.g. an enumerator).
    //
    // Other collections include immutable maps and sets
    // plus all the standard .NET collections

    // lists use square brackets
    let list1 = [ "a"; "b" ]
    let list2 = "c" :: list1 // :: is prepending
    let list3 = list1 @ list2 // @ is concat

    // list comprehensions (aka generators)
    let squares =
        [ for i in 1..10 do
              yield i * i ]

    // Recursion on list using (::) operator
    let rec sum list =
        match list with
        | [] -> 0
        | x :: xs -> x + sum xs
        
    // A prime number generator
    // - this is using a short notation for the pattern matching syntax
    // - (p::xs) is 'first :: tail' of the list, could also be written as p :: xs
    //   this means this matches 'p' (the first item in the list), and xs is the rest of the list
    //   this is called the 'cons pattern'
    // - uses 'rec' keyword, which is necessary when using recursion
    let rec sieve =
        function
        | (p :: xs) ->
            p
            :: sieve
                [ for x in xs do
                      if x % p > 0 then
                          yield x ]
        | [] -> []

    let primes = sieve [ 2..50 ]
    printfn "%A" primes

    // pattern matching for lists
    let listMatcher aList =
        match aList with
        | [] -> printfn "the list is empty"
        | [ first ] -> printfn "the list has one element %A " first
        | [ first; second ] -> printfn "list is %A and %A" first second
        | first :: _ -> printfn "the list has more than two elements, first element %A" first

    listMatcher [ 1; 2; 3; 4 ]
    listMatcher [ 1; 2 ]
    listMatcher [ 1 ]
    listMatcher []

    // recursion using lists
    let rec sum aList =
        match aList with
        | [] -> 0
        | x :: xs -> x + sum xs

    sum [ 1..10 ] |> ignore

    // ================================================-----
    // Standard library functions
    // ================================================-----

    // map
    let add3 x = x + 3
    [ 1..10 ] |> List.map add3 |> ignore

    // filter
    let even x = x % 2 = 0
    [ 1..10 ] |> List.filter even |> ignore

module ArrayExamples =

    // arrays use square brackets with bar
    let array1 = [| "a"; "b" |]
    let first = array1.[0] // indexed access using dot

    // pattern matching for arrays is same as for lists
    let arrayMatcher aList =
        match aList with
        | [||] -> printfn "the array is empty"
        | [| first |] -> printfn "the array has one element %A " first
        | [| first; second |] -> printfn "array is %A and %A" first second
        | _ -> printfn "the array has more than two elements"

    arrayMatcher [| 1; 2; 3; 4 |]

    // Standard library functions just as for List

    [| 1..10 |]
    |> Array.map (fun i -> i + 3)
    |> Array.filter (fun i -> i % 2 = 0)
    |> Array.iter (printfn "value is %i. ")

module SequenceExamples =

    // sequences use curly braces
    let seq1 =
        seq {
            yield "a"
            yield "b"
        }

    // sequences can use yield and
    // can contain subsequences
    let strange =
        seq {
            // "yield" adds one element
            yield 1
            yield 2

            // "yield!" adds a whole subsequence
            yield! [ 5..10 ]

            yield!
                seq {
                    for i in 1..10 do
                        if i % 2 = 0 then
                            yield i
                }
        }
    // test
    strange |> Seq.toList |> ignore


    // Sequences can be created using "unfold"
    // Here's the fibonacci series
    let fib = Seq.unfold (fun (fst, snd) -> Some(fst + snd, (snd, fst + snd))) (0, 1)

    // test
    let fib10 = fib |> Seq.take 10 |> Seq.toList
    printf "first 10 fibs are %A" fib10

module DataTypeExamples =

    // All data is immutable by default

    // Tuples are quick 'n easy anonymous types
    // -- Use a comma to create a tuple
    let twoTuple = 1, 2
    let threeTuple = "a", 2, true

    // Pattern match to unpack
    let x, y = twoTuple // sets x = 1, y = 2

    // ================================================
    // Record types have named fields

    // Use "type" with curly braces to define a record type
    type Person = { First: string; Last: string }

    // Use "let" with curly braces to create a record
    let person1 = { First = "John"; Last = "Doe" }

    // Pattern match to unpack
    let { First = first } = person1 // sets first="John"

    // ================================================
    // Union types (aka variants) have a set of choices
    // Only one case can be valid at a time.

    // Use "type" with bar/pipe to define a union type
    type Temp =
        | DegreesC of float
        | DegreesF of float

    // Use one of the cases to create one
    let temp1 = DegreesF 98.6
    let temp2 = DegreesC 37.0

    // Pattern match on all cases to unpack
    let printTemp =
        function
        | DegreesC t -> printfn "%f degC" t
        | DegreesF t -> printfn "%f degF" t

    printTemp temp1
    printTemp temp2

    // ================================================
    // Recursive types

    // Types can be combined recursively in complex ways
    // without having to create subclasses
    type Employee =
        | Worker of Person
        | Manager of Employee list

    let jdoe = { First = "John"; Last = "Doe" }
    let worker = Worker jdoe

    // ================================================
    // Modeling with types

    // Union types are great for modeling state without using flags
    type EmailAddress =
        | ValidEmailAddress of string
        | InvalidEmailAddress of string

    let trySendEmail email =
        match email with // use pattern matching
        | ValidEmailAddress address -> () // send
        | InvalidEmailAddress address -> () // don't send

    // The combination of union types and record types together
    // provide a great foundation for domain driven design.
    // You can create hundreds of little types that accurately
    // reflect the domain.

    type CartItem = { ProductCode: string; Qty: int }
    type Payment = Payment of float
    type ActiveCartData = { UnpaidItems: CartItem list }

    type PaidCartData =
        { PaidItems: CartItem list
          Payment: Payment }

    type ShoppingCart =
        | EmptyCart // no data
        | ActiveCart of ActiveCartData
        | PaidCart of PaidCartData

    // ================================================
    // Built in behavior for types
    // ================================================

    // Core types have useful "out-of-the-box" behavior, no coding needed.
    // * Immutability
    // * Pretty printing when debugging
    // * Equality and comparison
    // * Serialization

    // Pretty printing using %A
    printfn "twoTuple=%A,\nPerson=%A,\nTemp=%A,\nEmployee=%A" twoTuple person1 temp1 worker

    // Equality and comparison built in.
    // Here's an example with cards.
    type Suit =
        | Club
        | Diamond
        | Spade
        | Heart

    type Rank =
        | Two
        | Three
        | Four
        | Five
        | Six
        | Seven
        | Eight
        | Nine
        | Ten
        | Jack
        | Queen
        | King
        | Ace

    let hand =
        [ Club, Ace; Heart, Three; Heart, Ace; Spade, Jack; Diamond, Two; Diamond, Ace ]

    // sorting
    List.sort hand |> printfn "sorted hand is (low to high) %A"
    List.max hand |> printfn "high card is %A"
    List.min hand |> printfn "low card is %A"

module ActivePatternExamples =

    // F# has a special type of pattern matching called "active patterns"
    // where the pattern can be parsed or detected dynamically.

    // "banana clips" are the syntax for active patterns

    // You can use "elif" instead of "else if" in conditional expressions.
    // They are equivalent in F#

    // for example, define an "active" pattern to match character types...
    let (|Digit|Letter|Whitespace|Other|) ch =
        if System.Char.IsDigit(ch) then Digit
        elif System.Char.IsLetter(ch) then Letter
        elif System.Char.IsWhiteSpace(ch) then Whitespace
        else Other

    // ... and then use it to make parsing logic much clearer
    let printChar ch =
        match ch with
        | Digit -> printfn "%c is a Digit" ch
        | Letter -> printfn "%c is a Letter" ch
        | Whitespace -> printfn "%c is a Whitespace" ch
        | _ -> printfn "%c is something else" ch

    // print a list
    [ 'a'; 'b'; '1'; ' '; '-'; 'c' ] |> List.iter printChar

    // ================================================================================================ *)================================================ *)================================================ *)================================================ *)-----
    // FizzBuzz using active patterns
    // ================================================================================================ *)================================================ *)================================================ *)================================================ *)-----

    // You can create partial matching patterns as well
    // Just use underscore in the definition, and return Some if matched.
    let (|MultOf3|_|) i =
        if i % 3 = 0 then Some MultOf3 else None

    let (|MultOf5|_|) i =
        if i % 5 = 0 then Some MultOf5 else None

    // the main function
    let fizzBuzz i =
        match i with
        | MultOf3 & MultOf5 -> printf "FizzBuzz, "
        | MultOf3 -> printf "Fizz, "
        | MultOf5 -> printf "Buzz, "
        | _ -> printf "%i, " i

    // test
    [ 1..20 ] |> List.iter fizzBuzz

module AlgorithmExamples =

    // F# has a high signal/noise ratio, so code reads almost like the actual algorithm

    let square x = x * x

    (* = Example: define sumOfSquares function ================================================ *)
    let sumOfSquares n =
        [ 1..n ] // 1) take all the numbers from 1 to n
        |> List.map square // 2) square each one
        |> List.sum // 3) sum the results

    // test
    sumOfSquares 100 |> printfn "Sum of squares = %A"

    (* = Example: define a sort function ================================================ *)
    let rec sort list =
        match list with
        // If the list is empty
        | [] -> [] // return an empty list
        // If the list is not empty
        | firstElem :: otherElements -> // take the first element
            let smallerElements = // extract the smaller elements
                otherElements // from the remaining ones
                |> List.filter (fun e -> e < firstElem)
                |> sort // and sort them

            let largerElements = // extract the larger ones
                otherElements // from the remaining ones
                |> List.filter (fun e -> e >= firstElem)
                |> sort // and sort them
            // Combine the 3 parts into a new list and return it
            List.concat [ smallerElements; [ firstElem ]; largerElements ]

    // test
    sort [ 1; 5; 23; 18; 9; 1; 3 ] |> printfn "Sorted = %A"

module AsyncExample =

    // F# has built-in features to help with async code without encountering the "pyramid of doom"
    // The following example downloads a set of web pages in parallel.

    open System.Net
    open System
    open System.IO
    open Microsoft.FSharp.Control.CommonExtensions

    // Fetch the contents of a URL asynchronously
    let fetchUrlAsync url =
        async {
            // "async" keyword and curly braces creates an "async" object
            let req = WebRequest.Create(Uri(url))
            use! resp = req.AsyncGetResponse()
            // use! is async assignment
            use stream = resp.GetResponseStream()
            // "use" triggers automatic close() on resource at end of scope
            use reader = new IO.StreamReader(stream)

            let html = reader.ReadToEnd()
            printfn "finished downloading %s" url
        }

    // a list of sites to fetch
    let sites =
        [ "http://www.bing.com"
          "http://www.google.com"
          "http://www.microsoft.com"
          "http://www.amazon.com"
          "http://www.yahoo.com" ]

    // do it
    sites
    |> List.map fetchUrlAsync // make a list of async tasks
    |> Async.Parallel // set up the tasks to run in parallel
    |> Async.RunSynchronously // start them off
    |> ignore

module NetCompatibilityExamples =

    // F# can do almost everything C# can do, and it integrates
    // seamlessly with .NET or Mono libraries.

    // ================================================- work with existing library functions  ================================================ *)-

    let (i1success, i1) = System.Int32.TryParse("123")

    if i1success then
        printfn "parsed as %i" i1
    else
        printfn "parse failed"

    // ================================================- Implement interfaces on the fly! ================================================ *)-

    // create a new object that implements IDisposable
    let makeResource name =
        { new System.IDisposable with
            member this.Dispose() = printfn "%s disposed" name }

    let useAndDisposeResources =
        use r1 = makeResource "first resource"
        printfn "using first resource"

        for i in [ 1..3 ] do
            let resourceName = sprintf "\tinner resource %d" i
            use temp = makeResource resourceName
            printfn "\tdo something with %s" resourceName

        use r2 = makeResource "second resource"
        printfn "using second resource"
        printfn "done."

    // ================================================- Object oriented code ================================================ *)-

    // F# is also a fully fledged OO language.
    // It supports classes, inheritance, virtual methods, etc.

    // interface with generic type
    type IEnumerator<'a> =
        abstract member Current: 'a
        abstract MoveNext: unit -> bool

    // abstract base class with virtual methods
    [<AbstractClass>]
    type Shape() =
        // readonly properties
        abstract member Width: int
        abstract member Height: int
        // non-virtual method
        member this.BoundingArea = this.Height * this.Width
        // virtual method with base implementation
        abstract member Print: unit -> unit
        default this.Print() = printfn "I'm a shape"

    // concrete class that inherits from base class and overrides
    type Rectangle(x: int, y: int) =
        inherit Shape()
        override this.Width = x
        override this.Height = y
        override this.Print() = printfn "I'm a Rectangle"

    // test
    let r = Rectangle(2, 3)
    printfn "The width is %i" r.Width
    printfn "The area is %i" r.BoundingArea
    r.Print()

    // ================================================- extension methods  ================================================ *)-

    // Just as in C#, F# can extend existing classes with extension methods.
    type System.String with

        member this.StartsWithA = this.StartsWith "A"

    // test
    let s = "Alice"
    printfn "'%s' starts with an 'A' = %A" s s.StartsWithA

    // ================================================- events  ================================================ *)-

    type MyButton() =
        let clickEvent = new Event<_>()

        [<CLIEvent>]
        member this.OnClick = clickEvent.Publish

        member this.TestEvent(arg) = clickEvent.Trigger(this, arg)

    // test
    let myButton = new MyButton()
    myButton.OnClick.Add(fun (sender, arg) -> printfn "Click event with arg=%O" arg)

    myButton.TestEvent("Hello World!")

module Printing =
    // The printf/printfn functions are similar to the Console.Write/WriteLine functions in C#.
    printfn "Printing an int %i, a float %f, a bool %b" 1 2.0 true
    printfn "A string %s, and something generic %A" "hello" [ 1; 2; 3; 4 ]

    // all complex types have pretty printing built in
    printfn "twoTuple=%A,\nPerson=%A,\nTemp=%A,\nEmployee=%A" twoTuple person1 temp worker

    // There are also sprintf/sprintfn functions for formatting data into a string, similar to String.Format.

    true

module CompilerDirectives =
    // Compiler Directives: Load another F# source file into FSI.
    (*
            #load "../lib/StringParsing.fs"
    *)

    // Reference a .NET assembly (/ symbol is recommended for Mono compatibility).
    (*
            #r "../lib/FSharp.Markdown.dll"
    *)

    // Include a directory in assembly search paths.
    (*
            #I "./lib"
            #r "FSharp.Markdown.dll"
    *)

    // Other important directives are conditional execution in FSI (INTERACTIVE) and querying current directory (__SOURCE_DIRECTORY__).
    (*
            #if INTERACTIVE
            let path = __SOURCE_DIRECTORY__ + "../lib"
            #else
            let path = "../../../lib"
            #endif
    *)

    true
