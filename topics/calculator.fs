namespace first_steps_with_fsharp

module calculator = 

    let from whom =
        sprintf "from %s" whom

    let message = from "F#"
    printfn "Hello world %s" message

    printfn "Welcome to the calculator program"

    // printfn "Type the first number"
    // let firstNo = System.Console.ReadLine()
    let firstNo: string = "23"

    // printfn "Type the second number"
    // let secondNo = System.Console.ReadLine()
    let secondNo = "43"
    
    printfn "First %s, Second %s" firstNo secondNo
    
    let firstVal = int firstNo
    let secondVal = int secondNo
    // printfn "The sum is %i" (firstVal + secondVal)
    
    // let sum = int str
    let sum = (int firstNo) + (int secondNo)
    printfn "The sum is %i" sum
    
    // Operatoren
    let no = 10
    let isDivisibleByTwo = no % 2 = 0
    printfn "Divisible by two %b" isDivisibleByTwo
