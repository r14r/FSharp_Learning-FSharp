namespace first_steps_with_fsharp

module create_and_architect_with_functions =

    (**)
    let basemethod =
        let add2 a = a + 2
        let multiply3 a = a * 3

        let addAndMultiply a =
            let sum = add2 a
            let product = multiply3 sum

            product

        printfn "%i" (addAndMultiply 2)

    (**)
    let composition =
        let add2 a = a + 2
        let multiply3 a = a * 3
        let addAndMultiply = add2 >> multiply3

        printfn "%i" (addAndMultiply 2)

    (**)
    let pipeline =
        printfn "# create_and_architect_with_functions: pipeline ----------------------------------"

        let list = [ 4; 3; 1 ]
        let sort (list: int list) = List.sort list

        let print (list: int list) =
            List.iter (fun x -> printfn "item %i" x) list

        list |> sort |> print

    (**)
    let exercise =
        printfn "# create_and_architect_with_functions: exercise ----------------------------------"

        let cards = [ 21; 3; 1; 7; 9; 23 ]

        let cardFace card =
            let no = card % 13

            if no = 1 then "Ace"
            elif no = 0 then "King"
            elif no = 12 then "Queen"
            elif no = 11 then "Jack"
            else string no

        let suit card =
            let no = card / 13

            if no = 0 then "Hearts"
            elif no = 1 then "Spades"
            elif no = 2 then "Diamonds"
            else "Clubs"

        let shuffle list =
            let random = System.Random()
            list |> List.sortBy (fun x -> random.Next())

        let printCard card =
            printfn "%s of %s" (cardFace card) (suit card)

        let printAll list = 
            List.iter (fun x -> printCard (x)) list

        let take (no: int) (list) = 
            List.take no list

        cards |> shuffle |> take 3 |> printAll
