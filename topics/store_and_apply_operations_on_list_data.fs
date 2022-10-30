namespace first_steps_with_fsharp

module store_and_apply_operations_on_list_data =
    type Person = { FirstName: string; LastName: string  }
    type MagicCreature = { Name : string; Level: int; Attack: int }
    type OrderItem = { Name: string; Cost:int }
    type WeatherMeasurement = { Date: string; Temperature: float }
                    
    let list_main =
        printfn "# store_and_apply_operations_on_list_data ------------------------------------"

        (*
            let cards = [
                "Ace"
                "King"
                "Queen"
            ]
        *)

        let cards = [ "Ace"; "King"; "Queen" ]
        printfn "cards: %A" cards

        let numbers = [ 1..5 ]
        printfn "numbers: %A" numbers

        let newList = "Jack" :: cards
        printfn "new list with '::': %A" newList

        let otherCardList = [ "Jack"; "10" ]
        let newList2 = cards @ otherCardList
        printfn "join two lists: %A" newList2


        let otherCardList = [ "10"; "9" ]

        let newList3 = cards |> List.append [ "Jack" ]
        let newList4 = cards |> List.append otherCardList

        printfn "append two lists: %A" newList3
        printfn "append two lists: %A" newList4

        let list = [ 1; 2; 3; 4 ]
        printfn "Item 1 of list %A: %i" list (list.Item 1)

        printfn "Head:   %i" list.Head
        printfn "Tail:   %A" list.Tail
        printfn "Empty:  %b" list.IsEmpty
        printfn "Length: %i" list.Length






        0

    let list_exercise =
        printfn "# store_and_apply_operations_on_list_data: exercise ------------------------------"

        let cards = [ 0..5 ]

        printfn "Cards: %A" cards

        let drawCard (list: int list) =
            printfn "Head: %i" list.Head
            list.Tail

        let result = cards |> drawCard |> drawCard

        let hand = []

        let drawCard (tuple: int list * int list) =
            let deck = fst tuple
            let draw = snd tuple

            printfn "deck: %A" deck
            printfn "draw: %A" draw

            let firstCard = deck.Head
            printfn "First Card: %i" firstCard

            let hand = draw |> List.append [ firstCard ]

            (deck.Tail, hand)

        let d, h = (cards, hand) |> drawCard |> drawCard

        printfn "Deck: %A Hand: %A" d h // Deck: [2; 3; 4; 5] Hand: [1; 0]

        //
        0


    let list_iteration =
        let cards = [ 1..5 ]
        List.iter (fun i -> printfn "%i" i) cards // 1 2 3 4 5

        for i in cards do
            printfn "%i" i

    let list_map = 

        let people = [
            { FirstName="Albert"; LastName= "Einstein" }
            { FirstName="Marie"; LastName="Curie" }
        ]

        let nobelPrizeWinners = List.map (fun person -> person.FirstName + person.LastName) people 
        printfn "%A" nobelPrizeWinners // ["Albert Einstein"; "Marie Curie"]

    let list_filter=
        let cards = [ 1 .. 5 ]

        let filteredList = List.filter(fun i-> i % 2 = 0) cards

        List.iter(fun i -> printfn "item %i" i) filteredList // item 2 item 4

    let list_sort=
        let list = [2; 1; 5; 3]
        let sortedList = List.sort list // 1 2 3 5 

        let fruits = ["Banana"; "Apple"; "Pineapple"]
        let sortedFruits = List.sortBy (fun (fruit : string) -> fruit.Length) fruits // Apple, Banana, Pineapple

        let creatures = [
            { Name="Dragon"; Level=2; Attack=20 }
            { Name="Orc"; Level=1; Attack=5 }
            { Name="Demon"; Level=2; Attack=10 } 
        ]

        // comparison function, -1 = less than, 1 = larger than, 0 = equal
        let compareCreatures c1 c2 =
            if c1.Level < c2.Level then -1
            else if c1.Level > c2.Level then 1
            else if c1.Attack < c2.Attack then -1
            else if c1.Attack > c2.Attack then 1
            else 0

        let sorted = List.sortWith compareCreatures creatures // { Name="Orc"; Level=1; Attack=5 }, { Name="Demon"; Level=2; Attack=10 }, { Name="Dragon"; Level=2; Attack=20 }

        0

    let list_search=
        let list = [1; 2; 3; 4]
        let found = List.find( fun x -> x % 2 = 0) list // 2 - Only the first element that matches the condition is returned.

        let findValue aValue aList =
            let found = aList |> List.tryFind(fun item -> item = aValue)

            match found with
            | Some value -> printfn "%i" value
            | None -> printfn "Not found"

        findValue 1 list // 1
        findValue 5 list // Not found


        let found = List.tryFindIndex(fun x -> x = 4) list
        match found with
        | Some index -> printfn "%i" index
        | None -> printfn "Not found"

    let list_arithmetic_operations=
        let sum = List.sum [1 .. 5] // sum = 15 
        
        
        let orderItems = [
            { Name="XBox"; Cost=500 }
            { Name="Book"; Cost=10 }
            { Name="Movie ticket"; Cost=7 }
            ]

        let sum = List.sumBy(fun item -> item.Cost) orderItems
        printfn "%i" sum // 517

        let numbers = [ 1.0; 2.5; 3.0 ]
        let avg = List.average numbers
        printfn "%f" avg // 2.166667

        let measurements = [
            { Date="07/20/2021"; Temperature=21.3 }
            { Date="07/21/2021"; Temperature=23.2 }
            { Date="07/22/2021"; Temperature=20.7 }
        ]

        let avgBy = List.averageBy(fun m -> m.Temperature) measurements
        printfn "%f" avgBy // 21.733333

    let list_operations=
        // 0 = 11, 11, 12, 13 = 10, else the actual number
        let cardValue card =
            let value = card % 13

            if value = 0 then 11
            elif value = 10 || value = 11 || value = 12 then 10
            else value

        let hand = [0; 25; 31]

        let sum = List.sumBy(fun card -> cardValue card) hand
        printfn "%i" sum

        sum