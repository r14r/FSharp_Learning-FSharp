namespace first_steps_with_fsharp

module if_then_else=

    let main=
        printfn "# if_then_else ---------------------------------------------------------------------------"

        let age1 = 66
        if age1 > 65 
        then printfn "Senior citizen"
        else printfn "Citizen"

        let age2 = 64
        let message2 = if age2 > 65 then "Senior citizen" else "Citizen"
        printfn "%s" message2


        let cardValue1 = 1
        let cardDescription1 = if cardValue1 = 1 then "Ace" elif cardValue1 = 14 then "Ace" else "A card"
        printfn "%s" cardDescription1


        let cardNo2 = 12

        let cardDescription2 = 
            if cardNo2 = 1 || cardNo2 = 14 then "Ace"
            elif cardNo2 = 11 then "Jack"
            elif cardNo2 = 12 then "Queen"
            elif cardNo2 = 13 then "King"
            else string cardNo2
        
        printfn "%s" cardDescription2

        0